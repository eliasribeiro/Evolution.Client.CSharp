using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Profile;

/// <summary>
/// Representa a resposta da busca de perfil de negócio.
/// </summary>
public class FetchBusinessProfileResponse
{
    /// <summary>
    /// ID único do WhatsApp do usuário.
    /// </summary>
    [JsonPropertyName("wuid")]
    public string? Wuid { get; set; }

    /// <summary>
    /// Nome do negócio.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Categoria do negócio.
    /// </summary>
    [JsonPropertyName("category")]
    public string? Category { get; set; }

    /// <summary>
    /// Descrição do negócio.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Endereço do negócio.
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    /// <summary>
    /// Email do negócio.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Website do negócio.
    /// </summary>
    [JsonPropertyName("website")]
    public string? Website { get; set; }

    /// <summary>
    /// URL da foto de perfil.
    /// </summary>
    [JsonPropertyName("profilePictureUrl")]
    public string? ProfilePictureUrl { get; set; }
}