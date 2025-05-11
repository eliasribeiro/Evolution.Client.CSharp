using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message.SendStatus
{
    public class RequestStatusMessage
    {
        [JsonPropertyName("instance")]
        public string Instance { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
} 