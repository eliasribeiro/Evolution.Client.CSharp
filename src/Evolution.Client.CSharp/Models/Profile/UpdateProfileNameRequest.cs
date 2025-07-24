using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Profile;

/// <summary>
/// Representa a requisição para atualizar o nome do perfil.
/// </summary>
public class UpdateProfileNameRequest
{
    /// <summary>
    /// Novo nome para o perfil.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}