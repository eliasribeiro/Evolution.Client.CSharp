using Evolution.Client.Core.Http;

namespace Evolution.Client.Modules;

/// <summary>
/// Implementação do módulo RabbitMQ
/// </summary>
internal class RabbitMQModule : IRabbitMQModule
{
    private readonly IHttpService _httpService;

    public RabbitMQModule(IHttpService httpService)
    {
        _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
    }

    public async Task<RabbitMQResponse> SetAsync(
        string instanceName,
        SetRabbitMQRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);
        ValidateRabbitMQRequest(request);

        return await _httpService.PostAsync<SetRabbitMQRequest, RabbitMQResponse>(
            $"rabbitmq/set/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<RabbitMQResponse> FindAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<RabbitMQResponse>(
            $"rabbitmq/find/{instanceName}",
            cancellationToken);
    }

    public async Task<RabbitMQStatsResponse> GetStatsAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<RabbitMQStatsResponse>(
            $"rabbitmq/stats/{instanceName}",
            cancellationToken);
    }

    public async Task<RabbitMQResponse> TestConnectionAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.PostAsync<object, RabbitMQResponse>(
            $"rabbitmq/test/{instanceName}",
            new { },
            cancellationToken);
    }

    public async Task<RabbitMQResponse> ReconnectAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.PostAsync<object, RabbitMQResponse>(
            $"rabbitmq/reconnect/{instanceName}",
            new { },
            cancellationToken);
    }

    public async Task<RabbitMQResponse> DisconnectAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.PostAsync<object, RabbitMQResponse>(
            $"rabbitmq/disconnect/{instanceName}",
            new { },
            cancellationToken);
    }

    public async Task<RabbitMQResponse> PublishTestMessageAsync(
        string instanceName,
        string message,
        string? routingKey = null,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateMessage(message);

        var request = new { message, routingKey };

        return await _httpService.PostAsync<object, RabbitMQResponse>(
            $"rabbitmq/test/message/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<RabbitMQResponse> CheckExchangeAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<RabbitMQResponse>(
            $"rabbitmq/exchange/check/{instanceName}",
            cancellationToken);
    }

    public async Task<RabbitMQResponse> CheckQueueAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<RabbitMQResponse>(
            $"rabbitmq/queue/check/{instanceName}",
            cancellationToken);
    }

    public async Task<RabbitMQResponse> PurgeQueueAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.PostAsync<object, RabbitMQResponse>(
            $"rabbitmq/queue/purge/{instanceName}",
            new { },
            cancellationToken);
    }

    public async Task DeleteAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        await _httpService.DeleteAsync(
            $"rabbitmq/delete/{instanceName}",
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

    private static void ValidateMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentException("Mensagem não pode ser vazia", nameof(message));
        }
    }

    private static void ValidateRabbitMQRequest(SetRabbitMQRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Uri))
        {
            throw new ArgumentException("URI é obrigatória", nameof(request.Uri));
        }

        if (!Uri.TryCreate(request.Uri, UriKind.Absolute, out var uri) || uri.Scheme != "amqp" && uri.Scheme != "amqps")
        {
            throw new ArgumentException("URI deve ser uma URI AMQP válida (amqp:// ou amqps://)", nameof(request.Uri));
        }

        if (string.IsNullOrWhiteSpace(request.Exchange))
        {
            throw new ArgumentException("Exchange é obrigatória", nameof(request.Exchange));
        }

        if (request.Events == null || request.Events.Length == 0)
        {
            throw new ArgumentException("Pelo menos um evento deve ser especificado", nameof(request.Events));
        }

        // Validar se os eventos são válidos
        var validEvents = RabbitMQEvents.AllEvents;
        var invalidEvents = request.Events.Where(e => !validEvents.Contains(e)).ToArray();
        if (invalidEvents.Length > 0)
        {
            throw new ArgumentException($"Eventos inválidos: {string.Join(", ", invalidEvents)}", nameof(request.Events));
        }

        // Validar tipo de exchange
        var validExchangeTypes = new[] { "direct", "topic", "fanout", "headers" };
        if (!validExchangeTypes.Contains(request.ExchangeType.ToLower()))
        {
            throw new ArgumentException($"ExchangeType deve ser um dos seguintes: {string.Join(", ", validExchangeTypes)}", nameof(request.ExchangeType));
        }

        if (string.IsNullOrWhiteSpace(request.DefaultRoutingKey))
        {
            throw new ArgumentException("DefaultRoutingKey é obrigatória", nameof(request.DefaultRoutingKey));
        }

        if (request.ConnectionTimeout < 1 || request.ConnectionTimeout > 300)
        {
            throw new ArgumentException("ConnectionTimeout deve estar entre 1 e 300 segundos", nameof(request.ConnectionTimeout));
        }

        if (request.HeartbeatInterval < 1 || request.HeartbeatInterval > 300)
        {
            throw new ArgumentException("HeartbeatInterval deve estar entre 1 e 300 segundos", nameof(request.HeartbeatInterval));
        }

        if (request.MaxReconnectAttempts < 0 || request.MaxReconnectAttempts > 100)
        {
            throw new ArgumentException("MaxReconnectAttempts deve estar entre 0 e 100", nameof(request.MaxReconnectAttempts));
        }

        if (request.ReconnectInterval < 1 || request.ReconnectInterval > 300)
        {
            throw new ArgumentException("ReconnectInterval deve estar entre 1 e 300 segundos", nameof(request.ReconnectInterval));
        }

        // Validações opcionais
        if (request.MessageTTL.HasValue && (request.MessageTTL.Value < 1 || request.MessageTTL.Value > 2147483647))
        {
            throw new ArgumentException("MessageTTL deve estar entre 1 e 2147483647 milissegundos", nameof(request.MessageTTL));
        }

        if (request.MaxLength.HasValue && request.MaxLength.Value < 1)
        {
            throw new ArgumentException("MaxLength deve ser maior que 0", nameof(request.MaxLength));
        }

        if (request.MaxLengthBytes.HasValue && request.MaxLengthBytes.Value < 1)
        {
            throw new ArgumentException("MaxLengthBytes deve ser maior que 0", nameof(request.MaxLengthBytes));
        }

        if (!string.IsNullOrEmpty(request.OverflowBehaviour))
        {
            var validOverflowBehaviours = new[] { "drop-head", "reject-publish", "reject-publish-dlx" };
            if (!validOverflowBehaviours.Contains(request.OverflowBehaviour))
            {
                throw new ArgumentException($"OverflowBehaviour deve ser um dos seguintes: {string.Join(", ", validOverflowBehaviours)}", nameof(request.OverflowBehaviour));
            }
        }
    }
}
