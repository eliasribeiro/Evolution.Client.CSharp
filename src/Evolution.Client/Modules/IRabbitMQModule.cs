namespace Evolution.Client.Modules;

/// <summary>
/// Interface para operações relacionadas ao RabbitMQ
/// </summary>
public interface IRabbitMQModule
{
    /// <summary>
    /// Configura RabbitMQ para uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Configuração do RabbitMQ</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da configuração do RabbitMQ</returns>
    Task<RabbitMQResponse> SetAsync(
        string instanceName,
        SetRabbitMQRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca configuração do RabbitMQ de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Configuração do RabbitMQ</returns>
    Task<RabbitMQResponse> FindAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca estatísticas do RabbitMQ de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Estatísticas do RabbitMQ</returns>
    Task<RabbitMQStatsResponse> GetStatsAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Testa a conexão com RabbitMQ
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Status da conexão</returns>
    Task<RabbitMQResponse> TestConnectionAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Força reconexão com RabbitMQ
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado da reconexão</returns>
    Task<RabbitMQResponse> ReconnectAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Desconecta do RabbitMQ
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado da desconexão</returns>
    Task<RabbitMQResponse> DisconnectAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Publica uma mensagem de teste na exchange
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="message">Mensagem de teste</param>
    /// <param name="routingKey">Routing key (opcional, usa a padrão se não fornecida)</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado da publicação</returns>
    Task<RabbitMQResponse> PublishTestMessageAsync(
        string instanceName,
        string message,
        string? routingKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica se a exchange existe
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Status da exchange</returns>
    Task<RabbitMQResponse> CheckExchangeAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica se a fila existe
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Status da fila</returns>
    Task<RabbitMQResponse> CheckQueueAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Purga mensagens da fila
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado da operação</returns>
    Task<RabbitMQResponse> PurgeQueueAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove configuração do RabbitMQ de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Task de conclusão</returns>
    Task DeleteAsync(
        string instanceName,
        CancellationToken cancellationToken = default);
}
