using Evolution.Client.CSharp.Core.Http;

namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Implementação do módulo Evolution Bot
/// </summary>
internal class EvolutionBotModule : IEvolutionBotModule
{
    private readonly IHttpService _httpService;

    public EvolutionBotModule(IHttpService httpService)
    {
        _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
    }

    public async Task<EvolutionBotResponse> CreateBotAsync(
        string instanceName,
        CreateEvolutionBotRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<CreateEvolutionBotRequest, EvolutionBotResponse>(
            $"evolutionBot/create/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<EvolutionBotResponse> FindBotAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<EvolutionBotResponse>(
            $"evolutionBot/find/{instanceName}",
            cancellationToken);
    }

    public async Task<EvolutionBotListResponse> FetchBotsAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<EvolutionBotListResponse>(
            $"evolutionBot/fetch/{instanceName}",
            cancellationToken);
    }

    public async Task<EvolutionBotResponse> UpdateBotAsync(
        string instanceName,
        UpdateEvolutionBotRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PutAsync<UpdateEvolutionBotRequest, EvolutionBotResponse>(
            $"evolutionBot/update/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task DeleteBotAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        await _httpService.DeleteAsync(
            $"evolutionBot/delete/{instanceName}",
            cancellationToken);
    }

    public async Task<EvolutionBotSettingsResponse> SetSettingsAsync(
        string instanceName,
        EvolutionBotSettingsRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<EvolutionBotSettingsRequest, EvolutionBotSettingsResponse>(
            $"evolutionBot/setSettings/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<EvolutionBotSettingsResponse> FindSettingsAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<EvolutionBotSettingsResponse>(
            $"evolutionBot/findSettings/{instanceName}",
            cancellationToken);
    }

    public async Task<EvolutionBotSessionResponse> ChangeStatusSessionAsync(
        string instanceName,
        ChangeEvolutionBotStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<ChangeEvolutionBotStatusRequest, EvolutionBotSessionResponse>(
            $"evolutionBot/changeStatus/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<EvolutionBotSessionResponse> FetchSessionAsync(
        string instanceName,
        string number,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateNumber(number);

        return await _httpService.GetAsync<EvolutionBotSessionResponse>(
            $"evolutionBot/fetchSession/{instanceName}?number={number}",
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
