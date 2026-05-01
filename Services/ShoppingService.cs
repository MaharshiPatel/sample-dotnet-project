using NLog;
using ToddlerToys.Data;
using ToddlerToys.Models;

namespace ToddlerToys.Services;

public class ShoppingService
{
    private static readonly ILogger Log = LogManager.GetCurrentClassLogger();
    private readonly List<OrderItem> _cart = new();

    public void ShowCatalog()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║               🧸  TINY TREASURES - Toddler Toy Catalog  🧸                      ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════╝");
        Console.WriteLine();

        var categories = ProductCatalog.Products.GroupBy(p => p.Category);
        foreach (var group in categories)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"  ┌─ {group.Key.ToUpper()} ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  {"#",-4} {"Product Name",-36} {"Age",-12} {"Price",7}  {"Availability",-14}  Description");
            Console.WriteLine($"  {new string('─', 100)}");
            Console.ResetColor();

            foreach (var p in group)
            {
                var availability = p.Stock > 10 ? "In Stock" : p.Stock > 0 ? $"Low Stock" : "Out of Stock";
                Console.ForegroundColor = p.Stock > 10 ? ConsoleColor.Green : p.Stock > 0 ? ConsoleColor.Yellow : ConsoleColor.Red;
                Console.Write($"  [{p.Id,3}]");
                Console.ResetColor();
                Console.Write($" {p.Name,-36} {p.AgeRange,-12} ${p.Price,6:F2}  ");
                Console.ForegroundColor = p.Stock > 10 ? ConsoleColor.Green : p.Stock > 0 ? ConsoleColor.Yellow : ConsoleColor.Red;
                Console.Write($"{availability,-14}");
                Console.ResetColor();
                Console.WriteLine($"  {p.Description}");
            }
            Console.WriteLine();
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"  {ProductCatalog.Products.Count} products across {ProductCatalog.Products.GroupBy(p => p.Category).Count()} categories");
        Console.ResetColor();
        Console.WriteLine();
    }

    public void ShowCart()
    {
        Console.WriteLine();
        Console.WriteLine("  ┌─────────────────────────────────────────────┐");
        Console.WriteLine("  │                 YOUR CART                   │");
        Console.WriteLine("  ├─────────────────────────────────────────────┤");

        if (_cart.Count == 0)
        {
            Console.WriteLine("  │             Cart is empty                   │");
        }
        else
        {
            foreach (var item in _cart)
            {
                Console.WriteLine($"  │ {item.Product.Name,-28} x{item.Quantity,-3}  ${item.Subtotal,6:F2} │");
            }
            Console.WriteLine("  ├─────────────────────────────────────────────┤");
            decimal subtotal = _cart.Sum(i => i.Subtotal);
            decimal tax = Math.Round(subtotal * 0.08m, 2);
            Console.WriteLine($"  │ {"Subtotal:",-35} ${subtotal,6:F2} │");
            Console.WriteLine($"  │ {"Tax (8%):",-35} ${tax,6:F2} │");
            Console.WriteLine($"  │ {"TOTAL:",-35} ${subtotal + tax,6:F2} │");
        }

        Console.WriteLine("  └─────────────────────────────────────────────┘");
    }

    public bool AddToCart(int productId, int quantity)
    {
        var product = ProductCatalog.Products.FirstOrDefault(p => p.Id == productId);
        if (product == null)
        {
            Log.Warn("AddToCart: product not found | ProductId={ProductId}", productId);
            return false;
        }
        if (quantity <= 0 || quantity > product.Stock)
        {
            Log.Warn("AddToCart: invalid quantity | ProductId={ProductId} Requested={Qty} Stock={Stock}", productId, quantity, product.Stock);
            return false;
        }

        var existing = _cart.FirstOrDefault(i => i.Product.Id == productId);
        if (existing != null)
        {
            existing.Quantity += quantity;
            Log.Info("Cart updated | Product={Name} Quantity={Qty}", product.Name, existing.Quantity);
        }
        else
        {
            _cart.Add(new OrderItem { Product = product, Quantity = quantity });
            Log.Info("Cart item added | Product={Name} Qty={Qty} Price={Price:F2}", product.Name, quantity, product.Price);
        }
        return true;
    }

    public bool RemoveFromCart(int productId)
    {
        var item = _cart.FirstOrDefault(i => i.Product.Id == productId);
        if (item == null)
        {
            Log.Warn("RemoveFromCart: product not in cart | ProductId={ProductId}", productId);
            return false;
        }
        _cart.Remove(item);
        Log.Info("Cart item removed | Product={Name}", item.Product.Name);
        return true;
    }

    public bool HasItems() => _cart.Count > 0;

    public Order Checkout(Customer customer)
    {
        var order = new Order
        {
            OrderNumber = $"TT-{DateTime.Now:yyyyMMdd}-{Random.Shared.Next(1000, 9999)}",
            OrderDate   = DateTime.Now,
            Customer    = customer,
            Items       = new List<OrderItem>(_cart)
        };

        Log.Info("Order created | OrderNumber={OrderNumber} Customer={Name} Items={Count} Total={Total:F2}",
            order.OrderNumber, customer.Name, order.Items.Count, order.Total);

        foreach (var item in _cart)
        {
            item.Product.Stock -= item.Quantity;
            Log.Debug("Stock adjusted | Product={Name} NewStock={Stock}", item.Product.Name, item.Product.Stock);
        }

        _cart.Clear();
        return order;
    }

    public Customer CollectCustomerInfo()
    {
        Console.WriteLine();
        Console.WriteLine("  ── Customer Information ──");
        Console.Write("  Full Name  : "); var name = Console.ReadLine() ?? "Guest";
        Console.Write("  Email      : "); var email = Console.ReadLine() ?? "";
        Console.Write("  Phone      : "); var phone = Console.ReadLine() ?? "";
        Console.Write("  Address    : "); var address = Console.ReadLine() ?? "";

        return new Customer { Name = name, Email = email, Phone = phone, Address = address };
    }
}
