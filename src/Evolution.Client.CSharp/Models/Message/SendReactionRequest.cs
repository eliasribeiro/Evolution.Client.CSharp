using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message;

/// <summary>
/// Representa uma requisição para enviar reação a uma mensagem.
/// </summary>
public class SendReactionRequest
{
    /// <summary>
    /// Chave da mensagem para reagir.
    /// </summary>
    [JsonPropertyName("key")]
    public required ReactionMessageKey Key { get; set; }

    /// <summary>
    /// Emoji da reação.
    /// </summary>
    [JsonPropertyName("reaction")]
    public required string Reaction { get; set; }
}

/// <summary>
/// Representa a chave da mensagem para reação.
/// </summary>
public class ReactionMessageKey
{
    /// <summary>
    /// JID remoto do chat de contato ou grupo.
    /// </summary>
    [JsonPropertyName("remoteJid")]
    public required string RemoteJid { get; set; }

    /// <summary>
    /// Se a mensagem foi enviada pelo proprietário da instância ou não.
    /// </summary>
    [JsonPropertyName("fromMe")]
    public required bool FromMe { get; set; }

    /// <summary>
    /// ID da mensagem.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}