using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.Models;

/// <summary>
/// ViewModel para buscar grupo por código de convite.
/// </summary>
public class FindGroupByInviteCodeViewModel
{
    /// <summary>
    /// Nome da instância.
    /// </summary>
    [Required(ErrorMessage = "Nome da instância é obrigatório")]
    [Display(Name = "Nome da Instância")]
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Código de convite do grupo.
    /// </summary>
    [Required(ErrorMessage = "Código de convite é obrigatório")]
    [Display(Name = "Código de Convite")]
    public string InviteCode { get; set; } = string.Empty;

    /// <summary>
    /// Informações do grupo encontrado.
    /// </summary>
    public GroupInfoViewModel? GroupInfo { get; set; }

    /// <summary>
    /// Resultado da operação.
    /// </summary>
    public string? Result { get; set; }

    /// <summary>
    /// Indica se houve erro na operação.
    /// </summary>
    public bool HasError { get; set; }
}

/// <summary>
/// ViewModel para informações do grupo.
/// </summary>
public class GroupInfoViewModel
{
    /// <summary>
    /// ID do grupo.
    /// </summary>
    [Display(Name = "ID do Grupo")]
    public string? Id { get; set; }

    /// <summary>
    /// Assunto (nome) do grupo.
    /// </summary>
    [Display(Name = "Nome do Grupo")]
    public string? Subject { get; set; }

    /// <summary>
    /// Descrição do grupo.
    /// </summary>
    [Display(Name = "Descrição")]
    public string? Description { get; set; }

    /// <summary>
    /// Código de convite.
    /// </summary>
    [Display(Name = "Código de Convite")]
    public string? InviteCode { get; set; }

    /// <summary>
    /// URL de convite.
    /// </summary>
    [Display(Name = "URL de Convite")]
    public string? InviteUrl { get; set; }

    /// <summary>
    /// Número de participantes.
    /// </summary>
    [Display(Name = "Número de Participantes")]
    public int? Size { get; set; }

    /// <summary>
    /// Criador do grupo.
    /// </summary>
    [Display(Name = "Criador")]
    public string? Owner { get; set; }

    /// <summary>
    /// Data de criação.
    /// </summary>
    [Display(Name = "Data de Criação")]
    public DateTime? CreationDate { get; set; }
}