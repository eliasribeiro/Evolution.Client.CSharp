using FluentAssertions;
using Evolution.Client.Core.Http;
using Evolution.Client.Modules;
using NSubstitute;

namespace Evolution.Client.Tests.Modules;

public class MessagesModuleTests
{
    private readonly IHttpService _httpService;
    private readonly MessagesModule _messagesModule;

    public MessagesModuleTests()
    {
        _httpService = Substitute.For<IHttpService>();
        _messagesModule = new MessagesModule(_httpService);
    }

    [Fact]
    public void Constructor_WithNullHttpService_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        var action = () => new MessagesModule(null!);
        action.Should().Throw<ArgumentNullException>()
            .WithParameterName("httpService");
    }

    [Fact]
    public async Task SendTextAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SendTextMessageRequest
        {
            Number = "5511999999999",
            Text = "Hello, World!"
        };
        var expectedResponse = new SendMessageResponse
        {
            MessageId = "msg-123",
            Status = "sent",
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        _httpService.PostAsync<SendTextMessageRequest, SendMessageResponse>(
            Arg.Any<string>(),
            Arg.Any<SendTextMessageRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _messagesModule.SendTextAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<SendTextMessageRequest, SendMessageResponse>(
            $"message/sendText/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task SendTextAsync_WithNullInstanceName_ShouldThrowArgumentException()
    {
        // Arrange
        var request = new SendTextMessageRequest
        {
            Number = "5511999999999",
            Text = "Hello, World!"
        };

        // Act & Assert
        var action = async () => await _messagesModule.SendTextAsync(null!, request);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Nome da instância é obrigatório*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task SendTextAsync_WithInvalidInstanceName_ShouldThrowArgumentException(string instanceName)
    {
        // Arrange
        var request = new SendTextMessageRequest
        {
            Number = "5511999999999",
            Text = "Hello, World!"
        };

        // Act & Assert
        var action = async () => await _messagesModule.SendTextAsync(instanceName, request);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Nome da instância é obrigatório*");
    }

    [Fact]
    public async Task SendTextAsync_WithNullRequest_ShouldThrowArgumentNullException()
    {
        // Arrange
        var instanceName = "test-instance";

        // Act & Assert
        var action = async () => await _messagesModule.SendTextAsync(instanceName, null!);
        await action.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("request");
    }

    [Fact]
    public async Task SendMediaAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SendMediaMessageRequest
        {
            Number = "5511999999999",
            Media = "https://example.com/image.jpg",
            MediaType = "image",
            Caption = "Test image"
        };
        var expectedResponse = new SendMessageResponse
        {
            MessageId = "msg-456",
            Status = "sent",
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        _httpService.PostAsync<SendMediaMessageRequest, SendMessageResponse>(
            Arg.Any<string>(),
            Arg.Any<SendMediaMessageRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _messagesModule.SendMediaAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<SendMediaMessageRequest, SendMessageResponse>(
            $"message/sendMedia/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task MarkAsReadAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new MarkAsReadRequest
        {
            Number = "5511999999999",
            MessageIds = new[] { "msg-123", "msg-456" }
        };

        // Act
        await _messagesModule.MarkAsReadAsync(instanceName, request);

        // Assert
        await _httpService.Received(1).PutAsync(
            $"chat/markMessageAsRead/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var messageId = "msg-123";

        // Act
        await _messagesModule.DeleteAsync(instanceName, messageId);

        // Assert
        await _httpService.Received(1).DeleteAsync(
            $"message/delete/{instanceName}/{messageId}",
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteAsync_WithNullMessageId_ShouldThrowArgumentException()
    {
        // Arrange
        var instanceName = "test-instance";

        // Act & Assert
        var action = async () => await _messagesModule.DeleteAsync(instanceName, null!);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("ID da mensagem é obrigatório*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task DeleteAsync_WithInvalidMessageId_ShouldThrowArgumentException(string messageId)
    {
        // Arrange
        var instanceName = "test-instance";

        // Act & Assert
        var action = async () => await _messagesModule.DeleteAsync(instanceName, messageId);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("ID da mensagem é obrigatório*");
    }

    [Fact]
    public async Task SendStatusAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SendStatusMessageRequest
        {
            Type = "text",
            Content = "Status message",
            BackgroundColor = "#FF0000"
        };
        var expectedResponse = new SendMessageResponse
        {
            MessageId = "status-123",
            Status = "sent",
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        _httpService.PostAsync<SendStatusMessageRequest, SendMessageResponse>(
            Arg.Any<string>(),
            Arg.Any<SendStatusMessageRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _messagesModule.SendStatusAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<SendStatusMessageRequest, SendMessageResponse>(
            $"message/sendStatus/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task SendStickerAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SendStickerMessageRequest
        {
            Number = "5511999999999",
            Sticker = "https://example.com/sticker.webp"
        };
        var expectedResponse = new SendMessageResponse
        {
            MessageId = "sticker-123",
            Status = "sent",
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        _httpService.PostAsync<SendStickerMessageRequest, SendMessageResponse>(
            Arg.Any<string>(),
            Arg.Any<SendStickerMessageRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _messagesModule.SendStickerAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<SendStickerMessageRequest, SendMessageResponse>(
            $"message/sendSticker/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task SendContactAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SendContactMessageRequest
        {
            Number = "5511999999999",
            Contacts = new[]
            {
                new MessageContactInfo
                {
                    FullName = "John Doe",
                    PhoneNumbers = new[]
                    {
                        new PhoneNumber { Number = "5511888888888", Type = "MOBILE" }
                    }
                }
            }
        };
        var expectedResponse = new SendMessageResponse
        {
            MessageId = "contact-123",
            Status = "sent",
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        _httpService.PostAsync<SendContactMessageRequest, SendMessageResponse>(
            Arg.Any<string>(),
            Arg.Any<SendContactMessageRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _messagesModule.SendContactAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<SendContactMessageRequest, SendMessageResponse>(
            $"message/sendContact/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task SendReactionAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SendReactionMessageRequest
        {
            Number = "5511999999999",
            Key = new MessageKey { Id = "msg-123" },
            Reaction = "👍"
        };
        var expectedResponse = new SendMessageResponse
        {
            MessageId = "reaction-123",
            Status = "sent",
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        _httpService.PostAsync<SendReactionMessageRequest, SendMessageResponse>(
            Arg.Any<string>(),
            Arg.Any<SendReactionMessageRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _messagesModule.SendReactionAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<SendReactionMessageRequest, SendMessageResponse>(
            $"message/sendReaction/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task SendPollAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SendPollMessageRequest
        {
            Number = "5511999999999",
            Name = "Favorite Color",
            Options = new[]
            {
                new PollOption { OptionName = "Red" },
                new PollOption { OptionName = "Blue" },
                new PollOption { OptionName = "Green" }
            },
            SelectableOptionsCount = 1
        };
        var expectedResponse = new SendMessageResponse
        {
            MessageId = "poll-123",
            Status = "sent",
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        _httpService.PostAsync<SendPollMessageRequest, SendMessageResponse>(
            Arg.Any<string>(),
            Arg.Any<SendPollMessageRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _messagesModule.SendPollAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<SendPollMessageRequest, SendMessageResponse>(
            $"message/sendPoll/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task SendListAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SendListMessageRequest
        {
            Number = "5511999999999",
            Title = "Menu Options",
            Description = "Choose an option",
            ButtonText = "Select",
            Sections = new[]
            {
                new ListSection
                {
                    Title = "Main Dishes",
                    Rows = new[]
                    {
                        new ListItem { Id = "1", Title = "Pizza", Description = "Delicious pizza" },
                        new ListItem { Id = "2", Title = "Burger", Description = "Tasty burger" }
                    }
                }
            }
        };
        var expectedResponse = new SendMessageResponse
        {
            MessageId = "list-123",
            Status = "sent",
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        _httpService.PostAsync<SendListMessageRequest, SendMessageResponse>(
            Arg.Any<string>(),
            Arg.Any<SendListMessageRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _messagesModule.SendListAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<SendListMessageRequest, SendMessageResponse>(
            $"message/sendList/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task SendButtonAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SendButtonMessageRequest
        {
            Number = "5511999999999",
            Title = "Choose Action",
            Description = "Please select an action",
            Footer = "Footer text",
            Buttons = new[]
            {
                new MessageButton { Id = "1", Title = "Yes", DisplayText = "Yes" },
                new MessageButton { Id = "2", Title = "No", DisplayText = "No" }
            }
        };
        var expectedResponse = new SendMessageResponse
        {
            MessageId = "button-123",
            Status = "sent",
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        _httpService.PostAsync<SendButtonMessageRequest, SendMessageResponse>(
            Arg.Any<string>(),
            Arg.Any<SendButtonMessageRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _messagesModule.SendButtonAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<SendButtonMessageRequest, SendMessageResponse>(
            $"message/sendButtons/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task SendAudioAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SendAudioMessageRequest
        {
            Number = "5511999999999",
            Audio = "https://example.com/audio.mp3"
        };
        var expectedResponse = new SendMessageResponse
        {
            MessageId = "audio-123",
            Status = "sent",
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        };

        _httpService.PostAsync<SendAudioMessageRequest, SendMessageResponse>(
            Arg.Any<string>(),
            Arg.Any<SendAudioMessageRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _messagesModule.SendAudioAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<SendAudioMessageRequest, SendMessageResponse>(
            $"message/sendAudio/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }
}
