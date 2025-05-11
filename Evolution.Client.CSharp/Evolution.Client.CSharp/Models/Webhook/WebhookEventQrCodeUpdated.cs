using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Webhook
{
    public class WebhookEventQrCodeUpdated
    {
        [JsonPropertyName("instance")]
        public string Instance { get; set; }

        [JsonPropertyName("qrcode")]
        public string QrCode { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }
} 