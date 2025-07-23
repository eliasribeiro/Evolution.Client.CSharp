using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Instance;

/// <summary>
/// Representa a resposta do endpoint /instance/logout/{instance} da API Evolution.
/// </summary>
public class LogoutInstanceResponse
{
    /// <summary>
    /// Obtém ou define o status da operação.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Obtém ou define se houve erro na operação.
    /// </summary>
    [JsonPropertyName("error")]
    public bool Error { get; set; }

    /// <summary>
    /// Obtém ou define a resposta detalhada da operação.
    /// </summary>
    [JsonPropertyName("response")]
    public LogoutResponseDetails? Response { get; set; }
}

/// <summary>
/// Representa os detalhes da resposta de logout.
/// </summary>
public class LogoutResponseDetails
{
    /// <summary>
    /// Obtém ou define a mensagem de resposta.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}

/// <summary>
/// Representa a resposta de erro do logout da instância.
/// </summary>
public class LogoutInstanceErrorResponse
{
    /// <summary>
    /// Obtém ou define o código de status HTTP.
    /// </summary>
    [JsonPropertyName("status")]
    public int Status { get; set; }

    /// <summary>
    /// Obtém ou define o tipo de erro.
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>
    /// Obtém ou define a resposta detalhada do erro.
    /// </summary>
    [JsonPropertyName("response")]
    public LogoutErrorResponseDetails? Response { get; set; }
}

/// <summary>
/// Representa os detalhes da resposta de erro do logout.
/// </summary>
public class LogoutErrorResponseDetails
{
    /// <summary>
    /// Obtém ou define as mensagens de erro.
    /// </summary>
    [JsonPropertyName("message")]
    public string[]? Message { get; set; }
}