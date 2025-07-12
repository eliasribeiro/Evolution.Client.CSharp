using FluentAssertions;
using Evolution.Client.CSharp.Core.Http;
using Evolution.Client.CSharp.Modules;
using NSubstitute;

namespace Evolution.Client.CSharp.Tests.Modules;

public class ProfileModuleTests
{
    private readonly IHttpService _httpService;
    private readonly ProfileModule _profileModule;

    public ProfileModuleTests()
    {
        _httpService = Substitute.For<IHttpService>();
        _profileModule = new ProfileModule(_httpService);
    }

    [Fact]
    public void Constructor_WithNullHttpService_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        var action = () => new ProfileModule(null!);
        action.Should().Throw<ArgumentNullException>()
            .WithParameterName("httpService");
    }

    [Fact]
    public async Task FetchBusinessProfileAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new FetchBusinessProfileRequest
        {
            Number = "5511999999999"
        };
        var expectedResponse = new BusinessProfile
        {
            Id = "business123",
            Name = "Minha Empresa",
            Category = "Tecnologia",
            Email = "contato@empresa.com"
        };

        _httpService.PostAsync<FetchBusinessProfileRequest, BusinessProfile>(
            Arg.Any<string>(),
            Arg.Any<FetchBusinessProfileRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _profileModule.FetchBusinessProfileAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<FetchBusinessProfileRequest, BusinessProfile>(
            $"chat/fetchBusinessProfile/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task FetchProfileAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new FetchProfileRequest
        {
            Number = "5511999999999"
        };
        var expectedResponse = new UserProfile
        {
            Name = "João Silva",
            Status = "Disponível",
            Number = "5511999999999"
        };

        _httpService.PostAsync<FetchProfileRequest, UserProfile>(
            Arg.Any<string>(),
            Arg.Any<FetchProfileRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _profileModule.FetchProfileAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<FetchProfileRequest, UserProfile>(
            $"chat/fetchProfile/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdateProfileNameAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new UpdateProfileNameRequest
        {
            Name = "Novo Nome"
        };
        var expectedResponse = new UpdateProfileResponse
        {
            Success = true,
            Message = "Nome atualizado com sucesso"
        };

        _httpService.PostAsync<UpdateProfileNameRequest, UpdateProfileResponse>(
            Arg.Any<string>(),
            Arg.Any<UpdateProfileNameRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _profileModule.UpdateProfileNameAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<UpdateProfileNameRequest, UpdateProfileResponse>(
            $"chat/updateProfileName/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdateProfileStatusAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new UpdateProfileStatusRequest
        {
            Status = "Novo Status"
        };
        var expectedResponse = new UpdateProfileResponse
        {
            Success = true,
            Message = "Status atualizado com sucesso"
        };

        _httpService.PostAsync<UpdateProfileStatusRequest, UpdateProfileResponse>(
            Arg.Any<string>(),
            Arg.Any<UpdateProfileStatusRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _profileModule.UpdateProfileStatusAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<UpdateProfileStatusRequest, UpdateProfileResponse>(
            $"chat/updateProfileStatus/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdateProfilePictureAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new UpdateProfilePictureRequest
        {
            Picture = "https://example.com/picture.jpg"
        };
        var expectedResponse = new UpdateProfileResponse
        {
            Success = true,
            Message = "Foto atualizada com sucesso"
        };

        _httpService.PostAsync<UpdateProfilePictureRequest, UpdateProfileResponse>(
            Arg.Any<string>(),
            Arg.Any<UpdateProfilePictureRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _profileModule.UpdateProfilePictureAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<UpdateProfilePictureRequest, UpdateProfileResponse>(
            $"chat/updateProfilePicture/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task RemoveProfilePictureAsync_WithValidInstanceName_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var expectedResponse = new UpdateProfileResponse
        {
            Success = true,
            Message = "Foto removida com sucesso"
        };

        _httpService.DeleteAsync<UpdateProfileResponse>(
            Arg.Any<string>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _profileModule.RemoveProfilePictureAsync(instanceName);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).DeleteAsync<UpdateProfileResponse>(
            $"chat/removeProfilePicture/{instanceName}",
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task FetchPrivacySettingsAsync_WithValidInstanceName_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var expectedResponse = new PrivacySettings
        {
            ReadReceipts = "all",
            Profile = "contacts",
            Status = "contacts",
            Online = "all",
            GroupAdd = "contacts",
            CallAdd = "all"
        };

        _httpService.GetAsync<PrivacySettings>(
            Arg.Any<string>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _profileModule.FetchPrivacySettingsAsync(instanceName);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).GetAsync<PrivacySettings>(
            $"chat/fetchPrivacySettings/{instanceName}",
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdatePrivacySettingsAsync_WithValidParameters_ShouldCallHttpService()
    {
        // Arrange
        var instanceName = "test-instance";
        var request = new UpdatePrivacySettingsRequest
        {
            ReadReceipts = "all",
            Profile = "contacts",
            Status = "contacts",
            Online = "all",
            GroupAdd = "contacts",
            CallAdd = "all"
        };
        var expectedResponse = new UpdateProfileResponse
        {
            Success = true,
            Message = "Configurações de privacidade atualizadas"
        };

        _httpService.PostAsync<UpdatePrivacySettingsRequest, UpdateProfileResponse>(
            Arg.Any<string>(),
            Arg.Any<UpdatePrivacySettingsRequest>(),
            Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Act
        var result = await _profileModule.UpdatePrivacySettingsAsync(instanceName, request);

        // Assert
        result.Should().Be(expectedResponse);
        await _httpService.Received(1).PostAsync<UpdatePrivacySettingsRequest, UpdateProfileResponse>(
            $"chat/updatePrivacySettings/{instanceName}",
            request,
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task FetchBusinessProfileAsync_WithNullInstanceName_ShouldThrowArgumentException()
    {
        // Arrange
        var request = new FetchBusinessProfileRequest();

        // Act & Assert
        var action = async () => await _profileModule.FetchBusinessProfileAsync(null!, request);
        await action.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Nome da instância é obrigatório*");
    }

    [Fact]
    public async Task FetchBusinessProfileAsync_WithNullRequest_ShouldThrowArgumentNullException()
    {
        // Arrange
        var instanceName = "test-instance";

        // Act & Assert
        var action = async () => await _profileModule.FetchBusinessProfileAsync(instanceName, null!);
        await action.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("request");
    }
}
