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

    /// <summary>
    /// Busca mensagens da instância especificada.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os critérios de busca (opcional).</param>
    /// <returns>Uma lista de mensagens encontradas com informações de paginação.</returns>
    public async Task<FindMessagesResponse> FindMessagesAsync(string instanceName, FindMessagesRequest? request = null)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        try
        {
            _logger.LogInformation("Buscando mensagens para a instância: {InstanceName}", instanceName);

            var endpoint = $"/chat/findMessages/{instanceName}";
            
            // Se não há critérios de busca, cria uma requisição vazia
            var searchRequest = request ?? new FindMessagesRequest();
            
            var jsonContent = JsonSerializer.Serialize(searchRequest, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<FindMessagesResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("Busca de mensagens concluída com sucesso para a instância: {InstanceName}. Total de mensagens encontradas: {Count}", 
                    instanceName, result?.Messages?.Total ?? 0);

                return result ?? new FindMessagesResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao buscar mensagens para a instância: {InstanceName}. Status: {StatusCode}, Erro: {Error}", 
                    instanceName, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' não encontrada.");
                }

                throw new HttpRequestException($"Erro ao buscar mensagens: {response.StatusCode} - {errorContent}");
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
            _logger.LogError(ex, "Erro inesperado ao buscar mensagens para a instância: {InstanceName}", instanceName);
            throw;
        }
    }

    /// <summary>
    /// Busca chats da instância especificada.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os critérios de busca (opcional).</param>
    /// <returns>Uma lista de chats encontrados.</returns>
    public async Task<FindChatsResponse> FindChatsAsync(string instanceName, FindChatsRequest? request = null)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        try
        {
            _logger.LogInformation("Buscando chats para a instância: {InstanceName}", instanceName);

            var endpoint = $"/chat/findChats/{instanceName}";

            // Se não há critérios de busca, cria uma requisição vazia
            var searchRequest = request ?? new FindChatsRequest();

            var jsonContent = JsonSerializer.Serialize(searchRequest, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<FindChatsResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("Busca de chats concluída com sucesso para a instância: {InstanceName}. Total de chats encontrados: {Count}",
                    instanceName, result?.Count ?? 0);

                return result ?? new FindChatsResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao buscar chats para a instância: {InstanceName}. Status: {StatusCode}, Erro: {Error}",
                    instanceName, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' não encontrada.");
                }

                throw new HttpRequestException($"Erro ao buscar chats: {response.StatusCode} - {errorContent}");
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
            _logger.LogError(ex, "Erro inesperado ao buscar chats para a instância: {InstanceName}", instanceName);
            throw;
        }
    }

    /// <summary>
    /// Busca a URL da foto de perfil de um número do WhatsApp.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo o número do WhatsApp.</param>
    /// <returns>A URL da foto de perfil do usuário.</returns>
    public async Task<FetchProfilePicUrlResponse> FetchProfilePicUrlAsync(string instanceName, FetchProfilePicUrlRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request), "A requisição é obrigatória.");

        if (string.IsNullOrWhiteSpace(request.Number))
            throw new ArgumentException("O número do WhatsApp é obrigatório.", nameof(request.Number));

        try
        {
            _logger.LogInformation("Buscando URL da foto de perfil para o número: {Number} na instância: {InstanceName}",
                request.Number, instanceName);

            var endpoint = $"/chat/fetchProfilePictureUrl/{instanceName}";

            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<FetchProfilePicUrlResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("URL da foto de perfil obtida com sucesso para o número: {Number} na instância: {InstanceName}",
                    request.Number, instanceName);

                return result ?? new FetchProfilePicUrlResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao buscar URL da foto de perfil para o número: {Number} na instância: {InstanceName}. Status: {StatusCode}, Erro: {Error}",
                    request.Number, instanceName, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' não encontrada ou número '{request.Number}' não existe no WhatsApp.");
                }

                throw new HttpRequestException($"Erro ao buscar URL da foto de perfil: {response.StatusCode} - {errorContent}");
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
            _logger.LogError(ex, "Erro inesperado ao buscar URL da foto de perfil para o número: {Number} na instância: {InstanceName}",
                request.Number, instanceName);
            throw;
        }
    }

    /// <summary>
    /// Deleta uma mensagem para todos os participantes da conversa.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo as informações da mensagem a ser deletada.</param>
    /// <returns>A resposta da operação de deleção.</returns>
    public async Task<DeleteMessageForEveryoneResponse> DeleteMessageForEveryoneAsync(string instanceName, DeleteMessageForEveryoneRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request), "A requisição é obrigatória.");

        if (string.IsNullOrWhiteSpace(request.Id))
            throw new ArgumentException("O ID da mensagem é obrigatório.", nameof(request.Id));

        if (string.IsNullOrWhiteSpace(request.RemoteJid))
            throw new ArgumentException("O Remote JID é obrigatório.", nameof(request.RemoteJid));

        try
        {
            _logger.LogInformation("Deletando mensagem para todos. Instância: {InstanceName}, ID da mensagem: {MessageId}, Remote JID: {RemoteJid}",
                instanceName, request.Id, request.RemoteJid);

            var endpoint = $"/chat/deleteMessageForEveryone/{instanceName}";

            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, endpoint)
            {
                Content = content
            });

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DeleteMessageForEveryoneResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("Mensagem deletada com sucesso para todos. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}",
                    instanceName, request.Id, result?.Status ?? "desconhecido");

                return result ?? new DeleteMessageForEveryoneResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao deletar mensagem para todos. Instância: {InstanceName}, ID da mensagem: {MessageId}. Status: {StatusCode}, Erro: {Error}",
                    instanceName, request.Id, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' não encontrada ou mensagem com ID '{request.Id}' não existe.");
                }

                throw new HttpRequestException($"Erro ao deletar mensagem para todos: {response.StatusCode} - {errorContent}");
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
            _logger.LogError(ex, "Erro inesperado ao deletar mensagem para todos. Instância: {InstanceName}, ID da mensagem: {MessageId}",
                instanceName, request.Id);
            throw;
        }
    }

    /// <summary>
    /// Obtém o base64 de uma mensagem de mídia.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo as informações da mensagem de mídia.</param>
    /// <returns>Os dados da mídia em base64.</returns>
    public async Task<GetBase64FromMediaMessageResponse> GetBase64FromMediaMessageAsync(string instanceName, GetBase64FromMediaMessageRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (request.Message?.Key == null || string.IsNullOrWhiteSpace(request.Message.Key.Id))
            throw new ArgumentException("O ID da mensagem é obrigatório.", nameof(request));

        try
        {
            _logger.LogInformation("Obtendo base64 da mensagem de mídia para a instância: {InstanceName}, ID da mensagem: {MessageId}", 
                instanceName, request.Message.Key.Id);

            var endpoint = $"/chat/getBase64FromMediaMessage/{instanceName}";
            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<GetBase64FromMediaMessageResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("Base64 da mensagem de mídia obtido com sucesso para a instância: {InstanceName}, ID da mensagem: {MessageId}, Tipo de mídia: {MediaType}", 
                    instanceName, request.Message.Key.Id, result?.MediaType ?? "desconhecido");

                return result ?? new GetBase64FromMediaMessageResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao obter base64 da mensagem de mídia para a instância: {InstanceName}, ID da mensagem: {MessageId}. Status: {StatusCode}, Erro: {Error}", 
                    instanceName, request.Message.Key.Id, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' ou mensagem com ID '{request.Message.Key.Id}' não encontrada.");
                }

                throw new HttpRequestException($"Erro ao obter base64 da mensagem de mídia: {response.StatusCode} - {errorContent}");
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
            _logger.LogError(ex, "Erro inesperado ao obter base64 da mensagem de mídia para a instância: {InstanceName}, ID da mensagem: {MessageId}", 
                instanceName, request.Message?.Key?.Id ?? "desconhecido");
            throw;
        }
    }

    /// <summary>
    /// Atualiza o conteúdo de uma mensagem existente.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo as informações da mensagem a ser atualizada.</param>
    /// <returns>As informações da mensagem atualizada.</returns>
    public async Task<UpdateMessageResponse> UpdateMessageAsync(string instanceName, UpdateMessageRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.Number))
            throw new ArgumentException("O número é obrigatório.", nameof(request.Number));

        if (string.IsNullOrWhiteSpace(request.Text))
            throw new ArgumentException("O texto da mensagem é obrigatório.", nameof(request.Text));

        if (request.Key == null || string.IsNullOrWhiteSpace(request.Key.Id) || string.IsNullOrWhiteSpace(request.Key.RemoteJid))
            throw new ArgumentException("A chave da mensagem (ID e RemoteJid) é obrigatória.", nameof(request.Key));

        try
        {
            _logger.LogInformation("Atualizando mensagem para a instância: {InstanceName}, ID da mensagem: {MessageId}, Número: {Number}", 
                instanceName, request.Key.Id, request.Number);

            var endpoint = $"/chat/updateMessage/{instanceName}";
            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<UpdateMessageResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("Mensagem atualizada com sucesso para a instância: {InstanceName}, ID da mensagem: {MessageId}", 
                    instanceName, request.Key.Id);

                return result ?? new UpdateMessageResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao atualizar mensagem para a instância: {InstanceName}, ID da mensagem: {MessageId}. Status: {StatusCode}, Erro: {Error}", 
                    instanceName, request.Key.Id, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' ou mensagem com ID '{request.Key.Id}' não encontrada.");
                }

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new ArgumentException($"Dados inválidos para atualização da mensagem: {errorContent}");
                }

                throw new HttpRequestException($"Erro ao atualizar mensagem: {response.StatusCode} - {errorContent}");
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
            _logger.LogError(ex, "Erro inesperado ao atualizar mensagem para a instância: {InstanceName}, ID da mensagem: {MessageId}", 
                instanceName, request.Key?.Id ?? "desconhecido");
            throw;
        }
    }
}