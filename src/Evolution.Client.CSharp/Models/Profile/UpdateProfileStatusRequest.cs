using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Profile;

/// <summary>
/// Representa a requisição para atualizar o status do perfil.
/// </summary>
public class UpdateProfileStatusRequest
{
    /// <summary>
    /// Novo status para o perfil.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}