using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Instance;

/// <summary>
/// Representa a resposta do endpoint fetch-instances da API Evolution.
/// </summary>
public class InstanceResponse
{
    /// <summary>
    /// Obtém ou define os detalhes da instância.
    /// </summary>
    [JsonPropertyName("instance")]
    public InstanceDetails? Instance { get; set; }
}

/// <summary>
/// Representa os detalhes de uma instância do WhatsApp.
/// </summary>
public class InstanceDetails
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
    /// Obtém ou define o proprietário da instância (JID do WhatsApp).
    /// </summary>
    [JsonPropertyName("owner")]
    public string? Owner { get; set; }

    /// <summary>
    /// Obtém ou define o nome do perfil.
    /// </summary>
    [JsonPropertyName("profileName")]
    public string? ProfileName { get; set; }

    /// <summary>
    /// Obtém ou define a URL da foto do perfil.
    /// </summary>
    [JsonPropertyName("profilePictureUrl")]
    public string? ProfilePictureUrl { get; set; }

    /// <summary>
    /// Obtém ou define o status do perfil.
    /// </summary>
    [JsonPropertyName("profileStatus")]
    public string? ProfileStatus { get; set; }

    /// <summary>
    /// Obtém ou define o status da instância (open, close, connecting, etc.).
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Obtém ou define a URL do servidor.
    /// </summary>
    [JsonPropertyName("serverUrl")]
    public string? ServerUrl { get; set; }

    /// <summary>
    /// Obtém ou define a chave de API.
    /// </summary>
    [JsonPropertyName("apikey")]
    public string? ApiKey { get; set; }

    /// <summary>
    /// Obtém ou define as informações de integração.
    /// </summary>
    [JsonPropertyName("integration")]
    public IntegrationDetails? Integration { get; set; }
}

/// <summary>
/// Representa os detalhes de integração de uma instância.
/// </summary>
public class IntegrationDetails
{
    /// <summary>
    /// Obtém ou define o tipo de integração.
    /// </summary>
    [JsonPropertyName("integration")]
    public string? IntegrationType { get; set; }

    /// <summary>
    /// Obtém ou define o token de integração.
    /// </summary>
    [JsonPropertyName("token")]
    public string? Token { get; set; }

    /// <summary>
    /// Obtém ou define a URL do webhook para o WhatsApp Business.
    /// </summary>
    [JsonPropertyName("webhook_wa_business")]
    public string? WebhookWaBusiness { get; set; }
}