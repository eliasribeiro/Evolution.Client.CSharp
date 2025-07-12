namespace Evolution.Client.Modules;

/// <summary>
/// Interface para operações relacionadas ao WebSocket
/// </summary>
public interface IWebSocketModule
{
    /// <summary>
    /// Configura WebSocket para uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Configuração do WebSocket</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da configuração do WebSocket</returns>
    Task<WebSocketResponse> SetAsync(
        string instanceName,
        SetWebSocketRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca configuração do WebSocket de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Configuração do WebSocket</returns>
    Task<WebSocketResponse> FindAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca estatísticas do WebSocket de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Estatísticas do WebSocket</returns>
    Task<WebSocketStatsResponse> GetStatsAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Testa a conexão WebSocket
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Status da conexão</returns>
    Task<WebSocketResponse> TestConnectionAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Força reconexão do WebSocket
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado da reconexão</returns>
    Task<WebSocketResponse> ReconnectAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Desconecta o WebSocket
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado da desconexão</returns>
    Task<WebSocketResponse> DisconnectAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Envia um ping para testar a conexão
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado do ping com latência</returns>
    Task<WebSocketResponse> PingAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove configuração do WebSocket de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Task de conclusão</returns>
    Task DeleteAsync(
        string instanceName,
        CancellationToken cancellationToken = default);
}
