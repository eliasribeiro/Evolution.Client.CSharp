using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Evolution.Client.Core.Http;

/// <summary>
/// Serviço para realizar requisições HTTP para a Evolution API
/// </summary>
internal class HttpService : IHttpService
{
    private readonly HttpClient _httpClient;
    private readonly EvolutionClientOptions _options;
    private readonly ILogger? _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public HttpService(
        HttpClient httpClient,
        EvolutionClientOptions options,
        ILogger? logger = null)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger;

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
    }

    public async Task<TResponse> GetAsync<TResponse>(
        string endpoint,
        CancellationToken cancellationToken = default)
    {
        return await SendRequestAsync<TResponse>(
            HttpMethod.Get,
            endpoint,
            null,
            cancellationToken);
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        return await SendRequestAsync<TResponse>(
            HttpMethod.Post,
            endpoint,
            request,
            cancellationToken);
    }

    public async Task<TResponse> PutAsync<TRequest, TResponse>(
        string endpoint,
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        return await SendRequestAsync<TResponse>(
            HttpMethod.Put,
            endpoint,
            request,
            cancellationToken);
    }

    public async Task<TResponse> DeleteAsync<TResponse>(
        string endpoint,
        CancellationToken cancellationToken = default)
    {
        return await SendRequestAsync<TResponse>(
            HttpMethod.Delete,
            endpoint,
            null,
            cancellationToken);
    }

    public async Task PostAsync<TRequest>(
        string endpoint,
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        await SendRequestAsync(
            HttpMethod.Post,
            endpoint,
            request,
            cancellationToken);
    }

    public async Task PutAsync<TRequest>(
        string endpoint,
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        await SendRequestAsync(
            HttpMethod.Put,
            endpoint,
            request,
            cancellationToken);
    }

    public async Task DeleteAsync(
        string endpoint,
        CancellationToken cancellationToken = default)
    {
        await SendRequestAsync(
            HttpMethod.Delete,
            endpoint,
            null,
            cancellationToken);
    }

    private async Task<TResponse> SendRequestAsync<TResponse>(
        HttpMethod method,
        string endpoint,
        object? requestBody,
        CancellationToken cancellationToken)
    {
        var response = await SendRequestAsync(method, endpoint, requestBody, cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(content))
        {
            return default!;
        }

        try
        {
            return JsonSerializer.Deserialize<TResponse>(content, _jsonOptions)!;
        }
        catch (JsonException ex)
        {
            _logger?.LogError(ex, "Erro ao deserializar resposta JSON: {Content}", content);
            throw new EvolutionApiException("Erro ao processar resposta da API", ex);
        }
    }

    private async Task<HttpResponseMessage> SendRequestAsync(
        HttpMethod method,
        string endpoint,
        object? requestBody,
        CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(method, endpoint);

        // Adicionar headers customizados
        foreach (var header in _options.CustomHeaders)
        {
            request.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        // Adicionar corpo da requisição se necessário
        if (requestBody != null)
        {
            var json = JsonSerializer.Serialize(requestBody, _jsonOptions);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            if (_options.LogRequestBody)
            {
                _logger?.LogDebug("Request body: {RequestBody}", json);
            }
        }

        if (_options.LogHttpRequests)
        {
            _logger?.LogDebug("Enviando requisição {Method} para {Endpoint}", method, endpoint);
        }

        var response = await _httpClient.SendAsync(request, cancellationToken);

        if (_options.LogHttpRequests)
        {
            _logger?.LogDebug("Resposta recebida: {StatusCode}", response.StatusCode);
        }

        if (!response.IsSuccessStatusCode)
        {
            await HandleErrorResponseAsync(response, cancellationToken);
        }

        return response;
    }

    private async Task HandleErrorResponseAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (_options.LogResponseBody)
        {
            _logger?.LogError("Erro na resposta da API: {StatusCode} - {Content}", 
                response.StatusCode, content);
        }

        var message = response.StatusCode switch
        {
            HttpStatusCode.Unauthorized => "Credenciais inválidas ou expiradas",
            HttpStatusCode.Forbidden => "Acesso negado para este recurso",
            HttpStatusCode.NotFound => "Recurso não encontrado",
            HttpStatusCode.TooManyRequests => "Limite de requisições excedido",
            HttpStatusCode.InternalServerError => "Erro interno do servidor",
            HttpStatusCode.BadGateway => "Erro de gateway",
            HttpStatusCode.ServiceUnavailable => "Serviço temporariamente indisponível",
            HttpStatusCode.GatewayTimeout => "Timeout do gateway",
            _ => $"Erro na requisição: {response.StatusCode}"
        };

        throw new EvolutionApiException(message, response.StatusCode, content);
    }
}
