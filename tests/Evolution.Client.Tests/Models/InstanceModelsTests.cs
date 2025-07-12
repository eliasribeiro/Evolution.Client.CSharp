using FluentAssertions;
using Evolution.Client.Modules;

namespace Evolution.Client.Tests.Models;

public class InstanceModelsTests
{
    [Fact]
    public void CreateInstanceRequest_ShouldInitializeWithDefaults()
    {
        // Act
        var request = new CreateInstanceRequest();

        // Assert
        request.InstanceName.Should().BeEmpty();
        request.Number.Should().BeNull();
        request.Token.Should().BeNull();
        request.Qrcode.Should().BeNull();
        request.Integration.Should().Be("WHATSAPP-BAILEYS");
    }

    [Fact]
    public void CreateInstanceRequest_ShouldAllowPropertyAssignment()
    {
        // Arrange
        var instanceName = "test-instance";
        var number = "5511999999999";
        var token = "test-token";
        var qrcode = true;
        var rejectCall = false;

        // Act
        var request = new CreateInstanceRequest
        {
            InstanceName = instanceName,
            Number = number,
            Token = token,
            Qrcode = qrcode,
            RejectCall = rejectCall
        };

        // Assert
        request.InstanceName.Should().Be(instanceName);
        request.Number.Should().Be(number);
        request.Token.Should().Be(token);
        request.Qrcode.Should().Be(qrcode);
        request.RejectCall.Should().Be(rejectCall);
    }

    [Fact]
    public void CreateInstanceResponse_ShouldInitializeWithDefaults()
    {
        // Act
        var response = new CreateInstanceResponse();

        // Assert
        response.Instance.Should().BeNull();
        response.Hash.Should().BeNull();
        response.Settings.Should().BeNull();
    }

    [Fact]
    public void InstanceInfo_ShouldInitializeWithDefaults()
    {
        // Act
        var info = new InstanceInfo();

        // Assert
        info.Instance.Should().BeNull();
        info.Hash.Should().BeNull();
        info.Settings.Should().BeNull();
        info.Webhook.Should().BeNull();
        info.Rabbitmq.Should().BeNull();
        info.Sqs.Should().BeNull();
    }

    [Fact]
    public void ConnectionStatus_ShouldInitializeWithDefaults()
    {
        // Act
        var status = new ConnectionStatus();

        // Assert
        status.Instance.Should().BeEmpty();
        status.State.Should().BeEmpty();
    }

    [Fact]
    public void SetPresenceRequest_ShouldAllowPropertyAssignment()
    {
        // Arrange
        var presence = "available";

        // Act
        var request = new SetPresenceRequest
        {
            Presence = presence
        };

        // Assert
        request.Presence.Should().Be(presence);
    }

    [Fact]
    public void SetPresenceResponse_ShouldInitializeWithDefaults()
    {
        // Act
        var response = new SetPresenceResponse();

        // Assert
        response.Presence.Should().BeEmpty();
    }
}
