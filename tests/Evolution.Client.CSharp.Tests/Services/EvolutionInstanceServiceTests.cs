using System.Net;
using System.Text.Json;
using Evolution.Client.CSharp.Configuration;
using Evolution.Client.CSharp.Models.Instance;
using Evolution.Client.CSharp.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace Evolution.Client.CSharp.Tests.Services;

public class EvolutionInstanceServiceTests
{
    private readonly Mock<IOptions<EvolutionApiOptions>> _optionsMock;
    private readonly Mock<ILogger<EvolutionInstanceService>> _loggerMock;
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly EvolutionInstanceService _service;

    public EvolutionInstanceServiceTests()
    {
        // Configurar mocks
        _optionsMock = new Mock<IOptions<EvolutionApiOptions>>();
        _optionsMock.Setup(o => o.Value).Returns(new EvolutionApiOptions
        {
            BaseUrl = "http://test-api.com",
            ApiKey = "test-api-key",
            TimeoutSeconds = 30
        });

        _loggerMock = new Mock<ILogger<EvolutionInstanceService>>();
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);

        // Criar o serviço
        _service = new EvolutionInstanceService(_httpClient, _optionsMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task FetchInstancesAsync_ReturnsValidResponse_WhenApiCallSucceeds()
    {
        // Arrange
        var instancesArray = new List<InstanceResponse>
        {
            new InstanceResponse
            {
                Id = "347f2197-7026-489c-b3db-ac40a08c0e91",
                Name = "Elias",
                ConnectionStatus = "close",
                Integration = "WHATSAPP-BAILEYS",
                Token = "9E3F517F-8CFC-43DD-8CBD-0BDD6B85F4D1",
                ClientName = "evolution_exchange",
                CreatedAt = DateTime.Parse("2025-07-23T13:48:10.241Z"),
                UpdatedAt = DateTime.Parse("2025-07-23T13:48:10.241Z"),
                Setting = new InstanceSettingV2
                {
                    Id = "cmdg0qt8s0009n54q3gs93dja",
                    RejectCall = false,
                    MsgCall = "",
                    GroupsIgnore = false,
                    AlwaysOnline = false,
                    ReadMessages = false,
                    ReadStatus = false,
                    SyncFullHistory = false,
                    WavoipToken = "",
                    CreatedAt = DateTime.Parse("2025-07-23T13:48:10.251Z"),
                    UpdatedAt = DateTime.Parse("2025-07-23T13:48:10.251Z"),
                    InstanceId = "347f2197-7026-489c-b3db-ac40a08c0e91"
                },
                Count = new InstanceCountV2
                {
                    Message = 0,
                    Contact = 0,
                    Chat = 0
                }
            }
        };

        var jsonResponse = JsonSerializer.Serialize(instancesArray);

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri != null &&
                    req.RequestUri.ToString() == "http://test-api.com/instance/fetchInstances"),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse)
            });

        // Act
        var result = await _service.FetchInstancesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("347f2197-7026-489c-b3db-ac40a08c0e91", result[0].Id);
        Assert.Equal("Elias", result[0].Name);
        Assert.Equal("close", result[0].ConnectionStatus);
        Assert.Equal("WHATSAPP-BAILEYS", result[0].Integration);
        Assert.Equal("9E3F517F-8CFC-43DD-8CBD-0BDD6B85F4D1", result[0].Token);
        Assert.Equal("evolution_exchange", result[0].ClientName);
        
        // Verifica as configurações
        Assert.NotNull(result[0].Setting);
        Assert.Equal("cmdg0qt8s0009n54q3gs93dja", result[0].Setting!.Id);
        Assert.False(result[0].Setting!.RejectCall);
        
        // Verifica as contagens
        Assert.NotNull(result[0].Count);
        Assert.Equal(0, result[0].Count!.Message);
        Assert.Equal(0, result[0].Count!.Contact);
        Assert.Equal(0, result[0].Count!.Chat);
    }

    [Fact]
    public async Task FetchInstancesAsync_ThrowsHttpRequestException_WhenApiCallFails()
    {
        // Arrange
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent("{\"message\":\"Internal Server Error\"}")
            });

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => _service.FetchInstancesAsync());
    }

    [Fact]
    public async Task FetchInstancesAsync_ThrowsJsonException_WhenResponseIsInvalid()
    {
        // Arrange
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("invalid json")
            });

        // Act & Assert
        await Assert.ThrowsAsync<JsonException>(() => _service.FetchInstancesAsync());
    }
}