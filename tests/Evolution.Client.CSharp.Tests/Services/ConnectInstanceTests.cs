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

public class ConnectInstanceTests
{
    private readonly Mock<IOptions<EvolutionApiOptions>> _optionsMock;
    private readonly Mock<ILogger<EvolutionInstanceService>> _loggerMock;
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly EvolutionInstanceService _service;

    public ConnectInstanceTests()
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
    public async Task ConnectInstanceAsync_ReturnsValidResponse_WhenApiCallSucceeds()
    {
        // Arrange
        var instanceName = "test-instance";
        var expectedResponse = new ConnectInstanceResponse
        {
            PairingCode = null,
            Code = "2@cEErzA9HuHpurlxLeZ742fs/b4Rx824NjtzJZLHRQp7xrFdoyPpn4t2p3GGhxnT5oww+EtiRF0qDWPzhP82PN7vnPbM8tGQOY6Q=",
            Base64 = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAVwAAAFcCAYAAACEFgYsAAAjFklEQVR4AezB0a0kya5ty1GBJQX1oi5TBOpCvaYadfvv",
            Count = 2
        };

        var jsonResponse = JsonSerializer.Serialize(expectedResponse);

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri!.ToString() == $"http://test-api.com/instance/connect/{instanceName}"),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse)
            });

        // Act
        var result = await _service.ConnectInstanceAsync(instanceName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Code, result.Code);
        Assert.Equal(expectedResponse.Base64, result.Base64);
        Assert.Equal(expectedResponse.Count, result.Count);
        Assert.Null(result.PairingCode);
    }

    [Fact]
    public async Task ConnectInstanceAsync_ThrowsArgumentException_WhenInstanceNameIsEmpty()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.ConnectInstanceAsync(""));
    }

    [Fact]
    public async Task ConnectInstanceAsync_ThrowsHttpRequestException_WhenApiCallFails()
    {
        // Arrange
        var instanceName = "test-instance";

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri!.ToString() == $"http://test-api.com/instance/connect/{instanceName}"),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("{\"error\":\"Instance not found\"}")
            });

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => _service.ConnectInstanceAsync(instanceName));
    }
}