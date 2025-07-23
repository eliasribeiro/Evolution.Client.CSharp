using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message;

/// <summary>
/// Representa a resposta do envio de uma mensagem de texto.
/// </summary>
public class SendTextResponse
{
    /// <summary>
    /// Chave da mensagem enviada.
    /// </summary>
    [JsonPropertyName("key")]
    public MessageKey Key { get; set; } = new();

    /// <summary>
    /// Conteúdo da mensagem enviada.
    /// </summary>
    [JsonPropertyName("message")]
    public SentMessageContent Message { get; set; } = new();

    /// <summary>
    /// Timestamp da mensagem.
    /// </summary>
    [JsonPropertyName("messageTimestamp")]
    public string MessageTimestamp { get; set; } = string.Empty;

    /// <summary>
    /// Status da mensagem.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}

/// <summary>
/// Representa a chave de uma mensagem enviada.
/// </summary>
public class MessageKey
{
    /// <summary>
    /// JID remoto (destinatário) da mensagem.
    /// </summary>
    [JsonPropertyName("remoteJid")]
    public string RemoteJid { get; set; } = string.Empty;

    /// <summary>
    /// Indica se a mensagem foi enviada por mim.
    /// </summary>
    [JsonPropertyName("fromMe")]
    public bool FromMe { get; set; }

    /// <summary>
    /// ID único da mensagem.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
}

/// <summary>
/// Representa o conteúdo de uma mensagem enviada.
/// </summary>
public class SentMessageContent
{
    /// <summary>
    /// Mensagem de texto estendida.
    /// </summary>
    [JsonPropertyName("extendedTextMessage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ExtendedTextMessage? ExtendedTextMessage { get; set; }

    /// <summary>
    /// Conversa simples (para mensagens de texto básicas).
    /// </summary>
    [JsonPropertyName("conversation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Conversation { get; set; }
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
}
