using System.Net;
using System.Text;
using System.Text.Json;
using Evolution.Client.CSharp.Configuration;
using Evolution.Client.CSharp.Models.Message;
using Evolution.Client.CSharp.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace Evolution.Client.CSharp.Tests.Services;

public class EvolutionMessageServiceTests
{
    private readonly Mock<ILogger<EvolutionMessageService>> _mockLogger;
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly Mock<IOptions<EvolutionApiOptions>> _mockOptions;
    private readonly HttpClient _httpClient;
    private readonly EvolutionMessageService _service;

    public EvolutionMessageServiceTests()
    {
        _mockLogger = new Mock<ILogger<EvolutionMessageService>>();
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
        _service = new EvolutionMessageService(_httpClient, _mockOptions.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task SendTextAsync_ValidRequest_ReturnsSuccess()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SendTextRequest
        {
            Number = "5511999999999",
            Text = "Olá! Como você está?",
            LinkPreview = true
        };

        var expectedResponse = new SendTextResponse
        {
            Key = new MessageKey
            {
                RemoteJid = "5511999999999@s.whatsapp.net",
                FromMe = true,
                Id = "BAE594145F4C59B4"
            },
            Message = new SentMessageContent
            {
                ExtendedTextMessage = new ExtendedTextMessage
                {
                    Text = "Olá! Como você está?"
                }
            },
            MessageTimestamp = 1717689097,
            Status = "PENDING"
        };

        var responseJson = JsonSerializer.Serialize(expectedResponse);
        var httpResponse = new HttpResponseMessage(HttpStatusCode.Created)
        {
            Content = new StringContent(responseJson, Encoding.UTF8, "application/json")
        };

        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);

        // Act
        var result = await _service.SendTextAsync(instanceName, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("BAE594145F4C59B4", result.Key.Id);
        Assert.Equal("5511999999999@s.whatsapp.net", result.Key.RemoteJid);
        Assert.True(result.Key.FromMe);
        Assert.Equal("Olá! Como você está?", result.Message.ExtendedTextMessage?.Text);
        Assert.Equal("PENDING", result.Status);
        Assert.Equal(1717689097, result.MessageTimestamp);
    }

    [Fact]
    public async Task SendTextAsync_EmptyInstanceName_ThrowsArgumentException()
    {
        // Arrange
        var request = new SendTextRequest
        {
            Number = "5511999999999",
            Text = "Teste"
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.SendTextAsync("", request));
        await Assert.ThrowsAsync<ArgumentException>(() => _service.SendTextAsync(null!, request));
    }

    [Fact]
    public async Task SendTextAsync_NullRequest_ThrowsArgumentNullException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.SendTextAsync("test-instance", null!));
    }

    [Fact]
    public async Task SendTextAsync_EmptyNumber_ThrowsArgumentException()
    {
        // Arrange
        var request = new SendTextRequest
        {
            Number = "",
            Text = "Teste"
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.SendTextAsync("test-instance", request));
    }

    [Fact]
    public async Task SendTextAsync_EmptyText_ThrowsArgumentException()
    {
        // Arrange
        var request = new SendTextRequest
        {
            Number = "5511999999999",
            Text = ""
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.SendTextAsync("test-instance", request));
    }

    [Fact]
    public async Task SendTextAsync_HttpError_ThrowsHttpRequestException()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SendTextRequest
        {
            Number = "5511999999999",
            Text = "Teste"
        };

        var httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
            Content = new StringContent("Bad Request", Encoding.UTF8, "application/json")
        };

        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => _service.SendTextAsync(instanceName, request));
    }

    [Fact]
    public async Task SendTextAsync_InstanceNotFound_ThrowsInvalidOperationException()
    {
        // Arrange
        var instanceName = "nonexistent-instance";
        var request = new SendTextRequest
        {
            Number = "5511999999999",
            Text = "Teste"
        };

        var httpResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
            Content = new StringContent("Instance not found", Encoding.UTF8, "application/json")
        };

        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.SendTextAsync(instanceName, request));
    }

    [Fact]
    public async Task SendTextAsync_WithQuotedMessage_SendsCorrectRequest()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SendTextRequest
        {
            Number = "5511999999999",
            Text = "Esta é uma resposta",
            Quoted = new QuotedMessage
            {
                Key = new QuotedMessageKey { Id = "QUOTED_MESSAGE_ID" },
                Message = new QuotedMessageContent { Conversation = "Mensagem original" }
            }
        };

        var expectedResponse = new SendTextResponse
        {
            Key = new MessageKey
            {
                RemoteJid = "5511999999999@s.whatsapp.net",
                FromMe = true,
                Id = "NEW_MESSAGE_ID"
            },
            Message = new SentMessageContent
            {
                ExtendedTextMessage = new ExtendedTextMessage
                {
                    Text = "Esta é uma resposta"
                }
            },
            MessageTimestamp = 1717689097,
            Status = "PENDING"
        };

        var responseJson = JsonSerializer.Serialize(expectedResponse);
        var httpResponse = new HttpResponseMessage(HttpStatusCode.Created)
        {
            Content = new StringContent(responseJson, Encoding.UTF8, "application/json")
        };

        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);

        // Act
        var result = await _service.SendTextAsync(instanceName, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("NEW_MESSAGE_ID", result.Key.Id);
        Assert.Equal("Esta é uma resposta", result.Message.ExtendedTextMessage?.Text);
    }

    [Fact]
    public async Task SendTextAsync_WithMentions_SendsCorrectRequest()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SendTextRequest
        {
            Number = "5511999999999",
            Text = "Olá pessoal!",
            MentionsEveryOne = true,
            Mentioned = new List<string> { "5511888888888", "5511777777777" }
        };

        var expectedResponse = new SendTextResponse
        {
            Key = new MessageKey
            {
                RemoteJid = "5511999999999@s.whatsapp.net",
                FromMe = true,
                Id = "MENTION_MESSAGE_ID"
            },
            Message = new SentMessageContent
            {
                ExtendedTextMessage = new ExtendedTextMessage
                {
                    Text = "Olá pessoal!"
                }
            },
            MessageTimestamp = 1717689097,
            Status = "PENDING"
        };

        var responseJson = JsonSerializer.Serialize(expectedResponse);
        var httpResponse = new HttpResponseMessage(HttpStatusCode.Created)
        {
            Content = new StringContent(responseJson, Encoding.UTF8, "application/json")
        };

        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);

        // Act
        var result = await _service.SendTextAsync(instanceName, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("MENTION_MESSAGE_ID", result.Key.Id);
        Assert.Equal("Olá pessoal!", result.Message.ExtendedTextMessage?.Text);
    }
}
