using Evolution.Client.CSharp.Models.Instance.Create;
using Evolution.Client.CSharp.Models.Message.SendText;
using Evolution.Client.CSharp.Models.Message.SendMedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Client.CSharp.Services.Messages
{
    public class MessageService
    {
        private readonly EvolutionClient client;
        public MessageService(EvolutionClient client)
        {
            this.client = client;
        }
        public async Task<ResponseMessage> SendText(string instance, RequestMessage request) => await this.client.PostAsync<ResponseMessage>($"message/sendText/{instance}", request);
        public async Task<ResponseMediaMessage> SendMedia(string instance, RequestMediaMessage request)
        {
            var files = new Dictionary<string, (string, byte[], string)>
            {
                { "file", (request.FileName, request.FileBytes, request.MimeType) }
            };
            return await this.client.PostAsync<ResponseMediaMessage>($"message/sendMedia/{instance}", request, null, files);
        }
    }
}
