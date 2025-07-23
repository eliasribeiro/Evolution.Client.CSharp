using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Instance;

/// <summary>
/// Representa a resposta do endpoint /instance/connectionState/{instance} da API Evolution.
/// </summary>
public class ConnectionStateResponse
{
    /// <summary>
    /// Obtém ou define as informações da instância e seu estado de conexão.
    /// </summary>
    [JsonPropertyName("instance")]
    public InstanceConnectionState? Instance { get; set; }
}

/// <summary>
/// Representa as informações de estado de conexão de uma instância.
/// </summary>
public class InstanceConnectionState
{
    /// <summary>
    /// Obtém ou define o nome da instância.
    /// </summary>
    [JsonPropertyName("instanceName")]
    public string? InstanceName { get; set; }

    /// <summary>
    /// Obtém ou define o estado de conexão da instância.
    /// Valores possíveis: "open", "close", "connecting", etc.
    /// </summary>
    [JsonPropertyName("state")]
    public string? State { get; set; }
}