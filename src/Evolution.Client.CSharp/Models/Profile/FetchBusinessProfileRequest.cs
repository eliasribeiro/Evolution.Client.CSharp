using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Profile;

/// <summary>
/// Representa a requisição para buscar o perfil de negócio.
/// </summary>
public class FetchBusinessProfileRequest
{
    /// <summary>
    /// Número do telefone com código do país.
    /// </summary>
    [JsonPropertyName("number")]
    public string Number { get; set; } = string.Empty;
}