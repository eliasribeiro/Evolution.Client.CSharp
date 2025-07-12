using Evolution.Client.CSharp.Core.Http;

namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Implementação do módulo WebSocket
/// </summary>
internal class WebSocketModule : IWebSocketModule
{
    private readonly IHttpService _httpService;

    public WebSocketModule(IHttpService httpService)
    {
        _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
    }

    public async Task<WebSocketResponse> SetAsync(
        string instanceName,
        SetWebSocketRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);
        ValidateWebSocketRequest(request);

        return await _httpService.PostAsync<SetWebSocketRequest, WebSocketResponse>(
            $"websocket/set/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<WebSocketResponse> FindAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<WebSocketResponse>(
            $"websocket/find/{instanceName}",
            cancellationToken);
    }

    public async Task<WebSocketStatsResponse> GetStatsAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<WebSocketStatsResponse>(
            $"websocket/stats/{instanceName}",
            cancellationToken);
    }

    public async Task<WebSocketResponse> TestConnectionAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.PostAsync<object, WebSocketResponse>(
            $"websocket/test/{instanceName}",
            new { },
            cancellationToken);
    }

    public async Task<WebSocketResponse> ReconnectAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.PostAsync<object, WebSocketResponse>(
            $"websocket/reconnect/{instanceName}",
            new { },
            cancellationToken);
    }

    public async Task<WebSocketResponse> DisconnectAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.PostAsync<object, WebSocketResponse>(
            $"websocket/disconnect/{instanceName}",
            new { },
            cancellationToken);
    }

    public async Task<WebSocketResponse> PingAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.PostAsync<object, WebSocketResponse>(
            $"websocket/ping/{instanceName}",
            new { },
            cancellationToken);
    }

    public async Task DeleteAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        await _httpService.DeleteAsync(
            $"websocket/delete/{instanceName}",
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

    private static void ValidateWebSocketRequest(SetWebSocketRequest request)
    {
        if (request.Events == null || request.Events.Length == 0)
        {
            throw new ArgumentException("Pelo menos um evento deve ser especificado", nameof(request.Events));
        }

        // Validar se os eventos são válidos
        var validEvents = WebSocketEvents.AllEvents;
        var invalidEvents = request.Events.Where(e => !validEvents.Contains(e)).ToArray();
        if (invalidEvents.Length > 0)
        {
            throw new ArgumentException($"Eventos inválidos: {string.Join(", ", invalidEvents)}", nameof(request.Events));
        }

        if (!string.IsNullOrEmpty(request.WebSocketUrl) && !Uri.TryCreate(request.WebSocketUrl, UriKind.Absolute, out _))
        {
            throw new ArgumentException("WebSocketUrl deve ser uma URL válida", nameof(request.WebSocketUrl));
        }

        if (request.ConnectionTimeout < 1 || request.ConnectionTimeout > 300)
        {
            throw new ArgumentException("ConnectionTimeout deve estar entre 1 e 300 segundos", nameof(request.ConnectionTimeout));
        }

        if (request.PingInterval < 1 || request.PingInterval > 300)
        {
            throw new ArgumentException("PingInterval deve estar entre 1 e 300 segundos", nameof(request.PingInterval));
        }

        if (request.MaxReconnectAttempts < 0 || request.MaxReconnectAttempts > 100)
        {
            throw new ArgumentException("MaxReconnectAttempts deve estar entre 0 e 100", nameof(request.MaxReconnectAttempts));
        }

        if (request.ReconnectInterval < 1 || request.ReconnectInterval > 300)
        {
            throw new ArgumentException("ReconnectInterval deve estar entre 1 e 300 segundos", nameof(request.ReconnectInterval));
        }
    }
}
