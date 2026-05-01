using NLog;
using ToddlerToys.Services;
using ToddlerToys.Services.Payments;

NLog.LogManager.Setup().LoadConfigurationFromFile("nlog.config");
var logger = NLog.LogManager.GetCurrentClassLogger();

try
{
    logger.Info("Tiny Treasures store starting");

    var shopping = new ShoppingService();
    var receipt  = new ReceiptGenerator();

    shopping.ShowCatalog();

    while (true)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("  Commands: [A] Add item   [R] Remove item   [C] View cart   [K] Catalog   [Q] Checkout & Quit");
        Console.ResetColor();
        Console.Write("  > ");
        var input = Console.ReadLine()?.Trim().ToUpper();
        logger.Debug("User command: {Command}", input);

        switch (input)
        {
            case "A":
                Console.Write("  Product ID: ");
                if (int.TryParse(Console.ReadLine(), out int pid))
                {
                    Console.Write("  Quantity  : ");
                    if (int.TryParse(Console.ReadLine(), out int qty))
                    {
                        if (shopping.AddToCart(pid, qty))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("  Added to cart.");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("  Invalid product ID, quantity, or insufficient stock.");
                        }
                        Console.ResetColor();
                    }
                }
                break;

            case "R":
                Console.Write("  Product ID to remove: ");
                if (int.TryParse(Console.ReadLine(), out int rid))
                {
                    if (shopping.RemoveFromCart(rid))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("  Item removed from cart.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("  Item not found in cart.");
                        Console.ResetColor();
                    }
                }
                break;

            case "C":
                shopping.ShowCart();
                break;

            case "K":
                shopping.ShowCatalog();
                break;

            case "Q":
                if (!shopping.HasItems())
                {
                    Console.WriteLine("  Cart is empty. Add items before checking out.");
                    break;
                }

                shopping.ShowCart();
                Console.Write("\n  Proceed to checkout? (Y/N): ");
                if (Console.ReadLine()?.Trim().ToUpper() != "Y") break;

                var customer = shopping.CollectCustomerInfo();
                var order    = shopping.Checkout(customer);

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  -- Select Payment Method --");
                Console.ResetColor();
                Console.WriteLine("  [1] Plaid  -- ACH bank transfer (1-3 business days)");
                Console.WriteLine("  [2] Affirm -- Buy Now, Pay Later in installments");
                Console.Write("  Choice: ");
                var choice = Console.ReadLine()?.Trim();

                IPaymentProvider provider = choice == "2"
                    ? new AffirmPaymentProvider()
                    : new PlaidPaymentProvider();

                logger.Info("Payment method selected | Provider={Provider} Order={OrderNumber}", provider.Name, order.OrderNumber);
                Console.WriteLine($"\n  Processing payment via {provider.Name}...");

                var payment = await provider.ProcessAsync(order);
                order.Payment = payment;

                if (payment.Success)
                {
                    logger.Info("Payment successful | Provider={Provider} TransactionId={TxId} Order={OrderNumber}",
                        payment.Provider, payment.TransactionId, order.OrderNumber);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n  Payment approved! Transaction: {payment.TransactionId}");
                    Console.WriteLine($"  {payment.Message}");
                    Console.ResetColor();
                }
                else
                {
                    logger.Warn("Payment failed | Provider={Provider} Reason={Message} Order={OrderNumber}",
                        payment.Provider, payment.Message, order.OrderNumber);

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n  Payment failed: {payment.Message}");
                    Console.ResetColor();
                    goto exit;
                }

                Console.WriteLine("  Generating PDF receipt...");
                var pdfPath = receipt.Generate(order);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"  Receipt saved: {pdfPath}");
                Console.ResetColor();
                goto exit;

            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  Unknown command.");
                Console.ResetColor();
                break;
        }
        Console.WriteLine();
    }

    exit:
    logger.Info("Tiny Treasures store exiting");
}
catch (Exception ex)
{
    LogManager.GetCurrentClassLogger().Error(ex, "Unhandled exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
