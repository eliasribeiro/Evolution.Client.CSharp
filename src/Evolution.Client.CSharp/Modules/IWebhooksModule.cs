namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Interface para operações relacionadas a webhooks
/// </summary>
public interface IWebhooksModule
{
    /// <summary>
    /// Configura webhook para uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Configuração do webhook</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da configuração do webhook</returns>
    Task<SetWebhookResponse> SetAsync(
        string instanceName,
        SetWebhookRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém configuração do webhook de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Configuração do webhook</returns>
    Task<WebhookConfig> GetAsync(
        string instanceName,
        CancellationToken cancellationToken = default);
}
