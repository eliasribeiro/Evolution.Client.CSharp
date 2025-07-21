namespace Evolution.Client.CSharp.Models;

/// <summary>
/// Representa a resposta do endpoint get-information da API Evolution.
/// </summary>
public class InformationResponse : ApiResponse
{
    /// <summary>
    /// Obtém ou define a versão atual da API.
    /// </summary>
    public string? Version { get; set; }

    /// <summary>
    /// Obtém ou define a URL para a documentação Swagger da API.
    /// </summary>
    public string? Swagger { get; set; }

    /// <summary>
    /// Obtém ou define a URL para o gerenciador da API.
    /// </summary>
    public string? Manager { get; set; }

    /// <summary>
    /// Obtém ou define a URL para a documentação detalhada da API.
    /// </summary>
    public string? Documentation { get; set; }
}