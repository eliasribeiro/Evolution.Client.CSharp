using Evolution.Client.CSharp.Models.Instance.Create;
using Evolution.Client.CSharp.Models.Message.SendText;
using Evolution.Client.CSharp.Models.Message.SendMedia;
using Evolution.Client.CSharp.Models.Message.SendButton;
using Evolution.Client.CSharp.Models.Message.SendPoll;
using Evolution.Client.CSharp.Models.Message.SendList;
using Evolution.Client.CSharp.Models.Message.SendStatus;
using Evolution.Client.CSharp.Models.Message.SendLocation;
using Evolution.Client.CSharp.Models.Message.SendContact;
using Evolution.Client.CSharp.Models.Message.SendReaction;
using Evolution.Client.CSharp.Models.Message.SendSticker;
using Evolution.Client.CSharp.Models.Message.SendAudio;
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
        public async Task<ResponseButtonMessage> SendButton(string instance, RequestButtonMessage request)
        {
            return await this.client.PostAsync<ResponseButtonMessage>($"message/sendButton/{instance}", request);
        }
        public async Task<ResponsePollMessage> SendPoll(string instance, RequestPollMessage request)
        {
            return await this.client.PostAsync<ResponsePollMessage>($"message/sendPoll/{instance}", request);
        }
        public async Task<ResponseListMessage> SendList(string instance, RequestListMessage request)
        {
            return await this.client.PostAsync<ResponseListMessage>($"message/sendList/{instance}", request);
        }
        public async Task<ResponseStatusMessage> SendStatus(string instance, RequestStatusMessage request)
        {
            return await this.client.PostAsync<ResponseStatusMessage>($"message/sendStatus/{instance}", request);
        }
        public async Task<ResponseLocationMessage> SendLocation(string instance, RequestLocationMessage request)
        {
            return await this.client.PostAsync<ResponseLocationMessage>($"message/sendLocation/{instance}", request);
        }
        public async Task<ResponseContactMessage> SendContact(string instance, RequestContactMessage request)
        {
            return await this.client.PostAsync<ResponseContactMessage>($"message/sendContact/{instance}", request);
        }
        public async Task<ResponseReactionMessage> SendReaction(string instance, RequestReactionMessage request)
        {
            return await this.client.PostAsync<ResponseReactionMessage>($"message/sendReaction/{instance}", request);
        }
        public async Task<ResponseStickerMessage> SendSticker(string instance, RequestStickerMessage request)
        {
            var files = new Dictionary<string, (string, byte[], string)>
            {
                { "file", (request.FileName, request.FileBytes, request.MimeType) }
            };
            return await this.client.PostAsync<ResponseStickerMessage>($"message/sendSticker/{instance}", request, null, files);
        }
        public async Task<ResponseAudioMessage> SendAudio(string instance, RequestAudioMessage request)
        {
            var files = new Dictionary<string, (string, byte[], string)>
            {
                { "file", (request.FileName, request.FileBytes, request.MimeType) }
            };
            return await this.client.PostAsync<ResponseAudioMessage>($"message/sendAudio/{instance}", request, null, files);
        }
    }
}
