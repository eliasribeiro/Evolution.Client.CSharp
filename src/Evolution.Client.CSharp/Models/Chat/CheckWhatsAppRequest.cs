using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Chat;

/// <summary>
/// Representa a requisição para o endpoint /chat/whatsappNumbers/{instance} da API Evolution.
/// </summary>
public class CheckWhatsAppRequest
{
    /// <summary>
    /// Obtém ou define a lista de números de telefone para verificar.
    /// </summary>
    [JsonPropertyName("numbers")]
    public List<string> Numbers { get; set; } = new();
}