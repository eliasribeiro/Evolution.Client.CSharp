using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.ViewModels
{
    /// <summary>
    /// ViewModel para envio de contatos.
    /// </summary>
    public class SendContactViewModel
    {
        /// <summary>
        /// Número do destinatário.
        /// </summary>
        [Required(ErrorMessage = "O número do destinatário é obrigatório.")]
        [Display(Name = "Número do Destinatário")]
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Nome completo do contato.
        /// </summary>
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [Display(Name = "Nome Completo")]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Organização do contato.
        /// </summary>
        [Display(Name = "Organização")]
        public string? Organization { get; set; }

        /// <summary>
        /// Telefone principal do contato.
        /// </summary>
        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Display(Name = "Telefone")]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Telefone secundário do contato.
        /// </summary>
        [Display(Name = "Telefone Secundário")]
        public string? SecondaryPhoneNumber { get; set; }

        /// <summary>
        /// Email principal do contato.
        /// </summary>
        [EmailAddress(ErrorMessage = "Email inválido.")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        /// <summary>
        /// Email secundário do contato.
        /// </summary>
        [EmailAddress(ErrorMessage = "Email secundário inválido.")]
        [Display(Name = "Email Secundário")]
        public string? SecondaryEmail { get; set; }

        /// <summary>
        /// URL do contato.
        /// </summary>
        [Url(ErrorMessage = "URL inválida.")]
        [Display(Name = "Website")]
        public string? Url { get; set; }

        /// <summary>
        /// Atraso em milissegundos antes de enviar.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "O atraso deve ser um valor positivo.")]
        [Display(Name = "Atraso (ms)")]
        public int? Delay { get; set; }

        /// <summary>
        /// Desabilitar prévia de link.
        /// </summary>
        [Display(Name = "Desabilitar Prévia de Link")]
        public bool DisableLinkPreview { get; set; }

        /// <summary>
        /// Menções (números separados por vírgula).
        /// </summary>
        [Display(Name = "Menções (números separados por vírgula)")]
        public string? Mentions { get; set; }

        /// <summary>
        /// ID da mensagem para citar.
        /// </summary>
        [Display(Name = "ID da Mensagem para Citar")]
        public string? QuotedMessageId { get; set; }

        /// <summary>
        /// Remetente da mensagem citada.
        /// </summary>
        [Display(Name = "Remetente da Mensagem Citada")]
        public string? QuotedMessageSender { get; set; }

        /// <summary>
        /// JID remoto da mensagem citada.
        /// </summary>
        [Display(Name = "JID Remoto da Mensagem Citada")]
        public string? QuotedMessageRemoteJid { get; set; }
    }
}