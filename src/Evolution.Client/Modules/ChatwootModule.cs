using Evolution.Client.Core.Http;

namespace Evolution.Client.Modules;

/// <summary>
/// Implementação do módulo Chatwoot
/// </summary>
internal class ChatwootModule : IChatwootModule
{
    private readonly IHttpService _httpService;

    public ChatwootModule(IHttpService httpService)
    {
        _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
    }

    public async Task<ChatwootResponse> SetAsync(
        string instanceName,
        SetChatwootRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);
        ValidateChatwootRequest(request);

        return await _httpService.PostAsync<SetChatwootRequest, ChatwootResponse>(
            $"chatwoot/set/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<ChatwootResponse> FindAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<ChatwootResponse>(
            $"chatwoot/find/{instanceName}",
            cancellationToken);
    }

    public async Task<ChatwootStatsResponse> GetStatsAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<ChatwootStatsResponse>(
            $"chatwoot/stats/{instanceName}",
            cancellationToken);
    }

    public async Task<ChatwootResponse> TestConnectionAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.PostAsync<object, ChatwootResponse>(
            $"chatwoot/test/{instanceName}",
            new { },
            cancellationToken);
    }

    public async Task<ChatwootResponse> SyncContactsAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.PostAsync<object, ChatwootResponse>(
            $"chatwoot/sync/contacts/{instanceName}",
            new { },
            cancellationToken);
    }

    public async Task<ChatwootResponse> SyncMessagesAsync(
        string instanceName,
        int days = 7,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateDays(days);

        return await _httpService.PostAsync<object, ChatwootResponse>(
            $"chatwoot/sync/messages/{instanceName}?days={days}",
            new { },
            cancellationToken);
    }

    public async Task DeleteAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        await _httpService.DeleteAsync(
            $"chatwoot/delete/{instanceName}",
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

    private static void ValidateChatwootRequest(SetChatwootRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.AccountId))
        {
            throw new ArgumentException("AccountId é obrigatório", nameof(request.AccountId));
        }

        if (string.IsNullOrWhiteSpace(request.Token))
        {
            throw new ArgumentException("Token é obrigatório", nameof(request.Token));
        }

        if (string.IsNullOrWhiteSpace(request.Url))
        {
            throw new ArgumentException("URL é obrigatória", nameof(request.Url));
        }

        if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
        {
            throw new ArgumentException("URL deve ser uma URL válida", nameof(request.Url));
        }

        if (request.DaysLimitImportMessages < 1 || request.DaysLimitImportMessages > 365)
        {
            throw new ArgumentException("DaysLimitImportMessages deve estar entre 1 e 365", nameof(request.DaysLimitImportMessages));
        }
    }

    private static void ValidateDays(int days)
    {
        if (days < 1 || days > 365)
        {
            throw new ArgumentException("Dias deve estar entre 1 e 365", nameof(days));
        }
    }
}
