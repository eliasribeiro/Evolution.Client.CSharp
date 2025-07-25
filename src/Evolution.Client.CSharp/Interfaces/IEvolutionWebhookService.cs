using Evolution.Client.CSharp.Models.Webhook;

namespace Evolution.Client.CSharp.Interfaces;

/// <summary>
/// Interface for Evolution API Webhook operations
/// </summary>
public interface IEvolutionWebhookService
{
    /// <summary>
    /// Set webhook configuration for an instance
    /// </summary>
    /// <param name="instanceName">Name of the instance</param>
    /// <param name="request">Webhook configuration request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Webhook configuration response</returns>
    Task<SetWebhookResponse> SetWebhookAsync(string instanceName, SetWebhookRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Find webhook configuration for an instance
    /// </summary>
    /// <param name="instanceName">Name of the instance</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Webhook configuration response</returns>
    Task<FindWebhookResponse> FindWebhookAsync(string instanceName, CancellationToken cancellationToken = default);
}