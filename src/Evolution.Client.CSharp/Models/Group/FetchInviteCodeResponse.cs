using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Group;

/// <summary>
/// Representa a resposta da busca do código de convite de um grupo.
/// </summary>
public class FetchInviteCodeResponse
{
    /// <summary>
    /// URL do convite do grupo WhatsApp.
    /// </summary>
    [JsonPropertyName("inviteUrl")]
    public string? InviteUrl { get; set; }

    /// <summary>
    /// Código do convite do grupo WhatsApp.
    /// </summary>
    [JsonPropertyName("inviteCode")]
    public string? InviteCode { get; set; }
}