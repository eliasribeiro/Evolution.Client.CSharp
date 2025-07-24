using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Chat;

/// <summary>
/// Representa a resposta da operação de atualizar mensagem.
/// </summary>
public class UpdateMessageResponse
{
    /// <summary>
    /// Chave da mensagem atualizada.
    /// </summary>
    [JsonPropertyName("key")]
    public UpdateMessageResponseKey Key { get; set; } = new();

    /// <summary>
    /// Conteúdo da mensagem atualizada.
    /// </summary>
    [JsonPropertyName("message")]
    public UpdatedMessageContent Message { get; set; } = new();

    /// <summary>
    /// Timestamp da atualização.
    /// </summary>
    [JsonPropertyName("messageTimestamp")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? MessageTimestamp { get; set; }

    /// <summary>
    /// Status da operação.
    /// </summary>
    [JsonPropertyName("status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Status { get; set; }
}

/// <summary>
/// Representa a chave da mensagem atualizada na resposta.
/// </summary>
public class UpdateMessageResponseKey
{
    /// <summary>
    /// JID remoto do chat.
    /// </summary>
    [JsonPropertyName("remoteJid")]
    public string RemoteJid { get; set; } = string.Empty;

    /// <summary>
    /// Indica se a mensagem foi enviada pelo proprietário da instância.
    /// </summary>
    [JsonPropertyName("fromMe")]
    public bool FromMe { get; set; }

    /// <summary>
    /// ID da mensagem.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Participante (para grupos).
    /// </summary>
    [JsonPropertyName("participant")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Participant { get; set; }
}

/// <summary>
/// Representa o conteúdo da mensagem atualizada.
/// </summary>
public class UpdatedMessageContent
{
    /// <summary>
    /// Mensagem de texto atualizada.
    /// </summary>
    [JsonPropertyName("conversation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Conversation { get; set; }

    /// <summary>
    /// Mensagem de texto estendida (para mensagens longas).
    /// </summary>
    [JsonPropertyName("extendedTextMessage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ExtendedTextMessage? ExtendedTextMessage { get; set; }

    /// <summary>
    /// Contexto da mensagem (para respostas).
    /// </summary>
    [JsonPropertyName("messageContextInfo")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MessageContextInfo? MessageContextInfo { get; set; }
}

/// <summary>
/// Representa uma mensagem de texto estendida.
/// </summary>
public class ExtendedTextMessage
{
    /// <summary>
    /// Texto da mensagem.
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Informações de contexto da mensagem.
    /// </summary>
    [JsonPropertyName("contextInfo")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MessageContextInfo? ContextInfo { get; set; }
}