using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Chat;

/// <summary>
/// Representa a resposta da busca de mensagens.
/// </summary>
public class FindMessagesResponse
{
    /// <summary>
    /// Dados das mensagens encontradas.
    /// </summary>
    [JsonPropertyName("messages")]
    public MessagesData Messages { get; set; } = new();
}

/// <summary>
/// Representa os dados das mensagens com informações de paginação.
/// </summary>
public class MessagesData
{
    /// <summary>
    /// Total de mensagens encontradas.
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }

    /// <summary>
    /// Total de páginas disponíveis.
    /// </summary>
    [JsonPropertyName("pages")]
    public int Pages { get; set; }

    /// <summary>
    /// Página atual.
    /// </summary>
    [JsonPropertyName("currentPage")]
    public int CurrentPage { get; set; }

    /// <summary>
    /// Lista de mensagens encontradas.
    /// </summary>
    [JsonPropertyName("records")]
    public List<MessageRecord> Records { get; set; } = new();
}

/// <summary>
/// Representa uma mensagem encontrada.
/// </summary>
public class MessageRecord
{
    /// <summary>
    /// ID único da mensagem.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Chave da mensagem.
    /// </summary>
    [JsonPropertyName("key")]
    public MessageRecordKey Key { get; set; } = new();

    /// <summary>
    /// Nome do remetente.
    /// </summary>
    [JsonPropertyName("pushName")]
    public string PushName { get; set; } = string.Empty;

    /// <summary>
    /// Tipo da mensagem.
    /// </summary>
    [JsonPropertyName("messageType")]
    public string MessageType { get; set; } = string.Empty;

    /// <summary>
    /// Conteúdo da mensagem.
    /// </summary>
    [JsonPropertyName("message")]
    public MessageContent Message { get; set; } = new();

    /// <summary>
    /// Timestamp da mensagem.
    /// </summary>
    [JsonPropertyName("messageTimestamp")]
    public long MessageTimestamp { get; set; }

    /// <summary>
    /// ID da instância.
    /// </summary>
    [JsonPropertyName("instanceId")]
    public string InstanceId { get; set; } = string.Empty;

    /// <summary>
    /// Fonte da mensagem (android, ios, etc.).
    /// </summary>
    [JsonPropertyName("source")]
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Informações de contexto da mensagem.
    /// </summary>
    [JsonPropertyName("contextInfo")]
    public MessageContextInfo? ContextInfo { get; set; }

    /// <summary>
    /// Atualizações da mensagem.
    /// </summary>
    [JsonPropertyName("MessageUpdate")]
    public List<object> MessageUpdate { get; set; } = new();
}

/// <summary>
/// Representa a chave de uma mensagem no registro.
/// </summary>
public class MessageRecordKey
{
    /// <summary>
    /// ID da mensagem.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Indica se a mensagem foi enviada por mim.
    /// </summary>
    [JsonPropertyName("fromMe")]
    public bool FromMe { get; set; }

    /// <summary>
    /// JID remoto (contato ou grupo) da mensagem.
    /// </summary>
    [JsonPropertyName("remoteJid")]
    public string RemoteJid { get; set; } = string.Empty;
}

/// <summary>
/// Representa o conteúdo de uma mensagem.
/// </summary>
public class MessageContent
{
    /// <summary>
    /// Texto da conversa (para mensagens de texto).
    /// </summary>
    [JsonPropertyName("conversation")]
    public string? Conversation { get; set; }

    /// <summary>
    /// Informações de contexto da mensagem.
    /// </summary>
    [JsonPropertyName("messageContextInfo")]
    public MessageContextInfo? MessageContextInfo { get; set; }
}

/// <summary>
/// Representa informações de contexto de uma mensagem.
/// </summary>
public class MessageContextInfo
{
    /// <summary>
    /// Segredo da mensagem.
    /// </summary>
    [JsonPropertyName("messageSecret")]
    public string? MessageSecret { get; set; }

    /// <summary>
    /// Metadados da lista de dispositivos.
    /// </summary>
    [JsonPropertyName("deviceListMetadata")]
    public DeviceListMetadata? DeviceListMetadata { get; set; }

    /// <summary>
    /// Versão dos metadados da lista de dispositivos.
    /// </summary>
    [JsonPropertyName("deviceListMetadataVersion")]
    public int? DeviceListMetadataVersion { get; set; }

    /// <summary>
    /// Tempo de expiração da mensagem.
    /// </summary>
    [JsonPropertyName("expiration")]
    public int? Expiration { get; set; }

    /// <summary>
    /// Modo de desaparecimento da mensagem.
    /// </summary>
    [JsonPropertyName("disappearingMode")]
    public DisappearingMode? DisappearingMode { get; set; }
}

/// <summary>
/// Representa metadados da lista de dispositivos.
/// </summary>
public class DeviceListMetadata
{
    /// <summary>
    /// Hash da chave do remetente.
    /// </summary>
    [JsonPropertyName("senderKeyHash")]
    public string? SenderKeyHash { get; set; }

    /// <summary>
    /// Timestamp do remetente.
    /// </summary>
    [JsonPropertyName("senderTimestamp")]
    public string? SenderTimestamp { get; set; }

    /// <summary>
    /// Hash da chave do destinatário.
    /// </summary>
    [JsonPropertyName("recipientKeyHash")]
    public string? RecipientKeyHash { get; set; }

    /// <summary>
    /// Timestamp do destinatário.
    /// </summary>
    [JsonPropertyName("recipientTimestamp")]
    public string? RecipientTimestamp { get; set; }
}

/// <summary>
/// Representa o modo de desaparecimento de uma mensagem.
/// </summary>
public class DisappearingMode
{
    /// <summary>
    /// Gatilho do modo de desaparecimento.
    /// </summary>
    [JsonPropertyName("trigger")]
    public string? Trigger { get; set; }

    /// <summary>
    /// Iniciador do modo de desaparecimento.
    /// </summary>
    [JsonPropertyName("initiator")]
    public string? Initiator { get; set; }
}