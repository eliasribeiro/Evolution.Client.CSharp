using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message.SendMedia
{
    public class RequestMediaMessage
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("caption")]
        public string? Caption { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } // image, video, document, audio, etc.

        // O arquivo será enviado via multipart, mas aqui guardamos metadados
        [JsonIgnore]
        public string FileName { get; set; }
        [JsonIgnore]
        public byte[] FileBytes { get; set; }
        [JsonIgnore]
        public string MimeType { get; set; }
    }
} 