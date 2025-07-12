namespace Evolution.Client.Modules;

/// <summary>
/// Interface para operações relacionadas ao Evolution Bot
/// </summary>
public interface IEvolutionBotModule
{
    /// <summary>
    /// Cria bot Evolution para uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Configuração do bot Evolution</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da criação do bot</returns>
    Task<EvolutionBotResponse> CreateBotAsync(
        string instanceName,
        CreateEvolutionBotRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca bot Evolution de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Configuração do bot Evolution</returns>
    Task<EvolutionBotResponse> FindBotAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca todos os bots Evolution de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista de bots Evolution</returns>
    Task<EvolutionBotListResponse> FetchBotsAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza configuração do bot Evolution
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Nova configuração do bot</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da atualização</returns>
    Task<EvolutionBotResponse> UpdateBotAsync(
        string instanceName,
        UpdateEvolutionBotRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove bot Evolution de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Task de conclusão</returns>
    Task DeleteBotAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Configura settings do bot Evolution
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Configurações do bot</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da configuração</returns>
    Task<EvolutionBotSettingsResponse> SetSettingsAsync(
        string instanceName,
        EvolutionBotSettingsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca settings do bot Evolution
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Configurações do bot</returns>
    Task<EvolutionBotSettingsResponse> FindSettingsAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Altera status da sessão do bot Evolution
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados para alterar o status</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da alteração</returns>
    Task<EvolutionBotSessionResponse> ChangeStatusSessionAsync(
        string instanceName,
        ChangeEvolutionBotStatusRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca sessão do bot Evolution
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="number">Número do contato</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Dados da sessão</returns>
    Task<EvolutionBotSessionResponse> FetchSessionAsync(
        string instanceName,
        string number,
        CancellationToken cancellationToken = default);
}
