using System.Net.Http.Headers;
using System.Text.Json;
using Evolution.Client.CSharp.Configuration;
using Evolution.Client.CSharp.Interfaces;
using Evolution.Client.CSharp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Evolution.Client.CSharp.Services;

/// <summary>
/// Implementação do serviço de informações da API Evolution.
/// </summary>
public class EvolutionInformationService : IEvolutionInformationService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<EvolutionInformationService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="EvolutionInformationService"/>.
    /// </summary>
    /// <param name="httpClient">O cliente HTTP para fazer requisições à API.</param>
    /// <param name="options">As opções de configuração da API.</param>
    /// <param name="logger">O logger para registrar informações e erros.</param>
    public EvolutionInformationService(
        HttpClient httpClient,
        IOptions<EvolutionApiOptions> options,
        ILogger<EvolutionInformationService> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        var apiOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));

        // Configura o cliente HTTP
        _httpClient.BaseAddress = new Uri(apiOptions.BaseUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(apiOptions.TimeoutSeconds);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // Adiciona o cabeçalho de autenticação se a chave de API estiver definida
        if (!string.IsNullOrEmpty(apiOptions.ApiKey))
        {
            _httpClient.DefaultRequestHeaders.Add("apikey", apiOptions.ApiKey);
        }

        // Configura as opções de serialização JSON
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    /// <summary>
    /// Obtém informações sobre a API Evolution.
    /// </summary>
    /// <returns>Um objeto contendo informações sobre a API.</returns>
    public async Task<InformationResponse> GetInformationAsync()
    {
        try
        {
            _logger.LogInformation("Obtendo informações da API Evolution");

            // Faz a requisição GET para o endpoint raiz
            var response = await _httpClient.GetAsync("/");

            // Verifica se a requisição foi bem-sucedida
            response.EnsureSuccessStatusCode();

            // Lê o conteúdo da resposta
            var content = await response.Content.ReadAsStringAsync();

            // Desserializa a resposta JSON
            var result = JsonSerializer.Deserialize<InformationResponse>(content, _jsonOptions);

            if (result == null)
            {
                throw new JsonException("Falha ao desserializar a resposta da API");
            }

            _logger.LogInformation("Informações da API obtidas com sucesso. Versão: {Version}", result.Version);

            return result;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Erro ao fazer requisição HTTP para a API Evolution");
            throw;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Erro ao desserializar a resposta da API Evolution");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao obter informações da API Evolution");
            throw;
        }
    }
}