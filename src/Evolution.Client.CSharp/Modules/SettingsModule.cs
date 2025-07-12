using Evolution.Client.CSharp.Core.Http;

namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Implementação do módulo de configurações
/// </summary>
internal class SettingsModule : ISettingsModule
{
    private readonly IHttpService _httpService;

    public SettingsModule(IHttpService httpService)
    {
        _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
    }

    public async Task<SetSettingsResponse> SetAsync(
        string instanceName,
        SetSettingsRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<SetSettingsRequest, SetSettingsResponse>(
            $"settings/set/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<SettingsConfig> GetAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<SettingsConfig>(
            $"settings/find/{instanceName}",
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
