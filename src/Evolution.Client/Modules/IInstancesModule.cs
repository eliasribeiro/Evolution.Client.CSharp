namespace Evolution.Client.Modules;

/// <summary>
/// Interface para operações relacionadas a instâncias
/// </summary>
public interface IInstancesModule
{
    /// <summary>
    /// Cria uma nova instância
    /// </summary>
    /// <param name="request">Dados para criação da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Dados da instância criada</returns>
    Task<CreateInstanceResponse> CreateAsync(
        CreateInstanceRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém informações de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Informações da instância</returns>
    Task<InstanceInfo> GetAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lista todas as instâncias
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista de instâncias</returns>
    Task<IEnumerable<InstanceInfo>> ListAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deleta uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task DeleteAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Conecta uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Dados de conexão</returns>
    Task<ConnectInstanceResponse> ConnectAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Desconecta uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task DisconnectAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Reinicia uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task RestartAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém o status de conexão de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Status de conexão</returns>
    Task<ConnectionStatus> GetConnectionStatusAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Define a presença de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados da presença</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da definição de presença</returns>
    Task<SetPresenceResponse> SetPresenceAsync(
        string instanceName,
        SetPresenceRequest request,
        CancellationToken cancellationToken = default);
}
