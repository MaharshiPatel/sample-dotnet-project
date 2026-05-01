using ToddlerToys.Models;

namespace ToddlerToys.Services.Payments;

public interface IPaymentProvider
{
    string Name { get; }
    Task<PaymentResult> ProcessAsync(Order order);
}
