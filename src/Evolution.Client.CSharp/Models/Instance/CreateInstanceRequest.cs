using System.Text.Json.Serialization;
using Evolution.Client.CSharp.Converters;

namespace Evolution.Client.CSharp.Models.Instance;

/// <summary>
/// Representa a requisição para criar uma nova instância na API Evolution.
/// </summary>
public class CreateInstanceRequest
{
    /// <summary>
    /// Obtém ou define o nome da instância (obrigatório).
    /// </summary>
    [JsonPropertyName("instanceName")]
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Obtém ou define o token da API (deixe vazio para criar dinamicamente).
    /// </summary>
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Obtém ou define se deve criar QR Code automaticamente após a criação.
    /// </summary>
    [JsonPropertyName("qrcode")]
    public bool? QrCode { get; set; }

    /// <summary>
    /// Obtém ou define o número do proprietário da instância com código do país (ex: 559999999999).
    /// </summary>
    [JsonPropertyName("number")]
    public string? Number { get; set; }

    /// <summary>
    /// Obtém ou define o tipo de integração do WhatsApp.
    /// </summary>
    [JsonPropertyName("integration")]
    [JsonConverter(typeof(WhatsAppIntegrationConverter))]
    public WhatsAppIntegration? Integration { get; set; }

    /// <summary>
    /// Obtém ou define a URL do webhook.
    /// </summary>
    [JsonPropertyName("webhook")]
    public string? Webhook { get; set; }

    /// <summary>
    /// Obtém ou define se deve habilitar webhook por eventos.
    /// </summary>
    [JsonPropertyName("webhook_by_events")]
    public bool? WebhookByEvents { get; set; }

    /// <summary>
    /// Obtém ou define os eventos a serem enviados para o webhook.
    /// </summary>
    [JsonPropertyName("events")]
    public string[]? Events { get; set; }

    /// <summary>
    /// Obtém ou define se deve rejeitar chamadas do WhatsApp automaticamente.
    /// </summary>
    [JsonPropertyName("reject_call")]
    public bool? RejectCall { get; set; }

    /// <summary>
    /// Obtém ou define a mensagem a ser enviada quando uma chamada for rejeitada automaticamente.
    /// </summary>
    [JsonPropertyName("msg_call")]
    public string? MsgCall { get; set; }

    /// <summary>
    /// Obtém ou define se deve ignorar mensagens de grupo.
    /// </summary>
    [JsonPropertyName("groups_ignore")]
    public bool? GroupsIgnore { get; set; }

    /// <summary>
    /// Obtém ou define se deve manter o WhatsApp sempre online.
    /// </summary>
    [JsonPropertyName("always_online")]
    public bool? AlwaysOnline { get; set; }

    /// <summary>
    /// Obtém ou define se deve enviar confirmações de leitura para mensagens recebidas.
    /// </summary>
    [JsonPropertyName("read_messages")]
    public bool? ReadMessages { get; set; }

    /// <summary>
    /// Obtém ou define se deve mostrar status de leitura das mensagens enviadas.
    /// </summary>
    [JsonPropertyName("read_status")]
    public bool? ReadStatus { get; set; }

    /// <summary>
    /// Obtém ou define se deve habilitar websocket.
    /// </summary>
    [JsonPropertyName("websocket_enabled")]
    public bool? WebsocketEnabled { get; set; }

    /// <summary>
    /// Obtém ou define os eventos a serem enviados para o websocket.
    /// </summary>
    [JsonPropertyName("websocket_events")]
    public string[]? WebsocketEvents { get; set; }

    /// <summary>
    /// Obtém ou define se deve habilitar RabbitMQ.
    /// </summary>
    [JsonPropertyName("rabbitmq_enabled")]
    public bool? RabbitmqEnabled { get; set; }

    /// <summary>
    /// Obtém ou define os eventos a serem enviados para o RabbitMQ.
    /// </summary>
    [JsonPropertyName("rabbitmq_events")]
    public string[]? RabbitmqEvents { get; set; }

    /// <summary>
    /// Obtém ou define se deve habilitar SQS.
    /// </summary>
    [JsonPropertyName("sqs_enabled")]
    public bool? SqsEnabled { get; set; }

    /// <summary>
    /// Obtém ou define os eventos a serem enviados para o SQS.
    /// </summary>
    [JsonPropertyName("sqs_events")]
    public string[]? SqsEvents { get; set; }

