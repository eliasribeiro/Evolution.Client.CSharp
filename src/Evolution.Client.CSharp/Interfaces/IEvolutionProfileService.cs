using Evolution.Client.CSharp.Models.Profile;

namespace Evolution.Client.CSharp.Interfaces;

/// <summary>
/// Interface para o serviço de configurações de perfil da API Evolution.
/// </summary>
public interface IEvolutionProfileService
{
    /// <summary>
    /// Busca o perfil de negócio de um número do WhatsApp.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo o número para buscar o perfil de negócio.</param>
    /// <returns>O perfil de negócio encontrado.</returns>
    Task<FetchBusinessProfileResponse> FetchBusinessProfileAsync(string instanceName, FetchBusinessProfileRequest request);

    /// <summary>
    /// Busca o perfil de um usuário do WhatsApp.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo o número para buscar o perfil.</param>
    /// <returns>O perfil do usuário encontrado.</returns>
    Task<FetchProfileResponse> FetchProfileAsync(string instanceName, FetchProfileRequest request);

    /// <summary>
    /// Atualiza o nome do perfil da instância.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo o novo nome do perfil.</param>
    /// <returns>O resultado da operação de atualização.</returns>
    Task<UpdateProfileNameResponse> UpdateProfileNameAsync(string instanceName, UpdateProfileNameRequest request);

    /// <summary>
    /// Atualiza o status do perfil da instância.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo o novo status do perfil.</param>
    /// <returns>O resultado da operação de atualização.</returns>
    Task<UpdateProfileStatusResponse> UpdateProfileStatusAsync(string instanceName, UpdateProfileStatusRequest request);

    /// <summary>
    /// Atualiza a foto do perfil da instância.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo a nova foto do perfil.</param>
    /// <returns>O resultado da operação de atualização.</returns>
    Task<UpdateProfilePictureResponse> UpdateProfilePictureAsync(string instanceName, UpdateProfilePictureRequest request);

    /// <summary>
    /// Remove a foto do perfil da instância.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <returns>O resultado da operação de remoção.</returns>
    Task<RemoveProfilePictureResponse> RemoveProfilePictureAsync(string instanceName);

    /// <summary>
    /// Busca as configurações de privacidade da instância.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <returns>As configurações de privacidade atuais.</returns>
    Task<FetchPrivacySettingsResponse> FetchPrivacySettingsAsync(string instanceName);

    /// <summary>
    /// Atualiza as configurações de privacidade da instância.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo as novas configurações de privacidade.</param>
    /// <returns>O resultado da operação de atualização.</returns>
    Task<UpdatePrivacySettingsResponse> UpdatePrivacySettingsAsync(string instanceName, UpdatePrivacySettingsRequest request);
}