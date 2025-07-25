using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Group;

/// <summary>
/// Representa a resposta da atualização da foto de um grupo.
/// </summary>
public class UpdateGroupPictureResponse
{
    /// <summary>
    /// Indica se a operação foi bem-sucedida.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// Mensagem de resposta.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}