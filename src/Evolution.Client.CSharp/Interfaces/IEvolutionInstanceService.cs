using Evolution.Client.CSharp.Models.Instance;

namespace Evolution.Client.CSharp.Interfaces;

/// <summary>
/// Interface para o serviço de instâncias da API Evolution.
/// </summary>
public interface IEvolutionInstanceService
{
    /// <summary>
    /// Cria uma nova instância na API Evolution.
    /// </summary>
    /// <param name="request">Os dados da instância a ser criada.</param>
    /// <returns>A resposta contendo as informações da instância criada.</returns>
    /// <remarks>
    /// Este método faz uma requisição POST para o endpoint /instance/create.
    /// </remarks>
    Task<CreateInstanceResponse> CreateInstanceAsync(CreateInstanceRequest request);

    /// <summary>
    /// Conecta uma instância existente e retorna os dados de conexão (QR code).
    /// </summary>
    /// <param name="instanceName">O nome da instância a ser conectada.</param>
    /// <returns>A resposta contendo os dados de conexão, incluindo o QR code.</returns>
    /// <remarks>
    /// Este método faz uma requisição GET para o endpoint /instance/connect/{instanceName}.
    /// </remarks>
    Task<ConnectInstanceResponse> ConnectInstanceAsync(string instanceName);

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
    Task<InstancesResponse> FetchInstancesV2Async();

    /// <summary>
    /// Obtém o estado de conexão de uma instância específica.
    /// </summary>
    /// <param name="instanceName">O nome da instância para verificar o estado de conexão.</param>
    /// <returns>A resposta contendo o estado de conexão da instância.</returns>
    /// <remarks>
    /// Este método faz uma requisição GET para o endpoint /instance/connectionState/{instanceName}.
    /// Os estados possíveis incluem: "open", "close", "connecting", etc.
    /// </remarks>
    Task<ConnectionStateResponse> GetConnectionStateAsync(string instanceName);

    /// <summary>
    /// Faz logout de uma instância específica.
    /// </summary>
    /// <param name="instanceName">O nome da instância para fazer logout.</param>
    /// <returns>A resposta contendo o resultado da operação de logout.</returns>
    /// <remarks>
    /// Este método faz uma requisição DELETE para o endpoint /instance/logout/{instanceName}.
    /// A instância deve estar conectada para que o logout seja bem-sucedido.
    /// </remarks>
    Task<LogoutInstanceResponse> LogoutInstanceAsync(string instanceName);
}
