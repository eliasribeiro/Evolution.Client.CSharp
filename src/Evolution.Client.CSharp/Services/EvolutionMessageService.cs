using System.Net;
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

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
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
}
