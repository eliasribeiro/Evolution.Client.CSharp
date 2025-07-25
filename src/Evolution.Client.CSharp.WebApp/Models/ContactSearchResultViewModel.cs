using Evolution.Client.CSharp.Models.Chat;

namespace Evolution.Client.CSharp.WebApp.Models;

/// <summary>
/// ViewModel para resultados de busca de contatos.
/// </summary>
public class ContactSearchResultViewModel
{
    /// <summary>
    /// Lista de contatos encontrados.
    /// </summary>
    public IEnumerable<ContactItem> Contacts { get; set; } = new List<ContactItem>();

    /// <summary>
    /// Indica se houve erro na busca.
    /// </summary>
    public bool HasError { get; set; }

    /// <summary>
    /// Mensagem de erro, se houver.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Total de contatos encontrados.
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

/// <summary>
/// Representa um resultado de contato.
/// </summary>
public class ContactResult
{
    /// <summary>
    /// ID do contato.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Nome do contato.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Nome de notificação.
    /// </summary>
    public string NotifyName { get; set; } = string.Empty;

    /// <summary>
    /// Nome verificado.
    /// </summary>
    public string VerifiedName { get; set; } = string.Empty;

    /// <summary>
    /// Nome push.
    /// </summary>
    public string PushName { get; set; } = string.Empty;

    /// <summary>
    /// JID remoto.
    /// </summary>
    public string RemoteJid { get; set; } = string.Empty;

    /// <summary>
    /// URL da foto de perfil.
    /// </summary>
    public string? ProfilePictureUrl { get; set; }
}