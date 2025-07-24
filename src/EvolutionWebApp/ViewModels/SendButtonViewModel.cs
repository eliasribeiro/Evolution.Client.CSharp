using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.ViewModels
{
    /// <summary>
    /// ViewModel para envio de botões.
    /// </summary>
    public class SendButtonViewModel
    {
        /// <summary>
        /// Número do destinatário.
        /// </summary>
        [Required(ErrorMessage = "O número do destinatário é obrigatório.")]
        [Display(Name = "Número do Destinatário")]
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Título dos botões.
        /// </summary>
        [Required(ErrorMessage = "O título é obrigatório.")]
        [Display(Name = "Título")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Descrição dos botões.
        /// </summary>
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [Display(Name = "Descrição")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Rodapé dos botões.
        /// </summary>
        [Required(ErrorMessage = "O rodapé é obrigatório.")]
        [Display(Name = "Rodapé")]
        public string Footer { get; set; } = string.Empty;

        /// <summary>
        /// Botões (formato JSON).
        /// </summary>
        [Required(ErrorMessage = "Os botões são obrigatórios.")]
        [Display(Name = "Botões (JSON)")]
        public string Buttons { get; set; } = string.Empty;

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