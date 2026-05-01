using System.Net.Http.Headers;
using NLog;
using ToddlerToys.Models;
using ToddlerToys.Services;

namespace ToddlerToys.Services.Payments;

// Affirm Buy Now Pay Later integration (sandbox)
// Docs: https://docs.affirm.com/affirm-developers/docs/checkout-api-reference
public class AffirmPaymentProvider : IPaymentProvider
{
    private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

    private readonly string _publicKey;
    private readonly string _privateKey;
    private readonly HttpClient _http;
    private const string BaseUrl = "https://sandbox.affirm.com/api/v2";

    public string Name => "Affirm (Buy Now, Pay Later)";

    public AffirmPaymentProvider() : this(new HttpClient()) { }

    public AffirmPaymentProvider(HttpClient httpClient)
    {
        _publicKey  = Environment.GetEnvironmentVariable("AFFIRM_PUBLIC_KEY")
                      ?? throw new InvalidOperationException("AFFIRM_PUBLIC_KEY environment variable is not set.");
        _privateKey = Environment.GetEnvironmentVariable("AFFIRM_PRIVATE_KEY")
                      ?? throw new InvalidOperationException("AFFIRM_PRIVATE_KEY environment variable is not set.");
        _http       = httpClient;
    }

    public async Task<PaymentResult> ProcessAsync(Order order)
    {
        Log.Info("Starting Affirm BNPL payment | Order={OrderNumber} Amount={Total:F2}", order.OrderNumber, order.Total);

        try
        {
            // Step 1: create Affirm checkout session
            Log.Debug("Step 1: Creating Affirm checkout session");
            var response     = await _http.PostAsync(BaseUrl + "/checkout/",
                                   JsonHelper.ToHttpContent(BuildCheckoutPayload(order)));
            var checkoutObj  = await JsonHelper.ReadResponseAsync(response, "Affirm /checkout/");
            var checkoutResp = JsonHelper.Deserialize<AffirmCheckoutResponse>(checkoutObj?.ToString() ?? "{}");

            if (string.IsNullOrEmpty(checkoutResp?.CheckoutToken))
            {
                Log.Warn("Affirm checkout token missing | ErrorCode={Code}", checkoutResp?.ErrorCode);
                return DemoResult(order);
            }

            var checkoutToken = checkoutResp.CheckoutToken;
            var redirectUrl   = checkoutResp.RedirectUrl
                                ?? $"https://sandbox.affirm.com/checkout/{checkoutToken}";
            Log.Info("Affirm checkout created | Token={Token}", checkoutToken);

            // Step 2: customer opens Affirm to approve installment plan
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  ── Affirm Buy Now, Pay Later ──");
            Console.ResetColor();
            Console.WriteLine($"  Open the following URL to complete your Affirm installment plan:");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  {redirectUrl}");
            Console.ResetColor();
            Console.WriteLine("  After approving, paste your checkout token here.");
            Console.Write("  Checkout Token: ");
            var userToken = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(userToken))
                userToken = checkoutToken;

            Log.Debug("Affirm checkout token received | Token={Token}", userToken);

            // Step 3: authorize/capture the charge
            Log.Debug("Step 2: Authorizing Affirm charge");
            var credentials = Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes($"{_publicKey}:{_privateKey}"));

            var chargeReq = new HttpRequestMessage(HttpMethod.Post, BaseUrl + "/charges/")
            {
                Content = JsonHelper.ToHttpContent(new { checkout_token = userToken, order_id = order.OrderNumber }),
                Headers = { Authorization = new AuthenticationHeaderValue("Basic", credentials) }
            };
            var chargeResponse = await _http.SendAsync(chargeReq);
            var chargeObj      = await JsonHelper.ReadResponseAsync(chargeResponse, "Affirm /charges/");
            var chargeResp     = JsonHelper.Deserialize<AffirmChargeResponse>(chargeObj?.ToString() ?? "{}");

            if (!string.IsNullOrEmpty(chargeResp?.Id))
            {
                var amount = chargeResp.Amount / 100m;
                Log.Info("Affirm charge authorized | ChargeId={ChargeId} Amount={Amount:F2}",
                    chargeResp.Id, amount);

                return new PaymentResult
                {
                    Success       = true,
                    Provider      = Name,
                    TransactionId = chargeResp.Id,
                    Message       = "Approved — pay over time with Affirm",
                    ProcessedAt   = DateTime.Now
                };
            }

            var errMsg = chargeResp?.Message ?? "Charge authorization failed";
            Log.Warn("Affirm charge authorization failed | Message={Message}", errMsg);
            return new PaymentResult { Success = false, Provider = Name, Message = errMsg };
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Affirm payment failed | Order={OrderNumber}", order.OrderNumber);
            return new PaymentResult { Success = false, Provider = Name, Message = ex.Message };
        }
    }

    private object BuildCheckoutPayload(Order order) => new
    {
        merchant = new
        {
            public_api_key            = _publicKey,
            user_confirmation_url     = "https://tinytreasures.com/confirm",
            user_cancel_url           = "https://tinytreasures.com/cancel",
            name                      = "Tiny Treasures"
        },
        shipping = new
        {
            name    = new { full = order.Customer.Name },
            address = new { line1 = order.Customer.Address, city = "Toytown", state = "CA", zipcode = "90210", country = "USA" }
        },
        billing = new
        {
            name         = new { full = order.Customer.Name },
            email        = order.Customer.Email,
            phone_number = order.Customer.Phone
        },
        items = order.Items.Select(i => new
        {
            display_name  = i.Product.Name,
            sku           = i.Product.SKU,
            unit_price    = (int)(i.Product.Price * 100),
            qty           = i.Quantity,
            categories    = new[] { new[] { i.Product.Category } }
        }).ToArray(),
        order_id = order.OrderNumber,
        currency = "USD",
        tax      = (int)(order.Tax * 100),
        total    = (int)(order.Total * 100)
    };

    private PaymentResult DemoResult(Order order)
    {
        var fakeId = $"AFFRM-DEMO-{order.OrderNumber}";
        Log.Info("Affirm demo mode | FakeChargeId={ChargeId}", fakeId);
        return new PaymentResult
        {
            Success       = true,
            Provider      = Name,
            TransactionId = fakeId,
            Message       = "Approved — pay over time with Affirm (demo mode)",
            ProcessedAt   = DateTime.Now
        };
    }
}
