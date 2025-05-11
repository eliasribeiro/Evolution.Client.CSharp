using Evolution.Client.CSharp.Models.Message.Group;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evolution.Client.CSharp.Services.Messages
{
    public class GroupService
    {
        private readonly EvolutionClient client;
        public GroupService(EvolutionClient client)
        {
            this.client = client;
        }

        public async Task<ListGroupsResponse> ListGroups(string instance)
            => await this.client.GetAsync<ListGroupsResponse>($"group/list/{instance}");

        public async Task<ResponseMessage> SendText(string instance, RequestGroupTextMessage request)
            => await this.client.PostAsync<ResponseMessage>($"group/sendText/{instance}", request);

        public async Task<ResponseMediaMessage> SendMedia(string instance, RequestGroupMediaMessage request)
        {
            var files = new Dictionary<string, (string, byte[], string)>
            {
                { "file", (request.FileName, request.FileBytes, request.MimeType) }
            };
            return await this.client.PostAsync<ResponseMediaMessage>($"group/sendMedia/{instance}", request, null, files);
        }
    }
} 