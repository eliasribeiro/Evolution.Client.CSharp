using System.Net;
using System.Text.Json;
using Evolution.Client.CSharp.Configuration;
using Evolution.Client.CSharp.Models;
using Evolution.Client.CSharp.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace Evolution.Client.CSharp.Tests.Services;

public class EvolutionInformationServiceTests
{
    private readonly Mock<IOptions<EvolutionApiOptions>> _optionsMock;
    private readonly Mock<ILogger<EvolutionInformationService>> _loggerMock;
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly EvolutionInformationService _service;

    public EvolutionInformationServiceTests()
    {
        // Configurar mocks
        _optionsMock = new Mock<IOptions<EvolutionApiOptions>>();
        _optionsMock.Setup(o => o.Value).Returns(new EvolutionApiOptions
        {
            BaseUrl = "http://test-api.com",
            ApiKey = "test-api-key",
            TimeoutSeconds = 30
        });

        _loggerMock = new Mock<ILogger<EvolutionInformationService>>();
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);

        // Criar o serviço
        _service = new EvolutionInformationService(_httpClient, _optionsMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetInformationAsync_ReturnsValidResponse_WhenApiCallSucceeds()
    {
        // Arrange
        var expectedResponse = new InformationResponse
        {
            Status = 200,
            Message = "Welcome to the Evolution API, it is working!",
            Version = "1.7.4",
            Swagger = "http://example.evolution-api.com/docs",
            Manager = "http://example.evolution-api.com/manager",
            Documentation = "https://doc.evolution-api.com"
        };

        var jsonResponse = JsonSerializer.Serialize(expectedResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse)
            });

        // Act
        var result = await _service.GetInformationAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Status, result.Status);
        Assert.Equal(expectedResponse.Message, result.Message);
        Assert.Equal(expectedResponse.Version, result.Version);
        Assert.Equal(expectedResponse.Swagger, result.Swagger);
        Assert.Equal(expectedResponse.Manager, result.Manager);
        Assert.Equal(expectedResponse.Documentation, result.Documentation);

        _httpMessageHandlerMock
            .Protected()
            .Verify<Task<HttpResponseMessage>>(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri != null && req.RequestUri.ToString() == "http://test-api.com/"),
                ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task GetInformationAsync_ThrowsHttpRequestException_WhenApiCallFails()
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
                Content = new StringContent("Internal Server Error")
            });

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => _service.GetInformationAsync());
    }

    [Fact]
    public async Task GetInformationAsync_ThrowsJsonException_WhenResponseIsInvalid()
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
                Content = new StringContent("Invalid JSON")
            });

        // Act & Assert
        await Assert.ThrowsAsync<JsonException>(() => _service.GetInformationAsync());
    }
}