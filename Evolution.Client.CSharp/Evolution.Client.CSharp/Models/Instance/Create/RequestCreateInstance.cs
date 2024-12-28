namespace Evolution.Client.CSharp.Models.Instance.Create;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class RequestCreateInstance
{
    [JsonPropertyName("instanceName")]
    public string InstanceName { get; set; }

    [JsonPropertyName("token")]
    public string? Token { get; set; }

    [JsonPropertyName("number")]
    public string? Number { get; set; }

    [JsonPropertyName("qrcode")]
    public bool Qrcode { get; set; }

    [JsonPropertyName("integration")]
    public string Integration { get; set; } = "WHATSAPP-BAILEYS";

    [JsonPropertyName("rejectCall")]
    public bool? RejectCall { get; set; }

    [JsonPropertyName("msgCall")]
    public string? MsgCall { get; set; }

    [JsonPropertyName("groupsIgnore")]
    public bool? GroupsIgnore { get; set; }

    [JsonPropertyName("alwaysOnline")]
    public bool? AlwaysOnline { get; set; }

    [JsonPropertyName("readMessages")]
    public bool? ReadMessages { get; set; }

    [JsonPropertyName("readStatus")]
    public bool? ReadStatus { get; set; }

    [JsonPropertyName("syncFullHistory")]
    public bool? SyncFullHistory { get; set; }

    [JsonPropertyName("proxyHost")]
    public string? ProxyHost { get; set; }

    [JsonPropertyName("proxyPort")]
    public string? ProxyPort { get; set; }

    [JsonPropertyName("proxyProtocol")]
    public string? ProxyProtocol { get; set; }

    [JsonPropertyName("proxyUsername")]
    public string? ProxyUsername { get; set; }

    [JsonPropertyName("proxyPassword")]
    public string? ProxyPassword { get; set; }

    [JsonPropertyName("webhook")]
    public WebhookConfig? Webhook { get; set; }

    [JsonPropertyName("rabbitmq")]
    public RabbitMqConfig? Rabbitmq { get; set; }

    [JsonPropertyName("sqs")]
    public SqsConfig? Sqs { get; set; }

    [JsonPropertyName("chatwootAccountId")]
    public string? ChatwootAccountId { get; set; }

    [JsonPropertyName("chatwootToken")]
    public string? ChatwootToken { get; set; }

    [JsonPropertyName("chatwootUrl")]
    public string? ChatwootUrl { get; set; }

    [JsonPropertyName("chatwootSignMsg")]
    public bool? ChatwootSignMsg { get; set; }

    [JsonPropertyName("chatwootReopenConversation")]
    public bool? ChatwootReopenConversation { get; set; }

    [JsonPropertyName("chatwootConversationPending")]
    public bool? ChatwootConversationPending { get; set; }

    [JsonPropertyName("chatwootImportContacts")]
    public bool? ChatwootImportContacts { get; set; }

    [JsonPropertyName("chatwootNameInbox")]
    public string? ChatwootNameInbox { get; set; }

    [JsonPropertyName("chatwootMergeBrazilContacts")]
    public bool? ChatwootMergeBrazilContacts { get; set; }

    [JsonPropertyName("chatwootImportMessages")]
    public bool? ChatwootImportMessages { get; set; }

    [JsonPropertyName("chatwootDaysLimitImportMessages")]
    public int? ChatwootDaysLimitImportMessages { get; set; }

    [JsonPropertyName("chatwootOrganization")]
    public string? ChatwootOrganization { get; set; }

    [JsonPropertyName("chatwootLogo")]
    public string? ChatwootLogo { get; set; }
}

public class WebhookConfig
{
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("byEvents")]
    public bool? ByEvents { get; set; }

    [JsonPropertyName("base64")]
    public bool? Base64 { get; set; }

    [JsonPropertyName("headers")]
    public Dictionary<string, string>? Headers { get; set; }

    [JsonPropertyName("events")]
    public List<string>? Events { get; set; }
}

public class RabbitMqConfig
{
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }

    [JsonPropertyName("events")]
    public List<string>? Events { get; set; }
}

public class SqsConfig
{
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; set; }

    [JsonPropertyName("events")]
    public List<string>? Events { get; set; }
}