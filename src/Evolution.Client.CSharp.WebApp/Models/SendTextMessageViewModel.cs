using System.ComponentModel.DataAnnotations;

namespace Evolution.Client.CSharp.WebApp.Models;

/// <summary>
/// ViewModel para envio de mensagens de texto.
/// </summary>
public class SendTextMessageViewModel
{
    /// <summary>
    /// Nome da instância.
    /// </summary>
    [Display(Name = "Nome da Instância")]
    [Required(ErrorMessage = "Nome da instância é obrigatório.")]
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Número do destinatário.
    /// </summary>
    [Display(Name = "Número do Destinatário")]
    [Required(ErrorMessage = "Número do destinatário é obrigatório.")]
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Texto da mensagem.
    /// </summary>
    [Display(Name = "Texto da Mensagem")]
    [Required(ErrorMessage = "Texto da mensagem é obrigatório.")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Delay em milissegundos.
    /// </summary>
    [Display(Name = "Delay (ms)")]
    public int? Delay { get; set; }

    /// <summary>
    /// Habilitar preview de links.
    /// </summary>
    [Display(Name = "Preview de Links")]
    public bool LinkPreview { get; set; } = true;

    /// <summary>
    /// Mencionar todos no grupo.
    /// </summary>
    [Display(Name = "Mencionar Todos")]
    public bool? MentionsEveryOne { get; set; }

    /// <summary>
    /// Lista de usuários mencionados (um por linha).
    /// </summary>
    [Display(Name = "Usuários Mencionados")]
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