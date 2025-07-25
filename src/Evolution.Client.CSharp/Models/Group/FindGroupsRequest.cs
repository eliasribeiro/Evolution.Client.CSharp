using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Group;

/// <summary>
/// Representa a requisição para buscar grupos.
/// </summary>
public class FindGroupsRequest
{
    /// <summary>
    /// Indica se deve buscar os participantes dos grupos.
    /// </summary>
    [JsonPropertyName("getParticipants")]
    public bool GetParticipants { get; set; }
}