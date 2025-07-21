using Evolution.Client.CSharp.Models;

namespace Evolution.Client.CSharp.Interfaces;

/// <summary>
/// Interface para o serviço de informações da API Evolution.
/// </summary>
public interface IEvolutionInformationService
{
    /// <summary>
    /// Obtém informações sobre a API Evolution.
    /// </summary>
    /// <returns>Um objeto contendo informações sobre a API.</returns>
    Task<InformationResponse> GetInformationAsync();
}