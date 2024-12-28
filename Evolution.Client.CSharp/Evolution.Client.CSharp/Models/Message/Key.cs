using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Evolution.Client.CSharp.Models.Message
{

    public class Key
    {
        [JsonPropertyName("remoteJid")]
        public string RemoteJid { get; set; }

        [JsonPropertyName("fromMe")]
        public bool FromMe { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
