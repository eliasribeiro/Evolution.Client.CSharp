using System.Net.Http.Headers;
using System.Text.Json;
using Evolution.Client.CSharp.Configuration;
using Evolution.Client.CSharp.Interfaces;
using Evolution.Client.CSharp.Models.Instance;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Evolution.Client.CSharp.Services;

/// <summary>
/// Implementação do serviço de instâncias da API Evolution.
/// </summary>
public class EvolutionInstanceService : IEvolutionInstanceService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<EvolutionInstanceService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="EvolutionInstanceService"/>.
    /// </summary>
    /// <param name="httpClient">O cliente HTTP para fazer requisições à API.</param>
    /// <param name="options">As opções de configuração da API.</param>
    /// <param name="logger">O logger para registrar informações e erros.</param>
    public EvolutionInstanceService(
        HttpClient httpClient,
        IOptions<EvolutionApiOptions> options,
        ILogger<EvolutionInstanceService> logger)
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
    /// Obtém todas as instâncias disponíveis.
    /// </summary>
    /// <returns>Uma lista de instâncias disponíveis.</returns>
    /// <remarks>
    /// Este método faz uma requisição GET para o endpoint /instance/fetchInstances.
    /// </remarks>
    public async Task<InstancesResponse> FetchInstancesAsync()
    {
        try
        {
            _logger.LogInformation("Obtendo instâncias da API Evolution");

            // Faz a requisição GET para o endpoint de instâncias
            var response = await _httpClient.GetAsync("/instance/fetchInstances");

            // Verifica se a requisição foi bem-sucedida
            response.EnsureSuccessStatusCode();

            // Lê o conteúdo da resposta
            var content = await response.Content.ReadAsStringAsync();

            // Desserializa a resposta JSON
            var result = JsonSerializer.Deserialize<InstancesResponse>(content, _jsonOptions);

            if (result == null)
            {
                throw new JsonException("Falha ao desserializar a resposta da API");
            }

            _logger.LogInformation("Instâncias obtidas com sucesso. Total: {Count}", result.Count);

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
            _logger.LogError(ex, "Erro inesperado ao obter instâncias da API Evolution");
            throw;
        }
    }

    /// <summary>
    /// Obtém todas as instâncias disponíveis usando o modelo V2.
    /// </summary>
    /// <returns>Uma lista de instâncias disponíveis no formato V2.</returns>
    /// <remarks>
    /// Este método faz uma requisição GET para o endpoint /instance/fetchInstances e retorna os dados no formato V2.
    /// </remarks>
    public async Task<InstancesResponseV2> FetchInstancesV2Async()
    {
        try
        {
            _logger.LogInformation("Obtendo instâncias da API Evolution (formato V2)");

            // Faz a requisição GET para o endpoint de instâncias
            var response = await _httpClient.GetAsync("/instance/fetchInstances");

            // Verifica se a requisição foi bem-sucedida
            response.EnsureSuccessStatusCode();

            // Lê o conteúdo da resposta
            var content = await response.Content.ReadAsStringAsync();

            // Desserializa a resposta JSON
            var result = JsonSerializer.Deserialize<InstancesResponseV2>(content, _jsonOptions);

            if (result == null)
            {
                throw new JsonException("Falha ao desserializar a resposta da API");
            }

            _logger.LogInformation("Instâncias obtidas com sucesso (formato V2). Total: {Count}", result.Count);

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
            _logger.LogError(ex, "Erro inesperado ao obter instâncias da API Evolution (formato V2)");
            throw;
        }
    }
}