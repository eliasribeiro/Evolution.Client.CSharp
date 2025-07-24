using System.ComponentModel.DataAnnotations;

namespace Evolution.Client.CSharp.WebApp.Models;

/// <summary>
/// ViewModel para envio de status.
/// </summary>
public class SendStatusViewModel
{
    /// <summary>
    /// Nome da instância.
    /// </summary>
    [Required(ErrorMessage = "O nome da instância é obrigatório.")]
    [Display(Name = "Nome da Instância")]
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Tipo do status.
    /// </summary>
    [Required(ErrorMessage = "O tipo do status é obrigatório.")]
    [Display(Name = "Tipo do Status")]
    public string Type { get; set; } = "text";

    /// <summary>
    /// Conteúdo do status.
    /// </summary>
    [Required(ErrorMessage = "O conteúdo do status é obrigatório.")]
    [Display(Name = "Conteúdo")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Legenda opcional para imagem ou vídeo.
    /// </summary>
    [Display(Name = "Legenda")]
    public string? Caption { get; set; }

    /// <summary>
    /// Cor de fundo (exemplo: #008000).
    /// </summary>
    [Required(ErrorMessage = "A cor de fundo é obrigatória.")]
    [Display(Name = "Cor de Fundo")]
    [RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "A cor deve estar no formato hexadecimal (#RRGGBB).")]
    public string BackgroundColor { get; set; } = "#008000";

    /// <summary>
    /// Fonte do texto.
    /// </summary>
    [Required(ErrorMessage = "A fonte é obrigatória.")]
    [Display(Name = "Fonte")]
    [Range(1, 5, ErrorMessage = "A fonte deve estar entre 1 e 5.")]
    public int Font { get; set; } = 1;

    /// <summary>
    /// Indica se deve enviar para todos os contatos.
    /// </summary>
    [Display(Name = "Enviar para Todos os Contatos")]
    public bool AllContacts { get; set; } = true;

    /// <summary>
    /// Lista de números para enviar o status (quando AllContacts = false).
    /// </summary>
    [Display(Name = "Lista de Contatos (um por linha)")]
    public string? StatusJidListText { get; set; }

    /// <summary>
    /// Resultado da operação.
    /// </summary>
    public string? Result { get; set; }

    /// <summary>
    /// Indica se houve erro na operação.
    /// </summary>
    public bool HasError { get; set; }
}