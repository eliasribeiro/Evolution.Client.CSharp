using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Instance;

/// <summary>
/// Representa a resposta da criação de uma instância na API Evolution.
/// </summary>
public class CreateInstanceResponse
{
    /// <summary>
    /// Obtém ou define as informações da instância criada.
    /// </summary>
    [JsonPropertyName("instance")]
    public CreatedInstance? Instance { get; set; }

    /// <summary>
    /// Obtém ou define o hash da instância.
    /// </summary>
    [JsonPropertyName("hash")]
    public string? Hash { get; set; }

    /// <summary>
    /// Obtém ou define as configurações do webhook.
    /// </summary>
    [JsonPropertyName("webhook")]
    public object? Webhook { get; set; }

    /// <summary>
    /// Obtém ou define as configurações do websocket.
    /// </summary>
    [JsonPropertyName("websocket")]
    public object? Websocket { get; set; }

    /// <summary>
    /// Obtém ou define as configurações do RabbitMQ.
    /// </summary>
    [JsonPropertyName("rabbitmq")]
    public object? Rabbitmq { get; set; }

    /// <summary>
    /// Obtém ou define as configurações do SQS.
    /// </summary>
    [JsonPropertyName("sqs")]
    public object? Sqs { get; set; }

    /// <summary>
    /// Obtém ou define as configurações da instância.
    /// </summary>
    [JsonPropertyName("settings")]
    public InstanceSettings? Settings { get; set; }
}

/// <summary>
/// Representa as informações da instância criada.
/// </summary>
public class CreatedInstance
{
    /// <summary>
    /// Obtém ou define o nome da instância.
    /// </summary>
    [JsonPropertyName("instanceName")]
    public string? InstanceName { get; set; }

    /// <summary>
    /// Obtém ou define o ID da instância.
    /// </summary>
    [JsonPropertyName("instanceId")]
    public string? InstanceId { get; set; }

    /// <summary>
    /// Obtém ou define o tipo de integração.
    /// </summary>
    [JsonPropertyName("integration")]
    public string? Integration { get; set; }

    /// <summary>
    /// Obtém ou define o webhook do WhatsApp Business.
    /// </summary>
    [JsonPropertyName("webhookWaBusiness")]
    public string? WebhookWaBusiness { get; set; }

    /// <summary>
    /// Obtém ou define o token de acesso do WhatsApp Business.
    /// </summary>
    [JsonPropertyName("accessTokenWaBusiness")]
    public string? AccessTokenWaBusiness { get; set; }

    /// <summary>
    /// Obtém ou define o status da instância.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }
}

/// <summary>
/// Representa as configurações da instância criada.
/// </summary>
public class InstanceSettings
{
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
    /// Obtém ou define o token WAVOIP.
    /// </summary>
    [JsonPropertyName("wavoipToken")]
    public string? WavoipToken { get; set; }
}

/// <summary>
/// Representa a resposta de erro da API Evolution.
/// </summary>
public class CreateInstanceErrorResponse
{
    /// <summary>
    /// Obtém ou define o código de status HTTP.
    /// </summary>
    [JsonPropertyName("status")]
    public int Status { get; set; }

    /// <summary>
    /// Obtém ou define o tipo de erro.
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>
    /// Obtém ou define a resposta detalhada do erro.
    /// </summary>
    [JsonPropertyName("response")]
    public ErrorResponseDetails? Response { get; set; }
}

/// <summary>
/// Representa os detalhes da resposta de erro.
/// </summary>
public class ErrorResponseDetails
{
    /// <summary>
    /// Obtém ou define as mensagens de erro.
    /// </summary>
    [JsonPropertyName("message")]
    public string[]? Message { get; set; }
}