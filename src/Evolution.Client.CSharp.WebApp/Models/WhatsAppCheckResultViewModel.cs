namespace Evolution.Client.CSharp.WebApp.Models;

/// <summary>
/// ViewModel para resultado da verificação de números do WhatsApp.
/// </summary>
public class WhatsAppCheckResultViewModel
{
    /// <summary>
    /// Lista de resultados da verificação.
    /// </summary>
    public IEnumerable<WhatsAppCheckResult> Results { get; set; } = new List<WhatsAppCheckResult>();

    /// <summary>
    /// Indica se houve erro na verificação.
    /// </summary>
    public bool HasError { get; set; }

    /// <summary>
    /// Mensagem de erro, se houver.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Total de resultados.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Nome da instância.
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Números verificados.
    /// </summary>
    public string Numbers { get; set; } = string.Empty;
}