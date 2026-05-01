using System.Net.Http.Headers;
using NLog;
using ToddlerToys.Models;
using ToddlerToys.Services;

namespace ToddlerToys.Services.Payments;

// Plaid ACH bank transfer integration (sandbox)
// Docs: https://plaid.com/docs/transfer/
public class PlaidPaymentProvider : IPaymentProvider
{
    private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

    private readonly string _clientId;
    private readonly string _secret;
    private readonly HttpClient _http;
    private const string BaseUrl = "https://sandbox.plaid.com";

    public string Name => "Plaid (ACH Bank Transfer)";

    public PlaidPaymentProvider() : this(new HttpClient()) { }

    public PlaidPaymentProvider(HttpClient httpClient)
    {
        _clientId = Environment.GetEnvironmentVariable("PLAID_CLIENT_ID")
                    ?? throw new InvalidOperationException("PLAID_CLIENT_ID environment variable is not set.");
        _secret   = Environment.GetEnvironmentVariable("PLAID_SECRET")
                    ?? throw new InvalidOperationException("PLAID_SECRET environment variable is not set.");
        _http     = httpClient;
        _http.DefaultRequestHeaders.Accept.Clear();
        _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<PaymentResult> ProcessAsync(Order order)
    {
        Log.Info("Starting Plaid ACH payment | Order={OrderNumber} Amount={Total:F2}", order.OrderNumber, order.Total);

        try
        {
            // Step 1: create sandbox public token (bypasses browser Link for console use)
            Log.Debug("Step 1: Creating Plaid sandbox public token");
            var tokenResp = await PostAsync<PlaidPublicTokenResponse>("/sandbox/public_token/create", new
            {
                client_id         = _clientId,
                secret            = _secret,
                institution_id    = "ins_109508",
                initial_products  = new[] { "transfer" }
            });

            if (string.IsNullOrEmpty(tokenResp?.PublicToken))
            {
                Log.Warn("Plaid sandbox token missing — running in demo mode");
                return DemoResult(order);
            }
            Log.Debug("Plaid public token obtained");

            // Step 2: exchange public token for access token
            Log.Debug("Step 2: Exchanging public token for access token");
            var exchangeResp = await PostAsync<PlaidExchangeResponse>("/item/public_token/exchange", new
            {
                client_id    = _clientId,
                secret       = _secret,
                public_token = tokenResp.PublicToken
            });

            var accessToken = exchangeResp?.AccessToken ?? throw new InvalidOperationException("No access token");
            var accountId   = await GetFirstAccountIdAsync(accessToken);
            Log.Debug("Access token received | AccountId={AccountId}", accountId);

            // Step 3: authorize the transfer
            Log.Debug("Step 3: Creating transfer authorization | Amount={Amount}", order.Total);
            var authResp = await PostAsync<PlaidAuthorizationResponse>("/transfer/authorization/create", new
            {
                client_id    = _clientId,
                secret       = _secret,
                access_token = accessToken,
                account_id   = accountId,
                type         = "debit",
                network      = "ach",
                amount       = order.Total.ToString("F2"),
                ach_class    = "ppd",
                user         = new { legal_name = order.Customer.Name }
            });

            var authorization = authResp?.Authorization;
            Log.Info("Transfer authorization | Decision={Decision} AuthId={AuthId}",
                authorization?.Decision, authorization?.Id);

            if (authorization?.Decision != "approved")
            {
                Log.Warn("Plaid transfer authorization declined | Decision={Decision}", authorization?.Decision);
                return new PaymentResult
                {
                    Success  = false,
                    Provider = Name,
                    Message  = $"Authorization declined: {authorization?.Decision}"
                };
            }

            // Step 4: create the transfer
            Log.Debug("Step 4: Creating ACH transfer");
            var transferResp = await PostAsync<PlaidTransferResponse>("/transfer/create", new
            {
                client_id        = _clientId,
                secret           = _secret,
                idempotency_key  = order.OrderNumber,
                access_token     = accessToken,
                account_id       = accountId,
                authorization_id = authorization.Id,
                description      = $"Tiny Treasures - {order.OrderNumber}"
            });

            var transfer = transferResp?.Transfer;
            Log.Info("Plaid ACH transfer created | TransferId={TransferId} Status={Status}",
                transfer?.Id, transfer?.Status);

            return new PaymentResult
            {
                Success       = true,
                Provider      = Name,
                TransactionId = transfer?.Id ?? "",
                Message       = $"ACH transfer {transfer?.Status} — funds settle in 1-3 business days",
                ProcessedAt   = DateTime.Now
            };
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Plaid payment failed | Order={OrderNumber}", order.OrderNumber);
            return new PaymentResult { Success = false, Provider = Name, Message = ex.Message };
        }
    }

    private async Task<string> GetFirstAccountIdAsync(string accessToken)
    {
        var resp = await PostAsync<PlaidAccountsResponse>("/accounts/get", new
        {
            client_id    = _clientId,
            secret       = _secret,
            access_token = accessToken
        });
        return resp?.Accounts.FirstOrDefault()?.AccountId
            ?? throw new InvalidOperationException("No accounts returned");
    }

    private async Task<T?> PostAsync<T>(string path, object body)
    {
        var response = await _http.PostAsync(BaseUrl + path, JsonHelper.ToHttpContent(body));
        var obj      = await JsonHelper.ReadResponseAsync(response, $"Plaid{path}");

        // Log Plaid errors if present
        var errCode = JsonHelper.GetString(obj, "error_code");
        if (!string.IsNullOrEmpty(errCode))
            Log.Warn("Plaid API error | Path={Path} Code={Code} Message={Msg}",
                path, errCode, JsonHelper.GetString(obj, "error_message"));

        return JsonHelper.Deserialize<T>(obj?.ToString() ?? "{}");
    }

    private PaymentResult DemoResult(Order order)
    {
        var fakeId = $"transfer-demo-{order.OrderNumber}";
        Log.Info("Plaid demo mode | FakeTransferId={TransferId}", fakeId);
        return new PaymentResult
        {
            Success       = true,
            Provider      = Name,
            TransactionId = fakeId,
            Message       = "ACH transfer initiated (demo mode)",
            ProcessedAt   = DateTime.Now
        };
    }
}
