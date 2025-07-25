using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Group;

/// <summary>
/// Representa a resposta do envio de convite de grupo.
/// </summary>
public class SendGroupInviteResponse
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
    /// Lista de resultados para cada número.
    /// </summary>
    [JsonPropertyName("results")]
    public List<InviteResult>? Results { get; set; }
}

/// <summary>
/// Representa o resultado do envio de convite para um número específico.
/// </summary>
public class InviteResult
{
    /// <summary>
    /// Número de telefone.
    /// </summary>
    [JsonPropertyName("number")]
    public string? Number { get; set; }

    /// <summary>
    /// Indica se o convite foi enviado com sucesso.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// Mensagem de status.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}