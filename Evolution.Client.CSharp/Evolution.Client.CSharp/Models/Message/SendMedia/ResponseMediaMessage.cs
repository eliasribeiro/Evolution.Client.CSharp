using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message.SendMedia
{
    public class ResponseMediaMessage
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("messageId")]
        public string MessageId { get; set; }

        [JsonPropertyName("messageType")]
        public string MessageType { get; set; }

        [JsonPropertyName("instanceId")]
        public string InstanceId { get; set; }
    }
} 