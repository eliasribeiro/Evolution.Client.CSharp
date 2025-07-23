using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Chat;

/// <summary>
/// Representa a resposta da busca da URL da foto de perfil.
/// </summary>
public class FetchProfilePicUrlResponse
{
    /// <summary>
    /// WhatsApp User ID (WUID) do usuário.
    /// </summary>
    [JsonPropertyName("wuid")]
    public string Wuid { get; set; } = string.Empty;

    /// <summary>
    /// URL da foto de perfil do usuário.
    /// </summary>
    [JsonPropertyName("profilePictureUrl")]
    public string ProfilePictureUrl { get; set; } = string.Empty;
}
