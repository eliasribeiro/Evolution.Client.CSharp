using Evolution.Client.CSharp.Core.Http;

namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Implementação do módulo Flowise
/// </summary>
internal class FlowiseModule : IFlowiseModule
{
    private readonly IHttpService _httpService;

    public FlowiseModule(IHttpService httpService)
    {
        _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
    }

    public async Task<FlowiseBotResponse> CreateBotAsync(
        string instanceName,
        CreateFlowiseBotRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<CreateFlowiseBotRequest, FlowiseBotResponse>(
            $"flowise/create/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<FlowiseBotListResponse> FindBotsAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<FlowiseBotListResponse>(
            $"flowise/findBots/{instanceName}",
            cancellationToken);
    }

    public async Task<FlowiseBotResponse> FindBotAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<FlowiseBotResponse>(
            $"flowise/findBot/{instanceName}",
            cancellationToken);
    }

    public async Task<FlowiseBotResponse> UpdateBotAsync(
        string instanceName,
        UpdateFlowiseBotRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<UpdateFlowiseBotRequest, FlowiseBotResponse>(
            $"flowise/update/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task DeleteBotAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        await _httpService.DeleteAsync(
            $"flowise/delete/{instanceName}",
            cancellationToken);
    }

    public async Task<FlowiseSettingsResponse> SetSettingsAsync(
        string instanceName,
        FlowiseSettingsRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<FlowiseSettingsRequest, FlowiseSettingsResponse>(
            $"flowise/setSettings/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<FlowiseSettingsResponse> FindSettingsAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<FlowiseSettingsResponse>(
            $"flowise/findSettings/{instanceName}",
            cancellationToken);
    }

    public async Task<FlowiseSessionListResponse> ChangeSessionStatusAsync(
        string instanceName,
        ChangeFlowiseSessionStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<ChangeFlowiseSessionStatusRequest, FlowiseSessionListResponse>(
            $"flowise/changeStatus/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<FlowiseSessionListResponse> FindSessionsAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<FlowiseSessionListResponse>(
            $"flowise/findSessions/{instanceName}",
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
}
