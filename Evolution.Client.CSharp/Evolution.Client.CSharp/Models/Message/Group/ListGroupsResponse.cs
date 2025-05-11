using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message.Group
{
    public class ListGroupsResponse
    {
        [JsonPropertyName("groups")]
        public List<GroupInfo> Groups { get; set; }
    }
} 