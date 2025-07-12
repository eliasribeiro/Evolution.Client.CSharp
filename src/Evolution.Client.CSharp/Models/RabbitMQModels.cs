namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Requisição para configurar RabbitMQ
/// </summary>
public class SetRabbitMQRequest
{
    /// <summary>
    /// Indica se está habilitado
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// URI de conexão RabbitMQ (amqp://user:password@host:port/vhost)
    /// </summary>
    public string Uri { get; set; } = string.Empty;

    /// <summary>
    /// Nome da exchange
    /// </summary>
    public string Exchange { get; set; } = string.Empty;

    /// <summary>
    /// Tipo da exchange (direct, topic, fanout, headers)
    /// </summary>
    public string ExchangeType { get; set; } = "topic";

    /// <summary>
    /// Exchange durável (sobrevive a reinicializações do servidor)
    /// </summary>
    public bool ExchangeDurable { get; set; } = true;

    /// <summary>
    /// Auto-deletar exchange quando não há mais filas vinculadas
    /// </summary>
    public bool ExchangeAutoDelete { get; set; } = false;

    /// <summary>
    /// Eventos a serem enviados para o RabbitMQ
    /// </summary>
    public string[] Events { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Routing key padrão para mensagens
    /// </summary>
    public string DefaultRoutingKey { get; set; } = "evolution.events";

    /// <summary>
    /// Nome da fila (opcional, se não fornecida será gerada automaticamente)
    /// </summary>
    public string? QueueName { get; set; }

    /// <summary>
    /// Fila durável
    /// </summary>
    public bool QueueDurable { get; set; } = true;

    /// <summary>
    /// Fila exclusiva (apenas uma conexão pode usar)
    /// </summary>
    public bool QueueExclusive { get; set; } = false;

    /// <summary>
    /// Auto-deletar fila quando não há mais consumidores
    /// </summary>
    public bool QueueAutoDelete { get; set; } = false;

    /// <summary>
    /// TTL (Time To Live) das mensagens em milissegundos
    /// </summary>
    public int? MessageTTL { get; set; }

    /// <summary>
    /// Máximo de mensagens na fila
    /// </summary>
    public int? MaxLength { get; set; }

    /// <summary>
    /// Máximo de bytes na fila
    /// </summary>
    public long? MaxLengthBytes { get; set; }

    /// <summary>
    /// Política de overflow (drop-head, reject-publish, reject-publish-dlx)
    /// </summary>
    public string? OverflowBehaviour { get; set; }

    /// <summary>
    /// Dead Letter Exchange
    /// </summary>
    public string? DeadLetterExchange { get; set; }

    /// <summary>
    /// Dead Letter Routing Key
    /// </summary>
    public string? DeadLetterRoutingKey { get; set; }

    /// <summary>
    /// Timeout de conexão em segundos
    /// </summary>
    public int ConnectionTimeout { get; set; } = 30;

    /// <summary>
    /// Intervalo de heartbeat em segundos
    /// </summary>
    public int HeartbeatInterval { get; set; } = 60;

    /// <summary>
    /// Reconectar automaticamente em caso de falha
    /// </summary>
    public bool AutoReconnect { get; set; } = true;

    /// <summary>
    /// Máximo de tentativas de reconexão
    /// </summary>
    public int MaxReconnectAttempts { get; set; } = 5;

    /// <summary>
    /// Intervalo entre tentativas de reconexão em segundos
    /// </summary>
    public int ReconnectInterval { get; set; } = 5;
}

/// <summary>
/// Resposta da configuração do RabbitMQ
/// </summary>
public class RabbitMQResponse
{
    /// <summary>
    /// Dados do RabbitMQ configurado
    /// </summary>
    public RabbitMQData? RabbitMQ { get; set; }

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
/// Dados do RabbitMQ
/// </summary>
public class RabbitMQData
{
    /// <summary>
    /// Nome da instância
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Configuração do RabbitMQ
    /// </summary>
    public RabbitMQConfig? RabbitMQ { get; set; }

    /// <summary>
    /// Status da conexão
    /// </summary>
    public RabbitMQConnectionStatus? ConnectionStatus { get; set; }

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
/// Configuração do RabbitMQ
/// </summary>
public class RabbitMQConfig
{
    /// <summary>
    /// Indica se está habilitado
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// URI de conexão (mascarada para segurança)
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>
    /// Nome da exchange
    /// </summary>
    public string? Exchange { get; set; }

    /// <summary>
    /// Tipo da exchange
    /// </summary>
    public string? ExchangeType { get; set; }

    /// <summary>
    /// Exchange durável
    /// </summary>
    public bool? ExchangeDurable { get; set; }

    /// <summary>
    /// Auto-deletar exchange
    /// </summary>
    public bool? ExchangeAutoDelete { get; set; }

    /// <summary>
    /// Eventos configurados
    /// </summary>
    public string[]? Events { get; set; }

    /// <summary>
    /// Routing key padrão
    /// </summary>
    public string? DefaultRoutingKey { get; set; }

    /// <summary>
    /// Nome da fila
    /// </summary>
    public string? QueueName { get; set; }

