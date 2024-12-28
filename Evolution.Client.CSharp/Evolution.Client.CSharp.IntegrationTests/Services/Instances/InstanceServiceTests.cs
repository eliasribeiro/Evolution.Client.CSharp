using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolution.Client.CSharp.IntegrationTests.Services.Instances
{
    public class InstanceServiceTests
    {
        private readonly EvolutionClient client;
        private readonly InstanceService instanceService;

        public InstanceServiceTests()
        {
            client = new EvolutionClient("https://00-servicos-service-evolution.a5m7vd.easypanel.host", "5CK99Gjz48P4ec1hXW60");
            instanceService = new InstanceService(client);
        }

        [Fact]
        public async Task CreateInstance_ShouldReturnResponseInstance()
        {
            // Arrange

            var request = new RequestCreateInstance { instanceName = "TestInstance" };

            // Act
            var result = await instanceService.CreateInstance(request);
            // Assert
            //Assert.NotNull(result);
        }
        /*
        [Fact]
        public async Task FetchInstance_ShouldReturnListOfResponseInstance()
        {
            // Arrange
            var response = new List<ResponseInstance> { new ResponseInstance { id = "1", name = "TestInstance" } };
            client.Setup(client => client.GetAsync<List<ResponseInstance>>("/instance/fetchInstances", null))
                      .ReturnsAsync(response);

            // Act
            var result = await instanceService.FetchInstance();

            // Assert
            Assert.Equal(response, result);
        }

        [Fact]
        public async Task InstanceConnect_ShouldReturnResponseInstanceConnect()
        {
            // Arrange
            var instance = "TestInstance";
            var response = new ResponseInstanceConnect { Code = "1234" };
            client.Setup(client => client.GetAsync<ResponseInstanceConnect>($"/instance/connect/{instance}", null))
                      .ReturnsAsync(response);

            // Act
            var result = await instanceService.InstanceConnect(instance);

            // Assert
            Assert.Equal(response, result);
        }

        [Fact]
        public async Task RestartInstance_ShouldReturnResponseInstanceConnect()
        {
            // Arrange
            var instance = "TestInstance";
            var response = new ResponseInstanceConnect { Code = "1234" };
            client.Setup(client => client.PostAsync<ResponseInstanceConnect>($"/instance/restart/{instance}", null, null, null))
                      .ReturnsAsync(response);

            // Act
            var result = await instanceService.RestartInstance(instance);

            // Assert
            Assert.Equal(response, result);
        }

        [Fact]
        public async Task ConnectionStatus_ShouldReturnResponseInstanceStatus()
        {
            // Arrange
            var instance = "TestInstance";
            var response = new ResponseInstanceStatus { Instance = new Instance { InstanceName = "TestInstance", State = "Connected" } };
            client.Setup(client => client.GetAsync<ResponseInstanceStatus>($"/instance/connectionState/{instance}", null))
                      .ReturnsAsync(response);

            // Act
            var result = await instanceService.ConnectionStatus(instance);

            // Assert
            Assert.Equal(response, result);
        }

        [Fact]
        public async Task LogoutInstance_ShouldReturnResponseLogoutInstance()
        {
            // Arrange
            var instance = "TestInstance";
            var response = new ResponseLogoutInstance { Status = "Success" };
            client.Setup(client => client.GetAsync<ResponseLogoutInstance>($"/instance/logout/{instance}", null))
                      .ReturnsAsync(response);

            // Act
            var result = await instanceService.LogoutInstance(instance);

            // Assert
            Assert.Equal(response, result);
        }

        [Fact]
        public async Task DeleteInstance_ShouldReturnResponseDeleteInstance()
        {
            // Arrange
            var instance = "TestInstance";
            var response = new ResponseDeleteInstance { Status = "Deleted" };
            client.Setup(client => client.GetAsync<ResponseDeleteInstance>($"/instance/delete/{instance}", null))
                      .ReturnsAsync(response);

            // Act
            var result = await instanceService.DeleteInstance(instance);

            // Assert
            Assert.Equal(response, result);
        }
        */
    }
}
