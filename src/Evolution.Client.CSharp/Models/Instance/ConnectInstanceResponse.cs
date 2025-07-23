using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Instance;

/// <summary>
/// Representa a resposta do endpoint /instance/connect da API Evolution.
/// </summary>
public class ConnectInstanceResponse
{
    /// <summary>
    /// Obtém ou define o código de pareamento (pairing code).
    /// </summary>
    [JsonPropertyName("pairingCode")]
    public string? PairingCode { get; set; }

    /// <summary>
    /// Obtém ou define o código de conexão.
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>
    /// Obtém ou define a imagem do QR code em formato base64.
    /// </summary>
    [JsonPropertyName("base64")]
    public string? Base64 { get; set; }

    /// <summary>
    /// Obtém ou define o contador de tentativas de conexão.
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }
}