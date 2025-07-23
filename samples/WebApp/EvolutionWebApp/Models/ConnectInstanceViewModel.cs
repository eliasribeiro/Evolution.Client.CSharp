using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.Models;

/// <summary>
/// Modelo para exibir os dados de conexão de uma instância.
/// </summary>
public class ConnectInstanceViewModel
{
    /// <summary>
    /// Nome da instância.
    /// </summary>
    [Display(Name = "Nome da Instância")]
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// QR code em formato Base64 para escaneamento.
    /// </summary>
    [Display(Name = "QR Code")]
    public string QrCodeBase64 { get; set; } = string.Empty;

    /// <summary>
    /// Código de pareamento para conexão sem QR code.
    /// </summary>
    [Display(Name = "Código de Pareamento")]
    public string? PairingCode { get; set; }

    /// <summary>
    /// Contagem de tentativas de conexão.
    /// </summary>
    [Display(Name = "Contagem")]
    public int Count { get; set; }
}