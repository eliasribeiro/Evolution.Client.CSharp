using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Profile;

/// <summary>
/// Representa a resposta da busca de perfil do usuário.
/// </summary>
public class FetchProfileResponse
{
    /// <summary>
    /// ID único do WhatsApp do usuário.
    /// </summary>
    [JsonPropertyName("wuid")]
    public string? Wuid { get; set; }

    /// <summary>
    /// Nome do usuário.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Status do usuário.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// URL da foto de perfil.
    /// </summary>
    [JsonPropertyName("profilePictureUrl")]
    public string? ProfilePictureUrl { get; set; }

    /// <summary>
    /// Indica se o usuário possui foto de perfil.
    /// </summary>
    [JsonPropertyName("hasProfilePicture")]
    public bool HasProfilePicture { get; set; }
}