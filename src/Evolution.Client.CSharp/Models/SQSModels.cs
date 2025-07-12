namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Requisição para configurar SQS (Simple Queue Service)
/// </summary>
public class SetSQSRequest
{
    /// <summary>
    /// Indica se está habilitado
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Chave de acesso AWS
    /// </summary>
    public string AccessKeyId { get; set; } = string.Empty;

    /// <summary>
    /// Chave secreta AWS
    /// </summary>
    public string SecretAccessKey { get; set; } = string.Empty;

    /// <summary>
    /// Região AWS
    /// </summary>
    public string Region { get; set; } = string.Empty;

    /// <summary>
    /// URL da fila SQS
    /// </summary>
    public string QueueUrl { get; set; } = string.Empty;

    /// <summary>
    /// Eventos a serem enviados para a fila
    /// </summary>
    public string[] Events { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Delay padrão para mensagens em segundos (0-900)
    /// </summary>
    public int DelaySeconds { get; set; } = 0;

    /// <summary>
    /// Tempo de visibilidade da mensagem em segundos (0-43200)
    /// </summary>
    public int VisibilityTimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Tempo de retenção da mensagem em segundos (60-1209600)
    /// </summary>
    public int MessageRetentionPeriod { get; set; } = 345600; // 4 dias

    /// <summary>
    /// Tamanho máximo da mensagem em bytes (1024-262144)
    /// </summary>
    public int MaxMessageSize { get; set; } = 262144; // 256KB

    /// <summary>
    /// Tempo de espera para recebimento de mensagens em segundos (0-20)
    /// </summary>
    public int ReceiveMessageWaitTimeSeconds { get; set; } = 0;

    /// <summary>
    /// Atributos customizados para as mensagens
    /// </summary>
    public Dictionary<string, string>? MessageAttributes { get; set; }

    /// <summary>
    /// Usar fila FIFO (First In, First Out)
    /// </summary>
    public bool UseFifoQueue { get; set; } = false;

    /// <summary>
    /// Grupo de mensagens para fila FIFO
    /// </summary>
    public string? MessageGroupId { get; set; }

    /// <summary>
    /// Deduplicação de conteúdo para fila FIFO
    /// </summary>
    public bool ContentBasedDeduplication { get; set; } = false;
}

/// <summary>
/// Resposta da configuração do SQS
/// </summary>
public class SQSResponse
{
    /// <summary>
    /// Dados do SQS configurado
    /// </summary>
    public SQSData? SQS { get; set; }

    /// <summary>
    /// Mensagem de status
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Indica se a operação foi bem-sucedida
    /// </summary>
    public bool Success { get; set; }
}

/// <summary>
/// Dados do SQS
/// </summary>
public class SQSData
{
    /// <summary>
    /// Nome da instância
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Configuração do SQS
    /// </summary>
    public SQSConfig? SQS { get; set; }

    /// <summary>
    /// Status da conexão com AWS
    /// </summary>
    public SQSConnectionStatus? ConnectionStatus { get; set; }

    /// <summary>
    /// Data de criação
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Data de atualização
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Configuração do SQS
/// </summary>
public class SQSConfig
{
    /// <summary>
    /// Indica se está habilitado
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Chave de acesso (mascarada para segurança)
    /// </summary>
    public string? AccessKeyId { get; set; }

    /// <summary>
    /// Chave secreta (mascarada para segurança)
    /// </summary>
    public string? SecretAccessKey { get; set; }

    /// <summary>
    /// Região AWS
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    /// URL da fila
    /// </summary>
    public string? QueueUrl { get; set; }

    /// <summary>
    /// Eventos configurados
    /// </summary>
    public string[]? Events { get; set; }

    /// <summary>
    /// Delay padrão
    /// </summary>
    public int? DelaySeconds { get; set; }

    /// <summary>
    /// Timeout de visibilidade
    /// </summary>
    public int? VisibilityTimeoutSeconds { get; set; }

    /// <summary>
    /// Período de retenção
    /// </summary>
    public int? MessageRetentionPeriod { get; set; }

    /// <summary>
    /// Tamanho máximo da mensagem
    /// </summary>
    public int? MaxMessageSize { get; set; }

