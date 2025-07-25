using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Group;

/// <summary>
/// Representa a requisição para atualizar a foto de um grupo.
/// </summary>
public class UpdateGroupPictureRequest
{
    /// <summary>
    /// URL da nova imagem do perfil do grupo.
    /// </summary>
    [JsonPropertyName("image")]
    public required string Image { get; set; }
}