using Evolution.Client.CSharp.Models.Chat;

namespace Evolution.Client.CSharp.WebApp.Models;

/// <summary>
/// ViewModel para resultados de busca de chats.
/// </summary>
public class ChatSearchResultViewModel
{
    /// <summary>
    /// Lista de chats encontrados.
    /// </summary>
    public IEnumerable<ChatRecord> Chats { get; set; } = new List<ChatRecord>();

    /// <summary>
    /// Indica se houve erro na busca.
    /// </summary>
    public bool HasError { get; set; }

    /// <summary>
    /// Mensagem de erro, se houver.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Total de chats encontrados.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Nome da instância.
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// ID de busca.
    /// </summary>
    public string? SearchId { get; set; }

    /// <summary>
    /// JID remoto de busca.
    /// </summary>
    public string? SearchRemoteJid { get; set; }

    /// <summary>
    /// Nome push de busca.
    /// </summary>
    public string? SearchPushName { get; set; }
}