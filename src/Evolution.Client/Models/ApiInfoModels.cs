namespace Evolution.Client.Models;

/// <summary>
/// Informações da API Evolution
/// </summary>
public class ApiInformation
{
    /// <summary>
    /// Status da API
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Mensagem de boas-vindas
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Versão da API
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// URL da documentação Swagger
    /// </summary>
    public string Swagger { get; set; } = string.Empty;

    /// <summary>
    /// URL do manager
    /// </summary>
    public string Manager { get; set; } = string.Empty;

    /// <summary>
    /// URL da documentação
    /// </summary>
    public string Documentation { get; set; } = string.Empty;
}