    /// <summary>
    /// Tempo de espera para recebimento
    /// </summary>
    public int? ReceiveMessageWaitTimeSeconds { get; set; }

    /// <summary>
    /// Atributos de mensagem
    /// </summary>
    public Dictionary<string, string>? MessageAttributes { get; set; }

    /// <summary>
    /// Usar fila FIFO
    /// </summary>
    public bool? UseFifoQueue { get; set; }

    /// <summary>
    /// ID do grupo de mensagens
    /// </summary>
    public string? MessageGroupId { get; set; }

    /// <summary>
    /// Deduplicação baseada em conteúdo
    /// </summary>
    public bool? ContentBasedDeduplication { get; set; }
}

/// <summary>
/// Status da conexão com SQS
/// </summary>
public class SQSConnectionStatus
{
    /// <summary>
    /// Estado da conexão (connected, disconnected, error)
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Indica se está conectado
    /// </summary>
    public bool IsConnected { get; set; }

    /// <summary>
    /// Última verificação de conexão
    /// </summary>
    public DateTime? LastChecked { get; set; }

    /// <summary>
    /// Último erro de conexão
    /// </summary>
    public string? LastError { get; set; }

    /// <summary>
    /// Região AWS ativa
    /// </summary>
    public string? ActiveRegion { get; set; }

    /// <summary>
    /// Nome da fila ativa
    /// </summary>
    public string? ActiveQueueName { get; set; }

    /// <summary>
    /// Tipo da fila (Standard ou FIFO)
    /// </summary>
    public string? QueueType { get; set; }
}

/// <summary>
/// Estatísticas do SQS
/// </summary>
public class SQSStats
{
    /// <summary>
    /// Total de mensagens enviadas
    /// </summary>
    public long MessagesSent { get; set; }

    /// <summary>
    /// Total de mensagens com falha
    /// </summary>
    public long MessagesFailed { get; set; }

    /// <summary>
    /// Total de eventos processados
    /// </summary>
    public long EventsProcessed { get; set; }

    /// <summary>
    /// Mensagens aproximadas na fila
    /// </summary>
    public int? ApproximateNumberOfMessages { get; set; }

    /// <summary>
    /// Mensagens aproximadas não visíveis
    /// </summary>
    public int? ApproximateNumberOfMessagesNotVisible { get; set; }

    /// <summary>
    /// Mensagens aproximadas atrasadas
    /// </summary>
    public int? ApproximateNumberOfMessagesDelayed { get; set; }

    /// <summary>
    /// Última atualização das estatísticas
    /// </summary>
    public DateTime? LastUpdated { get; set; }

    /// <summary>
    /// Tamanho médio das mensagens em bytes
    /// </summary>
    public double AverageMessageSize { get; set; }

    /// <summary>
    /// Taxa de sucesso de envio (0-100)
    /// </summary>
    public double SuccessRate { get; set; }
}

/// <summary>
/// Resposta das estatísticas do SQS
/// </summary>
public class SQSStatsResponse
{
    /// <summary>
    /// Estatísticas do SQS
    /// </summary>
    public SQSStats? Stats { get; set; }

    /// <summary>
    /// Status da conexão
    /// </summary>
    public SQSConnectionStatus? ConnectionStatus { get; set; }

    /// <summary>
    /// Nome da instância
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;
}

/// <summary>
/// Eventos disponíveis para SQS
/// </summary>
public static class SQSEvents
{
    /// <summary>
    /// Evento de mensagem recebida
    /// </summary>
    public const string MessageReceived = "message.received";

    /// <summary>
    /// Evento de mensagem enviada
    /// </summary>
    public const string MessageSent = "message.sent";

    /// <summary>
    /// Evento de status de mensagem
    /// </summary>
    public const string MessageStatus = "message.status";

    /// <summary>
    /// Evento de conexão da instância
    /// </summary>
    public const string InstanceConnect = "instance.connect";

    /// <summary>
    /// Evento de desconexão da instância
    /// </summary>
    public const string InstanceDisconnect = "instance.disconnect";

    /// <summary>
    /// Lista de todos os eventos disponíveis
    /// </summary>
    public static readonly string[] AllEvents = new[]
    {
        MessageReceived,
        MessageSent,
        MessageStatus,
        InstanceConnect,
        InstanceDisconnect
    };
}
