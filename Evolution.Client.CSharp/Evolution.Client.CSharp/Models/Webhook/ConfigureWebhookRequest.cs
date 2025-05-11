using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Webhook
{
    public class ConfigureWebhookRequest
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("webhook_by_events")]
        public bool WebhookByEvents { get; set; } = false;

        [JsonPropertyName("webhook_base64")]
        public bool WebhookBase64 { get; set; } = false;

        [JsonPropertyName("events")]
        public List<string> Events { get; set; }
    }
} 