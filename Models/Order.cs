namespace ToddlerToys.Models;

public class OrderItem
{
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Subtotal => Product.Price * Quantity;
}

public class Order
{
    public string OrderNumber { get; set; } = "";
    public DateTime OrderDate { get; set; }
    public Customer Customer { get; set; } = null!;
    public List<OrderItem> Items { get; set; } = new();
    public PaymentResult? Payment { get; set; }
    public decimal Subtotal => Items.Sum(i => i.Subtotal);
    public decimal Tax => Math.Round(Subtotal * 0.08m, 2);
    public decimal Total => Subtotal + Tax;
}

public class Customer
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Address { get; set; } = "";
}