    /// <summary>
    /// Obtém ou define a URL para a instância do typebot.
    /// </summary>
    [JsonPropertyName("typebot_url")]
    public string? TypebotUrl { get; set; }

    /// <summary>
    /// Obtém ou define o nome do fluxo do typebot.
    /// </summary>
    [JsonPropertyName("typebot")]
    public string? Typebot { get; set; }

    /// <summary>
    /// Obtém ou define os segundos para expirar o typebot.
    /// </summary>
    [JsonPropertyName("typebot_expire")]
    public int? TypebotExpire { get; set; }

    /// <summary>
    /// Obtém ou define a palavra-chave para finalizar o fluxo do typebot.
    /// </summary>
    [JsonPropertyName("typebot_keyword_finish")]
    public string? TypebotKeywordFinish { get; set; }

    /// <summary>
    /// Obtém ou define o atraso padrão para as mensagens do typebot.
    /// </summary>
    [JsonPropertyName("typebot_delay_message")]
    public int? TypebotDelayMessage { get; set; }

    /// <summary>
    /// Obtém ou define a mensagem para comandos desconhecidos do typebot.
    /// </summary>
    [JsonPropertyName("typebot_unknown_message")]
    public string? TypebotUnknownMessage { get; set; }

    /// <summary>
    /// Obtém ou define se o typebot deve escutar mensagens enviadas pelo número conectado.
    /// </summary>
    [JsonPropertyName("typebot_listening_from_me")]
    public bool? TypebotListeningFromMe { get; set; }

    /// <summary>
    /// Obtém ou define as configurações de proxy.
    /// </summary>
    [JsonPropertyName("proxy")]
    public ProxySettings? Proxy { get; set; }

    /// <summary>
    /// Obtém ou define o ID da conta do Chatwoot.
    /// </summary>
    [JsonPropertyName("chatwoot_account_id")]
    public int? ChatwootAccountId { get; set; }

    /// <summary>
    /// Obtém ou define o token de autenticação do Chatwoot.
    /// </summary>
    [JsonPropertyName("chatwoot_token")]
    public string? ChatwootToken { get; set; }

    /// <summary>
    /// Obtém ou define a URL do servidor Chatwoot.
    /// </summary>
    [JsonPropertyName("chatwoot_url")]
    public string? ChatwootUrl { get; set; }

    /// <summary>
    /// Obtém ou define se deve enviar assinatura de mensagem no Chatwoot.
    /// </summary>
    [JsonPropertyName("chatwoot_sign_msg")]
    public bool? ChatwootSignMsg { get; set; }

    /// <summary>
    /// Obtém ou define se deve reabrir conversação no Chatwoot.
    /// </summary>
    [JsonPropertyName("chatwoot_reopen_conversation")]
    public bool? ChatwootReopenConversation { get; set; }

    /// <summary>
    /// Obtém ou define se a conversação deve ficar pendente no Chatwoot.
    /// </summary>
    [JsonPropertyName("chatwoot_conversation_pending")]
    public bool? ChatwootConversationPending { get; set; }
}

/// <summary>
/// Representa os tipos de integração do WhatsApp disponíveis.
/// </summary>
public enum WhatsAppIntegration
{
    /// <summary>
    /// Integração WhatsApp Baileys.
    /// </summary>
    WhatsAppBaileys,

    /// <summary>
    /// Integração WhatsApp Business.
    /// </summary>
    WhatsAppBusiness
}

/// <summary>
/// Representa as configurações de proxy.
/// </summary>
public class ProxySettings
{
    /// <summary>
    /// Obtém ou define o host do proxy.
    /// </summary>
    [JsonPropertyName("host")]
    public string? Host { get; set; }

    /// <summary>
    /// Obtém ou define a porta do proxy.
    /// </summary>
    [JsonPropertyName("port")]
    public int? Port { get; set; }

    /// <summary>
    /// Obtém ou define o protocolo do proxy.
    /// </summary>
    [JsonPropertyName("protocol")]
    public string? Protocol { get; set; }

    /// <summary>
    /// Obtém ou define o nome de usuário do proxy.
    /// </summary>
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    /// <summary>
    /// Obtém ou define a senha do proxy.
    /// </summary>
    [JsonPropertyName("password")]
    public string? Password { get; set; }
}
