using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.ViewModels
{
    /// <summary>
    /// ViewModel para envio de listas.
    /// </summary>
    public class SendListViewModel
    {
        /// <summary>
        /// Número do destinatário.
        /// </summary>
        [Required(ErrorMessage = "O número do destinatário é obrigatório.")]
        [Display(Name = "Número do Destinatário")]
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Título da lista.
        /// </summary>
        [Required(ErrorMessage = "O título da lista é obrigatório.")]
        [Display(Name = "Título")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Descrição da lista.
        /// </summary>
        [Required(ErrorMessage = "A descrição da lista é obrigatória.")]
        [Display(Name = "Descrição")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Texto do botão.
        /// </summary>
        [Required(ErrorMessage = "O texto do botão é obrigatório.")]
        [Display(Name = "Texto do Botão")]
        public string ButtonText { get; set; } = string.Empty;

        /// <summary>
        /// Texto do rodapé.
        /// </summary>
        [Required(ErrorMessage = "O texto do rodapé é obrigatório.")]
        [Display(Name = "Rodapé")]
        public string FooterText { get; set; } = string.Empty;

        /// <summary>
        /// Seções da lista (formato JSON).
        /// </summary>
        [Required(ErrorMessage = "As seções da lista são obrigatórias.")]
        [Display(Name = "Seções (JSON)")]
        public string Sections { get; set; } = string.Empty;

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