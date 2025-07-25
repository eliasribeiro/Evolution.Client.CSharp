using System.ComponentModel.DataAnnotations;
using Evolution.Client.CSharp.Models.Group;

namespace EvolutionWebApp.Models;

/// <summary>
/// ViewModel para criação de grupo.
/// </summary>
public class CreateGroupViewModel
{
    /// <summary>
    /// Nome da instância.
    /// </summary>
    [Required(ErrorMessage = "Nome da instância é obrigatório")]
    [Display(Name = "Nome da Instância")]
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Assunto (nome) do grupo.
    /// </summary>
    [Required(ErrorMessage = "Assunto do grupo é obrigatório")]
    [Display(Name = "Nome do Grupo")]
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Descrição do grupo.
    /// </summary>
    [Display(Name = "Descrição do Grupo")]
    public string? Description { get; set; }

    /// <summary>
    /// Lista de participantes (números de telefone).
    /// </summary>
    [Required(ErrorMessage = "Pelo menos um participante é obrigatório")]
    [Display(Name = "Participantes (um por linha)")]
    public string Participants { get; set; } = string.Empty;

    /// <summary>
    /// Resultado da operação.
    /// </summary>
    public CreateGroupResponse? Result { get; set; }

    /// <summary>
    /// Mensagem de erro, se houver.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Indica se a operação foi bem-sucedida.
    /// </summary>
    public bool IsSuccess => Result != null && string.IsNullOrEmpty(ErrorMessage);
}