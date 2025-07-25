using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Evolution.Client.CSharp.Configuration;
using Evolution.Client.CSharp.Interfaces;
using Evolution.Client.CSharp.Models.Profile;
using Microsoft.Extensions.Options;

namespace Evolution.Client.CSharp.Services;

/// <summary>
/// Implementação do serviço de configurações de perfil da API Evolution.
/// </summary>
public class EvolutionProfileService : IEvolutionProfileService
{
    private readonly HttpClient _httpClient;
    private readonly EvolutionApiOptions _options;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="EvolutionProfileService"/>.
    /// </summary>
    /// <param name="httpClient">O cliente HTTP para fazer requisições.</param>
    /// <param name="options">As opções de configuração da API.</param>
    public EvolutionProfileService(HttpClient httpClient, IOptions<EvolutionApiOptions> options)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));

        // Configura o cliente HTTP
        _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // Adiciona o cabeçalho de autenticação se a chave de API estiver definida
        if (!string.IsNullOrEmpty(_options.ApiKey))
        {
            _httpClient.DefaultRequestHeaders.Add("apikey", _options.ApiKey);
        }
    }

    /// <summary>
    /// Busca o perfil de negócio de um número do WhatsApp.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo o número para buscar o perfil de negócio.</param>
    /// <returns>O perfil de negócio encontrado.</returns>
    public async Task<FetchBusinessProfileResponse> FetchBusinessProfileAsync(string instanceName, FetchBusinessProfileRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
        {
            throw new ArgumentException("O nome da instância não pode ser nulo ou vazio.", nameof(instanceName));
        }

        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"/chat/fetchBusinessProfile/{instanceName}", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<FetchBusinessProfileResponse>(responseContent) ?? new FetchBusinessProfileResponse();
    }

    /// <summary>
    /// Busca o perfil de um usuário do WhatsApp.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo o número para buscar o perfil.</param>
    /// <returns>O perfil do usuário encontrado.</returns>
    public async Task<FetchProfileResponse> FetchProfileAsync(string instanceName, FetchProfileRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
        {
            throw new ArgumentException("O nome da instância não pode ser nulo ou vazio.", nameof(instanceName));
        }

        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"/chat/fetchProfile/{instanceName}", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<FetchProfileResponse>(responseContent) ?? new FetchProfileResponse();
    }

    /// <summary>
    /// Atualiza o nome do perfil da instância.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo o novo nome do perfil.</param>
    /// <returns>O resultado da operação de atualização.</returns>
    public async Task<UpdateProfileNameResponse> UpdateProfileNameAsync(string instanceName, UpdateProfileNameRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
        {
            throw new ArgumentException("O nome da instância não pode ser nulo ou vazio.", nameof(instanceName));
        }

        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"/chat/updateProfileName/{instanceName}", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UpdateProfileNameResponse>(responseContent) ?? new UpdateProfileNameResponse();
    }

    /// <summary>
    /// Atualiza o status do perfil da instância.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo o novo status do perfil.</param>
    /// <returns>O resultado da operação de atualização.</returns>
    public async Task<UpdateProfileStatusResponse> UpdateProfileStatusAsync(string instanceName, UpdateProfileStatusRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
        {
            throw new ArgumentException("O nome da instância não pode ser nulo ou vazio.", nameof(instanceName));
        }

        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"/chat/updateProfileStatus/{instanceName}", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UpdateProfileStatusResponse>(responseContent) ?? new UpdateProfileStatusResponse();
    }

    /// <summary>
    /// Atualiza a foto do perfil da instância.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo a nova foto do perfil.</param>
    /// <returns>O resultado da operação de atualização.</returns>
    public async Task<UpdateProfilePictureResponse> UpdateProfilePictureAsync(string instanceName, UpdateProfilePictureRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
        {
            throw new ArgumentException("O nome da instância não pode ser nulo ou vazio.", nameof(instanceName));
        }

        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"/chat/updateProfilePicture/{instanceName}", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UpdateProfilePictureResponse>(responseContent) ?? new UpdateProfilePictureResponse();
    }

    /// <summary>
    /// Remove a foto do perfil da instância.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <returns>O resultado da operação de remoção.</returns>
    public async Task<RemoveProfilePictureResponse> RemoveProfilePictureAsync(string instanceName)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
        {
            throw new ArgumentException("O nome da instância não pode ser nulo ou vazio.", nameof(instanceName));
        }

        var response = await _httpClient.DeleteAsync($"/chat/removeProfilePicture/{instanceName}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<RemoveProfilePictureResponse>(responseContent) ?? new RemoveProfilePictureResponse();
    }

    /// <summary>
    /// Busca as configurações de privacidade da instância.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <returns>As configurações de privacidade atuais.</returns>
    public async Task<FetchPrivacySettingsResponse> FetchPrivacySettingsAsync(string instanceName)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
        {
            throw new ArgumentException("O nome da instância não pode ser nulo ou vazio.", nameof(instanceName));
        }

        var response = await _httpClient.GetAsync($"/chat/fetchPrivacySettings/{instanceName}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<FetchPrivacySettingsResponse>(responseContent) ?? new FetchPrivacySettingsResponse();
    }

    /// <summary>
    /// Atualiza as configurações de privacidade da instância.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo as novas configurações de privacidade.</param>
    /// <returns>O resultado da operação de atualização.</returns>
    public async Task<UpdatePrivacySettingsResponse> UpdatePrivacySettingsAsync(string instanceName, UpdatePrivacySettingsRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
        {
            throw new ArgumentException("O nome da instância não pode ser nulo ou vazio.", nameof(instanceName));
        }

        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"/chat/updatePrivacySettings/{instanceName}", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UpdatePrivacySettingsResponse>(responseContent) ?? new UpdatePrivacySettingsResponse();
    }
}