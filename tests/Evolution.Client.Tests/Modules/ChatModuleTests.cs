using FluentAssertions;
using Evolution.Client.Core.Http;
using Evolution.Client.Modules;
using NSubstitute;

namespace Evolution.Client.Tests.Modules;

public class ChatModuleTests
{
    private readonly IHttpService _httpService;
    private readonly ChatModule _chatModule;

    public ChatModuleTests()
    {
        _httpService = Substitute.For<IHttpService>();
        _chatModule = new ChatModule(_httpService);
    }

    [Fact]
    public void Constructor_WithNullHttpService_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        var action = () => new ChatModule(null!);
        action.Should().Throw<ArgumentNullException>()
            .WithParameterName("httpService");
    }

    [Fact]
    public async Task CheckWhatsAppNumbersAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new CheckWhatsAppNumbersRequest
        {
            Numbers = new[] { "5511999999999", "5511888888888" }
        };
        var expectedResponse = new[]
        {
            new WhatsAppNumberCheckResult
            {
                Exists = true,
                Jid = "5511999999999@s.whatsapp.net",
                Number = "5511999999999"
            }
        };

        _httpService.PostAsync<CheckWhatsAppNumbersRequest, IEnumerable<WhatsAppNumberCheckResult>>(
            Arg.Any<string>(),
            Arg.Any<CheckWhatsAppNumbersRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _chatModule.CheckWhatsAppNumbersAsync(instanceName, request);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
        await _httpService.Received(1).PostAsync<CheckWhatsAppNumbersRequest, IEnumerable<WhatsAppNumberCheckResult>>(
            $"chat/whatsappNumbers/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task MarkAsReadAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new MarkAsReadChatRequest
        {
            RemoteJid = "5511999999999@s.whatsapp.net",
            ReadMessages = true
        };
        var expectedResponse = new ChatOperationResponse
        {
            Success = true,
            Message = "Messages marked as read"
        };

        _httpService.PostAsync<MarkAsReadChatRequest, ChatOperationResponse>(
            Arg.Any<string>(),
            Arg.Any<MarkAsReadChatRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _chatModule.MarkAsReadAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<MarkAsReadChatRequest, ChatOperationResponse>(
            $"chat/markMessageAsRead/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ArchiveChatAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new ArchiveChatRequest
        {
            RemoteJid = "5511999999999@s.whatsapp.net",
            Archive = true
        };
        var expectedResponse = new ChatOperationResponse
        {
            Success = true,
            Message = "Chat archived"
        };

        _httpService.PostAsync<ArchiveChatRequest, ChatOperationResponse>(
            Arg.Any<string>(),
            Arg.Any<ArchiveChatRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _chatModule.ArchiveChatAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<ArchiveChatRequest, ChatOperationResponse>(
            $"chat/archiveChat/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task SendPresenceAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SendPresenceRequest
        {
            Number = "5511999999999",
            Presence = "composing",
            Delay = 1000
        };
        var expectedResponse = new ChatOperationResponse
        {
            Success = true,
            Message = "Presence sent"
        };

        _httpService.PostAsync<SendPresenceRequest, ChatOperationResponse>(
            Arg.Any<string>(),
            Arg.Any<SendPresenceRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _chatModule.SendPresenceAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<SendPresenceRequest, ChatOperationResponse>(
            $"chat/sendPresence/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task FetchProfilePictureUrlAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new FetchProfilePictureRequest
        {
            Number = "5511999999999"
        };
        var expectedResponse = new ProfilePictureResponse
        {
            ProfilePictureUrl = "https://example.com/profile.jpg"
        };

        _httpService.PostAsync<FetchProfilePictureRequest, ProfilePictureResponse>(
            Arg.Any<string>(),
            Arg.Any<FetchProfilePictureRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _chatModule.FetchProfilePictureUrlAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<FetchProfilePictureRequest, ProfilePictureResponse>(
            $"chat/fetchProfilePicture/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task FindChatsAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new FindChatsRequest
        {
            Where = new ChatSearchWhere
            {
                Archived = false
            },
            Limit = 10
        };
        var expectedResponse = new[]
        {
            new ChatInfo
            {
                RemoteJid = "5511999999999@s.whatsapp.net",
                Name = "Test Chat",
                Archived = false,
                UnreadCount = 5
            }
        };

        _httpService.PostAsync<FindChatsRequest, IEnumerable<ChatInfo>>(
            Arg.Any<string>(),
            Arg.Any<FindChatsRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _chatModule.FindChatsAsync(instanceName, request);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
        await _httpService.Received(1).PostAsync<FindChatsRequest, IEnumerable<ChatInfo>>(
            $"chat/findChats/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task CheckWhatsAppNumbersAsync_WithInvalidInstanceName_ShouldThrowArgumentException(string instanceName)
    {
        // Arrange
        var request = new CheckWhatsAppNumbersRequest
        {
            Numbers = new[] { "5511999999999" }
        };

        // Act & Assert
        var action = async () => await _chatModule.CheckWhatsAppNumbersAsync(instanceName, request);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Nome da instância é obrigatório*");
    }

    [Fact]
    public async Task CheckWhatsAppNumbersAsync_WithNullInstanceName_ShouldThrowArgumentException()
    {
        // Arrange
        var request = new CheckWhatsAppNumbersRequest
        {
            Numbers = new[] { "5511999999999" }
        };

        // Act & Assert
        var action = async () => await _chatModule.CheckWhatsAppNumbersAsync(null!, request);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Nome da instância é obrigatório*");
    }

    [Fact]
    public async Task CheckWhatsAppNumbersAsync_WithNullRequest_ShouldThrowArgumentNullException()
    {
        // Arrange
        var instanceName = "test-instance";

        // Act & Assert
        var action = async () => await _chatModule.CheckWhatsAppNumbersAsync(instanceName, null!);
        await action.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("request");
    }
}
