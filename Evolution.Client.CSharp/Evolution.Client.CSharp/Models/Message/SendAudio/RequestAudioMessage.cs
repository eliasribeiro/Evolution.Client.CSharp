using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message.SendAudio
{
    public class RequestAudioMessage
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonIgnore]
        public string FileName { get; set; }
        [JsonIgnore]
        public byte[] FileBytes { get; set; }
        [JsonIgnore]
        public string MimeType { get; set; }
    }
} 