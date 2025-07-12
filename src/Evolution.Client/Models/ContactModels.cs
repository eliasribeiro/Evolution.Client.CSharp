namespace Evolution.Client.Modules;

/// <summary>
/// Informações do contato
/// </summary>
public class ContactInfo
{
    /// <summary>
    /// ID do contato
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Nome do contato
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Número de telefone
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Status do contato
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// URL da foto do perfil
    /// </summary>
    public string? PictureUrl { get; set; }

    /// <summary>
    /// Indica se está bloqueado
    /// </summary>
    public bool IsBlocked { get; set; }

    /// <summary>
    /// Última vez visto online
    /// </summary>
    public DateTime? LastSeen { get; set; }
}

/// <summary>
/// Requisição para busca de contatos
/// </summary>
public class FindContactsRequest
{
    /// <summary>
    /// Termo de busca
    /// </summary>
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Limite de resultados
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Offset para paginação
    /// </summary>
    public int? Offset { get; set; }
}

/// <summary>
/// Status do número no WhatsApp
/// </summary>
public class WhatsAppNumberStatus
{
    /// <summary>
    /// Número de telefone
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Indica se existe no WhatsApp
    /// </summary>
    public bool ExistsOnWhatsApp { get; set; }

    /// <summary>
    /// JID do WhatsApp
    /// </summary>
    public string? Jid { get; set; }
}

/// <summary>
/// Informações da foto do perfil
/// </summary>
public class ProfilePictureInfo
{
    /// <summary>
    /// URL da foto
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// ID da foto
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Tipo da foto
    /// </summary>
    public string? Type { get; set; }
}
