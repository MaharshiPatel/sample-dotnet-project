using System;
using System.IO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class InvoiceDocument
{
    private readonly Invoice _invoice;

    public InvoiceDocument(Invoice invoice)
    {
        _invoice = invoice;
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public void Generate(string outputPath)
    {
        var doc = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.PageColor(Colors.White);

                page.Header().Row(row =>
                {
                    row.RelativeColumn().Column(column =>
                    {
                        column.Item().Text($"Invoice: {_invoice.InvoiceNumber}").FontSize(20).SemiBold();
                        column.Item().Text($"Date: {_invoice.InvoiceDate:yyyy-MM-dd}").FontSize(10);
                        column.Item().Text($"Seller: {_invoice.Seller.Name}").FontSize(10);
                        column.Item().Text(_invoice.Seller.Address).FontSize(10);
                    });

                    row.ConstantColumn(120).AlignRight().Element(e =>
                    {
                        if (!string.IsNullOrEmpty(_invoice.Seller.LogoPath) && File.Exists(_invoice.Seller.LogoPath))
                        {
                            var imageData = File.ReadAllBytes(_invoice.Seller.LogoPath);
                            e.Image(imageData, ImageScaling.FitArea);
                        }
                        else
                        {
                            e.AlignRight().Text(" ");
                        }
                    });
                });

                page.Content().PaddingVertical(10).Column(col =>
                {
                    col.Item().Text("Bill To:").Bold();
                    col.Item().Text(_invoice.Buyer.Name);
                    col.Item().Text(_invoice.Buyer.Address);

                    col.Item().PaddingTop(10).Element(ComposeItemsTable);
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Thank you for your business");
                });
            });
        });

        using var fs = File.OpenWrite(outputPath);
        doc.GeneratePdf(fs);
    }

    void ComposeItemsTable(IContainer container)
    {
        container.Table(table =>
        {
            // columns: desc, qty, unit, total
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(6);
                columns.RelativeColumn(1);
                columns.RelativeColumn(2);
                columns.RelativeColumn(2);
            });

            // header
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("Description");
                header.Cell().Element(CellStyle).AlignCenter().Text("Qty");
                header.Cell().Element(CellStyle).AlignRight().Text("Unit");
                header.Cell().Element(CellStyle).AlignRight().Text("Total");
            });

            foreach (var item in _invoice.Items)
            {
                table.Cell().Element(CellStyle).Text(item.Description);
                table.Cell().Element(CellStyle).AlignCenter().Text(item.Quantity.ToString());
                table.Cell().Element(CellStyle).AlignRight().Text(item.UnitPrice.ToString("C"));
                table.Cell().Element(CellStyle).AlignRight().Text(item.Total.ToString("C"));
            }

            // totals
            var subtotal = 0m;
            foreach (var i in _invoice.Items) subtotal += i.Total;
            var tax = Math.Round(subtotal * _invoice.TaxPercent / 100m, 2);
            var grand = subtotal + tax;

            table.Cell().ColumnSpan(3).BorderTop(1).Element(CellStyle).AlignRight().Text("Subtotal");
            table.Cell().BorderTop(1).Element(CellStyle).AlignRight().Text(subtotal.ToString("C"));

            table.Cell().ColumnSpan(3).Element(CellStyle).AlignRight().Text($"Tax ({_invoice.TaxPercent}%)");
            table.Cell().Element(CellStyle).AlignRight().Text(tax.ToString("C"));

            table.Cell().ColumnSpan(3).Element(CellStyle).AlignRight().Text("Total").Bold();
            table.Cell().Element(CellStyle).AlignRight().Text(grand.ToString("C")).Bold();
        });
    }

    static IContainer CellStyle(IContainer container)
    {
        return container.Padding(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
    }
}
