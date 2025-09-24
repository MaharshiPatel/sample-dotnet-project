using System;
using System.Collections.Generic;

public record Invoice
(
    string InvoiceNumber,
    DateTime InvoiceDate,
    Party Seller,
    Party Buyer,
    List<InvoiceItem> Items,
    decimal TaxPercent
);

public record Party(string Name, string Address, string? Email = null, string? Phone = null, string? LogoPath = null);

public record InvoiceItem(string Description, decimal UnitPrice, int Quantity)
{
    public decimal Total => UnitPrice * Quantity;
}
