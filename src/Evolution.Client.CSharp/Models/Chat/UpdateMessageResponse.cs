using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Chat;

/// <summary>
/// Representa a resposta da atualização de uma mensagem.
/// </summary>
public class UpdateMessageResponse
{
    /// <summary>
    /// Chave da mensagem atualizada.
    /// </summary>
    [JsonPropertyName("key")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public UpdatedMessageKey? Key { get; set; }

    /// <summary>
    /// Conteúdo da mensagem atualizada.
    /// </summary>
    [JsonPropertyName("message")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public UpdatedMessageContent? Message { get; set; }

    /// <summary>
    /// Timestamp da mensagem atualizada.
    /// </summary>
    [JsonPropertyName("messageTimestamp")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? MessageTimestamp { get; set; }

    /// <summary>
    /// Status da mensagem atualizada.
    /// </summary>
    [JsonPropertyName("status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Status { get; set; }
}

/// <summary>
/// Representa a chave da mensagem atualizada.
/// </summary>
public class UpdatedMessageKey
{
    /// <summary>
    /// JID remoto do chat (contato ou grupo).
    /// </summary>
    [JsonPropertyName("remoteJid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RemoteJid { get; set; }

    /// <summary>
    /// Indica se a mensagem foi enviada pela instância proprietária.
    /// </summary>
    [JsonPropertyName("fromMe")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? FromMe { get; set; }

    /// <summary>
    /// ID único da mensagem.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; set; }
}

/// <summary>
/// Representa o conteúdo da mensagem atualizada.
/// </summary>
public class UpdatedMessageContent
{
    /// <summary>
    /// Mensagem de texto estendida (para mensagens de texto).
    /// </summary>
    [JsonPropertyName("extendedTextMessage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ExtendedTextMessage? ExtendedTextMessage { get; set; }

    /// <summary>
    /// Conversação simples (para mensagens de texto simples).
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
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; set; }
}