namespace Evolution.Client.CSharp.Configuration;

/// <summary>
/// Opções de configuração para o cliente da API Evolution.
/// </summary>
public class EvolutionApiOptions
{
    /// <summary>
    /// Obtém ou define a URL base da API Evolution.
    /// </summary>
    public string BaseUrl { get; set; } = "http://localhost:8080";

    /// <summary>
    /// Obtém ou define a chave de API para autenticação.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// Obtém ou define o tempo limite para as requisições HTTP em segundos.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;
}