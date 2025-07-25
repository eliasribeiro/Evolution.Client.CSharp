using System.ComponentModel.DataAnnotations;
using Evolution.Client.CSharp.Models.Group;

namespace Evolution.Client.CSharp.WebApp.Models;

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
    [Display(Name = "Assunto do Grupo")]
    [StringLength(100, ErrorMessage = "O assunto deve ter no máximo 100 caracteres")]
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Descrição do grupo.
    /// </summary>
    [Display(Name = "Descrição")]
    [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
    public string? Description { get; set; }

    /// <summary>
    /// Lista de participantes (números de telefone separados por vírgula).
    /// </summary>
    [Required(ErrorMessage = "Pelo menos um participante é obrigatório")]
    [Display(Name = "Participantes (números separados por vírgula)")]
    public string Participants { get; set; } = string.Empty;

    /// <summary>
    /// Indica se houve erro na operação.
    /// </summary>
    public bool HasError { get; set; }

    /// <summary>
    /// Mensagem de erro, se houver.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Resultado da operação.
    /// </summary>
    public CreateGroupResponse? Result { get; set; }

    /// <summary>
    /// Converte o modelo de view para o modelo de requisição da API.
    /// </summary>
    /// <returns>O modelo de requisição para criar grupo.</returns>
    public CreateGroupRequest ToCreateGroupRequest()
    {
        var participantsList = Participants
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(p => p.Trim())
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .ToList();

        return new CreateGroupRequest
        {
            Subject = Subject,
            Description = Description ?? string.Empty,
            Participants = participantsList
        };
    }
}