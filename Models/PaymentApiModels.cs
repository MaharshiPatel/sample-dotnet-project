using Newtonsoft.Json;

namespace ToddlerToys.Models;

// ── Plaid response models ──────────────────────────────────────────────────

public class PlaidPublicTokenResponse
{
    [JsonProperty("public_token")]
    public string? PublicToken { get; set; }

    [JsonProperty("error_code")]
    public string? ErrorCode { get; set; }
}

public class PlaidExchangeResponse
{
    [JsonProperty("access_token")]
    public string? AccessToken { get; set; }

    [JsonProperty("item_id")]
    public string? ItemId { get; set; }
}

public class PlaidAccountsResponse
{
    [JsonProperty("accounts")]
    public List<PlaidAccount> Accounts { get; set; } = new();
}

public class PlaidAccount
{
    [JsonProperty("account_id")]
    public string? AccountId { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }
}

public class PlaidAuthorizationResponse
{
    [JsonProperty("authorization")]
    public PlaidAuthorization? Authorization { get; set; }
}

public class PlaidAuthorization
{
    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("decision")]
    public string? Decision { get; set; }

    [JsonProperty("rationale_code")]
    public string? RationaleCode { get; set; }
}

public class PlaidTransferResponse
{
    [JsonProperty("transfer")]
    public PlaidTransfer? Transfer { get; set; }
}

public class PlaidTransfer
{
    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("amount")]
    public string? Amount { get; set; }
}

// ── Affirm response models ─────────────────────────────────────────────────

public class AffirmCheckoutResponse
{
    [JsonProperty("checkout_token")]
    public string? CheckoutToken { get; set; }

    [JsonProperty("redirect_url")]
    public string? RedirectUrl { get; set; }

    [JsonProperty("error_code")]
    public string? ErrorCode { get; set; }
}

public class AffirmChargeResponse
{
    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("amount")]
    public int Amount { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("message")]
    public string? Message { get; set; }
}
