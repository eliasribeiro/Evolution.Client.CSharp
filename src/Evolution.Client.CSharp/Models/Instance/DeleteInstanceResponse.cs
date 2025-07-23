using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Instance;

/// <summary>
/// Resposta de sucesso para a operação de deletar instância
/// </summary>
public class DeleteInstanceResponse
{
    /// <summary>
    /// Status da resposta
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Indica se houve erro
    /// </summary>
    [JsonPropertyName("error")]
    public bool Error { get; set; }

    /// <summary>
    /// Detalhes da resposta
    /// </summary>
    [JsonPropertyName("response")]
    public DeleteResponseDetails Response { get; set; } = new();
}

/// <summary>
/// Detalhes da resposta de sucesso para deletar instância
/// </summary>
public class DeleteResponseDetails
{
    /// <summary>
    /// Mensagem de confirmação
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}

/// <summary>
/// Resposta de erro para a operação de deletar instância
/// </summary>
public class DeleteInstanceErrorResponse
{
    /// <summary>
    /// Status da resposta (código de erro)
    /// </summary>
    [JsonPropertyName("status")]
    public int Status { get; set; }

    /// <summary>
    /// Tipo do erro
    /// </summary>
    [JsonPropertyName("error")]
    public string Error { get; set; } = string.Empty;

    /// <summary>
    /// Detalhes da resposta de erro
    /// </summary>
    [JsonPropertyName("response")]
    public DeleteErrorResponseDetails Response { get; set; } = new();
}

/// <summary>
/// Detalhes da resposta de erro para deletar instância
/// </summary>
public class DeleteErrorResponseDetails
{
    /// <summary>
    /// Lista de mensagens de erro
    /// </summary>
    [JsonPropertyName("message")]
    public List<string> Message { get; set; } = new();
}