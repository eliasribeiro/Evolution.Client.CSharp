using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Chat;

/// <summary>
/// Representa a resposta da busca de chats.
/// </summary>
public class FindChatsResponse : List<ChatRecord>
{
}

/// <summary>
/// Representa um chat encontrado.
/// </summary>
public class ChatRecord
{
    /// <summary>
    /// ID único do chat.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// JID remoto do chat (contato ou grupo).
    /// </summary>
    [JsonPropertyName("remoteJid")]
    public string RemoteJid { get; set; } = string.Empty;

    /// <summary>
    /// Nome do contato ou grupo.
    /// </summary>
    [JsonPropertyName("pushName")]
    public string PushName { get; set; } = string.Empty;

    /// <summary>
    /// URL da foto de perfil.
    /// </summary>
    [JsonPropertyName("profilePicUrl")]
    public string ProfilePicUrl { get; set; } = string.Empty;

    /// <summary>
    /// Data da última atualização do chat.
    /// </summary>
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Início da janela de tempo do chat.
    /// </summary>
    [JsonPropertyName("windowStart")]
    public DateTime WindowStart { get; set; }

    /// <summary>
    /// Expiração da janela de tempo do chat.
    /// </summary>
    [JsonPropertyName("windowExpires")]
    public DateTime WindowExpires { get; set; }

    /// <summary>
    /// Indica se a janela do chat está ativa.
    /// </summary>
    [JsonPropertyName("windowActive")]
    public bool WindowActive { get; set; }
}
