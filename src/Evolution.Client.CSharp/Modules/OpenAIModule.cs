using Evolution.Client.CSharp.Core.Http;

namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Implementação do módulo OpenAI
/// </summary>
internal class OpenAIModule : IOpenAIModule
{
    private readonly IHttpService _httpService;

    public OpenAIModule(IHttpService httpService)
    {
        _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
    }

    public async Task<OpenAIBotResponse> CreateBotAsync(
        string instanceName,
        CreateOpenAIBotRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<CreateOpenAIBotRequest, OpenAIBotResponse>(
            $"openai/create/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<OpenAIBotResponse> FindBotAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<OpenAIBotResponse>(
            $"openai/find/{instanceName}",
            cancellationToken);
    }

    public async Task<OpenAIBotListResponse> FindBotsAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<OpenAIBotListResponse>(
            $"openai/findBots/{instanceName}",
            cancellationToken);
    }

    public async Task<OpenAIBotResponse> UpdateBotAsync(
        string instanceName,
        UpdateOpenAIBotRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PutAsync<UpdateOpenAIBotRequest, OpenAIBotResponse>(
            $"openai/update/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task DeleteBotAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        await _httpService.DeleteAsync(
            $"openai/delete/{instanceName}",
            cancellationToken);
    }

    public async Task<OpenAICredsResponse> FindCredsAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<OpenAICredsResponse>(
            $"openai/findCreds/{instanceName}",
            cancellationToken);
    }

    public async Task<OpenAICredsResponse> SetCredsAsync(
        string instanceName,
        SetOpenAICredsRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<SetOpenAICredsRequest, OpenAICredsResponse>(
            $"openai/setCreds/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task DeleteCredsAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        await _httpService.DeleteAsync(
            $"openai/deleteCreds/{instanceName}",
            cancellationToken);
    }

    public async Task<OpenAISettingsResponse> SetSettingsAsync(
        string instanceName,
        OpenAISettingsRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<OpenAISettingsRequest, OpenAISettingsResponse>(
            $"openai/settings/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<OpenAISettingsResponse> FindSettingsAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<OpenAISettingsResponse>(
            $"openai/findSettings/{instanceName}",
            cancellationToken);
    }

    public async Task<OpenAISessionResponse> ChangeStatusAsync(
        string instanceName,
        ChangeOpenAIStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<ChangeOpenAIStatusRequest, OpenAISessionResponse>(
            $"openai/changeStatus/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<OpenAISessionResponse> FindSessionAsync(
        string instanceName,
        string number,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateNumber(number);

        return await _httpService.GetAsync<OpenAISessionResponse>(
            $"openai/findSession/{instanceName}?number={number}",
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
