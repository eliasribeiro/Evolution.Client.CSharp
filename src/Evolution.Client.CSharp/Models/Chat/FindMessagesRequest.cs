using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Chat;

/// <summary>
/// Representa a requisição para buscar mensagens.
/// </summary>
public class FindMessagesRequest
{
    /// <summary>
    /// Critérios de busca para as mensagens.
    /// </summary>
    [JsonPropertyName("where")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public FindMessagesWhere? Where { get; set; }

    /// <summary>
    /// Número da página para paginação (opcional).
    /// </summary>
    [JsonPropertyName("page")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Page { get; set; }

    /// <summary>
    /// Número de registros por página (opcional).
    /// </summary>
    [JsonPropertyName("offset")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Offset { get; set; }
}

/// <summary>
/// Representa os critérios de busca para mensagens.
/// </summary>
public class FindMessagesWhere
{
    /// <summary>
    /// Chave da mensagem para buscar.
    /// </summary>
    [JsonPropertyName("key")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MessageKey? Key { get; set; }
}

/// <summary>
/// Representa a chave de uma mensagem.
/// </summary>
public class MessageKey
{
    /// <summary>
    /// ID da mensagem.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; set; }

    /// <summary>
    /// JID remoto (contato ou grupo) da mensagem.
    /// </summary>
    [JsonPropertyName("remoteJid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RemoteJid { get; set; }

    /// <summary>
    /// Indica se a mensagem foi enviada por mim.
    /// </summary>
    [JsonPropertyName("fromMe")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? FromMe { get; set; }
}