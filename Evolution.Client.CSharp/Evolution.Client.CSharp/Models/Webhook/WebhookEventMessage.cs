using System.Text.Json.Serialization;
using Evolution.Client.CSharp.Models.Message;

namespace Evolution.Client.CSharp.Models.Webhook
{
    public class WebhookEventMessage
    {
        [JsonPropertyName("numberId")]
        public string NumberId { get; set; }

        [JsonPropertyName("key")]
        public Key Key { get; set; }

        [JsonPropertyName("pushName")]
        public string PushName { get; set; }

        [JsonPropertyName("message")]
        public Message.Message Message { get; set; }

        [JsonPropertyName("messageType")]
        public string MessageType { get; set; }
    }
} 