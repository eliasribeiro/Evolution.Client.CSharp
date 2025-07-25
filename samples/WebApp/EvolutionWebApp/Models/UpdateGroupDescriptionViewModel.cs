using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.Models;

/// <summary>
/// ViewModel para atualização da descrição do grupo.
/// </summary>
public class UpdateGroupDescriptionViewModel
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
    /// Nova descrição do grupo.
    /// </summary>
    [Required(ErrorMessage = "Descrição é obrigatória")]
    [Display(Name = "Nova Descrição do Grupo")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Resultado da operação.
    /// </summary>
    public string? Result { get; set; }

    /// <summary>
    /// Indica se houve erro na operação.
    /// </summary>
    public bool HasError { get; set; }
}