using Evolution.Client.CSharp.Models.Instance;

namespace Evolution.Client.CSharp.Interfaces;

/// <summary>
/// Interface para o serviço de instâncias da API Evolution.
/// </summary>
public interface IEvolutionInstanceService
{
    /// <summary>
    /// Obtém todas as instâncias disponíveis.
    /// </summary>
    /// <returns>Uma lista de instâncias disponíveis.</returns>
    /// <remarks>
    /// Este método faz uma requisição GET para o endpoint /instance/fetchInstances.
    /// </remarks>
    Task<InstancesResponse> FetchInstancesAsync();
    
    /// <summary>
    /// Obtém todas as instâncias disponíveis usando o modelo V2.
    /// </summary>
    /// <returns>Uma lista de instâncias disponíveis no formato V2.</returns>
    /// <remarks>
    /// Este método faz uma requisição GET para o endpoint /instance/fetchInstances e retorna os dados no formato V2.
    /// </remarks>
    Task<InstancesResponseV2> FetchInstancesV2Async();
}