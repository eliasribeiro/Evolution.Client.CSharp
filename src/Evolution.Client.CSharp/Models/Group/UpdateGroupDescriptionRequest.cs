using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Group;

/// <summary>
/// Representa a requisição para atualizar a descrição de um grupo.
/// </summary>
public class UpdateGroupDescriptionRequest
{
    /// <summary>
    /// Nova descrição do grupo.
    /// </summary>
    [JsonPropertyName("description")]
    public required string Description { get; set; }
}