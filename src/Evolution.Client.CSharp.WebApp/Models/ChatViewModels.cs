using System.ComponentModel.DataAnnotations;

namespace Evolution.Client.CSharp.WebApp.Models;

/// <summary>
/// ViewModel para verificar números do WhatsApp.
/// </summary>
public class WhatsAppCheckViewModel
{
    /// <summary>
    /// Nome da instância.
    /// </summary>
    [Required(ErrorMessage = "Nome da instância é obrigatório")]
    [Display(Name = "Nome da Instância")]
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Números para verificar (separados por vírgula).
    /// </summary>
    [Required(ErrorMessage = "Pelo menos um número é obrigatório")]
    [Display(Name = "Números (separados por vírgula)")]
    public string Numbers { get; set; } = string.Empty;
}

/// <summary>
/// Resultado da verificação de número do WhatsApp.
/// </summary>
public class WhatsAppCheckResult
{
    /// <summary>
    /// Número verificado.
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Indica se o número existe no WhatsApp.
    /// </summary>
    public bool Exists { get; set; }

    /// <summary>
    /// JID do número.
    /// </summary>
    public string? Jid { get; set; }
}

/// <summary>
/// ViewModel para buscar URL de foto de perfil.
/// </summary>
public class ProfilePicUrlViewModel
{
    /// <summary>
    /// Nome da instância.
    /// </summary>
    [Required(ErrorMessage = "Nome da instância é obrigatório")]
    [Display(Name = "Nome da Instância")]
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Número ou JID do contato.
    /// </summary>
    [Required(ErrorMessage = "Número é obrigatório")]
    [Display(Name = "Número do Contato")]
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Resultado da busca.
    /// </summary>
    public ProfilePicUrlResult? Result { get; set; }
}

/// <summary>
/// Resultado da busca de URL de foto de perfil.
/// </summary>
public class ProfilePicUrlResult
{
    /// <summary>
    /// WUID do contato.
    /// </summary>
    public string? Wuid { get; set; }

    /// <summary>
    /// URL da foto de perfil.
    /// </summary>
    public string? ProfilePictureUrl { get; set; }

    /// <summary>
    /// Indica se a foto foi encontrada.
    /// </summary>
    public bool Found { get; set; }

    /// <summary>
    /// Mensagem de erro, se houver.
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// ViewModel para buscar mensagens.
/// </summary>
public class MessageSearchViewModel
{
    /// <summary>
    /// Nome da instância.
    /// </summary>
    [Required(ErrorMessage = "Nome da instância é obrigatório")]
    [Display(Name = "Nome da Instância")]
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// JID remoto do chat.
    /// </summary>
    [Required(ErrorMessage = "JID remoto é obrigatório")]
    [Display(Name = "JID Remoto")]
    public string RemoteJid { get; set; } = string.Empty;

    /// <summary>
    /// Limite de mensagens.
    /// </summary>
    [Display(Name = "Limite")]
    [Range(1, 100, ErrorMessage = "O limite deve estar entre 1 e 100")]
    public int Limit { get; set; } = 20;
}

/// <summary>
/// ViewModel para resultado de busca de mensagens.
/// </summary>
public class MessageSearchResultViewModel
{
    /// <summary>
    /// Lista de mensagens encontradas.
    /// </summary>
    public IEnumerable<MessageResult> Messages { get; set; } = new List<MessageResult>();

    /// <summary>
    /// Total de mensagens encontradas.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Página atual.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Tamanho da página.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Indica se houve erro na busca.
    /// </summary>
    public bool HasError { get; set; }

    /// <summary>
    /// Mensagem de erro, se houver.
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Representa um resultado de mensagem.
/// </summary>
public class MessageResult
{
    /// <summary>
    /// ID da mensagem.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Chave da mensagem.
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// ID da mensagem.
    /// </summary>
    public string? MessageId { get; set; }

    /// <summary>
    /// Nome do remetente.
    /// </summary>
    public string? PushName { get; set; }

    /// <summary>
    /// Conteúdo da mensagem.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Tipo da mensagem.
    /// </summary>
    public string? MessageType { get; set; }

    /// <summary>
    /// ID do chat.
    /// </summary>
    public string? ChatId { get; set; }

    /// <summary>
    /// Indica se a mensagem é do próprio usuário.
    /// </summary>
    public bool FromMe { get; set; }

    /// <summary>
    /// Data e hora da mensagem.
    /// </summary>
    public DateTime DateTime { get; set; }

    /// <summary>
    /// Status da mensagem.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Origem da mensagem.
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// Conteúdo da mensagem.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp da mensagem.
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    /// Remetente da mensagem.
    /// </summary>
    public string From { get; set; } = string.Empty;

    /// <summary>
    /// Destinatário da mensagem.
    /// </summary>
    public string To { get; set; } = string.Empty;
}