namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Requisição para configurar WebSocket
/// </summary>
public class SetWebSocketRequest
{
    /// <summary>
    /// Indica se está habilitado
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Eventos a serem enviados via WebSocket
    /// </summary>
    public string[] Events { get; set; } = Array.Empty<string>();

    /// <summary>
    /// URL do servidor WebSocket (opcional, se não fornecida usa a padrão)
    /// </summary>
    public string? WebSocketUrl { get; set; }

    /// <summary>
    /// Headers customizados para conexão WebSocket
    /// </summary>
    public Dictionary<string, string>? Headers { get; set; }

    /// <summary>
    /// Timeout para conexão em segundos
    /// </summary>
    public int ConnectionTimeout { get; set; } = 30;

    /// <summary>
    /// Intervalo de ping em segundos
    /// </summary>
    public int PingInterval { get; set; } = 25;

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
/// Resposta da configuração do WebSocket
/// </summary>
public class WebSocketResponse
{
    /// <summary>
    /// Dados do WebSocket configurado
    /// </summary>
    public WebSocketData? WebSocket { get; set; }

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
/// Dados do WebSocket
/// </summary>
public class WebSocketData
{
    /// <summary>
    /// Nome da instância
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Configuração do WebSocket
    /// </summary>
    public WebSocketConfig? WebSocket { get; set; }

    /// <summary>
    /// Status da conexão
    /// </summary>
    public WebSocketConnectionStatus? ConnectionStatus { get; set; }

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
/// Configuração do WebSocket
/// </summary>
public class WebSocketConfig
{
    /// <summary>
    /// Indica se está habilitado
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Eventos configurados
    /// </summary>
    public string[]? Events { get; set; }

    /// <summary>
    /// URL do WebSocket
    /// </summary>
    public string? WebSocketUrl { get; set; }

    /// <summary>
    /// Headers customizados
    /// </summary>
    public Dictionary<string, string>? Headers { get; set; }

    /// <summary>
    /// Timeout de conexão
    /// </summary>
    public int? ConnectionTimeout { get; set; }

    /// <summary>
    /// Intervalo de ping
    /// </summary>
    public int? PingInterval { get; set; }

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
/// Status da conexão WebSocket
/// </summary>
public class WebSocketConnectionStatus
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
    /// Latência da conexão em milissegundos
    /// </summary>
    public int? Latency { get; set; }
}

/// <summary>
/// Estatísticas do WebSocket
/// </summary>
public class WebSocketStats
{
    /// <summary>
    /// Total de mensagens enviadas
    /// </summary>
    public long MessagesSent { get; set; }

    /// <summary>
    /// Total de mensagens recebidas
    /// </summary>
    public long MessagesReceived { get; set; }

    /// <summary>
    /// Total de eventos processados
    /// </summary>
    public long EventsProcessed { get; set; }

    /// <summary>
    /// Total de erros de conexão
    /// </summary>
    public int ConnectionErrors { get; set; }

    /// <summary>
    /// Tempo total conectado em segundos
    /// </summary>
    public long TotalConnectedTime { get; set; }

    /// <summary>
    /// Uptime da conexão atual em segundos
    /// </summary>
    public long CurrentUptime { get; set; }

    /// <summary>
    /// Bytes enviados
    /// </summary>
    public long BytesSent { get; set; }

    /// <summary>
    /// Bytes recebidos
    /// </summary>
    public long BytesReceived { get; set; }
}

/// <summary>
/// Resposta das estatísticas do WebSocket
/// </summary>
public class WebSocketStatsResponse
{
    /// <summary>
    /// Estatísticas do WebSocket
    /// </summary>
    public WebSocketStats? Stats { get; set; }

    /// <summary>
    /// Status da conexão
    /// </summary>
    public WebSocketConnectionStatus? ConnectionStatus { get; set; }

    /// <summary>
    /// Nome da instância
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;
}

/// <summary>
/// Eventos disponíveis para WebSocket
/// </summary>
public static class WebSocketEvents
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
    /// Evento de QR Code
    /// </summary>
    public const string QrCode = "qrcode.updated";

    /// <summary>
    /// Evento de contato atualizado
    /// </summary>
    public const string ContactUpdate = "contact.update";

    /// <summary>
    /// Evento de grupo atualizado
    /// </summary>
    public const string GroupUpdate = "group.update";

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
        InstanceDisconnect,
        QrCode,
        ContactUpdate,
        GroupUpdate
    };
}
