using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message.Group
{
    public class RequestGroupTextMessage
    {
        [JsonPropertyName("groupId")]
        public string GroupId { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("mentions")]
        public List<string>? Mentions { get; set; }
    }
} 