namespace Evolution.Client.CSharp.Models.Instance.Create;
using System.Collections.Generic;

public class RequestCreateInstance
{
    public string instanceName { get; set; }
    public string? token { get; set; }
    public string? number { get; set; }
    public bool qrcode { get; set; }
    public string integration { get; set; } = "WHATSAPP-BAILEYS";
    public bool? rejectCall { get; set; }
    public string? msgCall { get; set; }
    public bool? groupsIgnore { get; set; }
    public bool? alwaysOnline { get; set; }
    public bool? readMessages { get; set; }
    public bool? readStatus { get; set; }
    public bool? syncFullHistory { get; set; }
    public string? proxyHost { get; set; }
    public string? proxyPort { get; set; }
    public string? proxyProtocol { get; set; }
    public string? proxyUsername { get; set; }
    public string? proxyPassword { get; set; }
    public WebhookConfig? webhook { get; set; }
    public RabbitMqConfig? rabbitmq { get; set; }
    public SqsConfig? sqs { get; set; }
    public string? chatwootAccountId { get; set; }
    public string? chatwootToken { get; set; }
    public string? chatwootUrl { get; set; }
    public bool? chatwootSignMsg { get; set; }
    public bool? chatwootReopenConversation { get; set; }
    public bool? chatwootConversationPending { get; set; }
    public bool? chatwootImportContacts { get; set; }
    public string? chatwootNameInbox { get; set; }
    public bool? chatwootMergeBrazilContacts { get; set; }
    public bool? chatwootImportMessages { get; set; }
    public int? chatwootDaysLimitImportMessages { get; set; }
    public string? chatwootOrganization { get; set; }
    public string? chatwootLogo { get; set; }
}

public class WebhookConfig
{
    public string? url { get; set; }
    public bool? byEvents { get; set; }
    public bool? base64 { get; set; }
    public Dictionary<string, string>? headers { get; set; }
    public List<string>? events { get; set; }
}

public class RabbitMqConfig
{
    public bool? enabled { get; set; }
    public List<string>? events { get; set; }
}

public class SqsConfig
{
    public bool? enabled { get; set; }
    public List<string>? events { get; set; }
}
