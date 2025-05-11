using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message.SendReaction
{
    public class RequestReactionMessage
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("messageId")]
        public string MessageId { get; set; }

        [JsonPropertyName("emoji")]
        public string Emoji { get; set; }
    }
} 