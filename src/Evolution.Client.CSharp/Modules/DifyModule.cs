using Evolution.Client.CSharp.Core.Http;

namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Implementação do módulo Dify
/// </summary>
internal class DifyModule : IDifyModule
{
    private readonly IHttpService _httpService;

    public DifyModule(IHttpService httpService)
    {
        _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
    }

    public async Task<DifyBotResponse> CreateBotAsync(
        string instanceName,
        CreateDifyBotRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<CreateDifyBotRequest, DifyBotResponse>(
            $"dify/create/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<DifyBotListResponse> FindBotsAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<DifyBotListResponse>(
            $"dify/find/{instanceName}",
            cancellationToken);
    }

    public async Task<DifyBotResponse> FindBotAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<DifyBotResponse>(
            $"dify/findBot/{instanceName}",
            cancellationToken);
    }

    public async Task<DifyBotResponse> UpdateBotAsync(
        string instanceName,
        UpdateDifyBotRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PutAsync<UpdateDifyBotRequest, DifyBotResponse>(
            $"dify/update/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<DifySettingsResponse> SetSettingsAsync(
        string instanceName,
        DifySettingsRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<DifySettingsRequest, DifySettingsResponse>(
            $"dify/setSettings/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<DifySettingsResponse> FindSettingsAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<DifySettingsResponse>(
            $"dify/findSettings/{instanceName}",
            cancellationToken);
    }

    public async Task<DifyStatusResponse> ChangeStatusAsync(
        string instanceName,
        ChangeDifyStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<ChangeDifyStatusRequest, DifyStatusResponse>(
            $"dify/changeStatus/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<DifyStatusResponse> FindStatusAsync(
        string instanceName,
        string number,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateNumber(number);

        return await _httpService.GetAsync<DifyStatusResponse>(
            $"dify/findStatus/{instanceName}?number={number}",
            cancellationToken);
    }

    private static void ValidateInstanceName(string instanceName)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
        {
            throw new ArgumentException("Nome da instância não pode ser vazio", nameof(instanceName));
        }
    }

    private static void ValidateRequest<T>(T request) where T : class
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }
    }

    private static void ValidateNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
        {
            throw new ArgumentException("Número não pode ser vazio", nameof(number));
        }
    }
}
