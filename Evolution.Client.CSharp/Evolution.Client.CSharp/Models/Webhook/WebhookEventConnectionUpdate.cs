using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Webhook
{
    public class WebhookEventConnectionUpdate
    {
        [JsonPropertyName("instance")]
        public string Instance { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }
} 