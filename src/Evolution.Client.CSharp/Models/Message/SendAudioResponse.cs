using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message;

/// <summary>
/// Representa a resposta do envio de áudio.
/// </summary>
public class SendAudioResponse
{
    /// <summary>
    /// Chave da mensagem que identifica a mensagem no chat.
    /// </summary>
    [JsonPropertyName("key")]
    public MessageKey Key { get; set; } = new();

    /// <summary>
    /// Nome do remetente (push name).
    /// </summary>
    [JsonPropertyName("pushName")]
    public string PushName { get; set; } = string.Empty;

    /// <summary>
    /// Status da mensagem (sent, received, pending).
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Conteúdo da mensagem enviada.
    /// </summary>
    [JsonPropertyName("message")]
    public SentMessageContent Message { get; set; } = new();

    /// <summary>
    /// Informações de contexto da mensagem.
    /// </summary>
    [JsonPropertyName("contextInfo")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? ContextInfo { get; set; }

    /// <summary>
    /// Tipo da mensagem.
    /// </summary>
    [JsonPropertyName("messageType")]
    public string MessageType { get; set; } = string.Empty;

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
    /// Origem da mensagem.
    /// </summary>
    [JsonPropertyName("source")]
    public string Source { get; set; } = string.Empty;
}