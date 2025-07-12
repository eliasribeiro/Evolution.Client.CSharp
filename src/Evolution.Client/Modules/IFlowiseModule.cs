namespace Evolution.Client.Modules;

/// <summary>
/// Interface para operações relacionadas ao Flowise
/// </summary>
public interface IFlowiseModule
{
    /// <summary>
    /// Cria bot Flowise para uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Configuração do bot Flowise</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da criação do bot</returns>
    Task<FlowiseBotResponse> CreateBotAsync(
        string instanceName,
        CreateFlowiseBotRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca todos os bots Flowise de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista de bots Flowise</returns>
    Task<FlowiseBotListResponse> FindBotsAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca bot Flowise específico de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Configuração do bot Flowise</returns>
    Task<FlowiseBotResponse> FindBotAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza configuração do bot Flowise
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Nova configuração do bot</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da atualização</returns>
    Task<FlowiseBotResponse> UpdateBotAsync(
        string instanceName,
        UpdateFlowiseBotRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove bot Flowise de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Task de conclusão</returns>
    Task DeleteBotAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Configura settings do Flowise
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Configurações do Flowise</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da configuração</returns>
    Task<FlowiseSettingsResponse> SetSettingsAsync(
        string instanceName,
        FlowiseSettingsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca settings do Flowise
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Configurações do Flowise</returns>
    Task<FlowiseSettingsResponse> FindSettingsAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Altera status da sessão do Flowise
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados para alterar o status</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da alteração</returns>
    Task<FlowiseSessionListResponse> ChangeSessionStatusAsync(
        string instanceName,
        ChangeFlowiseSessionStatusRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca sessões do Flowise
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista de sessões</returns>
    Task<FlowiseSessionListResponse> FindSessionsAsync(
        string instanceName,
        CancellationToken cancellationToken = default);
}
