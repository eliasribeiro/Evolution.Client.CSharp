using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Group;

/// <summary>
/// Representa a resposta da revogação do código de convite de um grupo.
/// </summary>
public class RevokeInviteCodeResponse
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

    /// <summary>
    /// Novo código de convite gerado.
    /// </summary>
    [JsonPropertyName("inviteCode")]
    public string? InviteCode { get; set; }
}