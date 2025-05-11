using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message.SendButton
{
    public class RequestButtonMessage
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("buttons")]
        public List<Button> Buttons { get; set; }
    }

    public class Button
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
} 