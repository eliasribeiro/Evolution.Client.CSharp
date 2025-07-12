using Evolution.Client.Core.Http;

namespace Evolution.Client.Modules;

/// <summary>
/// Implementação do módulo de perfil
/// </summary>
internal class ProfileModule : IProfileModule
{
    private readonly IHttpService _httpService;

    public ProfileModule(IHttpService httpService)
    {
        _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
    }

    public async Task<BusinessProfile> FetchBusinessProfileAsync(
        string instanceName,
        FetchBusinessProfileRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<FetchBusinessProfileRequest, BusinessProfile>(
            $"chat/fetchBusinessProfile/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<UserProfile> FetchProfileAsync(
        string instanceName,
        FetchProfileRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<FetchProfileRequest, UserProfile>(
            $"chat/fetchProfile/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<UpdateProfileResponse> UpdateProfileNameAsync(
        string instanceName,
        UpdateProfileNameRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<UpdateProfileNameRequest, UpdateProfileResponse>(
            $"chat/updateProfileName/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<UpdateProfileResponse> UpdateProfileStatusAsync(
        string instanceName,
        UpdateProfileStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<UpdateProfileStatusRequest, UpdateProfileResponse>(
            $"chat/updateProfileStatus/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<UpdateProfileResponse> UpdateProfilePictureAsync(
        string instanceName,
        UpdateProfilePictureRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<UpdateProfilePictureRequest, UpdateProfileResponse>(
            $"chat/updateProfilePicture/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<UpdateProfileResponse> RemoveProfilePictureAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.DeleteAsync<UpdateProfileResponse>(
            $"chat/removeProfilePicture/{instanceName}",
            cancellationToken);
    }

    public async Task<PrivacySettings> FetchPrivacySettingsAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<PrivacySettings>(
            $"chat/fetchPrivacySettings/{instanceName}",
            cancellationToken);
    }

    public async Task<UpdateProfileResponse> UpdatePrivacySettingsAsync(
        string instanceName,
        UpdatePrivacySettingsRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<UpdatePrivacySettingsRequest, UpdateProfileResponse>(
            $"chat/updatePrivacySettings/{instanceName}",
            request,
            cancellationToken);
    }

    private static void ValidateInstanceName(string instanceName)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância é obrigatório", nameof(instanceName));
    }

    private static void ValidateRequest<T>(T request) where T : class
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
    }
}
