namespace Evolution.Client.Modules;

/// <summary>
/// Interface para operações relacionadas ao perfil
/// </summary>
public interface IProfileModule
{
    /// <summary>
    /// Busca perfil de negócio
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados para busca do perfil</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Perfil de negócio</returns>
    Task<BusinessProfile> FetchBusinessProfileAsync(
        string instanceName,
        FetchBusinessProfileRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca perfil do usuário
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados para busca do perfil</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Perfil do usuário</returns>
    Task<UserProfile> FetchProfileAsync(
        string instanceName,
        FetchProfileRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza nome do perfil
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Novo nome do perfil</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da atualização</returns>
    Task<UpdateProfileResponse> UpdateProfileNameAsync(
        string instanceName,
        UpdateProfileNameRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza status do perfil
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Novo status do perfil</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da atualização</returns>
    Task<UpdateProfileResponse> UpdateProfileStatusAsync(
        string instanceName,
        UpdateProfileStatusRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza foto do perfil
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Nova foto do perfil</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da atualização</returns>
    Task<UpdateProfileResponse> UpdateProfilePictureAsync(
        string instanceName,
        UpdateProfilePictureRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove foto do perfil
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da remoção</returns>
    Task<UpdateProfileResponse> RemoveProfilePictureAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca configurações de privacidade
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Configurações de privacidade</returns>
    Task<PrivacySettings> FetchPrivacySettingsAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza configurações de privacidade
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Novas configurações de privacidade</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da atualização</returns>
    Task<UpdateProfileResponse> UpdatePrivacySettingsAsync(
        string instanceName,
        UpdatePrivacySettingsRequest request,
        CancellationToken cancellationToken = default);
}
