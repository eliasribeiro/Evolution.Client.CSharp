using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Webhook;

/// <summary>
/// Request model for setting webhook configuration
/// </summary>
public class SetWebhookRequest
{
    /// <summary>
    /// Enable webhook to instance
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    /// <summary>
    /// Webhook URL
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Enables Webhook by events
    /// </summary>
    [JsonPropertyName("webhookByEvents")]
    public bool WebhookByEvents { get; set; }

    /// <summary>
    /// Sends files in base64 when available
    /// </summary>
    [JsonPropertyName("webhookBase64")]
    public bool WebhookBase64 { get; set; }

    /// <summary>
    /// Events to be sent to the Webhook
    /// </summary>
    [JsonPropertyName("events")]
    public string[] Events { get; set; } = Array.Empty<string>();
}