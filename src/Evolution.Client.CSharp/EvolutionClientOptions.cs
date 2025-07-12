namespace Evolution.Client.CSharp;

/// <summary>
/// Opções de configuração para o EvolutionClient
/// </summary>
public class EvolutionClientOptions
{
    /// <summary>
    /// URL base da API Evolution
    /// </summary>
    public string BaseUrl { get; set; } = string.Empty;

    /// <summary>
    /// Chave de API para autenticação
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// Timeout para requisições HTTP (padrão: 30 segundos)
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// User-Agent personalizado para as requisições
    /// </summary>
    public string UserAgent { get; set; } = "Evolution.Client.CSharp.CSharp/1.0.0";

    /// <summary>
    /// Indica se deve validar certificados SSL (padrão: true)
    /// </summary>
    public bool ValidateSslCertificate { get; set; } = true;

    /// <summary>
    /// Headers customizados para incluir em todas as requisições
    /// </summary>
    public Dictionary<string, string> CustomHeaders { get; set; } = new();

    /// <summary>
    /// Indica se deve fazer log das requisições HTTP (padrão: false)
    /// </summary>
    public bool LogHttpRequests { get; set; } = false;

    /// <summary>
    /// Indica se deve fazer log do corpo das requisições (padrão: false)
    /// Atenção: pode expor dados sensíveis nos logs
    /// </summary>
    public bool LogRequestBody { get; set; } = false;

    /// <summary>
    /// Indica se deve fazer log do corpo das respostas (padrão: false)
    /// </summary>
    public bool LogResponseBody { get; set; } = false;
}
