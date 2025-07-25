using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Group;

/// <summary>
/// Representa a requisição para enviar convite de grupo.
/// </summary>
public class SendGroupInviteRequest
{
    /// <summary>
    /// Lista de números de telefone para enviar o convite (com código do país).
    /// </summary>
    [JsonPropertyName("numbers")]
    public required List<string> Numbers { get; set; }

    /// <summary>
    /// Texto personalizado para o convite (opcional).
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }
}