using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Instance.ConnectionStatus
{
    public class ResponseInstanceStatus
    {
        [JsonPropertyName("instance")]
        public Instance Instance { get; set; }
    }
}
