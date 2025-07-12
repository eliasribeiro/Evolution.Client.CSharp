namespace Evolution.Client.Modules;

/// <summary>
/// Interface para operações relacionadas ao Chatwoot
/// </summary>
public interface IChatwootModule
{
    /// <summary>
    /// Configura Chatwoot para uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Configuração do Chatwoot</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da configuração do Chatwoot</returns>
    Task<ChatwootResponse> SetAsync(
        string instanceName,
        SetChatwootRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca configuração do Chatwoot de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Configuração do Chatwoot</returns>
    Task<ChatwootResponse> FindAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca estatísticas do Chatwoot de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Estatísticas do Chatwoot</returns>
    Task<ChatwootStatsResponse> GetStatsAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Testa a conexão com o Chatwoot
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Status da conexão</returns>
    Task<ChatwootResponse> TestConnectionAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sincroniza contatos com o Chatwoot
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado da sincronização</returns>
    Task<ChatwootResponse> SyncContactsAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sincroniza mensagens com o Chatwoot
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="days">Número de dias para sincronizar (padrão: 7)</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado da sincronização</returns>
    Task<ChatwootResponse> SyncMessagesAsync(
        string instanceName,
        int days = 7,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove configuração do Chatwoot de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Task de conclusão</returns>
    Task DeleteAsync(
        string instanceName,
        CancellationToken cancellationToken = default);
}
