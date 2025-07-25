using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Group;

/// <summary>
/// Representa a requisição para criar um grupo.
/// </summary>
public class CreateGroupRequest
{
    /// <summary>
    /// Assunto (nome) do grupo.
    /// </summary>
    [JsonPropertyName("subject")]
    public required string Subject { get; set; }

    /// <summary>
    /// Descrição do grupo.
    /// </summary>
    [JsonPropertyName("description")]
    public required string Description { get; set; }

    /// <summary>
    /// Lista de números de telefone dos participantes do grupo (com código do país).
    /// </summary>
    [JsonPropertyName("participants")]
    public required List<string> Participants { get; set; }
}