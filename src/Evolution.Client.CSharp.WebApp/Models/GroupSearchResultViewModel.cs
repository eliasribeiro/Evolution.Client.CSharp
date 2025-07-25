using System.ComponentModel.DataAnnotations;

namespace Evolution.Client.CSharp.WebApp.Models;

/// <summary>
/// ViewModel para resultado de busca de grupos.
/// </summary>
public class GroupSearchResultViewModel
{
    /// <summary>
    /// Nome da instância.
    /// </summary>
    [Required(ErrorMessage = "Nome da instância é obrigatório")]
    [Display(Name = "Nome da Instância")]
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Indica se deve buscar os participantes dos grupos.
    /// </summary>
    [Display(Name = "Buscar Participantes")]
    public bool GetParticipants { get; set; }

    /// <summary>
    /// Lista de grupos encontrados.
    /// </summary>
    public List<GroupResult> Groups { get; set; } = new List<GroupResult>();

    /// <summary>
    /// Número total de grupos encontrados.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Página atual.
    /// </summary>
    public int CurrentPage { get; set; } = 1;

    /// <summary>
    /// Tamanho da página.
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Indica se houve erro na operação.
    /// </summary>
    public bool HasError { get; set; }

    /// <summary>
    /// Mensagem de erro, se houver.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Indica se a operação foi bem-sucedida.
    /// </summary>
    public bool IsSuccess => !HasError && string.IsNullOrEmpty(ErrorMessage);
}