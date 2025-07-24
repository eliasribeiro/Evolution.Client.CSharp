using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Profile;

/// <summary>
/// Representa a requisição para buscar o perfil do usuário.
/// </summary>
public class FetchProfileRequest
{
    /// <summary>
    /// Número do telefone com código do país.
    /// </summary>
    [JsonPropertyName("number")]
    public string Number { get; set; } = string.Empty;
}