using FluentAssertions;
using Evolution.Client.CSharp.Core.Http;
using Evolution.Client.CSharp.Modules;
using NSubstitute;

namespace Evolution.Client.CSharp.Tests.Modules;

public class InstancesModuleSetPresenceTests
{
    private readonly IHttpService _httpService;
    private readonly InstancesModule _instancesModule;

    public InstancesModuleSetPresenceTests()
    {
        _httpService = Substitute.For<IHttpService>();
        _instancesModule = new InstancesModule(_httpService);
    }

    [Fact]
    public async Task SetPresenceAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SetPresenceRequest
        {
            Presence = "available"
        };
        var expectedResponse = new SetPresenceResponse
        {
            Presence = "available"
        };

        _httpService.PostAsync<SetPresenceRequest, SetPresenceResponse>(
            Arg.Any<string>(),
            Arg.Any<SetPresenceRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _instancesModule.SetPresenceAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<SetPresenceRequest, SetPresenceResponse>(
            $"instance/setPresence/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task SetPresenceAsync_WithNullInstanceName_ShouldThrowArgumentException()
    {
        // Arrange
        var request = new SetPresenceRequest
        {
            Presence = "available"
        };

        // Act & Assert
        var action = async () => await _instancesModule.SetPresenceAsync(null!, request);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Nome da instância é obrigatório*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task SetPresenceAsync_WithInvalidInstanceName_ShouldThrowArgumentException(string instanceName)
    {
        // Arrange
        var request = new SetPresenceRequest
        {
            Presence = "available"
        };

        // Act & Assert
        var action = async () => await _instancesModule.SetPresenceAsync(instanceName, request);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Nome da instância é obrigatório*");
    }

    [Fact]
    public async Task SetPresenceAsync_WithNullRequest_ShouldThrowArgumentNullException()
    {
        // Arrange
        var instanceName = "test-instance";

        // Act & Assert
        var action = async () => await _instancesModule.SetPresenceAsync(instanceName, null!);
        await action.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("request");
    }

    [Fact]
    public async Task SetPresenceAsync_WithNullPresence_ShouldThrowArgumentException()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SetPresenceRequest
        {
            Presence = null!
        };

        // Act & Assert
        var action = async () => await _instancesModule.SetPresenceAsync(instanceName, request);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Presença é obrigatória*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task SetPresenceAsync_WithInvalidPresence_ShouldThrowArgumentException(string presence)
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SetPresenceRequest
        {
            Presence = presence
        };

        // Act & Assert
        var action = async () => await _instancesModule.SetPresenceAsync(instanceName, request);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Presença é obrigatória*");
    }

    [Theory]
    [InlineData("available")]
    [InlineData("unavailable")]
    [InlineData("composing")]
    [InlineData("recording")]
    [InlineData("paused")]
    public async Task SetPresenceAsync_WithValidPresenceValues_ShouldSucceed(string presence)
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SetPresenceRequest
        {
            Presence = presence
        };
        var expectedResponse = new SetPresenceResponse
        {
            Presence = presence
        };

        _httpService.PostAsync<SetPresenceRequest, SetPresenceResponse>(
            Arg.Any<string>(),
            Arg.Any<SetPresenceRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _instancesModule.SetPresenceAsync(instanceName, request);

        // Assert
        result.Should().NotBeNull();
        result.Presence.Should().Be(presence);
    }
}
