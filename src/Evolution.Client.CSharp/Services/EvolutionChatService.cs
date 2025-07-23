using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Evolution.Client.CSharp.Configuration;
using Evolution.Client.CSharp.Interfaces;
using Evolution.Client.CSharp.Models.Chat;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Evolution.Client.CSharp.Services;

/// <summary>
/// Implementação do serviço de chat da API Evolution.
/// </summary>
public class EvolutionChatService : IEvolutionChatService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<EvolutionChatService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="EvolutionChatService"/>.
    /// </summary>
    /// <param name="httpClient">O cliente HTTP para fazer requisições.</param>
    /// <param name="options">As opções de configuração da API Evolution.</param>
    /// <param name="logger">O logger para registrar informações.</param>
    public EvolutionChatService(HttpClient httpClient, IOptions<EvolutionApiOptions> options, ILogger<EvolutionChatService> logger)
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
    /// Verifica se os números fornecidos existem no WhatsApp.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os números a serem verificados.</param>
    /// <returns>Uma lista com informações sobre cada número verificado.</returns>
    public async Task<CheckWhatsAppResponse> CheckWhatsAppNumbersAsync(string instanceName, CheckWhatsAppRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request));

        try
        {
            _logger.LogInformation("Verificando números do WhatsApp para a instância: {InstanceName}", instanceName);

            var endpoint = $"/chat/whatsappNumbers/{instanceName}";
            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<CheckWhatsAppResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("Verificação de números do WhatsApp concluída com sucesso para a instância: {InstanceName}. Total de números verificados: {Count}", 
                    instanceName, result?.Count ?? 0);

                return result ?? new CheckWhatsAppResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao verificar números do WhatsApp para a instância: {InstanceName}. Status: {StatusCode}, Erro: {Error}", 
                    instanceName, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' não encontrada.");
                }

                throw new HttpRequestException($"Erro ao verificar números do WhatsApp: {response.StatusCode} - {errorContent}");
            }
        }
        catch (HttpRequestException)
        {
            throw; // Re-throw HttpRequestException para manter o tratamento específico
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Erro ao serializar/desserializar dados da API Evolution");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao verificar números do WhatsApp para a instância: {InstanceName}", instanceName);
            throw;
        }
    }

    /// <summary>
    /// Busca contatos da instância especificada.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os critérios de busca (opcional).</param>
    /// <returns>Uma lista de contatos encontrados.</returns>
    public async Task<FindContactsResponse> FindContactsAsync(string instanceName, FindContactsRequest? request = null)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        try
        {
            _logger.LogInformation("Buscando contatos para a instância: {InstanceName}", instanceName);

            var endpoint = $"/chat/findContacts/{instanceName}";
            
            // Se não há critérios de busca, cria uma requisição com where vazio
            var searchRequest = request ?? new FindContactsRequest 
            { 
                Where = new FindContactsWhere() 
            };
            
            var jsonContent = JsonSerializer.Serialize(searchRequest, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<FindContactsResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("Busca de contatos concluída com sucesso para a instância: {InstanceName}. Total de contatos encontrados: {Count}", 
                    instanceName, result?.Count ?? 0);

                return result ?? new FindContactsResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao buscar contatos para a instância: {InstanceName}. Status: {StatusCode}, Erro: {Error}", 
                    instanceName, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' não encontrada.");
                }

                throw new HttpRequestException($"Erro ao buscar contatos: {response.StatusCode} - {errorContent}");
            }
        }
        catch (HttpRequestException)
        {
            throw; // Re-throw HttpRequestException para manter o tratamento específico
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Erro ao serializar/desserializar dados da API Evolution");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao buscar contatos para a instância: {InstanceName}", instanceName);
            throw;
        }
    }
}