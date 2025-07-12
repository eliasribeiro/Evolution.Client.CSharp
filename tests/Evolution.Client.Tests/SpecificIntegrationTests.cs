using FluentAssertions;
using Evolution.Client.Modules;

namespace Evolution.Client.Tests;

public class ChatwootModuleValidationTests
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
        request.NameInbox.Should().BeEmpty();
        request.MergeBrazilContacts.Should().BeTrue();
        request.ImportContacts.Should().BeTrue();
        request.ImportMessages.Should().BeTrue();
        request.DaysLimitImportMessages.Should().Be(7);
        request.SignDelimiter.Should().Be("\n");
        request.AutoCreate.Should().BeTrue();
        request.Organization.Should().BeEmpty();
        request.Logo.Should().BeEmpty();
        request.IgnoreJids.Should().BeEmpty();
    }

    [Fact]
    public void ChatwootIntegrationConfig_ShouldAllowNullValues()
    {
        // Act
        var config = new ChatwootIntegrationConfig();

        // Assert
        config.Enabled.Should().BeFalse(); // bool default
        config.AccountId.Should().BeNull();
        config.Token.Should().BeNull();
        config.Url.Should().BeNull();
        config.SignMsg.Should().BeNull();
        config.ReopenConversation.Should().BeNull();
        config.ConversationPending.Should().BeNull();
    }
}

public class WebSocketModuleValidationTests
{
    [Fact]
    public void SetWebSocketRequest_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var request = new SetWebSocketRequest();

        // Assert
        request.Enabled.Should().BeTrue();
        request.Events.Should().BeEmpty();
        request.WebSocketUrl.Should().BeNull();
        request.Headers.Should().BeNull();
        request.ConnectionTimeout.Should().Be(30);
        request.PingInterval.Should().Be(25);
        request.AutoReconnect.Should().BeTrue();
        request.MaxReconnectAttempts.Should().Be(5);
        request.ReconnectInterval.Should().Be(5);
    }

    [Fact]
    public void WebSocketEvents_ShouldContainAllExpectedEvents()
    {
        // Assert
        WebSocketEvents.AllEvents.Should().Contain(WebSocketEvents.MessageReceived);
        WebSocketEvents.AllEvents.Should().Contain(WebSocketEvents.MessageSent);
        WebSocketEvents.AllEvents.Should().Contain(WebSocketEvents.MessageStatus);
        WebSocketEvents.AllEvents.Should().Contain(WebSocketEvents.Presence);
        WebSocketEvents.AllEvents.Should().Contain(WebSocketEvents.Call);
        WebSocketEvents.AllEvents.Should().Contain(WebSocketEvents.InstanceConnect);
        WebSocketEvents.AllEvents.Should().Contain(WebSocketEvents.InstanceDisconnect);
        WebSocketEvents.AllEvents.Should().Contain(WebSocketEvents.QrCode);
        WebSocketEvents.AllEvents.Should().Contain(WebSocketEvents.ContactUpdate);
        WebSocketEvents.AllEvents.Should().Contain(WebSocketEvents.GroupUpdate);
        
        WebSocketEvents.AllEvents.Should().HaveCount(10);
    }

    [Fact]
    public void WebSocketConnectionStatus_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var status = new WebSocketConnectionStatus();

        // Assert
        status.Status.Should().BeEmpty();
        status.IsConnected.Should().BeFalse();
        status.LastConnected.Should().BeNull();
        status.LastDisconnected.Should().BeNull();
        status.ReconnectCount.Should().Be(0);
        status.LastError.Should().BeNull();
        status.Latency.Should().BeNull();
    }
}

public class SQSModuleValidationTests
{
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
        request.DelaySeconds.Should().Be(0);
        request.VisibilityTimeoutSeconds.Should().Be(30);
        request.MessageRetentionPeriod.Should().Be(345600); // 4 dias
        request.MaxMessageSize.Should().Be(262144); // 256KB
        request.ReceiveMessageWaitTimeSeconds.Should().Be(0);
        request.MessageAttributes.Should().BeNull();
        request.UseFifoQueue.Should().BeFalse();
        request.MessageGroupId.Should().BeNull();
        request.ContentBasedDeduplication.Should().BeFalse();
    }

    [Fact]
    public void SQSEvents_ShouldContainAllExpectedEvents()
    {
        // Assert
        SQSEvents.AllEvents.Should().Contain(SQSEvents.MessageReceived);
        SQSEvents.AllEvents.Should().Contain(SQSEvents.MessageSent);
        SQSEvents.AllEvents.Should().Contain(SQSEvents.MessageStatus);
        SQSEvents.AllEvents.Should().Contain(SQSEvents.InstanceConnect);
        SQSEvents.AllEvents.Should().Contain(SQSEvents.InstanceDisconnect);
        
        SQSEvents.AllEvents.Should().HaveCount(5);
    }

    [Fact]
    public void SQSConnectionStatus_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var status = new SQSConnectionStatus();

        // Assert
        status.Status.Should().BeEmpty();
        status.IsConnected.Should().BeFalse();
        status.LastChecked.Should().BeNull();
        status.LastError.Should().BeNull();
        status.ActiveRegion.Should().BeNull();
        status.ActiveQueueName.Should().BeNull();
        status.QueueType.Should().BeNull();
    }
}

