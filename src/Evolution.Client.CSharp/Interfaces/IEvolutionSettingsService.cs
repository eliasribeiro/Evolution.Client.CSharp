using Evolution.Client.CSharp.Models.Settings;

namespace Evolution.Client.CSharp.Interfaces;

/// <summary>
/// Interface for Evolution API Settings operations
/// </summary>
public interface IEvolutionSettingsService
{
    /// <summary>
    /// Set settings configuration for an instance
    /// </summary>
    /// <param name="instanceName">Name of the instance</param>
    /// <param name="request">Settings configuration request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Settings configuration response</returns>
    Task<SetSettingsResponse> SetSettingsAsync(string instanceName, SetSettingsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Find settings configuration for an instance
    /// </summary>
    /// <param name="instanceName">Name of the instance</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Settings configuration response</returns>
    Task<FindSettingsResponse> FindSettingsAsync(string instanceName, CancellationToken cancellationToken = default);
}