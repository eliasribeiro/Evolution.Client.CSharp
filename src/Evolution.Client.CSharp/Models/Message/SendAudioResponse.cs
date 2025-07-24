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
    /// Conteúdo da mensagem enviada.
    /// </summary>
    [JsonPropertyName("message")]
    public SentMessageContent Message { get; set; } = new();

    /// <summary>
    /// Timestamp da mensagem representado como string.
    /// </summary>
    [JsonPropertyName("messageTimestamp")]
    public string MessageTimestamp { get; set; } = string.Empty;

    /// <summary>
    /// Status da mensagem (sent, received, pending).
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}