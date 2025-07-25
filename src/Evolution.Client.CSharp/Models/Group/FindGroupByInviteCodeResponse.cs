using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Group;

/// <summary>
/// Representa a resposta da busca de grupo por código de convite.
/// </summary>
public class FindGroupByInviteCodeResponse
{
    /// <summary>
    /// JID do grupo.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

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
    /// Código de convite do grupo.
    /// </summary>
    [JsonPropertyName("inviteCode")]
    public string? InviteCode { get; set; }

    /// <summary>
    /// URL de convite do grupo.
    /// </summary>
    [JsonPropertyName("inviteUrl")]
    public string? InviteUrl { get; set; }

    /// <summary>
    /// Número de participantes no grupo.
    /// </summary>
    [JsonPropertyName("size")]
    public int? Size { get; set; }

    /// <summary>
    /// Criador do grupo.
    /// </summary>
    [JsonPropertyName("owner")]
    public string? Owner { get; set; }

    /// <summary>
    /// Data de criação do grupo.
    /// </summary>
    [JsonPropertyName("creation")]
    public long? Creation { get; set; }
}