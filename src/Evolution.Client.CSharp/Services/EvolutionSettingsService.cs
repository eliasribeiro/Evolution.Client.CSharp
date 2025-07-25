using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Evolution.Client.CSharp.Configuration;
using Evolution.Client.CSharp.Interfaces;
using Evolution.Client.CSharp.Models.Settings;
using Microsoft.Extensions.Options;

namespace Evolution.Client.CSharp.Services;

/// <summary>
/// Service for Evolution API Settings operations
/// </summary>
public class EvolutionSettingsService : IEvolutionSettingsService
{
    private readonly HttpClient _httpClient;
    private readonly EvolutionApiOptions _configuration;

    /// <summary>
    /// Initializes a new instance of the EvolutionSettingsService
    /// </summary>
    /// <param name="httpClient">HTTP client for API requests</param>
    /// <param name="configuration">Evolution API configuration</param>
    public EvolutionSettingsService(HttpClient httpClient, IOptions<EvolutionApiOptions> configuration)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _configuration = configuration?.Value ?? throw new ArgumentNullException(nameof(configuration));

        // Configura o cliente HTTP
        _httpClient.BaseAddress = new Uri(_configuration.BaseUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(_configuration.TimeoutSeconds);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // Adiciona o cabeçalho de autenticação se a chave de API estiver definida
        if (!string.IsNullOrEmpty(_configuration.ApiKey))
        {
            _httpClient.DefaultRequestHeaders.Add("apikey", _configuration.ApiKey);
        }
    }

    /// <inheritdoc />
    public async Task<SetSettingsResponse> SetSettingsAsync(string instanceName, SetSettingsRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Instance name cannot be null or empty", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"settings/set/{instanceName}", content, cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<SetSettingsResponse>(responseContent) ?? new SetSettingsResponse();
    }

    /// <inheritdoc />
    public async Task<FindSettingsResponse> FindSettingsAsync(string instanceName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Instance name cannot be null or empty", nameof(instanceName));

        var response = await _httpClient.GetAsync($"settings/find/{instanceName}", cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<FindSettingsResponse>(responseContent) ?? new FindSettingsResponse();
    }
}