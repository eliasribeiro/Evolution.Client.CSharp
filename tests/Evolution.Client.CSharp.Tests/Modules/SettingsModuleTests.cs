using FluentAssertions;
using Evolution.Client.CSharp.Core.Http;
using Evolution.Client.CSharp.Modules;
using NSubstitute;

namespace Evolution.Client.CSharp.Tests.Modules;

public class SettingsModuleTests
{
    private readonly IHttpService _httpService;
    private readonly SettingsModule _settingsModule;

    public SettingsModuleTests()
    {
        _httpService = Substitute.For<IHttpService>();
        _settingsModule = new SettingsModule(_httpService);
    }

    [Fact]
    public void Constructor_WithNullHttpService_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        var action = () => new SettingsModule(null!);
        action.Should().Throw<ArgumentNullException>()
            .WithParameterName("httpService");
    }

    [Fact]
    public async Task SetAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new SetSettingsRequest
        {
            RejectCall = true,
            MsgCall = "Chamada rejeitada",
            GroupsIgnore = false,
            AlwaysOnline = true,
            ReadMessages = true,
            ReadStatus = false,
            SyncFullHistory = true
        };
        var expectedResponse = new SetSettingsResponse
        {
            Settings = new SettingsData
            {
                InstanceName = instanceName,
                Settings = new SettingsConfig
                {
                    RejectCall = true,
                    MsgCall = "Chamada rejeitada",
                    GroupsIgnore = false,
                    AlwaysOnline = true,
                    ReadMessages = true,
                    ReadStatus = false,
                    SyncFullHistory = true
                }
            }
        };

        _httpService.PostAsync<SetSettingsRequest, SetSettingsResponse>(
            Arg.Any<string>(),
            Arg.Any<SetSettingsRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _settingsModule.SetAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<SetSettingsRequest, SetSettingsResponse>(
            $"settings/set/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetAsync_WithValidInstanceName_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var expectedResponse = new SettingsConfig
        {
            RejectCall = true,
            GroupsIgnore = false,
            AlwaysOnline = true,
            ReadMessages = true,
            ReadStatus = false,
            SyncFullHistory = true
        };

        _httpService.GetAsync<SettingsConfig>(
            Arg.Any<string>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _settingsModule.GetAsync(instanceName);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).GetAsync<SettingsConfig>(
            $"settings/find/{instanceName}",
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task SetAsync_WithNullInstanceName_ShouldThrowArgumentException()
    {
        // Arrange
        var request = new SetSettingsRequest();

        // Act & Assert
        var action = async () => await _settingsModule.SetAsync(null!, request);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Nome da instância é obrigatório*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task SetAsync_WithInvalidInstanceName_ShouldThrowArgumentException(string instanceName)
    {
        // Arrange
        var request = new SetSettingsRequest();

        // Act & Assert
        var action = async () => await _settingsModule.SetAsync(instanceName, request);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Nome da instância é obrigatório*");
    }

    [Fact]
    public async Task SetAsync_WithNullRequest_ShouldThrowArgumentNullException()
    {
        // Arrange
        var instanceName = "test-instance";

        // Act & Assert
        var action = async () => await _settingsModule.SetAsync(instanceName, null!);
        await action.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("request");
    }

    [Fact]
    public async Task GetAsync_WithNullInstanceName_ShouldThrowArgumentException()
    {
        // Act & Assert
        var action = async () => await _settingsModule.GetAsync(null!);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Nome da instância é obrigatório*");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public async Task GetAsync_WithInvalidInstanceName_ShouldThrowArgumentException(string instanceName)
    {
        // Act & Assert
        var action = async () => await _settingsModule.GetAsync(instanceName);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Nome da instância é obrigatório*");
    }
}
