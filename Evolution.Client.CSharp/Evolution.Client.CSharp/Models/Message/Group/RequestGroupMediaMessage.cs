using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message.Group
{
    public class RequestGroupMediaMessage
    {
        [JsonPropertyName("groupId")]
        public string GroupId { get; set; }

        [JsonPropertyName("caption")]
        public string? Caption { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonIgnore]
        public string FileName { get; set; }
        [JsonIgnore]
        public byte[] FileBytes { get; set; }
        [JsonIgnore]
        public string MimeType { get; set; }
    }
} 