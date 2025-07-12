using Evolution.Client.CSharp.Core.Http;
using Evolution.Client.CSharp.Models;
using Evolution.Client.CSharp.Modules;
using Microsoft.Extensions.Logging;

namespace Evolution.Client;

/// <summary>
/// Cliente principal para interação com a Evolution API v2
/// </summary>
public sealed class EvolutionClient : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly IHttpService _httpService;
    private readonly ILogger<EvolutionClient>? _logger;
    private readonly bool _disposeHttpClient;

    /// <summary>
    /// Módulo para gerenciamento de instâncias
    /// </summary>
    public IInstancesModule Instances { get; }

    /// <summary>
    /// Módulo para gerenciamento de mensagens
    /// </summary>
    public IMessagesModule Messages { get; }

    /// <summary>
    /// Módulo para gerenciamento de webhooks
    /// </summary>
    public IWebhooksModule Webhooks { get; }

    /// <summary>
    /// Módulo para gerenciamento de grupos
    /// </summary>
    public IGroupsModule Groups { get; }

    /// <summary>
    /// Módulo para gerenciamento de contatos
    /// </summary>
    public IContactsModule Contacts { get; }

    /// <summary>
    /// Módulo para gerenciamento de configurações
    /// </summary>
    public ISettingsModule Settings { get; }

    /// <summary>
    /// Módulo para gerenciamento de perfil
    /// </summary>
    public IProfileModule Profile { get; }

    /// <summary>
    /// Módulo para operações de chat
    /// </summary>
    public IChatModule Chat { get; }

    /// <summary>
    /// Módulo para integração com TypeBot
    /// </summary>
    public ITypeBotModule TypeBot { get; }

    /// <summary>
    /// Módulo para integração com OpenAI
    /// </summary>
    public IOpenAIModule OpenAI { get; }

    /// <summary>
    /// Módulo para integração com Evolution Bot
    /// </summary>
    public IEvolutionBotModule EvolutionBot { get; }

    /// <summary>
    /// Módulo para integração com Dify
    /// </summary>
    public IDifyModule Dify { get; }

    /// <summary>
    /// Módulo para integração com Flowise
    /// </summary>
    public IFlowiseModule Flowise { get; }

    /// <summary>
    /// Módulo para integração com Chatwoot
    /// </summary>
    public IChatwootModule Chatwoot { get; }

    /// <summary>
    /// Módulo para integração com WebSocket
    /// </summary>
    public IWebSocketModule WebSocket { get; }

    /// <summary>
    /// Módulo para integração com SQS
    /// </summary>
    public ISQSModule SQS { get; }

    /// <summary>
    /// Módulo para integração com RabbitMQ
    /// </summary>
    public IRabbitMQModule RabbitMQ { get; }

    internal EvolutionClient(
        HttpClient httpClient,
        EvolutionClientOptions options,
        ILogger<EvolutionClient>? logger = null,
        bool disposeHttpClient = true)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger;
        _disposeHttpClient = disposeHttpClient;

        _httpService = new HttpService(_httpClient, options, _logger);

        // Inicializar módulos
        Instances = new InstancesModule(_httpService);
        Messages = new MessagesModule(_httpService);
        Webhooks = new WebhooksModule(_httpService);
        Groups = new GroupsModule(_httpService);
        Contacts = new ContactsModule(_httpService);
        Settings = new SettingsModule(_httpService);
        Profile = new ProfileModule(_httpService);
        Chat = new ChatModule(_httpService);

        // Inicializar módulos de integração
        TypeBot = new TypeBotModule(_httpService);
        OpenAI = new OpenAIModule(_httpService);
        EvolutionBot = new EvolutionBotModule(_httpService);
        Dify = new DifyModule(_httpService);
        Flowise = new FlowiseModule(_httpService);
        Chatwoot = new ChatwootModule(_httpService);
        WebSocket = new WebSocketModule(_httpService);
        SQS = new SQSModule(_httpService);
        RabbitMQ = new RabbitMQModule(_httpService);
    }

    /// <summary>
    /// Cria uma nova instância do EvolutionClient
    /// </summary>
    /// <param name="baseUrl">URL base da API Evolution</param>
    /// <param name="apiKey">Chave de API para autenticação</param>
    /// <param name="configure">Configuração adicional opcional</param>
    /// <returns>Nova instância do EvolutionClient</returns>
    public static EvolutionClient Create(
        string baseUrl,
        string apiKey,
        Action<EvolutionClientOptions>? configure = null)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
            throw new ArgumentException("Base URL não pode ser nula ou vazia", nameof(baseUrl));

        if (string.IsNullOrWhiteSpace(apiKey))
            throw new ArgumentException("API Key não pode ser nula ou vazia", nameof(apiKey));

        var options = new EvolutionClientOptions
        {
            BaseUrl = baseUrl,
            ApiKey = apiKey
        };

        configure?.Invoke(options);

        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(baseUrl);
        httpClient.DefaultRequestHeaders.Add("apikey", apiKey);
        httpClient.Timeout = options.Timeout;

        return new EvolutionClient(httpClient, options, disposeHttpClient: true);
    }

    /// <summary>
    /// Cria uma nova instância do EvolutionClient usando um HttpClient existente
    /// </summary>
    /// <param name="httpClient">HttpClient configurado</param>
    /// <param name="options">Opções de configuração</param>
    /// <param name="logger">Logger opcional</param>
    /// <returns>Nova instância do EvolutionClient</returns>
    public static EvolutionClient Create(
        HttpClient httpClient,
        EvolutionClientOptions options,
        ILogger<EvolutionClient>? logger = null)
    {
        return new EvolutionClient(httpClient, options, logger, disposeHttpClient: false);
    }

    /// <summary>
    /// Obtém informações sobre a Evolution API
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Informações da API</returns>
    public async Task<ApiInformation> GetInformationAsync(CancellationToken cancellationToken = default)
    {
        return await _httpService.GetAsync<ApiInformation>("", cancellationToken);
    }

    /// <summary>
    /// Libera os recursos utilizados pelo cliente
    /// </summary>
    public void Dispose()
    {
        if (_disposeHttpClient)
        {
            _httpClient?.Dispose();
        }
    }
}
