namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Interface para operações relacionadas a configurações
/// </summary>
public interface ISettingsModule
{
    /// <summary>
    /// Configura settings para uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Configurações da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da configuração</returns>
    Task<SetSettingsResponse> SetAsync(
        string instanceName,
        SetSettingsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém configurações de uma instância
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Configurações da instância</returns>
    Task<SettingsConfig> GetAsync(
        string instanceName,
        CancellationToken cancellationToken = default);
}
