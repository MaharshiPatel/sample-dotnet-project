using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;

namespace ToddlerToys.Services;

/// <summary>
/// Centralised JSON helper for REST API calls using Newtonsoft.Json.
/// </summary>
public static class JsonHelper
{
    private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

    private static readonly JsonSerializerSettings DefaultSettings = new()
    {
        NullValueHandling    = NullValueHandling.Ignore,
        DateFormatHandling   = DateFormatHandling.IsoDateFormat,
        Formatting           = Formatting.None,
    };

    // ── Serialization ──────────────────────────────────────────────────────

    /// Serialize any object to a JSON string.
    public static string Serialize(object obj)
        => JsonConvert.SerializeObject(obj, DefaultSettings);

    /// Serialize with indented formatting (useful for logging/debugging).
    public static string SerializePretty(object obj)
        => JsonConvert.SerializeObject(obj, Formatting.Indented, DefaultSettings);

    // ── Deserialization ────────────────────────────────────────────────────

    /// Deserialize JSON string into a strongly-typed object.
    public static T? Deserialize<T>(string json)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(json, DefaultSettings);
        }
        catch (JsonException ex)
        {
            Log.Error(ex, "JSON deserialization failed for type={Type} JSON={Json}", typeof(T).Name, json);
            return default;
        }
    }

    /// Parse a JSON string into a dynamic JObject for flexible field access.
    public static JObject? ParseObject(string json)
    {
        try
        {
            return JObject.Parse(json);
        }
        catch (JsonReaderException ex)
        {
            Log.Error(ex, "Failed to parse JSON object | JSON={Json}", json);
            return null;
        }
    }

    /// Parse a JSON string into a JArray.
    public static JArray? ParseArray(string json)
    {
        try
        {
            return JArray.Parse(json);
        }
        catch (JsonReaderException ex)
        {
            Log.Error(ex, "Failed to parse JSON array | JSON={Json}", json);
            return null;
        }
    }

    // ── JObject helpers ────────────────────────────────────────────────────

    /// Safely read a string value from a JObject (returns null if missing).
    public static string? GetString(JObject? obj, string path)
        => obj?.SelectToken(path)?.Value<string>();

    /// Safely read an int value from a JObject (returns null if missing).
    public static int? GetInt(JObject? obj, string path)
        => obj?.SelectToken(path)?.Value<int>();

    /// Safely read a bool value from a JObject.
    public static bool? GetBool(JObject? obj, string path)
        => obj?.SelectToken(path)?.Value<bool>();

    /// Safely read a decimal value from a JObject.
    public static decimal? GetDecimal(JObject? obj, string path)
        => obj?.SelectToken(path)?.Value<decimal>();

    /// Returns true if the JObject contains the given top-level key.
    public static bool HasKey(JObject? obj, string key)
        => obj?.ContainsKey(key) == true;

    // ── HttpContent helpers ────────────────────────────────────────────────

    /// Build a StringContent JSON body from an object (ready for HttpClient).
    public static StringContent ToHttpContent(object obj)
        => new(Serialize(obj), System.Text.Encoding.UTF8, "application/json");

    /// Read and parse an HttpResponseMessage body as a JObject.
    public static async Task<JObject?> ReadResponseAsync(HttpResponseMessage response, string apiLabel)
    {
        var raw = await response.Content.ReadAsStringAsync();
        Log.Debug("{Api} HTTP {Status} | Body={Body}", apiLabel, (int)response.StatusCode, raw);
        return ParseObject(raw);
    }
}
