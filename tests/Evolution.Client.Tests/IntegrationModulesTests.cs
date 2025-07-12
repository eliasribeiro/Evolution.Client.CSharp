using FluentAssertions;
using Evolution.Client.Modules;

namespace Evolution.Client.Tests;

public class IntegrationModulesTests
{
    [Fact]
    public void Create_WithValidParameters_ShouldInitializeAllIntegrationModules()
    {
        // Arrange
        var baseUrl = "https://api.evolution.com";
        var apiKey = "test-api-key";

        // Act
        var client = EvolutionClient.Create(baseUrl, apiKey);

        // Assert
        client.Should().NotBeNull();
        
        // Verify all integration modules are properly initialized
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
    public void TypeBotModule_ShouldBeOfCorrectType()
    {
        // Arrange
        var baseUrl = "https://api.evolution.com";
        var apiKey = "test-api-key";

        // Act
        var client = EvolutionClient.Create(baseUrl, apiKey);

        // Assert
        client.TypeBot.Should().BeAssignableTo<ITypeBotModule>();
    }

    [Fact]
    public void OpenAIModule_ShouldBeOfCorrectType()
    {
        // Arrange
        var baseUrl = "https://api.evolution.com";
        var apiKey = "test-api-key";

        // Act
        var client = EvolutionClient.Create(baseUrl, apiKey);

        // Assert
        client.OpenAI.Should().BeAssignableTo<IOpenAIModule>();
    }

    [Fact]
    public void EvolutionBotModule_ShouldBeOfCorrectType()
    {
        // Arrange
        var baseUrl = "https://api.evolution.com";
        var apiKey = "test-api-key";

        // Act
        var client = EvolutionClient.Create(baseUrl, apiKey);

        // Assert
        client.EvolutionBot.Should().BeAssignableTo<IEvolutionBotModule>();
    }

    [Fact]
    public void DifyModule_ShouldBeOfCorrectType()
    {
        // Arrange
        var baseUrl = "https://api.evolution.com";
        var apiKey = "test-api-key";

        // Act
        var client = EvolutionClient.Create(baseUrl, apiKey);

        // Assert
        client.Dify.Should().BeAssignableTo<IDifyModule>();
    }

    [Fact]
    public void FlowiseModule_ShouldBeOfCorrectType()
    {
        // Arrange
        var baseUrl = "https://api.evolution.com";
        var apiKey = "test-api-key";

        // Act
        var client = EvolutionClient.Create(baseUrl, apiKey);

        // Assert
        client.Flowise.Should().BeAssignableTo<IFlowiseModule>();
    }

    [Fact]
    public void ChatwootModule_ShouldBeOfCorrectType()
    {
        // Arrange
        var baseUrl = "https://api.evolution.com";
        var apiKey = "test-api-key";

        // Act
        var client = EvolutionClient.Create(baseUrl, apiKey);

        // Assert
        client.Chatwoot.Should().BeAssignableTo<IChatwootModule>();
    }

    [Fact]
    public void WebSocketModule_ShouldBeOfCorrectType()
    {
        // Arrange
        var baseUrl = "https://api.evolution.com";
        var apiKey = "test-api-key";

        // Act
        var client = EvolutionClient.Create(baseUrl, apiKey);

        // Assert
        client.WebSocket.Should().BeAssignableTo<IWebSocketModule>();
    }

    [Fact]
    public void SQSModule_ShouldBeOfCorrectType()
    {
        // Arrange
        var baseUrl = "https://api.evolution.com";
        var apiKey = "test-api-key";

        // Act
        var client = EvolutionClient.Create(baseUrl, apiKey);

        // Assert
        client.SQS.Should().BeAssignableTo<ISQSModule>();
    }

    [Fact]
    public void RabbitMQModule_ShouldBeOfCorrectType()
    {
        // Arrange
        var baseUrl = "https://api.evolution.com";
        var apiKey = "test-api-key";

        // Act
        var client = EvolutionClient.Create(baseUrl, apiKey);

        // Assert
        client.RabbitMQ.Should().BeAssignableTo<IRabbitMQModule>();
    }
}

public class TypeBotModuleValidationTests
{
    [Fact]
    public void CreateTypeBotRequest_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var request = new CreateTypeBotRequest();

