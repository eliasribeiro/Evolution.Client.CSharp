using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message.SendContact
{
    public class RequestContactMessage
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("contactName")]
        public string ContactName { get; set; }

        [JsonPropertyName("contactNumber")]
        public string ContactNumber { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
} 