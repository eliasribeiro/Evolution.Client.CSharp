namespace Evolution.Client.CSharp.Models;

/// <summary>
/// Representa a resposta base da API Evolution.
/// </summary>
public class ApiResponse
{
    /// <summary>
    /// Obtém ou define o código de status HTTP da resposta.
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Obtém ou define a mensagem descritiva da resposta.
    /// </summary>
    public string? Message { get; set; }
}