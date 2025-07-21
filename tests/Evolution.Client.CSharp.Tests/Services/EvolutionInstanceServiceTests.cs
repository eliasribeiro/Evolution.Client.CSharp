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
        var expectedResponse = new InstancesResponse
        {
            new InstanceResponse
            {
                Instance = new InstanceDetails
                {
                    InstanceName = "example-name",
                    InstanceId = "421a4121-a3d9-40cc-a8db-c3a1df353126",
                    Owner = "553198296801@s.whatsapp.net",
                    ProfileName = "Guilherme Gomes",
                    ProfileStatus = "This is the profile status.",
                    Status = "open",
                    ServerUrl = "https://example.evolution-api.com",
                    ApiKey = "B3844804-481D-47A4-B69C-F14B4206EB56",
                    Integration = new IntegrationDetails
                    {
                        IntegrationType = "WHATSAPP-BAILEYS",
                        WebhookWaBusiness = "https://example.evolution-api.com/webhook/whatsapp/db5e11d3-ded5-4d91-b3fb-48272688f206"
                    }
                }
            },
            new InstanceResponse
            {
                Instance = new InstanceDetails
                {
                    InstanceName = "teste-docs",
                    InstanceId = "af6c5b7c-ee27-4f94-9ea8-192393746ddd",
                    Status = "close",
                    ServerUrl = "https://example.evolution-api.com",
                    ApiKey = "123456",
                    Integration = new IntegrationDetails
                    {
                        Token = "123456",
                        WebhookWaBusiness = "https://example.evolution-api.com/webhook/whatsapp/teste-docs"
                    }
                }
            }
        };

        var jsonResponse = JsonSerializer.Serialize(expectedResponse);

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
        Assert.Equal(2, result.Count);
        Assert.Equal("example-name", result[0].Instance?.InstanceName);
        Assert.Equal("teste-docs", result[1].Instance?.InstanceName);
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