    /// <summary>
    /// Fila durável
    /// </summary>
    public bool? QueueDurable { get; set; }

    /// <summary>
    /// Fila exclusiva
    /// </summary>
    public bool? QueueExclusive { get; set; }

    /// <summary>
    /// Auto-deletar fila
    /// </summary>
    public bool? QueueAutoDelete { get; set; }

    /// <summary>
    /// TTL das mensagens
    /// </summary>
    public int? MessageTTL { get; set; }

    /// <summary>
    /// Máximo de mensagens
    /// </summary>
    public int? MaxLength { get; set; }

    /// <summary>
    /// Máximo de bytes
    /// </summary>
    public long? MaxLengthBytes { get; set; }

    /// <summary>
    /// Comportamento de overflow
    /// </summary>
    public string? OverflowBehaviour { get; set; }

    /// <summary>
    /// Dead Letter Exchange
    /// </summary>
    public string? DeadLetterExchange { get; set; }

    /// <summary>
    /// Dead Letter Routing Key
    /// </summary>
    public string? DeadLetterRoutingKey { get; set; }

    /// <summary>
    /// Timeout de conexão
    /// </summary>
    public int? ConnectionTimeout { get; set; }

    /// <summary>
    /// Intervalo de heartbeat
    /// </summary>
    public int? HeartbeatInterval { get; set; }

    /// <summary>
    /// Reconexão automática
    /// </summary>
    public bool? AutoReconnect { get; set; }

    /// <summary>
    /// Máximo de tentativas de reconexão
    /// </summary>
    public int? MaxReconnectAttempts { get; set; }

    /// <summary>
    /// Intervalo de reconexão
    /// </summary>
    public int? ReconnectInterval { get; set; }
}

/// <summary>
/// Status da conexão RabbitMQ
/// </summary>
public class RabbitMQConnectionStatus
{
    /// <summary>
    /// Estado da conexão (connected, disconnected, connecting, error)
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Indica se está conectado
    /// </summary>
    public bool IsConnected { get; set; }

    /// <summary>
    /// Última conexão bem-sucedida
    /// </summary>
    public DateTime? LastConnected { get; set; }

    /// <summary>
    /// Última desconexão
    /// </summary>
    public DateTime? LastDisconnected { get; set; }

    /// <summary>
    /// Número de reconexões realizadas
    /// </summary>
    public int ReconnectCount { get; set; }

    /// <summary>
    /// Último erro de conexão
    /// </summary>
    public string? LastError { get; set; }

    /// <summary>
    /// Host ativo
    /// </summary>
    public string? ActiveHost { get; set; }

    /// <summary>
    /// Porta ativa
    /// </summary>
    public int? ActivePort { get; set; }

    /// <summary>
    /// Virtual host ativo
    /// </summary>
    public string? ActiveVirtualHost { get; set; }

    /// <summary>
    /// Versão do servidor RabbitMQ
    /// </summary>
    public string? ServerVersion { get; set; }
}

/// <summary>
/// Estatísticas do RabbitMQ
/// </summary>
public class RabbitMQStats
{
    /// <summary>
    /// Total de mensagens publicadas
    /// </summary>
    public long MessagesPublished { get; set; }

    /// <summary>
    /// Total de mensagens com falha
    /// </summary>
    public long MessagesFailed { get; set; }

    /// <summary>
    /// Total de eventos processados
    /// </summary>
    public long EventsProcessed { get; set; }

    /// <summary>
    /// Mensagens na fila
    /// </summary>
    public int? QueueMessageCount { get; set; }

    /// <summary>
    /// Consumidores ativos na fila
    /// </summary>
    public int? QueueConsumerCount { get; set; }

    /// <summary>
    /// Taxa de publicação (mensagens/segundo)
    /// </summary>
    public double PublishRate { get; set; }

    /// <summary>
    /// Taxa de sucesso de publicação (0-100)
    /// </summary>
    public double SuccessRate { get; set; }

    /// <summary>
    /// Última atualização das estatísticas
    /// </summary>
    public DateTime? LastUpdated { get; set; }

    /// <summary>
    /// Bytes enviados
    /// </summary>
    public long BytesSent { get; set; }

    /// <summary>
    /// Tamanho médio das mensagens em bytes
    /// </summary>
    public double AverageMessageSize { get; set; }
}

/// <summary>
/// Resposta das estatísticas do RabbitMQ
/// </summary>
public class RabbitMQStatsResponse
{
    /// <summary>
    /// Estatísticas do RabbitMQ
    /// </summary>
    public RabbitMQStats? Stats { get; set; }

    /// <summary>
    /// Status da conexão
    /// </summary>
    public RabbitMQConnectionStatus? ConnectionStatus { get; set; }

    /// <summary>
    /// Nome da instância
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;
}

/// <summary>
/// Eventos disponíveis para RabbitMQ
/// </summary>
public static class RabbitMQEvents
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
    /// Evento de presença
    /// </summary>
    public const string Presence = "presence.update";

    /// <summary>
    /// Evento de chamada
    /// </summary>
    public const string Call = "call.received";

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
        Presence,
        Call,
        InstanceConnect,
        InstanceDisconnect
    };
}
