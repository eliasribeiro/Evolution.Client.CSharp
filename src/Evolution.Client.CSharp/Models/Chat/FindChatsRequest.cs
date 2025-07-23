using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Chat;

/// <summary>
/// Representa a requisição para buscar chats.
/// </summary>
public class FindChatsRequest
{
    /// <summary>
    /// Critérios de busca para os chats.
    /// </summary>
    [JsonPropertyName("where")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public FindChatsWhere? Where { get; set; }
}

/// <summary>
/// Representa os critérios de busca para chats.
/// </summary>
public class FindChatsWhere
{
    /// <summary>
    /// ID do chat para buscar (opcional).
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; set; }

    /// <summary>
    /// JID remoto do chat para buscar (opcional).
    /// </summary>
    [JsonPropertyName("remoteJid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RemoteJid { get; set; }

    /// <summary>
    /// Nome do contato/grupo para buscar (opcional).
    /// </summary>
    [JsonPropertyName("pushName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PushName { get; set; }
}
