using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.Models;

/// <summary>
/// ViewModel para buscar código de convite do grupo.
/// </summary>
public class FetchInviteCodeViewModel
{
    /// <summary>
    /// Nome da instância.
    /// </summary>
    [Required(ErrorMessage = "Nome da instância é obrigatório")]
    [Display(Name = "Nome da Instância")]
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// JID do grupo.
    /// </summary>
    [Required(ErrorMessage = "JID do grupo é obrigatório")]
    [Display(Name = "JID do Grupo")]
    public string GroupJid { get; set; } = string.Empty;

    /// <summary>
    /// Código de convite retornado.
    /// </summary>
    [Display(Name = "Código de Convite")]
    public string? InviteCode { get; set; }

    /// <summary>
    /// URL de convite retornada.
    /// </summary>
    [Display(Name = "URL de Convite")]
    public string? InviteUrl { get; set; }

    /// <summary>
    /// Resultado da operação.
    /// </summary>
    public string? Result { get; set; }

    /// <summary>
    /// Indica se houve erro na operação.
    /// </summary>
    public bool HasError { get; set; }
}