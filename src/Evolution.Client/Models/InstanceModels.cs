namespace Evolution.Client.Modules;

/// <summary>
/// Requisição para criação de instância
/// </summary>
public class CreateInstanceRequest
{
    /// <summary>
    /// Nome da instância
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Token de integração
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// Indica se deve gerar QR Code
    /// </summary>
    public bool? Qrcode { get; set; }

    /// <summary>
    /// Número de telefone (com código do país)
    /// </summary>
    public string? Number { get; set; }

    /// <summary>
    /// Tipo de integração
    /// </summary>
    public string? Integration { get; set; } = "WHATSAPP-BAILEYS";

    /// <summary>
    /// Rejeitar chamadas automaticamente
    /// </summary>
    public bool? RejectCall { get; set; }

    /// <summary>
    /// Mensagem para chamadas rejeitadas
    /// </summary>
    public string? MsgCall { get; set; }

    /// <summary>
    /// Ignorar grupos
    /// </summary>
    public bool? GroupsIgnore { get; set; }

    /// <summary>
    /// Sempre online
    /// </summary>
    public bool? AlwaysOnline { get; set; }

    /// <summary>
    /// Ler mensagens automaticamente
    /// </summary>
    public bool? ReadMessages { get; set; }

    /// <summary>
    /// Ler status automaticamente
    /// </summary>
    public bool? ReadStatus { get; set; }

    /// <summary>
    /// Sincronizar histórico completo
    /// </summary>
    public bool? SyncFullHistory { get; set; }

    /// <summary>
    /// Configurações de proxy
    /// </summary>
    public string? ProxyHost { get; set; }
    public string? ProxyPort { get; set; }
    public string? ProxyProtocol { get; set; }
    public string? ProxyUsername { get; set; }
    public string? ProxyPassword { get; set; }

    /// <summary>
    /// Configurações do webhook
    /// </summary>
    public WebhookSettings? Webhook { get; set; }

    /// <summary>
    /// Configurações do RabbitMQ
    /// </summary>
    public RabbitMqSettings? Rabbitmq { get; set; }

    /// <summary>
    /// Configurações do SQS
    /// </summary>
    public SqsSettings? Sqs { get; set; }

    /// <summary>
    /// Configurações do Chatwoot
    /// </summary>
    public int? ChatwootAccountId { get; set; }
    public string? ChatwootToken { get; set; }
    public string? ChatwootUrl { get; set; }
    public bool? ChatwootSignMsg { get; set; }
    public bool? ChatwootReopenConversation { get; set; }
    public bool? ChatwootConversationPending { get; set; }
    public bool? ChatwootImportContacts { get; set; }
    public string? ChatwootNameInbox { get; set; }
    public bool? ChatwootMergeBrazilContacts { get; set; }
    public bool? ChatwootImportMessages { get; set; }
    public int? ChatwootDaysLimitImportMessages { get; set; }
    public string? ChatwootOrganization { get; set; }
    public string? ChatwootLogo { get; set; }
}

/// <summary>
/// Configurações do webhook
/// </summary>
public class WebhookSettings
{
    /// <summary>
    /// URL do webhook
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Enviar por eventos
    /// </summary>
    public bool? ByEvents { get; set; }

    /// <summary>
    /// Enviar em base64
    /// </summary>
    public bool? Base64 { get; set; }

    /// <summary>
    /// Headers customizados
    /// </summary>
    public Dictionary<string, string>? Headers { get; set; }

    /// <summary>
    /// Eventos a serem enviados
    /// </summary>
    public string[]? Events { get; set; }
}

/// <summary>
/// Configurações do RabbitMQ
/// </summary>
public class RabbitMqSettings
{
    /// <summary>
    /// Indica se está habilitado
    /// </summary>
    public bool? Enabled { get; set; }

    /// <summary>
    /// Eventos a serem enviados
    /// </summary>
    public string[]? Events { get; set; }
}

/// <summary>
/// Configurações do SQS
/// </summary>
public class SqsSettings
{
    /// <summary>
    /// Indica se está habilitado
    /// </summary>
    public bool? Enabled { get; set; }

    /// <summary>
    /// Eventos a serem enviados
    /// </summary>
    public string[]? Events { get; set; }
}

/// <summary>
/// Resposta da criação de instância
/// </summary>
public class CreateInstanceResponse
{
    /// <summary>
    /// Dados da instância
    /// </summary>
    public InstanceData? Instance { get; set; }

    /// <summary>
    /// Hash da API
    /// </summary>
    public HashData? Hash { get; set; }

    /// <summary>
    /// Configurações da instância
    /// </summary>
    public InstanceSettings? Settings { get; set; }
}

/// <summary>
/// Dados da instância
/// </summary>
public class InstanceData
{
    /// <summary>
    /// Nome da instância
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// ID da instância
    /// </summary>
    public string InstanceId { get; set; } = string.Empty;

    /// <summary>
    /// Webhook do WhatsApp Business
    /// </summary>
    public string? WebhookWaBusiness { get; set; }

    /// <summary>
    /// Token de acesso do WhatsApp Business
    /// </summary>
    public string? AccessTokenWaBusiness { get; set; }

    /// <summary>
    /// Status da instância
    /// </summary>
    public string Status { get; set; } = string.Empty;
}

/// <summary>
/// Hash da API
/// </summary>
public class HashData
{
    /// <summary>
    /// Chave da API
    /// </summary>
    public string Apikey { get; set; } = string.Empty;
}

