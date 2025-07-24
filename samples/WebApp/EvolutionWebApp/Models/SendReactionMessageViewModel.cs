using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.Models
{
    /// <summary>
    /// ViewModel para a página de envio de reações.
    /// </summary>
    public class SendReactionMessageViewModel
    {
        [Display(Name = "Nome da Instância")]
        [Required(ErrorMessage = "Nome da instância é obrigatório.")]
        public string? InstanceName { get; set; }

        [Display(Name = "JID Remoto")]
        [Required(ErrorMessage = "JID remoto é obrigatório.")]
        public string? RemoteJid { get; set; }

        [Display(Name = "De Mim")]
        public string? FromMe { get; set; }

        [Display(Name = "ID da Mensagem")]
        [Required(ErrorMessage = "ID da mensagem é obrigatório.")]
        public string? MessageId { get; set; }

        [Display(Name = "Reação")]
        [Required(ErrorMessage = "Reação é obrigatória.")]
        public string? Reaction { get; set; }

        /// <summary>
        /// Resultado do envio da reação.
        /// </summary>
        public SendReactionResult? Result { get; set; }
    }

    /// <summary>
    /// Resultado do envio de reação.
    /// </summary>
    public class SendReactionResult
    {
        public string? MessageId { get; set; }
        public string? RemoteJid { get; set; }
        public bool FromMe { get; set; }
        public long MessageTimestamp { get; set; }
        public string? Status { get; set; }
    }
}