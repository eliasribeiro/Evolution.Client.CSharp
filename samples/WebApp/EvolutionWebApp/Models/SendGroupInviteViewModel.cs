using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.Models;

/// <summary>
/// ViewModel para enviar convite de grupo.
/// </summary>
public class SendGroupInviteViewModel
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
    /// Números para enviar o convite (um por linha).
    /// </summary>
    [Required(ErrorMessage = "Pelo menos um número é obrigatório")]
    [Display(Name = "Números (um por linha)")]
    public string Numbers { get; set; } = string.Empty;

    /// <summary>
    /// Texto opcional para acompanhar o convite.
    /// </summary>
    [Display(Name = "Texto do Convite (opcional)")]
    public string? Text { get; set; }

    /// <summary>
    /// Resultado da operação.
    /// </summary>
    public string? Result { get; set; }

    /// <summary>
    /// Indica se houve erro na operação.
    /// </summary>
    public bool HasError { get; set; }
}