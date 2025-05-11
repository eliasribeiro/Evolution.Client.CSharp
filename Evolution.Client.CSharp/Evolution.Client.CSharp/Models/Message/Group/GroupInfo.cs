using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message.Group
{
    public class GroupInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("participants")]
        public List<string> Participants { get; set; }
    }
} 