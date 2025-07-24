using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.ViewModels
{
    /// <summary>
    /// ViewModel para envio de enquetes.
    /// </summary>
    public class SendPollViewModel
    {
        /// <summary>
        /// Número do destinatário.
        /// </summary>
        [Required(ErrorMessage = "O número do destinatário é obrigatório.")]
        [Display(Name = "Número do Destinatário")]
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// Nome/título da enquete.
        /// </summary>
        [Required(ErrorMessage = "O título da enquete é obrigatório.")]
        [Display(Name = "Título da Enquete")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Número de opções selecionáveis.
        /// </summary>
        [Required(ErrorMessage = "O número de opções selecionáveis é obrigatório.")]
        [Range(1, 10, ErrorMessage = "O número de opções selecionáveis deve estar entre 1 e 10.")]
        [Display(Name = "Opções Selecionáveis")]
        public int SelectableCount { get; set; } = 1;

        /// <summary>
        /// Opções da enquete (uma por linha).
        /// </summary>
        [Required(ErrorMessage = "As opções da enquete são obrigatórias.")]
        [Display(Name = "Opções (uma por linha)")]
        public string Values { get; set; } = string.Empty;

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