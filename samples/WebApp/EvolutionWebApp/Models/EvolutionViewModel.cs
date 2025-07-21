namespace EvolutionWebApp.Models;

/// <summary>
/// Modelo de visualização para exibir informações da API Evolution.
/// </summary>
public class EvolutionViewModel
{
    /// <summary>
    /// Obtém ou define as informações da API.
    /// </summary>
    public Evolution.Client.CSharp.Models.InformationResponse? ApiInformation { get; set; }
    
    /// <summary>
    /// Obtém ou define as instâncias disponíveis.
    /// </summary>
    public Evolution.Client.CSharp.Models.Instance.InstancesResponse? Instances { get; set; }
    
    /// <summary>
    /// Obtém ou define uma mensagem de erro, se houver.
    /// </summary>
    public string? ErrorMessage { get; set; }
}