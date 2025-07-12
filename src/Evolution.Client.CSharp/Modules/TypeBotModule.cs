using Evolution.Client.CSharp.Core.Http;

namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Implementação do módulo TypeBot
/// </summary>
internal class TypeBotModule : ITypeBotModule
{
    private readonly IHttpService _httpService;

    public TypeBotModule(IHttpService httpService)
    {
        _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
    }

    public async Task<TypeBotResponse> CreateAsync(
        string instanceName,
        CreateTypeBotRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<CreateTypeBotRequest, TypeBotResponse>(
            $"typebot/create/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<TypeBotResponse> StartAsync(
        string instanceName,
        StartTypeBotRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<StartTypeBotRequest, TypeBotResponse>(
            $"typebot/start/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<TypeBotResponse> FindAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<TypeBotResponse>(
            $"typebot/find/{instanceName}",
            cancellationToken);
    }

    public async Task<TypeBotListResponse> FetchAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<TypeBotListResponse>(
            $"typebot/fetch/{instanceName}",
            cancellationToken);
    }

    public async Task<TypeBotResponse> UpdateAsync(
        string instanceName,
        UpdateTypeBotRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<UpdateTypeBotRequest, TypeBotResponse>(
            $"typebot/update/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task DeleteAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        await _httpService.DeleteAsync(
            $"typebot/delete/{instanceName}",
            cancellationToken);
    }

    public async Task<TypeBotSessionResponse> ChangeSessionStatusAsync(
        string instanceName,
        ChangeSessionStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<ChangeSessionStatusRequest, TypeBotSessionResponse>(
            $"typebot/changeStatus/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<TypeBotSessionResponse> FetchSessionAsync(
        string instanceName,
        string number,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateNumber(number);

        return await _httpService.GetAsync<TypeBotSessionResponse>(
            $"typebot/fetchSession/{instanceName}?number={number}",
            cancellationToken);
    }

    public async Task<TypeBotSettingsResponse> SetSettingsAsync(
        string instanceName,
        TypeBotSettingsRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<TypeBotSettingsRequest, TypeBotSettingsResponse>(
            $"typebot/settings/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<TypeBotSettingsResponse> FindSettingsAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<TypeBotSettingsResponse>(
            $"typebot/findSettings/{instanceName}",
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
