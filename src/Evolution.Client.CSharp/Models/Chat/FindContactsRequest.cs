using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Chat;

/// <summary>
/// Representa a requisição para buscar contatos.
/// </summary>
public class FindContactsRequest
{
    /// <summary>
    /// Critérios de busca para os contatos.
    /// </summary>
    [JsonPropertyName("where")]
    public FindContactsWhere? Where { get; set; }
}

/// <summary>
/// Representa os critérios de busca para contatos.
/// </summary>
public class FindContactsWhere
{
    /// <summary>
    /// ID do contato para buscar (opcional).
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; set; }

    /// <summary>
    /// JID remoto do contato para buscar (opcional).
    /// </summary>
    [JsonPropertyName("remoteJid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RemoteJid { get; set; }

    /// <summary>
    /// Nome do contato para buscar (opcional).
    /// </summary>
    [JsonPropertyName("pushName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PushName { get; set; }
}