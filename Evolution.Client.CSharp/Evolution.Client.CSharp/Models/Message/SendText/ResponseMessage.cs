using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Evolution.Client.CSharp.Models.Message.SendText
{
    public class ResponseMessage
    {
        public Key Key { get; set; }

        public string PushName { get; set; }

        public string Status { get; set; }

        public Message Message { get; set; }

        public object? ContextInfo { get; set; }

        public string MessageType { get; set; }

        public long MessageTimestamp { get; set; }

        public string InstanceId { get; set; }

        public string Source { get; set; }
    }
}
