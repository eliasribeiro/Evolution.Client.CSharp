using System.Text.Json.Serialization;
using Evolution.Client.CSharp.Models.Chat;

namespace Evolution.Client.CSharp.Models.Message;

/// <summary>
/// Representa a resposta do envio de reação.
/// </summary>
public class SendReactionResponse
{
    /// <summary>
    /// A chave da mensagem, que identifica a mensagem no chat.
    /// </summary>
    [JsonPropertyName("key")]
    public MessageKey Key { get; set; } = new();

    /// <summary>
    /// O conteúdo da mensagem.
    /// </summary>
    [JsonPropertyName("message")]
    public MessageContent Message { get; set; } = new();

    /// <summary>
    /// O timestamp da mensagem, representado como string.
    /// </summary>
    [JsonPropertyName("messageTimestamp")]
    public string MessageTimestamp { get; set; } = string.Empty;

    /// <summary>
    /// O status da mensagem, como enviado, recebido ou pendente.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}