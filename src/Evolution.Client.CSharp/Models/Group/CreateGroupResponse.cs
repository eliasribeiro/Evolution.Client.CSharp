using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Group;

/// <summary>
/// Representa a resposta da criação de um grupo.
/// </summary>
public class CreateGroupResponse
{
    /// <summary>
    /// JID do grupo criado.
    /// </summary>
    [JsonPropertyName("groupJid")]
    public string? GroupJid { get; set; }

    /// <summary>
    /// Assunto (nome) do grupo.
    /// </summary>
    [JsonPropertyName("subject")]
    public string? Subject { get; set; }

    /// <summary>
    /// Descrição do grupo.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Lista de participantes do grupo.
    /// </summary>
    [JsonPropertyName("participants")]
    public List<GroupParticipant>? Participants { get; set; }
}

/// <summary>
/// Representa um participante do grupo.
/// </summary>
public class GroupParticipant
{
    /// <summary>
    /// JID do participante.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Indica se o participante é administrador.
    /// </summary>
    [JsonPropertyName("admin")]
    public bool? Admin { get; set; }
}