using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Profile;

/// <summary>
/// Representa a resposta da atualização das configurações de privacidade.
/// </summary>
public class UpdatePrivacySettingsResponse
{
    /// <summary>
    /// Indica se a operação foi bem-sucedida.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// Mensagem de retorno da operação.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}