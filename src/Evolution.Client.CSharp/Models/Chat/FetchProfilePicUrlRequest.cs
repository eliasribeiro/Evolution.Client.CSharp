using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Chat;

/// <summary>
/// Representa a requisição para buscar a URL da foto de perfil.
/// </summary>
public class FetchProfilePicUrlRequest
{
    /// <summary>
    /// Número do WhatsApp para buscar a foto de perfil.
    /// </summary>
    [JsonPropertyName("number")]
    public string Number { get; set; } = string.Empty;
}
