using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Chat;

/// <summary>
/// Representa a resposta da operação de deletar mensagem para todos.
/// </summary>
public class DeleteMessageForEveryoneResponse
{
    /// <summary>
    /// Chave da mensagem deletada.
    /// </summary>
    [JsonPropertyName("key")]
    public DeleteMessageKey Key { get; set; } = new();

    /// <summary>
    /// Conteúdo da mensagem de protocolo.
    /// </summary>
    [JsonPropertyName("message")]
    public DeleteMessageContent Message { get; set; } = new();

    /// <summary>
    /// Timestamp da mensagem.
    /// </summary>
    [JsonPropertyName("messageTimestamp")]
    public string MessageTimestamp { get; set; } = string.Empty;

    /// <summary>
    /// Status da operação.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}

/// <summary>
/// Representa a chave da mensagem deletada.
/// </summary>
public class DeleteMessageKey
{
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
    /// ID da mensagem.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
}

/// <summary>
/// Representa o conteúdo da mensagem de protocolo.
/// </summary>
public class DeleteMessageContent
{
    /// <summary>
    /// Mensagem de protocolo para revogação.
    /// </summary>
    [JsonPropertyName("protocolMessage")]
    public ProtocolMessage ProtocolMessage { get; set; } = new();
}

/// <summary>
/// Representa a mensagem de protocolo para revogação.
/// </summary>
public class ProtocolMessage
{
    /// <summary>
    /// Chave da mensagem original que foi revogada.
    /// </summary>
    [JsonPropertyName("key")]
    public DeleteMessageKey Key { get; set; } = new();

    /// <summary>
    /// Tipo da operação de protocolo.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
}
