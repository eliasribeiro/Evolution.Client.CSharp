namespace Evolution.Client.Modules;

/// <summary>
/// Interface para operações relacionadas ao TypeBot
/// </summary>
public interface ITypeBotModule
{
    /// <summary>
    /// Cria/configura TypeBot para uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Configuração do TypeBot</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da configuração do TypeBot</returns>
    Task<TypeBotResponse> CreateAsync(
        string instanceName,
        CreateTypeBotRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Inicia TypeBot para um contato
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados para iniciar o TypeBot</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da inicialização</returns>
    Task<TypeBotResponse> StartAsync(
        string instanceName,
        StartTypeBotRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca TypeBot de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Configuração do TypeBot</returns>
    Task<TypeBotResponse> FindAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca todos os TypeBots de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista de TypeBots</returns>
    Task<TypeBotListResponse> FetchAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza configuração do TypeBot
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Nova configuração do TypeBot</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da atualização</returns>
    Task<TypeBotResponse> UpdateAsync(
        string instanceName,
        UpdateTypeBotRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove TypeBot de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Task de conclusão</returns>
    Task DeleteAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Altera status da sessão do TypeBot
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados para alterar o status</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da alteração</returns>
    Task<TypeBotSessionResponse> ChangeSessionStatusAsync(
        string instanceName,
        ChangeSessionStatusRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca sessão do TypeBot
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="number">Número do contato</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Dados da sessão</returns>
    Task<TypeBotSessionResponse> FetchSessionAsync(
        string instanceName,
        string number,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Configura settings do TypeBot
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Configurações do TypeBot</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da configuração</returns>
    Task<TypeBotSettingsResponse> SetSettingsAsync(
        string instanceName,
        TypeBotSettingsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca settings do TypeBot
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Configurações do TypeBot</returns>
    Task<TypeBotSettingsResponse> FindSettingsAsync(
        string instanceName,
        CancellationToken cancellationToken = default);
}