/// <summary>
/// Configurações da instância
/// </summary>
public class InstanceSettings
{
    /// <summary>
    /// Rejeitar chamadas
    /// </summary>
    public bool RejectCall { get; set; }

    /// <summary>
    /// Mensagem para chamadas
    /// </summary>
    public string MsgCall { get; set; } = string.Empty;

    /// <summary>
    /// Ignorar grupos
    /// </summary>
    public bool GroupsIgnore { get; set; }

    /// <summary>
    /// Sempre online
    /// </summary>
    public bool AlwaysOnline { get; set; }

    /// <summary>
    /// Ler mensagens
    /// </summary>
    public bool ReadMessages { get; set; }

    /// <summary>
    /// Ler status
    /// </summary>
    public bool ReadStatus { get; set; }

    /// <summary>
    /// Sincronizar histórico completo
    /// </summary>
    public bool SyncFullHistory { get; set; }
}

/// <summary>
/// Informações da instância
/// </summary>
public class InstanceInfo
{
    /// <summary>
    /// Dados da instância
    /// </summary>
    public InstanceData? Instance { get; set; }

    /// <summary>
    /// Hash da API
    /// </summary>
    public HashData? Hash { get; set; }

    /// <summary>
    /// Configurações da instância
    /// </summary>
    public InstanceSettings? Settings { get; set; }

    /// <summary>
    /// Configurações do webhook
    /// </summary>
    public WebhookConfig? Webhook { get; set; }

    /// <summary>
    /// Configurações do RabbitMQ
    /// </summary>
    public RabbitMqConfig? Rabbitmq { get; set; }

    /// <summary>
    /// Configurações do SQS
    /// </summary>
    public SqsConfig? Sqs { get; set; }

    /// <summary>
    /// Configurações do proxy
    /// </summary>
    public ProxyConfig? Proxy { get; set; }

    /// <summary>
    /// Configurações do Chatwoot
    /// </summary>
    public ChatwootConfig? Chatwoot { get; set; }
}

/// <summary>
/// Configurações do proxy
/// </summary>
public class ProxyConfig
{
    /// <summary>
    /// Host do proxy
    /// </summary>
    public string? Host { get; set; }

    /// <summary>
    /// Porta do proxy
    /// </summary>
    public string? Port { get; set; }

    /// <summary>
    /// Protocolo do proxy
    /// </summary>
    public string? Protocol { get; set; }

    /// <summary>
    /// Usuário do proxy
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Senha do proxy
    /// </summary>
    public string? Password { get; set; }
}

/// <summary>
/// Configurações do RabbitMQ
/// </summary>
public class RabbitMqConfig
{
    /// <summary>
    /// Indica se está habilitado
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Eventos configurados
    /// </summary>
    public string[]? Events { get; set; }
}

/// <summary>
/// Configurações do SQS
/// </summary>
public class SqsConfig
{
    /// <summary>
    /// Indica se está habilitado
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Eventos configurados
    /// </summary>
    public string[]? Events { get; set; }
}

/// <summary>
/// Configurações do Chatwoot
/// </summary>
public class ChatwootConfig
{
    /// <summary>
    /// ID da conta
    /// </summary>
    public int? AccountId { get; set; }

    /// <summary>
    /// Token de acesso
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// URL do Chatwoot
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Assinar mensagens
    /// </summary>
    public bool? SignMsg { get; set; }

    /// <summary>
    /// Reabrir conversas
    /// </summary>
    public bool? ReopenConversation { get; set; }

    /// <summary>
    /// Conversa pendente
    /// </summary>
    public bool? ConversationPending { get; set; }

    /// <summary>
    /// Importar contatos
    /// </summary>
    public bool? ImportContacts { get; set; }

    /// <summary>
    /// Nome da caixa de entrada
    /// </summary>
    public string? NameInbox { get; set; }

    /// <summary>
    /// Mesclar contatos do Brasil
    /// </summary>
    public bool? MergeBrazilContacts { get; set; }

    /// <summary>
    /// Importar mensagens
    /// </summary>
    public bool? ImportMessages { get; set; }

    /// <summary>
    /// Limite de dias para importar mensagens
    /// </summary>
    public int? DaysLimitImportMessages { get; set; }

    /// <summary>
    /// Organização
    /// </summary>
    public string? Organization { get; set; }

    /// <summary>
    /// Logo
    /// </summary>
    public string? Logo { get; set; }
}

/// <summary>
/// Resposta de conexão da instância
/// </summary>
public class ConnectInstanceResponse
{
    /// <summary>
    /// Base64 do QR Code
    /// </summary>
    public string Base64 { get; set; } = string.Empty;

    /// <summary>
    /// Código do QR Code
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Contagem do QR Code
    /// </summary>
    public int Count { get; set; }
}

/// <summary>
/// Status de conexão
/// </summary>
public class ConnectionStatus
{
    /// <summary>
    /// Nome da instância
    /// </summary>
    public string Instance { get; set; } = string.Empty;

    /// <summary>
    /// Estado da conexão
    /// </summary>
    public string State { get; set; } = string.Empty;
}

/// <summary>
/// Requisição para definir presença
/// </summary>
public class SetPresenceRequest
{
    /// <summary>
    /// Presença (available, unavailable, composing, recording, paused)
    /// </summary>
    public string Presence { get; set; } = string.Empty;
}

/// <summary>
/// Resposta de definir presença
/// </summary>
public class SetPresenceResponse
{
    /// <summary>
    /// Presença definida
    /// </summary>
    public string Presence { get; set; } = string.Empty;
}
