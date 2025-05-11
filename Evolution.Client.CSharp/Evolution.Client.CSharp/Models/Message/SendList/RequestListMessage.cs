using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message.SendList
{
    public class RequestListMessage
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("items")]
        public List<ListItem> Items { get; set; }
    }

    public class ListItem
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
} 