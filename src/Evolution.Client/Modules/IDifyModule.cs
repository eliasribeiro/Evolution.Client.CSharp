namespace Evolution.Client.Modules;

/// <summary>
/// Interface para operações relacionadas ao Dify
/// </summary>
public interface IDifyModule
{
    /// <summary>
    /// Cria bot Dify para uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Configuração do bot Dify</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da criação do bot</returns>
    Task<DifyBotResponse> CreateBotAsync(
        string instanceName,
        CreateDifyBotRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca bots Dify de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista de bots Dify</returns>
    Task<DifyBotListResponse> FindBotsAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca bot Dify específico de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Configuração do bot Dify</returns>
    Task<DifyBotResponse> FindBotAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza configuração do bot Dify
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Nova configuração do bot</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da atualização</returns>
    Task<DifyBotResponse> UpdateBotAsync(
        string instanceName,
        UpdateDifyBotRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Configura settings do Dify
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Configurações do Dify</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da configuração</returns>
    Task<DifySettingsResponse> SetSettingsAsync(
        string instanceName,
        DifySettingsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca settings do Dify
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Configurações do Dify</returns>
    Task<DifySettingsResponse> FindSettingsAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Altera status do bot Dify
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados para alterar o status</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da alteração</returns>
    Task<DifyStatusResponse> ChangeStatusAsync(
        string instanceName,
        ChangeDifyStatusRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca status do bot Dify
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="number">Número do contato</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Status do bot</returns>
    Task<DifyStatusResponse> FindStatusAsync(
        string instanceName,
        string number,
        CancellationToken cancellationToken = default);
}
