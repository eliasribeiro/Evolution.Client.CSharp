using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Evolution.Client.CSharp.Models.Message.SendText
{
    public class RequestMessage
    {
        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("delay")]
        public int? Delay { get; set; }

        [JsonPropertyName("quoted")]
        public QuotedMessage? Quoted { get; set; }

        [JsonPropertyName("linkPreview")]
        public bool? LinkPreview { get; set; }

        [JsonPropertyName("mentionsEveryOne")]
        public bool? MentionsEveryOne { get; set; }

        [JsonPropertyName("mentioned")]
        public List<string>? Mentioned { get; set; }
    }
    public class QuotedMessage
    {
        [JsonPropertyName("key")]
        public Key? Key { get; set; }

        [JsonPropertyName("message")]
        public Message? Message { get; set; }
    }

}
