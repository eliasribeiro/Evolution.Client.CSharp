using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Profile;

/// <summary>
/// Representa a resposta da remoção da foto do perfil.
/// </summary>
public class RemoveProfilePictureResponse
{
    /// <summary>
    /// Indica se a operação foi bem-sucedida.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// Mensagem de retorno da operação.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}