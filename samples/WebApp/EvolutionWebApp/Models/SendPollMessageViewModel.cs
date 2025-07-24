using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.Models
{
    /// <summary>
    /// ViewModel para a página de envio de enquetes.
    /// </summary>
    public class SendPollMessageViewModel
    {
        [Display(Name = "Nome da Instância")]
        [Required(ErrorMessage = "Nome da instância é obrigatório.")]
        public string? InstanceName { get; set; }

        [Display(Name = "Número do Destinatário")]
        [Required(ErrorMessage = "Número do destinatário é obrigatório.")]
        public string? Number { get; set; }

        [Display(Name = "Título da Enquete")]
        [Required(ErrorMessage = "Título da enquete é obrigatório.")]
        public string? Name { get; set; }

        [Display(Name = "Opções Selecionáveis")]
        [Range(1, int.MaxValue, ErrorMessage = "Número de opções selecionáveis deve ser maior que zero.")]
        public int SelectableCount { get; set; } = 1;

        [Display(Name = "Opções (uma por linha)")]
        [Required(ErrorMessage = "Opções da enquete são obrigatórias.")]
        public string? Values { get; set; }

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
        /// Resultado do envio da enquete.
        /// </summary>
        public SendPollResult? Result { get; set; }
    }

    /// <summary>
    /// Resultado do envio de enquete.
    /// </summary>
    public class SendPollResult
    {
        public string? MessageId { get; set; }
        public string? RemoteJid { get; set; }
        public bool FromMe { get; set; }
        public long MessageTimestamp { get; set; }
        public string? Status { get; set; }
    }
}