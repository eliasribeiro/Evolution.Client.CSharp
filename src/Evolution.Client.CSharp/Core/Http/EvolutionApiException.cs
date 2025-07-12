using System.Net;

namespace Evolution.Client.CSharp.Core.Http;

/// <summary>
/// Exceção específica para erros da Evolution API
/// </summary>
public class EvolutionApiException : Exception
{
    /// <summary>
    /// Código de status HTTP da resposta
    /// </summary>
    public HttpStatusCode? StatusCode { get; }

    /// <summary>
    /// Conteúdo da resposta de erro
    /// </summary>
    public string? ResponseContent { get; }

    /// <summary>
    /// Inicializa uma nova instância da exceção
    /// </summary>
    /// <param name="message">Mensagem de erro</param>
    public EvolutionApiException(string message) : base(message)
    {
    }

    /// <summary>
    /// Inicializa uma nova instância da exceção
    /// </summary>
    /// <param name="message">Mensagem de erro</param>
    /// <param name="innerException">Exceção interna</param>
    public EvolutionApiException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Inicializa uma nova instância da exceção
    /// </summary>
    /// <param name="message">Mensagem de erro</param>
    /// <param name="statusCode">Código de status HTTP</param>
    /// <param name="responseContent">Conteúdo da resposta</param>
    public EvolutionApiException(string message, HttpStatusCode statusCode, string? responseContent = null) 
        : base(message)
    {
        StatusCode = statusCode;
        ResponseContent = responseContent;
    }

    /// <summary>
    /// Inicializa uma nova instância da exceção
    /// </summary>
    /// <param name="message">Mensagem de erro</param>
    /// <param name="statusCode">Código de status HTTP</param>
    /// <param name="responseContent">Conteúdo da resposta</param>
    /// <param name="innerException">Exceção interna</param>
    public EvolutionApiException(
        string message, 
        HttpStatusCode statusCode, 
        string? responseContent, 
        Exception innerException) 
        : base(message, innerException)
    {
        StatusCode = statusCode;
        ResponseContent = responseContent;
    }

    /// <summary>
    /// Retorna uma representação em string da exceção
    /// </summary>
    public override string ToString()
    {
        var result = base.ToString();
        
        if (StatusCode.HasValue)
        {
            result += $"\nStatus Code: {StatusCode}";
        }
        
        if (!string.IsNullOrWhiteSpace(ResponseContent))
        {
            result += $"\nResponse Content: {ResponseContent}";
        }
        
        return result;
    }
}
