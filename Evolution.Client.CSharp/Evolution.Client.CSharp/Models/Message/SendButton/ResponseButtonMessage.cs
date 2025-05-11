using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message.SendButton
{
    public class ResponseButtonMessage
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("messageId")]
        public string MessageId { get; set; }

        [JsonPropertyName("instanceId")]
        public string InstanceId { get; set; }
    }
} 