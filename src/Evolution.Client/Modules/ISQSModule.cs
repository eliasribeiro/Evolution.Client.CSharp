namespace Evolution.Client.Modules;

/// <summary>
/// Interface para operações relacionadas ao SQS (Simple Queue Service)
/// </summary>
public interface ISQSModule
{
    /// <summary>
    /// Configura SQS para uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Configuração do SQS</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da configuração do SQS</returns>
    Task<SQSResponse> SetAsync(
        string instanceName,
        SetSQSRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca configuração do SQS de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Configuração do SQS</returns>
    Task<SQSResponse> FindAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca estatísticas do SQS de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Estatísticas do SQS</returns>
    Task<SQSStatsResponse> GetStatsAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Testa a conexão com AWS SQS
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Status da conexão</returns>
    Task<SQSResponse> TestConnectionAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica atributos da fila SQS
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Atributos da fila</returns>
    Task<SQSResponse> GetQueueAttributesAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Purga mensagens da fila SQS
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado da operação</returns>
    Task<SQSResponse> PurgeQueueAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Envia uma mensagem de teste para a fila
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="message">Mensagem de teste</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado do envio</returns>
    Task<SQSResponse> SendTestMessageAsync(
        string instanceName,
        string message,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove configuração do SQS de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Task de conclusão</returns>
    Task DeleteAsync(
        string instanceName,
        CancellationToken cancellationToken = default);
}
