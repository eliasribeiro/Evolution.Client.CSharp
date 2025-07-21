using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Instance;

/// <summary>
/// Representa a resposta do endpoint fetch-instances da API Evolution (versão 2).
/// </summary>
public class InstanceResponseV2
{
    /// <summary>
    /// Obtém ou define o ID da instância.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Obtém ou define o nome da instância.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Obtém ou define o status de conexão da instância.
    /// </summary>
    [JsonPropertyName("connectionStatus")]
    public string? ConnectionStatus { get; set; }

    /// <summary>
    /// Obtém ou define o JID do proprietário da instância.
    /// </summary>
    [JsonPropertyName("ownerJid")]
    public string? OwnerJid { get; set; }

    /// <summary>
    /// Obtém ou define o nome do perfil.
    /// </summary>
    [JsonPropertyName("profileName")]
    public string? ProfileName { get; set; }

    /// <summary>
    /// Obtém ou define a URL da foto do perfil.
    /// </summary>
    [JsonPropertyName("profilePicUrl")]
    public string? ProfilePicUrl { get; set; }

    /// <summary>
    /// Obtém ou define o tipo de integração.
    /// </summary>
    [JsonPropertyName("integration")]
    public string? Integration { get; set; }

    /// <summary>
    /// Obtém ou define o número de telefone associado à instância.
    /// </summary>
    [JsonPropertyName("number")]
    public string? Number { get; set; }

    /// <summary>
    /// Obtém ou define o ID de negócio (para contas business).
    /// </summary>
    [JsonPropertyName("businessId")]
    public string? BusinessId { get; set; }

    /// <summary>
    /// Obtém ou define o token de autenticação.
    /// </summary>
    [JsonPropertyName("token")]
    public string? Token { get; set; }

    /// <summary>
    /// Obtém ou define o nome do cliente.
    /// </summary>
    [JsonPropertyName("clientName")]
    public string? ClientName { get; set; }

    /// <summary>
    /// Obtém ou define o código de razão da desconexão.
    /// </summary>
    [JsonPropertyName("disconnectionReasonCode")]
    public string? DisconnectionReasonCode { get; set; }

    /// <summary>
    /// Obtém ou define o objeto de desconexão.
    /// </summary>
    [JsonPropertyName("disconnectionObject")]
    public object? DisconnectionObject { get; set; }

    /// <summary>
    /// Obtém ou define a data e hora da desconexão.
    /// </summary>
    [JsonPropertyName("disconnectionAt")]
    public DateTime? DisconnectionAt { get; set; }

    /// <summary>
    /// Obtém ou define a data e hora de criação da instância.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Obtém ou define a data e hora da última atualização da instância.
    /// </summary>
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Obtém ou define as configurações do Chatwoot.
    /// </summary>
    [JsonPropertyName("Chatwoot")]
    public object? Chatwoot { get; set; }

    /// <summary>
    /// Obtém ou define as configurações de proxy.
    /// </summary>
    [JsonPropertyName("Proxy")]
    public object? Proxy { get; set; }

    /// <summary>
    /// Obtém ou define as configurações do RabbitMQ.
    /// </summary>
    [JsonPropertyName("Rabbitmq")]
    public object? Rabbitmq { get; set; }

    /// <summary>
    /// Obtém ou define as configurações do SQS.
    /// </summary>
    [JsonPropertyName("Sqs")]
    public object? Sqs { get; set; }

    /// <summary>
    /// Obtém ou define as configurações do WebSocket.
    /// </summary>
    [JsonPropertyName("Websocket")]
    public object? Websocket { get; set; }

    /// <summary>
    /// Obtém ou define as configurações da instância.
    /// </summary>
    [JsonPropertyName("Setting")]
    public InstanceSettingV2? Setting { get; set; }

    /// <summary>
    /// Obtém ou define as contagens de mensagens, contatos e chats.
    /// </summary>
    [JsonPropertyName("_count")]
    public InstanceCountV2? Count { get; set; }
}

/// <summary>
/// Representa as configurações de uma instância.
/// </summary>
public class InstanceSettingV2
{
    /// <summary>
    /// Obtém ou define o ID da configuração.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Obtém ou define se as chamadas devem ser rejeitadas.
    /// </summary>
    [JsonPropertyName("rejectCall")]
    public bool RejectCall { get; set; }

    /// <summary>
    /// Obtém ou define a mensagem para chamadas.
    /// </summary>
    [JsonPropertyName("msgCall")]
    public string? MsgCall { get; set; }

    /// <summary>
    /// Obtém ou define se os grupos devem ser ignorados.
    /// </summary>
    [JsonPropertyName("groupsIgnore")]
    public bool GroupsIgnore { get; set; }

    /// <summary>
    /// Obtém ou define se o status online deve ser mantido.
    /// </summary>
    [JsonPropertyName("alwaysOnline")]
    public bool AlwaysOnline { get; set; }

    /// <summary>
    /// Obtém ou define se as mensagens devem ser marcadas como lidas.
    /// </summary>
    [JsonPropertyName("readMessages")]
    public bool ReadMessages { get; set; }

    /// <summary>
    /// Obtém ou define se os status devem ser marcados como lidos.
    /// </summary>
    [JsonPropertyName("readStatus")]
    public bool ReadStatus { get; set; }

    /// <summary>
    /// Obtém ou define se o histórico completo deve ser sincronizado.
    /// </summary>
    [JsonPropertyName("syncFullHistory")]
    public bool SyncFullHistory { get; set; }

    /// <summary>
    /// Obtém ou define o token VOIP do WhatsApp.
    /// </summary>
    [JsonPropertyName("wavoipToken")]
    public string? WavoipToken { get; set; }

    /// <summary>
    /// Obtém ou define a data e hora de criação da configuração.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Obtém ou define a data e hora da última atualização da configuração.
    /// </summary>
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Obtém ou define o ID da instância associada.
    /// </summary>
    [JsonPropertyName("instanceId")]
    public string? InstanceId { get; set; }
}

/// <summary>
/// Representa as contagens de mensagens, contatos e chats de uma instância.
/// </summary>
public class InstanceCountV2
{
    /// <summary>
    /// Obtém ou define o número de mensagens.
    /// </summary>
    [JsonPropertyName("Message")]
    public int Message { get; set; }

    /// <summary>
    /// Obtém ou define o número de contatos.
    /// </summary>
    [JsonPropertyName("Contact")]
    public int Contact { get; set; }

    /// <summary>
    /// Obtém ou define o número de chats.
    /// </summary>
    [JsonPropertyName("Chat")]
    public int Chat { get; set; }
}