using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Webhook;

/// <summary>
/// Response model for finding webhook configuration
/// </summary>
public class FindWebhookResponse
{
    /// <summary>
    /// Indicates whether the webhook is enabled
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    /// <summary>
    /// The URL of the webhook
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// List of events the webhook is subscribed to
    /// </summary>
    [JsonPropertyName("events")]
    public string[] Events { get; set; } = Array.Empty<string>();
}