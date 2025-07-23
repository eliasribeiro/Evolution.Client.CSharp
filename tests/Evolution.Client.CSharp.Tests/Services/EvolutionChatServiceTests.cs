using System.Net;
using System.Text;
using System.Text.Json;
using Evolution.Client.CSharp.Configuration;
using Evolution.Client.CSharp.Models.Chat;
using Evolution.Client.CSharp.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace Evolution.Client.CSharp.Tests.Services;

public class EvolutionChatServiceTests
{
    private readonly Mock<ILogger<EvolutionChatService>> _mockLogger;
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly Mock<IOptions<EvolutionApiOptions>> _mockOptions;
    private readonly HttpClient _httpClient;
    private readonly EvolutionChatService _service;

    public EvolutionChatServiceTests()
    {
        _mockLogger = new Mock<ILogger<EvolutionChatService>>();
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _mockOptions = new Mock<IOptions<EvolutionApiOptions>>();

        _mockOptions.Setup(x => x.Value).Returns(new EvolutionApiOptions
        {
            BaseUrl = "http://localhost:8080/",
            ApiKey = "test-key",
            TimeoutSeconds = 30
        });

        _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
        {
            BaseAddress = new Uri("http://localhost:8080/")
        };
        _service = new EvolutionChatService(_httpClient, _mockOptions.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task FindChatsAsync_WithValidInstanceName_ReturnsChats()
    {
        // Arrange
        var instanceName = "test-instance";
        var expectedChats = new List<ChatRecord>
        {
            new ChatRecord
            {
                Id = "cmdg73d102bawn54q5atqujn4",
                RemoteJid = "556186027665@s.whatsapp.net",
                PushName = "João Silva",
                ProfilePicUrl = "https://example.com/profile.jpg",
                UpdatedAt = DateTime.UtcNow,
                WindowStart = DateTime.UtcNow.AddHours(-1),
                WindowExpires = DateTime.UtcNow.AddHours(23),
                WindowActive = true
            },
            new ChatRecord
            {
                Id = "cmdg73d102bacn54qkn2vaslh",
                RemoteJid = "556192765693-1418901047@g.us",
                PushName = "Grupo Teste",
                ProfilePicUrl = "https://example.com/group.jpg",
                UpdatedAt = DateTime.UtcNow,
                WindowStart = DateTime.UtcNow.AddHours(-1),
                WindowExpires = DateTime.UtcNow.AddHours(23),
                WindowActive = true
            }
        };

        var responseJson = JsonSerializer.Serialize(expectedChats);
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseJson, Encoding.UTF8, "application/json")
        };

        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Post &&
                    req.RequestUri!.ToString().Contains($"/chat/findChats/{instanceName}")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        // Act
        var result = await _service.FindChatsAsync(instanceName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(expectedChats[0].Id, result[0].Id);
        Assert.Equal(expectedChats[0].RemoteJid, result[0].RemoteJid);
        Assert.Equal(expectedChats[0].PushName, result[0].PushName);
        Assert.Equal(expectedChats[1].Id, result[1].Id);
        Assert.Equal(expectedChats[1].RemoteJid, result[1].RemoteJid);
        Assert.Equal(expectedChats[1].PushName, result[1].PushName);
    }

    [Fact]
    public async Task FindChatsAsync_WithEmptyInstanceName_ThrowsArgumentException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.FindChatsAsync(""));
        await Assert.ThrowsAsync<ArgumentException>(() => _service.FindChatsAsync(null!));
        await Assert.ThrowsAsync<ArgumentException>(() => _service.FindChatsAsync("   "));
    }

    [Fact]
    public async Task FindChatsAsync_WithNotFoundResponse_ThrowsInvalidOperationException()
    {
        // Arrange
        var instanceName = "non-existent-instance";
        var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
            Content = new StringContent("Instance not found", Encoding.UTF8, "application/json")
        };

        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _service.FindChatsAsync(instanceName));
        Assert.Contains("não encontrada", exception.Message);
    }

    [Fact]
    public async Task FindChatsAsync_WithSearchCriteria_SendsCorrectRequest()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new FindChatsRequest
        {
            Where = new FindChatsWhere
            {
                RemoteJid = "556186027665@s.whatsapp.net",
                PushName = "João Silva"
            }
        };

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("[]", Encoding.UTF8, "application/json")
        };

        HttpRequestMessage? capturedRequest = null;
        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .Callback<HttpRequestMessage, CancellationToken>((req, _) => capturedRequest = req)
            .ReturnsAsync(responseMessage);

        // Act
        await _service.FindChatsAsync(instanceName, request);

        // Assert
        Assert.NotNull(capturedRequest);
        Assert.Equal(HttpMethod.Post, capturedRequest.Method);
        Assert.Contains($"/chat/findChats/{instanceName}", capturedRequest.RequestUri!.ToString());
        
        var requestContent = await capturedRequest.Content!.ReadAsStringAsync();
        var deserializedRequest = JsonSerializer.Deserialize<FindChatsRequest>(requestContent);
        Assert.NotNull(deserializedRequest?.Where);
        Assert.Equal(request.Where.RemoteJid, deserializedRequest.Where.RemoteJid);
        Assert.Equal(request.Where.PushName, deserializedRequest.Where.PushName);
    }

    [Fact]
    public async Task FetchProfilePicUrlAsync_WithValidRequest_ReturnsProfilePicUrl()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new FetchProfilePicUrlRequest
        {
            Number = "5511999999999"
        };
        var expectedResponse = new FetchProfilePicUrlResponse
        {
            Wuid = "5511999999999@s.whatsapp.net",
            ProfilePictureUrl = "https://pps.whatsapp.net/v/t61.24694-24/example.jpg"
        };

        var responseJson = JsonSerializer.Serialize(expectedResponse);
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseJson, Encoding.UTF8, "application/json")
        };

        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Post &&
                    req.RequestUri!.ToString().Contains($"/chat/fetchProfilePictureUrl/{instanceName}")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        // Act
        var result = await _service.FetchProfilePicUrlAsync(instanceName, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Wuid, result.Wuid);
        Assert.Equal(expectedResponse.ProfilePictureUrl, result.ProfilePictureUrl);
    }

    [Fact]
    public async Task FetchProfilePicUrlAsync_WithEmptyInstanceName_ThrowsArgumentException()
    {
        // Arrange
        var request = new FetchProfilePicUrlRequest { Number = "5511999999999" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.FetchProfilePicUrlAsync("", request));
        await Assert.ThrowsAsync<ArgumentException>(() => _service.FetchProfilePicUrlAsync(null!, request));
        await Assert.ThrowsAsync<ArgumentException>(() => _service.FetchProfilePicUrlAsync("   ", request));
    }

    [Fact]
    public async Task FetchProfilePicUrlAsync_WithNullRequest_ThrowsArgumentNullException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.FetchProfilePicUrlAsync("test-instance", null!));
    }

    [Fact]
    public async Task FetchProfilePicUrlAsync_WithEmptyNumber_ThrowsArgumentException()
    {
        // Arrange
        var request = new FetchProfilePicUrlRequest { Number = "" };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.FetchProfilePicUrlAsync("test-instance", request));
    }

    [Fact]
    public async Task FetchProfilePicUrlAsync_WithNotFoundResponse_ThrowsInvalidOperationException()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new FetchProfilePicUrlRequest { Number = "5511999999999" };
        var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
            Content = new StringContent("Instance not found", Encoding.UTF8, "application/json")
        };

        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _service.FetchProfilePicUrlAsync(instanceName, request));
        Assert.Contains("não encontrada", exception.Message);
    }
}
