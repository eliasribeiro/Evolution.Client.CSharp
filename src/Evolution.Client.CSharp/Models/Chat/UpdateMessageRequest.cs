using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Chat;

/// <summary>
/// Representa a requisição para atualizar uma mensagem.
/// </summary>
public class UpdateMessageRequest
{
    /// <summary>
    /// Número do telefone do destinatário com código do país.
    /// Obs.: Apesar da API documentar como integer, na verdade é string.
    /// </summary>
    [JsonPropertyName("number")]
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Novo conteúdo da mensagem.
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Chave da mensagem a ser atualizada.
    /// </summary>
    [JsonPropertyName("key")]
    public UpdateMessageKey Key { get; set; } = new();
}

/// <summary>
/// Representa a chave da mensagem para atualização.
/// </summary>
public class UpdateMessageKey
{
    /// <summary>
    /// JID remoto do chat do contato ou grupo.
    /// </summary>
    [JsonPropertyName("remoteJid")]
    public string RemoteJid { get; set; } = string.Empty;

    /// <summary>
    /// Indica se a mensagem foi enviada pelo proprietário da instância ou não.
    /// </summary>
    [JsonPropertyName("fromMe")]
    public bool FromMe { get; set; }

    /// <summary>
    /// ID da mensagem.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
}