public class RabbitMQModuleValidationTests
{
    [Fact]
    public void SetRabbitMQRequest_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var request = new SetRabbitMQRequest();

        // Assert
        request.Enabled.Should().BeTrue();
        request.Uri.Should().BeEmpty();
        request.Exchange.Should().BeEmpty();
        request.ExchangeType.Should().Be("topic");
        request.ExchangeDurable.Should().BeTrue();
        request.ExchangeAutoDelete.Should().BeFalse();
        request.Events.Should().BeEmpty();
        request.DefaultRoutingKey.Should().Be("evolution.events");
        request.QueueName.Should().BeNull();
        request.QueueDurable.Should().BeTrue();
        request.QueueExclusive.Should().BeFalse();
        request.QueueAutoDelete.Should().BeFalse();
        request.MessageTTL.Should().BeNull();
        request.MaxLength.Should().BeNull();
        request.MaxLengthBytes.Should().BeNull();
        request.OverflowBehaviour.Should().BeNull();
        request.DeadLetterExchange.Should().BeNull();
        request.DeadLetterRoutingKey.Should().BeNull();
        request.ConnectionTimeout.Should().Be(30);
        request.HeartbeatInterval.Should().Be(60);
        request.AutoReconnect.Should().BeTrue();
        request.MaxReconnectAttempts.Should().Be(5);
        request.ReconnectInterval.Should().Be(5);
    }

    [Fact]
    public void RabbitMQEvents_ShouldContainAllExpectedEvents()
    {
        // Assert
        RabbitMQEvents.AllEvents.Should().Contain(RabbitMQEvents.MessageReceived);
        RabbitMQEvents.AllEvents.Should().Contain(RabbitMQEvents.MessageSent);
        RabbitMQEvents.AllEvents.Should().Contain(RabbitMQEvents.MessageStatus);
        RabbitMQEvents.AllEvents.Should().Contain(RabbitMQEvents.Presence);
        RabbitMQEvents.AllEvents.Should().Contain(RabbitMQEvents.Call);
        RabbitMQEvents.AllEvents.Should().Contain(RabbitMQEvents.InstanceConnect);
        RabbitMQEvents.AllEvents.Should().Contain(RabbitMQEvents.InstanceDisconnect);
        
        RabbitMQEvents.AllEvents.Should().HaveCount(7);
    }

    [Fact]
    public void RabbitMQConnectionStatus_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var status = new RabbitMQConnectionStatus();

        // Assert
        status.Status.Should().BeEmpty();
        status.IsConnected.Should().BeFalse();
        status.LastConnected.Should().BeNull();
        status.LastDisconnected.Should().BeNull();
        status.ReconnectCount.Should().Be(0);
        status.LastError.Should().BeNull();
        status.ActiveHost.Should().BeNull();
        status.ActivePort.Should().BeNull();
        status.ActiveVirtualHost.Should().BeNull();
        status.ServerVersion.Should().BeNull();
    }
}

public class IntegrationStatsTests
{
    [Fact]
    public void ChatwootStats_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var stats = new ChatwootStats();

        // Assert
        stats.TotalConversations.Should().Be(0);
        stats.OpenConversations.Should().Be(0);
        stats.ResolvedConversations.Should().Be(0);
        stats.PendingConversations.Should().Be(0);
        stats.MessagesSent.Should().Be(0);
        stats.MessagesReceived.Should().Be(0);
        stats.LastSync.Should().BeNull();
    }

    [Fact]
    public void WebSocketStats_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var stats = new WebSocketStats();

        // Assert
        stats.MessagesSent.Should().Be(0);
        stats.MessagesReceived.Should().Be(0);
        stats.EventsProcessed.Should().Be(0);
        stats.ConnectionErrors.Should().Be(0);
        stats.TotalConnectedTime.Should().Be(0);
        stats.CurrentUptime.Should().Be(0);
        stats.BytesSent.Should().Be(0);
        stats.BytesReceived.Should().Be(0);
    }

    [Fact]
    public void SQSStats_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var stats = new SQSStats();

        // Assert
        stats.MessagesSent.Should().Be(0);
        stats.MessagesFailed.Should().Be(0);
        stats.EventsProcessed.Should().Be(0);
        stats.ApproximateNumberOfMessages.Should().BeNull();
        stats.ApproximateNumberOfMessagesNotVisible.Should().BeNull();
        stats.ApproximateNumberOfMessagesDelayed.Should().BeNull();
        stats.LastUpdated.Should().BeNull();
        stats.AverageMessageSize.Should().Be(0);
        stats.SuccessRate.Should().Be(0);
    }

    [Fact]
    public void RabbitMQStats_ShouldHaveCorrectDefaultValues()
    {
        // Act
        var stats = new RabbitMQStats();

        // Assert
        stats.MessagesPublished.Should().Be(0);
        stats.MessagesFailed.Should().Be(0);
        stats.EventsProcessed.Should().Be(0);
        stats.QueueMessageCount.Should().BeNull();
        stats.QueueConsumerCount.Should().BeNull();
        stats.PublishRate.Should().Be(0);
        stats.SuccessRate.Should().Be(0);
        stats.LastUpdated.Should().BeNull();
        stats.BytesSent.Should().Be(0);
        stats.AverageMessageSize.Should().Be(0);
    }
}
