using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Instance
{
    public class Instance
    {

        [JsonPropertyName("instanceName")]
        public string InstanceName { get; set; }
        [JsonPropertyName("state")]
        public string State { get; set; }
    }
}
