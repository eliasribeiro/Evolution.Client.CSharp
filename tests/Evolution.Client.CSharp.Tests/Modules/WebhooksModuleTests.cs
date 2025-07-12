using FluentAssertions;
using Evolution.Client.CSharp.Core.Http;
using Evolution.Client.CSharp.Modules;
using NSubstitute;

namespace Evolution.Client.CSharp.Tests.Modules;

public class WebhooksModuleTests
{
    private readonly IHttpService _httpService;
    private readonly WebhooksModule _webhooksModule;

    public WebhooksModuleTests()
    {
        _httpService = Substitute.For<IHttpService>();
        _webhooksModule = new WebhooksModule(_httpService);
    }

    [Fact]
    public void Constructor_WithNullHttpService_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        var action = () => new WebhooksModule(null!);
        action.Should().Throw<ArgumentNullException>()
            .WithParameterName("httpService");
    }

    [Fact]
    public async Task SetAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SetWebhookRequest
        {
            Enabled = true,
            Url = "https://webhook.example.com",
            WebhookByEvents = true,
            WebhookBase64 = true,
            Events = new[] { "MESSAGE_RECEIVED", "CONNECTION_UPDATE" }
        };
        var expectedResponse = new SetWebhookResponse
        {
            Webhook = new WebhookData
            {
                InstanceName = instanceName,
                Webhook = new WebhookConfig
                {
                    Url = "https://webhook.example.com",
                    Events = new[] { "MESSAGE_RECEIVED", "CONNECTION_UPDATE" },
                    Enabled = true,
                    WebhookByEvents = true,
                    WebhookBase64 = true
                }
            }
        };

        _httpService.PostAsync<SetWebhookRequest, SetWebhookResponse>(
            Arg.Any<string>(),
            Arg.Any<SetWebhookRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _webhooksModule.SetAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<SetWebhookRequest, SetWebhookResponse>(
            $"webhook/set/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetAsync_WithValidInstanceName_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var expectedResponse = new WebhookConfig
        {
            Url = "https://webhook.example.com",
            Events = new[] { "MESSAGE_RECEIVED", "CONNECTION_UPDATE" },
            Enabled = true,
            WebhookByEvents = true,
            WebhookBase64 = true
        };

        _httpService.GetAsync<WebhookConfig>(
            Arg.Any<string>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _webhooksModule.GetAsync(instanceName);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).GetAsync<WebhookConfig>(
            $"webhook/find/{instanceName}",
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task SetAsync_WithNullInstanceName_ShouldThrowArgumentException()
    {
        // Arrange
        var request = new SetWebhookRequest();

        // Act & Assert
        var action = async () => await _webhooksModule.SetAsync(null!, request);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Nome da instância é obrigatório*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task SetAsync_WithInvalidInstanceName_ShouldThrowArgumentException(string instanceName)
    {
        // Arrange
        var request = new SetWebhookRequest();

        // Act & Assert
        var action = async () => await _webhooksModule.SetAsync(instanceName, request);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Nome da instância é obrigatório*");
    }

    [Fact]
    public async Task SetAsync_WithNullRequest_ShouldThrowArgumentNullException()
    {
        // Arrange
        var instanceName = "test-instance";

        // Act & Assert
        var action = async () => await _webhooksModule.SetAsync(instanceName, null!);
        await action.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("request");
    }

    [Fact]
    public async Task GetAsync_WithNullInstanceName_ShouldThrowArgumentException()
    {
        // Act & Assert
        var action = async () => await _webhooksModule.GetAsync(null!);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Nome da instância é obrigatório*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task GetAsync_WithInvalidInstanceName_ShouldThrowArgumentException(string instanceName)
    {
        // Act & Assert
        var action = async () => await _webhooksModule.GetAsync(instanceName);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Nome da instância é obrigatório*");
    }

    [Fact]
    public async Task SetAsync_WithCompleteWebhookConfiguration_ShouldSucceed()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SetWebhookRequest
        {
            Enabled = true,
            Url = "https://webhook.example.com/evolution",
            WebhookByEvents = true,
            WebhookBase64 = false,
            Events = new[] 
            { 
                "APPLICATION_STARTUP",
                "QRCODE_UPDATED", 
                "CONNECTION_UPDATE",
                "STATUS_INSTANCE",
                "MESSAGES_UPSERT",
                "MESSAGES_UPDATE",
                "MESSAGES_DELETE",
                "SEND_MESSAGE",
                "CONTACTS_UPDATE",
                "CONTACTS_UPSERT",
                "PRESENCE_UPDATE",
                "CHATS_UPDATE",
                "CHATS_UPSERT",
                "CHATS_DELETE",
                "GROUPS_UPSERT",
                "GROUP_UPDATE",
                "GROUP_PARTICIPANTS_UPDATE",
                "NEW_JWT_TOKEN"
            },
            Headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer token123" },
                { "X-Custom-Header", "custom-value" }
            }
        };

        var expectedResponse = new SetWebhookResponse
        {
            Webhook = new WebhookData
            {
                InstanceName = instanceName,
                Webhook = new WebhookConfig
                {
                    Url = request.Url,
                    Events = request.Events,
                    Enabled = request.Enabled,
                    WebhookByEvents = request.WebhookByEvents,
                    WebhookBase64 = request.WebhookBase64,
                    Headers = request.Headers
                }
            }
        };

        _httpService.PostAsync<SetWebhookRequest, SetWebhookResponse>(
            Arg.Any<string>(),
            Arg.Any<SetWebhookRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _webhooksModule.SetAsync(instanceName, request);

        // Assert
        result.Should().NotBeNull();
        result.Webhook.Should().NotBeNull();
        result.Webhook!.InstanceName.Should().Be(instanceName);
        result.Webhook.Webhook.Should().NotBeNull();
        result.Webhook.Webhook!.Url.Should().Be(request.Url);
        result.Webhook.Webhook.Events.Should().BeEquivalentTo(request.Events);
        result.Webhook.Webhook.Enabled.Should().Be(request.Enabled);
        result.Webhook.Webhook.WebhookByEvents.Should().Be(request.WebhookByEvents);
        result.Webhook.Webhook.WebhookBase64.Should().Be(request.WebhookBase64);
        result.Webhook.Webhook.Headers.Should().BeEquivalentTo(request.Headers);
    }
}
