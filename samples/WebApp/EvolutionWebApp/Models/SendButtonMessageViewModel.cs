using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.Models
{
    /// <summary>
    /// ViewModel para a página de envio de botões.
    /// </summary>
    public class SendButtonMessageViewModel
    {
        [Display(Name = "Nome da Instância")]
        [Required(ErrorMessage = "Nome da instância é obrigatório.")]
        public string? InstanceName { get; set; }

        [Display(Name = "Número do Destinatário")]
        [Required(ErrorMessage = "Número do destinatário é obrigatório.")]
        public string? Number { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "Título é obrigatório.")]
        public string? Title { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Descrição é obrigatória.")]
        public string? Description { get; set; }

        [Display(Name = "Rodapé")]
        [Required(ErrorMessage = "Rodapé é obrigatório.")]
        public string? Footer { get; set; }

        [Display(Name = "Botões (JSON)")]
        [Required(ErrorMessage = "Botões são obrigatórios.")]
        public string? Buttons { get; set; }

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
        /// Resultado do envio dos botões.
        /// </summary>
        public SendButtonResult? Result { get; set; }
    }

    /// <summary>
    /// Resultado do envio de botões.
    /// </summary>
    public class SendButtonResult
    {
        public string? MessageId { get; set; }
        public string? RemoteJid { get; set; }
        public bool FromMe { get; set; }
        public long MessageTimestamp { get; set; }
        public string? Status { get; set; }
    }
}