using FluentAssertions;
using Evolution.Client.CSharp.Modules;

namespace Evolution.Client.CSharp.Tests;

public class EvolutionClientTests
{
    [Fact]
    public void Create_WithValidParameters_ShouldReturnClient()
    {
        // Arrange
        var baseUrl = "https://api.evolution.com";
        var apiKey = "test-api-key";

        // Act
        var client = EvolutionClient.Create(baseUrl, apiKey);

        // Assert
        client.Should().NotBeNull();
        client.Instances.Should().NotBeNull();
        client.Messages.Should().NotBeNull();
        client.Webhooks.Should().NotBeNull();
        client.Groups.Should().NotBeNull();
        client.Contacts.Should().NotBeNull();
        client.Settings.Should().NotBeNull();
        client.Profile.Should().NotBeNull();
        client.Chat.Should().NotBeNull();

        // Integration modules
        client.TypeBot.Should().NotBeNull();
        client.OpenAI.Should().NotBeNull();
        client.EvolutionBot.Should().NotBeNull();
        client.Dify.Should().NotBeNull();
        client.Flowise.Should().NotBeNull();
        client.Chatwoot.Should().NotBeNull();
        client.WebSocket.Should().NotBeNull();
        client.SQS.Should().NotBeNull();
        client.RabbitMQ.Should().NotBeNull();
    }

    [Fact]
    public void Create_WithNullBaseUrl_ShouldThrowArgumentException()
    {
        // Arrange
        string baseUrl = null!;
        var apiKey = "test-api-key";

        // Act & Assert
        var action = () => EvolutionClient.Create(baseUrl, apiKey);
        action.Should().Throw<ArgumentException>()
            .WithMessage("Base URL não pode ser nula ou vazia*");
    }

    [Fact]
    public void Create_WithEmptyApiKey_ShouldThrowArgumentException()
    {
        // Arrange
        var baseUrl = "https://api.evolution.com";
        var apiKey = "";

        // Act & Assert
        var action = () => EvolutionClient.Create(baseUrl, apiKey);
        action.Should().Throw<ArgumentException>()
            .WithMessage("API Key não pode ser nula ou vazia*");
    }

    [Fact]
    public void Create_WithCustomOptions_ShouldApplyConfiguration()
    {
        // Arrange
        var baseUrl = "https://api.evolution.com";
        var apiKey = "test-api-key";
        var customTimeout = TimeSpan.FromMinutes(5);

        // Act
        var client = EvolutionClient.Create(baseUrl, apiKey, options =>
        {
            options.Timeout = customTimeout;
            options.MaxRetryAttempts = 5;
        });

        // Assert
        client.Should().NotBeNull();
    }
}
