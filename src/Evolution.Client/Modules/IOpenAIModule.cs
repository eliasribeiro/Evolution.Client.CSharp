namespace Evolution.Client.Modules;

/// <summary>
/// Interface para operações relacionadas ao OpenAI
/// </summary>
public interface IOpenAIModule
{
    /// <summary>
    /// Cria bot OpenAI para uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Configuração do bot OpenAI</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da criação do bot</returns>
    Task<OpenAIBotResponse> CreateBotAsync(
        string instanceName,
        CreateOpenAIBotRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca bot OpenAI de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Configuração do bot OpenAI</returns>
    Task<OpenAIBotResponse> FindBotAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca todos os bots OpenAI de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista de bots OpenAI</returns>
    Task<OpenAIBotListResponse> FindBotsAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza configuração do bot OpenAI
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Nova configuração do bot</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da atualização</returns>
    Task<OpenAIBotResponse> UpdateBotAsync(
        string instanceName,
        UpdateOpenAIBotRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove bot OpenAI de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Task de conclusão</returns>
    Task DeleteBotAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca credenciais OpenAI
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Credenciais OpenAI</returns>
    Task<OpenAICredsResponse> FindCredsAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Configura credenciais OpenAI
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Credenciais OpenAI</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da configuração</returns>
    Task<OpenAICredsResponse> SetCredsAsync(
        string instanceName,
        SetOpenAICredsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove credenciais OpenAI
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Task de conclusão</returns>
    Task DeleteCredsAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Configura settings do OpenAI
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Configurações do OpenAI</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da configuração</returns>
    Task<OpenAISettingsResponse> SetSettingsAsync(
        string instanceName,
        OpenAISettingsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca settings do OpenAI
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Configurações do OpenAI</returns>
    Task<OpenAISettingsResponse> FindSettingsAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Altera status do OpenAI
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados para alterar o status</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da alteração</returns>
    Task<OpenAISessionResponse> ChangeStatusAsync(
        string instanceName,
        ChangeOpenAIStatusRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca sessão do OpenAI
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="number">Número do contato</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Dados da sessão</returns>
    Task<OpenAISessionResponse> FindSessionAsync(
        string instanceName,
        string number,
        CancellationToken cancellationToken = default);
}
