namespace Evolution.Client.CSharp.WebApp.Models;

/// <summary>
/// ViewModel para conectar uma instância e exibir QR code.
/// </summary>
public class ConnectInstanceViewModel
{
    /// <summary>
    /// Nome da instância.
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// QR code em formato Base64.
    /// </summary>
    public string QrCodeBase64 { get; set; } = string.Empty;

    /// <summary>
    /// Código de pareamento.
    /// </summary>
    public string? PairingCode { get; set; }

    /// <summary>
    /// Contador de tentativas.
    /// </summary>
    public int? Count { get; set; }
}