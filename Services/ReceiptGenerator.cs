using NLog;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ToddlerToys.Models;

namespace ToddlerToys.Services;

public class ReceiptGenerator
{
    private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

    public string Generate(Order order)
    {
        Log.Info("Generating PDF receipt | OrderNumber={OrderNumber} Customer={Name}", order.OrderNumber, order.Customer.Name);

        QuestPDF.Settings.License = LicenseType.Community;

        var outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "receipts");
        Directory.CreateDirectory(outputDir);
        var filePath = Path.Combine(outputDir, $"receipt-{order.OrderNumber}.pdf");

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Arial));

                page.Header().Element(ComposeHeader);
                page.Content().Element(c => ComposeContent(c, order));
                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Thank you for shopping at Tiny Treasures! ")
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                    text.Span("Questions? support@tinytreasures.com")
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                });
            });
        }).GeneratePdf(filePath);

        Log.Info("PDF receipt saved | Path={FilePath} SizeKB={Size}", filePath, new FileInfo(filePath).Length / 1024);
        return filePath;
    }

    private void ComposeHeader(IContainer container)
    {
        container.Column(col =>
        {
            col.Item().Row(row =>
            {
                row.RelativeItem().Column(inner =>
                {
                    inner.Item().Text("🧸 TINY TREASURES")
                        .FontSize(22).Bold().FontColor(Colors.DeepPurple.Medium);
                    inner.Item().Text("Toddler Toy Store")
                        .FontSize(11).FontColor(Colors.Grey.Darken1);
                    inner.Item().Text("123 Playtime Lane, Toytown, CA 90210")
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                    inner.Item().Text("Tel: (800) 555-TOYS  |  www.tinytreasures.com")
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                });

                row.ConstantItem(160).Column(inner =>
                {
                    inner.Item().AlignRight().Text("RECEIPT")
                        .FontSize(20).Bold().FontColor(Colors.DeepPurple.Darken2);
                    inner.Item().AlignRight().Text("Tiny Treasures, Inc.")
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                });
            });

            col.Item().PaddingVertical(8).LineHorizontal(1.5f).LineColor(Colors.DeepPurple.Medium);
        });
    }

    private void ComposeContent(IContainer container, Order order)
    {
        container.Column(col =>
        {
            // Order & Customer info
            col.Item().PaddingTop(8).Row(row =>
            {
                row.RelativeItem().Column(inner =>
                {
                    inner.Item().Text("BILL TO").Bold().FontSize(9).FontColor(Colors.Grey.Darken2);
                    inner.Item().Text(order.Customer.Name).Bold().FontSize(11);
                    inner.Item().Text(order.Customer.Email).FontSize(9);
                    inner.Item().Text(order.Customer.Phone).FontSize(9);
                    inner.Item().Text(order.Customer.Address).FontSize(9);
                });

                row.ConstantItem(200).Column(inner =>
                {
                    inner.Item().Text("ORDER DETAILS").Bold().FontSize(9).FontColor(Colors.Grey.Darken2);
                    inner.Item().Row(r =>
                    {
                        r.RelativeItem().Text("Order Number:").FontSize(9);
                        r.ConstantItem(120).AlignRight().Text(order.OrderNumber).Bold().FontSize(9);
                    });
                    inner.Item().Row(r =>
                    {
                        r.RelativeItem().Text("Order Date:").FontSize(9);
                        r.ConstantItem(120).AlignRight().Text(order.OrderDate.ToString("MMM dd, yyyy  hh:mm tt")).FontSize(9);
                    });
                    inner.Item().Row(r =>
                    {
                        r.RelativeItem().Text("Payment:").FontSize(9);
                        r.ConstantItem(120).AlignRight().Text("Paid - Credit Card").FontSize(9);
                    });
                });
            });

            col.Item().PaddingTop(16).Text("ITEMS PURCHASED")
                .Bold().FontSize(9).FontColor(Colors.Grey.Darken2);

            // Items table
            col.Item().PaddingTop(4).Table(table =>
            {
                table.ColumnsDefinition(cols =>
                {
                    cols.ConstantColumn(50);   // SKU
                    cols.RelativeColumn(3);    // Name
                    cols.ConstantColumn(70);   // Category
                    cols.ConstantColumn(40);   // Qty
                    cols.ConstantColumn(60);   // Unit Price
                    cols.ConstantColumn(70);   // Subtotal
                });

                // Header
                table.Header(header =>
                {
                    void HeaderCell(string text) =>
                        header.Cell().Background(Colors.DeepPurple.Lighten4)
                            .Padding(5).Text(text).Bold().FontSize(9);

                    HeaderCell("SKU");
                    HeaderCell("Product Name");
                    HeaderCell("Category");
                    HeaderCell("Qty");
                    HeaderCell("Unit Price");
                    HeaderCell("Subtotal");
                });

                // Rows
                bool shade = false;
                foreach (var item in order.Items)
                {
                    var bg = shade ? Colors.Grey.Lighten4 : Colors.White;
                    shade = !shade;

                    void Cell(IContainer c, string t, bool right = false)
                    {
                        var txt = c.Background(bg).Padding(5).Text(t).FontSize(9);
                        if (right) txt.AlignRight();
                    }

                    table.Cell().Element(c => Cell(c, item.Product.SKU));
                    table.Cell().Element(c =>
                    {
                        c.Background(bg).Padding(5).Column(col2 =>
                        {
                            col2.Item().Text(item.Product.Name).FontSize(9).Bold();
                            col2.Item().Text(item.Product.Description).FontSize(7.5f).FontColor(Colors.Grey.Medium);
                        });
                    });
                    table.Cell().Element(c => Cell(c, item.Product.Category));
                    table.Cell().Element(c => Cell(c, item.Quantity.ToString(), true));
                    table.Cell().Element(c => Cell(c, $"${item.Product.Price:F2}", true));
                    table.Cell().Element(c => Cell(c, $"${item.Subtotal:F2}", true));
                }
            });

            // Totals
            col.Item().PaddingTop(8).AlignRight().Width(200).Table(table =>
            {
                table.ColumnsDefinition(cols =>
                {
                    cols.RelativeColumn();
                    cols.ConstantColumn(80);
                });

                void TotalRow(string label, string value, bool bold = false, string? bg = null)
                {
                    var labelText = table.Cell().Background(bg ?? Colors.White).Padding(4).Text(label).FontSize(9);
                    if (bold) labelText.Bold();
                    var valueText = table.Cell().Background(bg ?? Colors.White).Padding(4).AlignRight().Text(value).FontSize(9);
                    if (bold) valueText.Bold();
                }

                TotalRow("Subtotal:", $"${order.Subtotal:F2}");
                TotalRow("Tax (8%):", $"${order.Tax:F2}");
                TotalRow("TOTAL:", $"${order.Total:F2}", bold: true, bg: Colors.DeepPurple.Lighten4);
            });

            // Payment info
            if (order.Payment != null)
            {
                col.Item().PaddingTop(12).Text("PAYMENT").Bold().FontSize(9).FontColor(Colors.Grey.Darken2);
                col.Item().PaddingTop(4).Border(1).BorderColor(Colors.Grey.Lighten2).Table(table =>
                {
                    table.ColumnsDefinition(cols =>
                    {
                        cols.RelativeColumn();
                        cols.RelativeColumn(2);
                    });

                    void PayRow(string label, string value)
                    {
                        table.Cell().Background(Colors.Grey.Lighten5).Padding(5).Text(label).FontSize(9).Bold();
                        table.Cell().Background(Colors.White).Padding(5).Text(value).FontSize(9);
                    }

                    PayRow("Method:",         order.Payment.Provider);
                    PayRow("Transaction ID:", order.Payment.TransactionId);
                    PayRow("Status:",         order.Payment.Success ? "✓ Approved" : "✗ Declined");
                    PayRow("Note:",           order.Payment.Message);
                    PayRow("Processed:",      order.Payment.ProcessedAt.ToString("MMM dd, yyyy  hh:mm tt"));
                });
            }

            // Confirmation banner
            col.Item().PaddingTop(16).Background(Colors.Green.Lighten4)
                .Border(1).BorderColor(Colors.Green.Lighten2).Padding(10).Row(row =>
                {
                    row.RelativeItem().Text($"✓  Order confirmed! {order.Items.Sum(i => i.Quantity)} item(s) will be shipped within 2-3 business days.")
                        .FontSize(9).FontColor(Colors.Green.Darken3);
                });
        });
    }
}
