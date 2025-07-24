using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.ViewModels
{
    /// <summary>
    /// ViewModel para envio de reações.
    /// </summary>
    public class SendReactionViewModel
    {
        /// <summary>
        /// JID remoto da mensagem.
        /// </summary>
        [Required(ErrorMessage = "O JID remoto é obrigatório.")]
        [Display(Name = "JID Remoto")]
        public string RemoteJid { get; set; } = string.Empty;

        /// <summary>
        /// Remetente da mensagem.
        /// </summary>
        [Display(Name = "Remetente")]
        public string? FromMe { get; set; }

        /// <summary>
        /// ID da mensagem.
        /// </summary>
        [Required(ErrorMessage = "O ID da mensagem é obrigatório.")]
        [Display(Name = "ID da Mensagem")]
        public string MessageId { get; set; } = string.Empty;

        /// <summary>
        /// Emoji da reação.
        /// </summary>
        [Required(ErrorMessage = "A reação é obrigatória.")]
        [Display(Name = "Reação (Emoji)")]
        public string Reaction { get; set; } = string.Empty;
    }
}