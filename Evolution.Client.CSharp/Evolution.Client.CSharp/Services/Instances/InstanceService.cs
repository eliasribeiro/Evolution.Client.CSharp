using Evolution.Client.CSharp.Models.Instance.ConnectionStatus;
using Evolution.Client.CSharp.Models.Instance.Create;
using Evolution.Client.CSharp.Models.Instance.FetchInstances;
using Evolution.Client.CSharp.Models.Instance.InstanceConnect;
using Evolution.Client.CSharp.Models.Instance.LogoutInstance;

namespace Evolution.Client.CSharp.Services.Instances
{
    public class InstanceService
    {
        private readonly EvolutionClient client;
        public InstanceService(EvolutionClient client)
        {
            this.client = client;
        }

        public async Task<ResponseInstance> CreateInstance(RequestCreateInstance request) => await this.client.PostAsync<ResponseInstance>("instance/create", request);
        public async Task<ResponseFetchInstances> FetchInstance() => await this.client.GetAsync<ResponseFetchInstances>("instance/fetchInstances");
        public async Task<ResponseInstanceConnect> InstanceConnect(string instance) => await this.client.GetAsync<ResponseInstanceConnect>($"instance/connect/{instance}");
        public async Task<ResponseInstanceConnect> RestartInstance(string instance) => await this.client.PostAsync<ResponseInstanceConnect>($"instance/restart/{instance}");
        public async Task<ResponseInstanceStatus> ConnectionStatus(string instance) => await this.client.GetAsync<ResponseInstanceStatus>($"instance/connectionState/{instance}");
        public async Task<ResponseLogoutInstance> LogoutInstance(string instance) => await this.client.DeleteAsync<ResponseLogoutInstance>($"instance/logout/{instance}");
        public async Task<ResponseDeleteInstance> DeleteInstance(string instance) => await this.client.DeleteAsync<ResponseDeleteInstance>($"instance/delete/{instance}");
    }
}
