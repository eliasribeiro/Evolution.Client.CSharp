using System.ComponentModel.DataAnnotations;

namespace Evolution.Client.CSharp.WebApp.Models;

/// <summary>
/// ViewModel para envio de mídia.
/// </summary>
public class SendMediaViewModel
{
    /// <summary>
    /// Nome da instância.
    /// </summary>
    [Required(ErrorMessage = "O nome da instância é obrigatório.")]
    [Display(Name = "Nome da Instância")]
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Número do telefone do destinatário.
    /// </summary>
    [Required(ErrorMessage = "O número do destinatário é obrigatório.")]
    [Display(Name = "Número do Destinatário")]
    [Phone(ErrorMessage = "Formato de telefone inválido.")]
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Tipo da mídia.
    /// </summary>
    [Required(ErrorMessage = "O tipo da mídia é obrigatório.")]
    [Display(Name = "Tipo da Mídia")]
    public string MediaType { get; set; } = "image";

    /// <summary>
    /// Tipo MIME da mídia.
    /// </summary>
    [Required(ErrorMessage = "O tipo MIME é obrigatório.")]
    [Display(Name = "Tipo MIME")]
    public string MimeType { get; set; } = "image/png";

    /// <summary>
    /// Legenda da mídia.
    /// </summary>
    [Display(Name = "Legenda")]
    public string? Caption { get; set; }

    /// <summary>
    /// URL ou base64 da mídia.
    /// </summary>
    [Required(ErrorMessage = "A mídia é obrigatória.")]
    [Display(Name = "URL ou Base64 da Mídia")]
    public string Media { get; set; } = string.Empty;

    /// <summary>
    /// Nome do arquivo.
    /// </summary>
    [Display(Name = "Nome do Arquivo")]
    public string? FileName { get; set; }

    /// <summary>
    /// Atraso em milissegundos.
    /// </summary>
    [Display(Name = "Atraso (ms)")]
    [Range(0, int.MaxValue, ErrorMessage = "O atraso deve ser um valor positivo.")]
    public int? Delay { get; set; }

    /// <summary>
    /// Indica se deve exibir preview de links.
    /// </summary>
    [Display(Name = "Exibir Preview de Links")]
    public bool? LinkPreview { get; set; }

    /// <summary>
    /// Indica se deve mencionar todos os participantes do grupo.
    /// </summary>
    [Display(Name = "Mencionar Todos")]
    public bool? MentionsEveryOne { get; set; }

    /// <summary>
    /// Lista de usuários mencionados.
    /// </summary>
    [Display(Name = "Usuários Mencionados (um por linha)")]
    public string? MentionedText { get; set; }

    /// <summary>
    /// ID da mensagem citada.
    /// </summary>
    [Display(Name = "ID da Mensagem Citada")]
    public string? QuotedMessageId { get; set; }

    /// <summary>
    /// Resultado da operação.
    /// </summary>
    public string? Result { get; set; }

    /// <summary>
    /// Indica se houve erro na operação.
    /// </summary>
    public bool HasError { get; set; }
}