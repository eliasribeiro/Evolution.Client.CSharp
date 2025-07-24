using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Profile;

/// <summary>
/// Representa a requisição para atualizar a foto do perfil.
/// </summary>
public class UpdateProfilePictureRequest
{
    /// <summary>
    /// URL da nova foto do perfil.
    /// </summary>
    [JsonPropertyName("picture")]
    public string Picture { get; set; } = string.Empty;
}