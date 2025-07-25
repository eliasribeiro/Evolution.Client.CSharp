using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Webhook;

/// <summary>
/// Response model for setting webhook configuration
/// </summary>
public class SetWebhookResponse
{
    /// <summary>
    /// Webhook configuration object
    /// </summary>
    [JsonPropertyName("webhook")]
    public WebhookInfo Webhook { get; set; } = new();
}

/// <summary>
/// Webhook information
/// </summary>
public class WebhookInfo
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
    /// List of events the webhook is subscribed to
    /// </summary>
    [JsonPropertyName("events")]
    public string[] Events { get; set; } = Array.Empty<string>();
}