using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.Models
{
    /// <summary>
    /// ViewModel para a página de envio de listas.
    /// </summary>
    public class SendListMessageViewModel
    {
        [Display(Name = "Nome da Instância")]
        [Required(ErrorMessage = "Nome da instância é obrigatório.")]
        public string? InstanceName { get; set; }

        [Display(Name = "Número do Destinatário")]
        [Required(ErrorMessage = "Número do destinatário é obrigatório.")]
        public string? Number { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "Título da lista é obrigatório.")]
        public string? Title { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Descrição da lista é obrigatória.")]
        public string? Description { get; set; }

        [Display(Name = "Texto do Botão")]
        [Required(ErrorMessage = "Texto do botão é obrigatório.")]
        public string? ButtonText { get; set; }

        [Display(Name = "Texto do Rodapé")]
        [Required(ErrorMessage = "Texto do rodapé é obrigatório.")]
        public string? FooterText { get; set; }

        [Display(Name = "Seções (JSON)")]
        [Required(ErrorMessage = "Seções da lista são obrigatórias.")]
        public string? Sections { get; set; }

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
        /// Resultado do envio da lista.
        /// </summary>
        public SendListResult? Result { get; set; }
    }

    /// <summary>
    /// Resultado do envio de lista.
    /// </summary>
    public class SendListResult
    {
        public string? MessageId { get; set; }
        public string? RemoteJid { get; set; }
        public bool FromMe { get; set; }
        public long MessageTimestamp { get; set; }
        public string? Status { get; set; }
    }
}