        // Assert
        request.Enabled.Should().BeTrue();
        request.Url.Should().BeEmpty();
        request.Typebot.Should().BeEmpty();
        request.TriggerType.Should().BeEmpty();
        request.TriggerOperator.Should().BeEmpty();
        request.TriggerValue.Should().BeEmpty();
        request.Expire.Should().Be(0);
        request.KeywordFinish.Should().BeEmpty();
        request.DelayMessage.Should().Be(0);
        request.UnknownMessage.Should().BeEmpty();
        request.ListeningFromMe.Should().BeFalse();
        request.StopBotFromMe.Should().BeFalse();
        request.KeepOpen.Should().BeFalse();
        request.DebounceTime.Should().Be(0);
    }

    [Fact]
    public void StartTypeBotRequest_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var request = new StartTypeBotRequest();

        // Assert
        request.Number.Should().BeEmpty();
        request.Variables.Should().BeNull();
    }

    [Fact]
    public void TypeBotSettingsRequest_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var request = new TypeBotSettingsRequest();

        // Assert
        request.Enabled.Should().BeTrue();
        request.Description.Should().BeEmpty();
    }
}

public class OpenAIModuleValidationTests
{
    [Fact]
    public void CreateOpenAIBotRequest_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var request = new CreateOpenAIBotRequest();

        // Assert
        request.Enabled.Should().BeTrue();
        request.OpenaiCredsId.Should().BeEmpty();
        request.BotType.Should().BeEmpty();
        request.AssistantId.Should().BeEmpty();
        request.FunctionUrl.Should().BeEmpty();
        request.Model.Should().BeEmpty();
        request.SystemMessages.Should().BeEmpty();
        request.AssistantMessages.Should().BeEmpty();
        request.UserMessages.Should().BeEmpty();
        request.MaxTokens.Should().Be(0);
        request.TriggerType.Should().BeEmpty();
        request.TriggerOperator.Should().BeEmpty();
        request.TriggerValue.Should().BeEmpty();
        request.Expire.Should().Be(0);
        request.KeywordFinish.Should().BeEmpty();
        request.DelayMessage.Should().Be(0);
        request.UnknownMessage.Should().BeEmpty();
        request.ListeningFromMe.Should().BeFalse();
        request.StopBotFromMe.Should().BeFalse();
        request.KeepOpen.Should().BeFalse();
        request.DebounceTime.Should().Be(0);
        request.IgnoreJids.Should().BeEmpty();
    }

    [Fact]
    public void SetOpenAICredsRequest_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var request = new SetOpenAICredsRequest();

        // Assert
        request.Name.Should().BeEmpty();
        request.ApiKey.Should().BeEmpty();
        request.OrganizationId.Should().BeNull();
    }
}

public class SimpleIntegrationModelsValidationTests
{
    [Fact]
    public void SetChatwootRequest_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var request = new SetChatwootRequest();

        // Assert
        request.Enabled.Should().BeTrue();
        request.AccountId.Should().BeEmpty();
        request.Token.Should().BeEmpty();
        request.Url.Should().BeEmpty();
        request.SignMsg.Should().BeTrue();
        request.ReopenConversation.Should().BeTrue();
        request.ConversationPending.Should().BeTrue();
    }

    [Fact]
    public void SetWebSocketRequest_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var request = new SetWebSocketRequest();

        // Assert
        request.Enabled.Should().BeTrue();
        request.Events.Should().BeEmpty();
    }

    [Fact]
    public void SetSQSRequest_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var request = new SetSQSRequest();

        // Assert
        request.Enabled.Should().BeTrue();
        request.AccessKeyId.Should().BeEmpty();
        request.SecretAccessKey.Should().BeEmpty();
        request.Region.Should().BeEmpty();
        request.QueueUrl.Should().BeEmpty();
        request.Events.Should().BeEmpty();
    }

    [Fact]
    public void SetRabbitMQRequest_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var request = new SetRabbitMQRequest();

        // Assert
        request.Enabled.Should().BeTrue();
        request.Uri.Should().BeEmpty();
        request.Exchange.Should().BeEmpty();
        request.Events.Should().BeEmpty();
    }
}
