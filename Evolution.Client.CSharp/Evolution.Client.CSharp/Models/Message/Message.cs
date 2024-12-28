using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Evolution.Client.CSharp.Models.Message
{
    public class Message
    {
        [JsonPropertyName("conversation")]
        public string Conversation { get; set; }
    }
}
