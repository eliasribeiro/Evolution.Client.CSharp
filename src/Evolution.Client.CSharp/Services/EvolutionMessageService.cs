using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Evolution.Client.CSharp.Configuration;
using Evolution.Client.CSharp.Interfaces;
using Evolution.Client.CSharp.Models.Message;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Evolution.Client.CSharp.Services;

/// <summary>
/// Serviço para operações relacionadas a mensagens na API Evolution.
/// </summary>
public class EvolutionMessageService : IEvolutionMessageService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<EvolutionMessageService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    /// <summary>
    /// Inicializa uma nova instância do serviço de mensagens.
    /// </summary>
    /// <param name="httpClient">Cliente HTTP configurado.</param>
    /// <param name="options">Opções de configuração da API.</param>
    /// <param name="logger">Logger para registrar operações.</param>
    public EvolutionMessageService(HttpClient httpClient, IOptions<EvolutionApiOptions> options, ILogger<EvolutionMessageService> logger)
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

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
    }

    /// <summary>
    /// Envia uma mensagem de texto para um destinatário.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os dados da mensagem.</param>
    /// <returns>A resposta com informações da mensagem enviada.</returns>
    public async Task<SendTextResponse> SendTextAsync(string instanceName, SendTextRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request), "A requisição é obrigatória.");

        if (string.IsNullOrWhiteSpace(request.Number))
            throw new ArgumentException("O número do destinatário é obrigatório.", nameof(request.Number));

        if (string.IsNullOrWhiteSpace(request.Text))
            throw new ArgumentException("O texto da mensagem é obrigatório.", nameof(request.Text));

        try
        {
            _logger.LogInformation("Enviando mensagem de texto. Instância: {InstanceName}, Destinatário: {Number}", 
                instanceName, request.Number);

            var endpoint = $"/message/sendText/{instanceName}";
            
            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<SendTextResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("Mensagem de texto enviada com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                    instanceName, result?.Key.Id ?? "desconhecido", result?.Status ?? "desconhecido");

                return result ?? new SendTextResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao enviar mensagem de texto. Instância: {InstanceName}, Destinatário: {Number}. Status: {StatusCode}, Erro: {Error}", 
                    instanceName, request.Number, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' não encontrada ou número '{request.Number}' inválido.");
                }

                throw new HttpRequestException($"Erro ao enviar mensagem de texto: {response.StatusCode} - {errorContent}");
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
            _logger.LogError(ex, "Erro inesperado ao enviar mensagem de texto. Instância: {InstanceName}, Destinatário: {Number}", 
                instanceName, request.Number);
            throw;
        }
    }

    /// <summary>
    /// Envia um status para os contatos.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os dados do status.</param>
    /// <returns>A resposta com informações do status enviado.</returns>
    public async Task<SendStatusResponse> SendStatusAsync(string instanceName, SendStatusRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request), "A requisição é obrigatória.");

        if (string.IsNullOrWhiteSpace(request.Type))
            throw new ArgumentException("O tipo do status é obrigatório.", nameof(request.Type));

        if (string.IsNullOrWhiteSpace(request.Content))
            throw new ArgumentException("O conteúdo do status é obrigatório.", nameof(request.Content));

        try
        {
            _logger.LogInformation("Enviando status. Instância: {InstanceName}, Tipo: {Type}", 
                instanceName, request.Type);

            var endpoint = $"/message/sendStatus/{instanceName}";
            
            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<SendStatusResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("Status enviado com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                    instanceName, result?.Key.Id ?? "desconhecido", result?.Status ?? "desconhecido");

                return result ?? new SendStatusResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao enviar status. Instância: {InstanceName}, Tipo: {Type}. Status: {StatusCode}, Erro: {Error}", 
                    instanceName, request.Type, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' não encontrada.");
                }

                throw new HttpRequestException($"Erro ao enviar status: {response.StatusCode} - {errorContent}");
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
            _logger.LogError(ex, "Erro inesperado ao enviar status. Instância: {InstanceName}, Tipo: {Type}", 
                instanceName, request.Type);
            throw;
        }
    }

    /// <summary>
    /// Envia mídia (imagem, vídeo ou documento) para um destinatário.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os dados da mídia.</param>
    /// <returns>A resposta com informações da mídia enviada.</returns>
    public async Task<SendMediaResponse> SendMediaAsync(string instanceName, SendMediaRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request), "A requisição é obrigatória.");

        if (string.IsNullOrWhiteSpace(request.Number))
            throw new ArgumentException("O número do destinatário é obrigatório.", nameof(request.Number));

        if (string.IsNullOrWhiteSpace(request.MediaType))
            throw new ArgumentException("O tipo da mídia é obrigatório.", nameof(request.MediaType));

        if (string.IsNullOrWhiteSpace(request.Media))
            throw new ArgumentException("A mídia é obrigatória.", nameof(request.Media));

        try
        {
            _logger.LogInformation("Enviando mídia. Instância: {InstanceName}, Destinatário: {Number}, Tipo: {MediaType}", 
                instanceName, request.Number, request.MediaType);

            var endpoint = $"/message/sendMedia/{instanceName}";
            
            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<SendMediaResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("Mídia enviada com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                    instanceName, result?.Key.Id ?? "desconhecido", result?.Status ?? "desconhecido");

                return result ?? new SendMediaResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao enviar mídia. Instância: {InstanceName}, Destinatário: {Number}, Tipo: {MediaType}. Status: {StatusCode}, Erro: {Error}", 
                    instanceName, request.Number, request.MediaType, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' não encontrada ou número '{request.Number}' inválido.");
                }

                throw new HttpRequestException($"Erro ao enviar mídia: {response.StatusCode} - {errorContent}");
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
            _logger.LogError(ex, "Erro inesperado ao enviar mídia. Instância: {InstanceName}, Destinatário: {Number}, Tipo: {MediaType}", 
                instanceName, request.Number, request.MediaType);
            throw;
        }
    }

    /// <summary>
    /// Envia um áudio para um destinatário.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os dados do áudio.</param>
    /// <returns>A resposta com informações do áudio enviado.</returns>
    public async Task<SendAudioResponse> SendAudioAsync(string instanceName, SendAudioRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request), "A requisição é obrigatória.");

        if (string.IsNullOrWhiteSpace(request.Number))
            throw new ArgumentException("O número do destinatário é obrigatório.", nameof(request.Number));

        if (string.IsNullOrWhiteSpace(request.Audio))
            throw new ArgumentException("O áudio é obrigatório.", nameof(request.Audio));

        try
        {
            _logger.LogInformation("Enviando áudio. Instância: {InstanceName}, Destinatário: {Number}", 
                instanceName, request.Number);

            var endpoint = $"/message/sendAudio/{instanceName}";
            
            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<SendAudioResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("Áudio enviado com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                    instanceName, result?.Key.Id ?? "desconhecido", result?.Status ?? "desconhecido");

                return result ?? new SendAudioResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao enviar áudio. Instância: {InstanceName}, Destinatário: {Number}. Status: {StatusCode}, Erro: {Error}", 
                    instanceName, request.Number, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' não encontrada ou número '{request.Number}' inválido.");
                }

                throw new HttpRequestException($"Erro ao enviar áudio: {response.StatusCode} - {errorContent}");
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
            _logger.LogError(ex, "Erro inesperado ao enviar áudio. Instância: {InstanceName}, Destinatário: {Number}", 
                instanceName, request.Number);
            throw;
        }
    }

    /// <summary>
    /// Envia um sticker para um destinatário.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os dados do sticker.</param>
    /// <returns>A resposta com informações do sticker enviado.</returns>
    public async Task<SendStickerResponse> SendStickerAsync(string instanceName, SendStickerRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request), "A requisição é obrigatória.");

        if (string.IsNullOrWhiteSpace(request.Number))
            throw new ArgumentException("O número do destinatário é obrigatório.", nameof(request.Number));

        if (string.IsNullOrWhiteSpace(request.Sticker))
            throw new ArgumentException("O sticker é obrigatório.", nameof(request.Sticker));

        try
        {
            _logger.LogInformation("Enviando sticker. Instância: {InstanceName}, Destinatário: {Number}", 
                instanceName, request.Number);

            var endpoint = $"/message/sendSticker/{instanceName}";
            
            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<SendStickerResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("Sticker enviado com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                    instanceName, result?.Key.Id ?? "desconhecido", result?.Status ?? "desconhecido");

                return result ?? new SendStickerResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao enviar sticker. Instância: {InstanceName}, Destinatário: {Number}. Status: {StatusCode}, Erro: {Error}", 
                    instanceName, request.Number, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' não encontrada ou número '{request.Number}' inválido.");
                }

                throw new HttpRequestException($"Erro ao enviar sticker: {response.StatusCode} - {errorContent}");
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
            _logger.LogError(ex, "Erro inesperado ao enviar sticker. Instância: {InstanceName}, Destinatário: {Number}", 
                instanceName, request.Number);
            throw;
        }
    }

    /// <summary>
    /// Envia uma localização para um destinatário.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os dados da localização.</param>
    /// <returns>A resposta com informações da localização enviada.</returns>
    public async Task<SendLocationResponse> SendLocationAsync(string instanceName, SendLocationRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request), "A requisição é obrigatória.");

        if (string.IsNullOrWhiteSpace(request.Number))
            throw new ArgumentException("O número do destinatário é obrigatório.", nameof(request.Number));

        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("O nome da localização é obrigatório.", nameof(request.Name));

        if (string.IsNullOrWhiteSpace(request.Address))
            throw new ArgumentException("O endereço da localização é obrigatório.", nameof(request.Address));

        if (request.Latitude == 0)
            throw new ArgumentException("A latitude da localização é obrigatória.", nameof(request.Latitude));

        if (request.Longitude == 0)
            throw new ArgumentException("A longitude da localização é obrigatória.", nameof(request.Longitude));

        try
        {
            _logger.LogInformation("Enviando localização. Instância: {InstanceName}, Destinatário: {Number}, Local: {Name}", 
                instanceName, request.Number, request.Name);

            var endpoint = $"/message/sendLocation/{instanceName}";
            
            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<SendLocationResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("Localização enviada com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                    instanceName, result?.Key.Id ?? "desconhecido", result?.Status ?? "desconhecido");

                return result ?? new SendLocationResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao enviar localização. Instância: {InstanceName}, Destinatário: {Number}, Local: {Name}. Status: {StatusCode}, Erro: {Error}", 
                    instanceName, request.Number, request.Name, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' não encontrada ou número '{request.Number}' inválido.");
                }

                throw new HttpRequestException($"Erro ao enviar localização: {response.StatusCode} - {errorContent}");
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
            _logger.LogError(ex, "Erro inesperado ao enviar localização. Instância: {InstanceName}, Destinatário: {Number}, Local: {Name}", 
                instanceName, request.Number, request.Name);
            throw;
        }
    }

    /// <summary>
    /// Envia contatos para um destinatário.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os dados dos contatos.</param>
    /// <returns>A resposta com informações dos contatos enviados.</returns>
    public async Task<SendContactResponse> SendContactAsync(string instanceName, SendContactRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request), "A requisição é obrigatória.");

        if (string.IsNullOrWhiteSpace(request.Number))
            throw new ArgumentException("O número do destinatário é obrigatório.", nameof(request.Number));

        if (request.Contact == null || !request.Contact.Any())
            throw new ArgumentException("Pelo menos um contato é obrigatório.", nameof(request.Contact));

        try
        {
            _logger.LogInformation("Enviando contatos. Instância: {InstanceName}, Destinatário: {Number}, Quantidade: {Count}", 
                instanceName, request.Number, request.Contact.Count);

            var endpoint = $"/message/sendContact/{instanceName}";
            
            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<SendContactResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("Contatos enviados com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                    instanceName, result?.Key.Id ?? "desconhecido", result?.Status ?? "desconhecido");

                return result ?? new SendContactResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao enviar contatos. Instância: {InstanceName}, Destinatário: {Number}. Status: {StatusCode}, Erro: {Error}", 
                    instanceName, request.Number, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' não encontrada ou número '{request.Number}' inválido.");
                }

                throw new HttpRequestException($"Erro ao enviar contatos: {response.StatusCode} - {errorContent}");
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
            _logger.LogError(ex, "Erro inesperado ao enviar contatos. Instância: {InstanceName}, Destinatário: {Number}", 
                instanceName, request.Number);
            throw;
        }
    }

    /// <summary>
    /// Envia reação a uma mensagem.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os dados da reação.</param>
    /// <returns>A resposta com informações da reação enviada.</returns>
    public async Task<SendReactionResponse> SendReactionAsync(string instanceName, SendReactionRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request), "A requisição é obrigatória.");

        if (request.Key == null)
            throw new ArgumentException("A chave da mensagem é obrigatória.", nameof(request.Key));

        if (string.IsNullOrWhiteSpace(request.Key.RemoteJid))
            throw new ArgumentException("O JID remoto é obrigatório.", nameof(request.Key.RemoteJid));

        if (string.IsNullOrWhiteSpace(request.Key.Id))
            throw new ArgumentException("O ID da mensagem é obrigatório.", nameof(request.Key.Id));

        if (string.IsNullOrWhiteSpace(request.Reaction))
            throw new ArgumentException("A reação é obrigatória.", nameof(request.Reaction));

        try
        {
            _logger.LogInformation("Enviando reação. Instância: {InstanceName}, Mensagem: {MessageId}, Reação: {Reaction}", 
                instanceName, request.Key.Id, request.Reaction);

            var endpoint = $"/message/sendReaction/{instanceName}";
            
            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<SendReactionResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("Reação enviada com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                    instanceName, result?.Key.Id ?? "desconhecido", result?.Status ?? "desconhecido");

                return result ?? new SendReactionResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao enviar reação. Instância: {InstanceName}, Mensagem: {MessageId}. Status: {StatusCode}, Erro: {Error}", 
                    instanceName, request.Key.Id, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' não encontrada ou mensagem '{request.Key.Id}' inválida.");
                }

                throw new HttpRequestException($"Erro ao enviar reação: {response.StatusCode} - {errorContent}");
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
            _logger.LogError(ex, "Erro inesperado ao enviar reação. Instância: {InstanceName}, Mensagem: {MessageId}", 
                instanceName, request.Key.Id);
            throw;
        }
    }

    /// <summary>
    /// Envia enquete para um destinatário.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os dados da enquete.</param>
    /// <returns>A resposta com informações da enquete enviada.</returns>
    public async Task<SendPollResponse> SendPollAsync(string instanceName, SendPollRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request), "A requisição é obrigatória.");

        if (string.IsNullOrWhiteSpace(request.Number))
            throw new ArgumentException("O número do destinatário é obrigatório.", nameof(request.Number));

        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("O nome da enquete é obrigatório.", nameof(request.Name));

        if (request.SelectableCount <= 0)
            throw new ArgumentException("O número de opções selecionáveis deve ser maior que zero.", nameof(request.SelectableCount));

        if (request.Values == null || !request.Values.Any())
            throw new ArgumentException("Pelo menos uma opção é obrigatória.", nameof(request.Values));

        try
        {
            _logger.LogInformation("Enviando enquete. Instância: {InstanceName}, Destinatário: {Number}, Título: {Name}", 
                instanceName, request.Number, request.Name);

            var endpoint = $"/message/sendPoll/{instanceName}";
            
            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<SendPollResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("Enquete enviada com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                    instanceName, result?.Key.Id ?? "desconhecido", result?.Status ?? "desconhecido");

                return result ?? new SendPollResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao enviar enquete. Instância: {InstanceName}, Destinatário: {Number}. Status: {StatusCode}, Erro: {Error}", 
                    instanceName, request.Number, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' não encontrada ou número '{request.Number}' inválido.");
                }

                throw new HttpRequestException($"Erro ao enviar enquete: {response.StatusCode} - {errorContent}");
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
            _logger.LogError(ex, "Erro inesperado ao enviar enquete. Instância: {InstanceName}, Destinatário: {Number}", 
                instanceName, request.Number);
            throw;
        }
    }

    /// <summary>
    /// Envia lista para um destinatário.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os dados da lista.</param>
    /// <returns>A resposta com informações da lista enviada.</returns>
    public async Task<SendListResponse> SendListAsync(string instanceName, SendListRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request), "A requisição é obrigatória.");

        if (string.IsNullOrWhiteSpace(request.Number))
            throw new ArgumentException("O número do destinatário é obrigatório.", nameof(request.Number));

        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("O título da lista é obrigatório.", nameof(request.Title));

        if (string.IsNullOrWhiteSpace(request.Description))
            throw new ArgumentException("A descrição da lista é obrigatória.", nameof(request.Description));

        if (string.IsNullOrWhiteSpace(request.ButtonText))
            throw new ArgumentException("O texto do botão é obrigatório.", nameof(request.ButtonText));

        if (string.IsNullOrWhiteSpace(request.FooterText))
            throw new ArgumentException("O texto do rodapé é obrigatório.", nameof(request.FooterText));

        if (request.Values == null || !request.Values.Any())
            throw new ArgumentException("Pelo menos um valor é obrigatório.", nameof(request.Values));

        try
        {
            _logger.LogInformation("Enviando lista. Instância: {InstanceName}, Destinatário: {Number}, Título: {Title}", 
                instanceName, request.Number, request.Title);

            var endpoint = $"/message/sendList/{instanceName}";
            
            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<SendListResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("Lista enviada com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                    instanceName, result?.Key.Id ?? "desconhecido", result?.Status ?? "desconhecido");

                return result ?? new SendListResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao enviar lista. Instância: {InstanceName}, Destinatário: {Number}. Status: {StatusCode}, Erro: {Error}", 
                    instanceName, request.Number, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' não encontrada ou número '{request.Number}' inválido.");
                }

                throw new HttpRequestException($"Erro ao enviar lista: {response.StatusCode} - {errorContent}");
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
            _logger.LogError(ex, "Erro inesperado ao enviar lista. Instância: {InstanceName}, Destinatário: {Number}", 
                instanceName, request.Number);
            throw;
        }
    }

    /// <summary>
    /// Envia botões para um destinatário.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os dados dos botões.</param>
    /// <returns>A resposta com informações dos botões enviados.</returns>
    public async Task<SendButtonResponse> SendButtonAsync(string instanceName, SendButtonRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("O nome da instância é obrigatório.", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request), "A requisição é obrigatória.");

        if (string.IsNullOrWhiteSpace(request.Number))
            throw new ArgumentException("O número do destinatário é obrigatório.", nameof(request.Number));

        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("O título dos botões é obrigatório.", nameof(request.Title));

        if (string.IsNullOrWhiteSpace(request.Description))
            throw new ArgumentException("A descrição dos botões é obrigatória.", nameof(request.Description));

        if (string.IsNullOrWhiteSpace(request.Footer))
            throw new ArgumentException("O rodapé dos botões é obrigatório.", nameof(request.Footer));

        if (request.Buttons == null || !request.Buttons.Any())
            throw new ArgumentException("Pelo menos um botão é obrigatório.", nameof(request.Buttons));

        try
        {
            _logger.LogInformation("Enviando botões. Instância: {InstanceName}, Destinatário: {Number}, Título: {Title}", 
                instanceName, request.Number, request.Title);

            var endpoint = $"/message/sendButton/{instanceName}";
            
            var jsonContent = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<SendButtonResponse>(responseContent, _jsonOptions);

                _logger.LogInformation("Botões enviados com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                    instanceName, result?.Key.Id ?? "desconhecido", result?.Status ?? "desconhecido");

                return result ?? new SendButtonResponse();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao enviar botões. Instância: {InstanceName}, Destinatário: {Number}. Status: {StatusCode}, Erro: {Error}", 
                    instanceName, request.Number, response.StatusCode, errorContent);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new InvalidOperationException($"Instância '{instanceName}' não encontrada ou número '{request.Number}' inválido.");
                }

                throw new HttpRequestException($"Erro ao enviar botões: {response.StatusCode} - {errorContent}");
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
            _logger.LogError(ex, "Erro inesperado ao enviar botões. Instância: {InstanceName}, Destinatário: {Number}", 
                instanceName, request.Number);
            throw;
        }
    }
}
