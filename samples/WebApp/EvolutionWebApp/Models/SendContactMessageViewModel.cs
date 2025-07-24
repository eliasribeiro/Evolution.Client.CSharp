using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.Models
{
    /// <summary>
    /// ViewModel para a página de envio de contatos.
    /// </summary>
    public class SendContactMessageViewModel
    {
        [Display(Name = "Nome da Instância")]
        [Required(ErrorMessage = "Nome da instância é obrigatório.")]
        public string? InstanceName { get; set; }

        [Display(Name = "Número do Destinatário")]
        [Required(ErrorMessage = "Número do destinatário é obrigatório.")]
        public string? Number { get; set; }

        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "Nome completo é obrigatório.")]
        public string? FullName { get; set; }

        [Display(Name = "Organização")]
        public string? Organization { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Telefone é obrigatório.")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Telefone Secundário")]
        public string? SecondaryPhoneNumber { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string? Email { get; set; }

        [Display(Name = "Email Secundário")]
        [EmailAddress(ErrorMessage = "Email secundário inválido.")]
        public string? SecondaryEmail { get; set; }

        [Display(Name = "URL")]
        [Url(ErrorMessage = "URL inválida.")]
        public string? Url { get; set; }

        [Display(Name = "Atraso (ms)")]
        [Range(0, int.MaxValue, ErrorMessage = "Atraso deve ser um número positivo.")]
        public int? Delay { get; set; }

        [Display(Name = "Visualização de Link")]
        public bool LinkPreview { get; set; }

        [Display(Name = "Mencionar Todos")]
        public bool MentionsEveryOne { get; set; }

        [Display(Name = "Usuários Mencionados")]
        public string? Mentioned { get; set; }

        [Display(Name = "ID da Mensagem Citada")]
        public string? QuotedMessageId { get; set; }

        [Display(Name = "Texto da Mensagem Citada")]
        public string? QuotedMessageText { get; set; }

        /// <summary>
        /// Resultado do envio do contato.
        /// </summary>
        public SendContactResult? Result { get; set; }
    }

    /// <summary>
    /// Resultado do envio de contato.
    /// </summary>
    public class SendContactResult
    {
        public string? MessageId { get; set; }
        public string? RemoteJid { get; set; }
        public bool FromMe { get; set; }
        public long MessageTimestamp { get; set; }
        public string? Status { get; set; }
    }
}