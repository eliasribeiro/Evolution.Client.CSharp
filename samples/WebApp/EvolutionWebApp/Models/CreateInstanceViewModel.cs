using System.ComponentModel.DataAnnotations;
using Evolution.Client.CSharp.Models.Instance;

namespace EvolutionWebApp.Models;

/// <summary>
/// Modelo de visualização para criar uma nova instância.
/// </summary>
public class CreateInstanceViewModel
{
    /// <summary>
    /// Obtém ou define o nome da instância (obrigatório).
    /// </summary>
    [Required(ErrorMessage = "O nome da instância é obrigatório")]
    [Display(Name = "Nome da Instância")]
    [StringLength(50, ErrorMessage = "O nome da instância deve ter no máximo 50 caracteres")]
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Obtém ou define o token da API (opcional - deixe vazio para criar dinamicamente).
    /// </summary>
    [Display(Name = "Token da API")]
    [StringLength(100, ErrorMessage = "O token deve ter no máximo 100 caracteres")]
    public string? Token { get; set; }

    /// <summary>
    /// Obtém ou define se deve criar QR Code automaticamente após a criação.
    /// </summary>
    [Display(Name = "Criar QR Code automaticamente")]
    public bool QrCode { get; set; } = true;

    /// <summary>
    /// Obtém ou define o número do proprietário da instância com código do país.
    /// </summary>
    [Display(Name = "Número do Proprietário")]
    [StringLength(15, ErrorMessage = "O número deve ter no máximo 15 caracteres")]
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Obtém ou define o tipo de integração do WhatsApp.
    /// </summary>
    [Display(Name = "Tipo de Integração")]
    public WhatsAppIntegration Integration { get; set; } = WhatsAppIntegration.WhatsAppBaileys;

    /// <summary>
    /// Obtém ou define a URL do webhook.
    /// </summary>
    [Display(Name = "URL do Webhook")]
    [Url(ErrorMessage = "Por favor, insira uma URL válida")]
    public string? Webhook { get; set; }

    /// <summary>
    /// Obtém ou define se deve habilitar webhook por eventos.
    /// </summary>
    [Display(Name = "Habilitar Webhook por Eventos")]
    public bool WebhookByEvents { get; set; }

    /// <summary>
    /// Obtém ou define se deve rejeitar chamadas do WhatsApp automaticamente.
    /// </summary>
    [Display(Name = "Rejeitar Chamadas Automaticamente")]
    public bool RejectCall { get; set; }

    /// <summary>
    /// Obtém ou define a mensagem a ser enviada quando uma chamada for rejeitada.
    /// </summary>
    [Display(Name = "Mensagem para Chamadas Rejeitadas")]
    [StringLength(200, ErrorMessage = "A mensagem deve ter no máximo 200 caracteres")]
    public string? MsgCall { get; set; }

    /// <summary>
    /// Obtém ou define se deve ignorar mensagens de grupo.
    /// </summary>
    [Display(Name = "Ignorar Mensagens de Grupo")]
    public bool GroupsIgnore { get; set; }

    /// <summary>
    /// Obtém ou define se deve manter o WhatsApp sempre online.
    /// </summary>
    [Display(Name = "Manter Sempre Online")]
    public bool AlwaysOnline { get; set; }

    /// <summary>
    /// Obtém ou define se deve enviar confirmações de leitura.
    /// </summary>
    [Display(Name = "Enviar Confirmações de Leitura")]
    public bool ReadMessages { get; set; }

    /// <summary>
    /// Obtém ou define se deve mostrar status de leitura.
    /// </summary>
    [Display(Name = "Mostrar Status de Leitura")]
    public bool ReadStatus { get; set; }

    /// <summary>
    /// Converte o modelo de view para o modelo de requisição da API.
    /// </summary>
    /// <returns>O modelo de requisição para criar instância.</returns>
    public CreateInstanceRequest ToCreateInstanceRequest()
    {
        return new CreateInstanceRequest
        {
            InstanceName = InstanceName,
            Token = string.IsNullOrWhiteSpace(Token) ? string.Empty : Token,
            QrCode = QrCode,
            Number = string.IsNullOrWhiteSpace(Number) ? string.Empty : Number,
            Integration = Integration,
            Webhook = string.IsNullOrWhiteSpace(Webhook) ? null : Webhook,
            WebhookByEvents = WebhookByEvents,
            RejectCall = RejectCall,
            MsgCall = string.IsNullOrWhiteSpace(MsgCall) ? null : MsgCall,
            GroupsIgnore = GroupsIgnore,
            AlwaysOnline = AlwaysOnline,
            ReadMessages = ReadMessages,
            ReadStatus = ReadStatus
        };
    }
}
