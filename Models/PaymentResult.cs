namespace ToddlerToys.Models;

public class PaymentResult
{
    public bool Success { get; set; }
    public string Provider { get; set; } = "";
    public string TransactionId { get; set; } = "";
    public string Message { get; set; } = "";
    public DateTime ProcessedAt { get; set; } = DateTime.Now;
}
