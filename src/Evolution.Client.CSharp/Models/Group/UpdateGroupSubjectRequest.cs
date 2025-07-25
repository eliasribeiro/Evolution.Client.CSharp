using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Group;

/// <summary>
/// Representa a requisição para atualizar o assunto de um grupo.
/// </summary>
public class UpdateGroupSubjectRequest
{
    /// <summary>
    /// Novo assunto (nome) do grupo.
    /// </summary>
    [JsonPropertyName("subject")]
    public required string Subject { get; set; }
}