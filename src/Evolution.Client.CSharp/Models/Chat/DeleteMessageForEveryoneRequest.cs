using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Chat;

/// <summary>
/// Representa a requisição para deletar uma mensagem para todos.
/// </summary>
public class DeleteMessageForEveryoneRequest
{
    /// <summary>
    /// ID da mensagem a ser deletada.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// JID remoto (contato ou grupo) da mensagem.
    /// </summary>
    [JsonPropertyName("remoteJid")]
    public string RemoteJid { get; set; } = string.Empty;

    /// <summary>
    /// Indica se a mensagem foi enviada por mim.
    /// </summary>
    [JsonPropertyName("fromMe")]
    public bool FromMe { get; set; }

    /// <summary>
    /// Participante (opcional, usado em grupos).
    /// </summary>
    [JsonPropertyName("participant")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Participant { get; set; }
}
