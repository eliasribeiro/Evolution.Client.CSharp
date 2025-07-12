using Evolution.Client.CSharp.Core.Http;

namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Implementação do módulo SQS (Simple Queue Service)
/// </summary>
internal class SQSModule : ISQSModule
{
    private readonly IHttpService _httpService;

    public SQSModule(IHttpService httpService)
    {
        _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
    }

    public async Task<SQSResponse> SetAsync(
        string instanceName,
        SetSQSRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);
        ValidateSQSRequest(request);

        return await _httpService.PostAsync<SetSQSRequest, SQSResponse>(
            $"sqs/set/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<SQSResponse> FindAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<SQSResponse>(
            $"sqs/find/{instanceName}",
            cancellationToken);
    }

    public async Task<SQSStatsResponse> GetStatsAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<SQSStatsResponse>(
            $"sqs/stats/{instanceName}",
            cancellationToken);
    }

    public async Task<SQSResponse> TestConnectionAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.PostAsync<object, SQSResponse>(
            $"sqs/test/{instanceName}",
            new { },
            cancellationToken);
    }

    public async Task<SQSResponse> GetQueueAttributesAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<SQSResponse>(
            $"sqs/queue/attributes/{instanceName}",
            cancellationToken);
    }

    public async Task<SQSResponse> PurgeQueueAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.PostAsync<object, SQSResponse>(
            $"sqs/queue/purge/{instanceName}",
            new { },
            cancellationToken);
    }

    public async Task<SQSResponse> SendTestMessageAsync(
        string instanceName,
        string message,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateMessage(message);

        var request = new { message };

        return await _httpService.PostAsync<object, SQSResponse>(
            $"sqs/test/message/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task DeleteAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        await _httpService.DeleteAsync(
            $"sqs/delete/{instanceName}",
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

    private static void ValidateSQSRequest(SetSQSRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.AccessKeyId))
        {
            throw new ArgumentException("AccessKeyId é obrigatório", nameof(request.AccessKeyId));
        }

        if (string.IsNullOrWhiteSpace(request.SecretAccessKey))
        {
            throw new ArgumentException("SecretAccessKey é obrigatório", nameof(request.SecretAccessKey));
        }

        if (string.IsNullOrWhiteSpace(request.Region))
        {
            throw new ArgumentException("Region é obrigatória", nameof(request.Region));
        }

        if (string.IsNullOrWhiteSpace(request.QueueUrl))
        {
            throw new ArgumentException("QueueUrl é obrigatória", nameof(request.QueueUrl));
        }

        if (!Uri.TryCreate(request.QueueUrl, UriKind.Absolute, out _))
        {
            throw new ArgumentException("QueueUrl deve ser uma URL válida", nameof(request.QueueUrl));
        }

        if (request.Events == null || request.Events.Length == 0)
        {
            throw new ArgumentException("Pelo menos um evento deve ser especificado", nameof(request.Events));
        }

        // Validar se os eventos são válidos
        var validEvents = SQSEvents.AllEvents;
        var invalidEvents = request.Events.Where(e => !validEvents.Contains(e)).ToArray();
        if (invalidEvents.Length > 0)
        {
            throw new ArgumentException($"Eventos inválidos: {string.Join(", ", invalidEvents)}", nameof(request.Events));
        }

        if (request.DelaySeconds < 0 || request.DelaySeconds > 900)
        {
            throw new ArgumentException("DelaySeconds deve estar entre 0 e 900", nameof(request.DelaySeconds));
        }

        if (request.VisibilityTimeoutSeconds < 0 || request.VisibilityTimeoutSeconds > 43200)
        {
            throw new ArgumentException("VisibilityTimeoutSeconds deve estar entre 0 e 43200", nameof(request.VisibilityTimeoutSeconds));
        }

        if (request.MessageRetentionPeriod < 60 || request.MessageRetentionPeriod > 1209600)
        {
            throw new ArgumentException("MessageRetentionPeriod deve estar entre 60 e 1209600", nameof(request.MessageRetentionPeriod));
        }

        if (request.MaxMessageSize < 1024 || request.MaxMessageSize > 262144)
        {
            throw new ArgumentException("MaxMessageSize deve estar entre 1024 e 262144", nameof(request.MaxMessageSize));
        }

        if (request.ReceiveMessageWaitTimeSeconds < 0 || request.ReceiveMessageWaitTimeSeconds > 20)
        {
            throw new ArgumentException("ReceiveMessageWaitTimeSeconds deve estar entre 0 e 20", nameof(request.ReceiveMessageWaitTimeSeconds));
        }

        // Validações específicas para fila FIFO
        if (request.UseFifoQueue)
        {
            if (!request.QueueUrl.EndsWith(".fifo"))
            {
                throw new ArgumentException("QueueUrl deve terminar com '.fifo' para filas FIFO", nameof(request.QueueUrl));
            }

            if (string.IsNullOrWhiteSpace(request.MessageGroupId))
            {
                throw new ArgumentException("MessageGroupId é obrigatório para filas FIFO", nameof(request.MessageGroupId));
            }
        }
    }
}
