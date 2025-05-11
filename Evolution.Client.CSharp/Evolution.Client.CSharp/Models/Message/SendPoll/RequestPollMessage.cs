using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message.SendPoll
{
    public class RequestPollMessage
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("question")]
        public string Question { get; set; }

        [JsonPropertyName("options")]
        public List<string> Options { get; set; }
    }
} 