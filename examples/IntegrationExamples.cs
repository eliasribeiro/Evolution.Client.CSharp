using Evolution.Client;
using Evolution.Client.Modules;

namespace Evolution.Client.Examples;

/// <summary>
/// Exemplos de uso das funcionalidades de integração
/// </summary>
public class IntegrationExamples
{
    private readonly EvolutionClient _client;

    public IntegrationExamples()
    {
        _client = EvolutionClient.Create("https://your-evolution-api-url.com", "your-api-key");
    }

    #region TypeBot Examples

    /// <summary>
    /// Exemplo de criação e configuração de TypeBot
    /// </summary>
    public async Task TypeBotExample()
    {
        var instanceName = "my-instance";

        // Criar TypeBot
        var createRequest = new CreateTypeBotRequest
        {
            Enabled = true,
            Url = "https://typebot.io",
            Typebot = "my-typebot-id",
            TriggerType = "keyword",
            TriggerOperator = "equals",
            TriggerValue = "oi",
            Expire = 30,
            KeywordFinish = "sair",
            DelayMessage = 1000,
            UnknownMessage = "Não entendi, digite 'sair' para finalizar",
            ListeningFromMe = false,
            StopBotFromMe = true,
            KeepOpen = false,
            DebounceTime = 500
        };

        var response = await _client.TypeBot.CreateAsync(instanceName, createRequest);
        Console.WriteLine($"TypeBot criado: {response.TypeBot?.InstanceName}");

        // Iniciar TypeBot para um contato
        var startRequest = new StartTypeBotRequest
        {
            Number = "5511999999999",
            Variables = new Dictionary<string, object>
            {
                { "name", "João" },
                { "email", "joao@example.com" }
            }
        };

        await _client.TypeBot.StartAsync(instanceName, startRequest);
        Console.WriteLine("TypeBot iniciado para o contato");

        // Buscar configuração do TypeBot
        var config = await _client.TypeBot.FindAsync(instanceName);
        Console.WriteLine($"TypeBot habilitado: {config.TypeBot?.TypeBot?.Enabled}");

        // Configurar settings
        var settingsRequest = new TypeBotSettingsRequest
        {
            Enabled = true,
            Description = "Configurações do TypeBot"
        };

        await _client.TypeBot.SetSettingsAsync(instanceName, settingsRequest);
        Console.WriteLine("Settings do TypeBot configuradas");
    }

    #endregion

    #region OpenAI Examples

    /// <summary>
    /// Exemplo de configuração de bot OpenAI
    /// </summary>
    public async Task OpenAIExample()
    {
        var instanceName = "my-instance";

        // Configurar credenciais OpenAI
        var credsRequest = new SetOpenAICredsRequest
        {
            Name = "My OpenAI Creds",
            ApiKey = "sk-your-openai-api-key",
            OrganizationId = "org-your-organization-id"
        };

        await _client.OpenAI.SetCredsAsync(instanceName, credsRequest);
        Console.WriteLine("Credenciais OpenAI configuradas");

        // Criar bot OpenAI
        var createBotRequest = new CreateOpenAIBotRequest
        {
            Enabled = true,
            OpenaiCredsId = "my-creds-id",
            BotType = "assistant",
            Model = "gpt-4",
            SystemMessages = new[] { "Você é um assistente útil e amigável." },
            MaxTokens = 1000,
            TriggerType = "all",
            TriggerOperator = "contains",
            TriggerValue = "",
            Expire = 60,
            DelayMessage = 2000,
            UnknownMessage = "Desculpe, não consegui processar sua mensagem.",
            ListeningFromMe = false,
            StopBotFromMe = true,
            KeepOpen = true,
            DebounceTime = 1000
        };

        var botResponse = await _client.OpenAI.CreateBotAsync(instanceName, createBotRequest);
        Console.WriteLine($"Bot OpenAI criado: {botResponse.Bot?.InstanceName}");

        // Buscar bots
        var bots = await _client.OpenAI.FindBotsAsync(instanceName);
        Console.WriteLine($"Total de bots: {bots.Bots?.Length}");
    }

    #endregion

    #region Evolution Bot Examples

    /// <summary>
    /// Exemplo de configuração de Evolution Bot
    /// </summary>
    public async Task EvolutionBotExample()
    {
        var instanceName = "my-instance";

        // Criar Evolution Bot
        var createRequest = new CreateEvolutionBotRequest
        {
            Enabled = true,
            Description = "Bot Evolution para atendimento",
            TriggerType = "keyword",
            TriggerOperator = "startsWith",
            TriggerValue = "bot",
            Expire = 45,
            KeywordFinish = "finalizar",
            DelayMessage = 1500,
            UnknownMessage = "Comando não reconhecido",
            ListeningFromMe = false,
            StopBotFromMe = true,
            KeepOpen = false,
            DebounceTime = 800
        };

        var response = await _client.EvolutionBot.CreateBotAsync(instanceName, createRequest);
        Console.WriteLine($"Evolution Bot criado: {response.Bot?.InstanceName}");

        // Buscar bots
        var bots = await _client.EvolutionBot.FetchBotsAsync(instanceName);
        Console.WriteLine($"Total de Evolution Bots: {bots.Bots?.Length}");
    }

    #endregion

    #region Dify Examples

    /// <summary>
    /// Exemplo de configuração de Dify
    /// </summary>
    public async Task DifyExample()
    {
        var instanceName = "my-instance";

        // Criar bot Dify
        var createRequest = new CreateDifyBotRequest
        {
            Enabled = true,
            Description = "Bot Dify para IA",
            ApiUrl = "https://api.dify.ai",
            ApiKey = "your-dify-api-key",
            TriggerType = "all",
            TriggerOperator = "contains",
            TriggerValue = "",
            Expire = 30,
            DelayMessage = 1000,
            UnknownMessage = "Não consegui processar sua solicitação",
            ListeningFromMe = false,
            StopBotFromMe = true,
            KeepOpen = true,
            DebounceTime = 500
        };

        var response = await _client.Dify.CreateBotAsync(instanceName, createRequest);
        Console.WriteLine($"Bot Dify criado: {response.Bot?.InstanceName}");

        // Configurar settings
        var settingsRequest = new DifySettingsRequest
        {
            Enabled = true,
            Description = "Configurações do Dify"
        };

        await _client.Dify.SetSettingsAsync(instanceName, settingsRequest);
        Console.WriteLine("Settings do Dify configuradas");
    }

    #endregion

    #region Flowise Examples

    /// <summary>
    /// Exemplo de configuração de Flowise
    /// </summary>
    public async Task FlowiseExample()
    {
        var instanceName = "my-instance";

        // Criar bot Flowise
        var createRequest = new CreateFlowiseBotRequest
        {
            Enabled = true,
            Description = "Bot Flowise para automação",
            ApiUrl = "https://flowise.example.com",
            ApiKey = "your-flowise-api-key",
            FlowId = "your-flow-id",
            TriggerType = "keyword",
            TriggerOperator = "equals",
            TriggerValue = "flow",
            Expire = 60,
            DelayMessage = 2000,
            UnknownMessage = "Fluxo não encontrado",
            ListeningFromMe = false,
            StopBotFromMe = true,
            KeepOpen = false,
            DebounceTime = 1000
        };

        var response = await _client.Flowise.CreateBotAsync(instanceName, createRequest);
        Console.WriteLine($"Bot Flowise criado: {response.Bot?.InstanceName}");

        // Buscar sessões
        var sessions = await _client.Flowise.FindSessionsAsync(instanceName);
        Console.WriteLine($"Total de sessões: {sessions.Sessions?.Length}");
    }

    #endregion

    #region Simple Integrations Examples

    /// <summary>
    /// Exemplo de configuração de integrações simples
    /// </summary>
    public async Task SimpleIntegrationsExample()
    {
        var instanceName = "my-instance";

        // Configurar Chatwoot
        var chatwootRequest = new SetChatwootRequest
        {
            Enabled = true,
            AccountId = "your-account-id",
            Token = "your-chatwoot-token",
            Url = "https://chatwoot.example.com",
            SignMsg = true,
            ReopenConversation = true,
            ConversationPending = false,
            NameInbox = "WhatsApp Evolution",
            MergeBrazilContacts = true,
            ImportContacts = true,
            ImportMessages = true,
            DaysLimitImportMessages = 7,
            AutoCreate = true,
            Organization = "Minha Empresa"
        };

        await _client.Chatwoot.SetAsync(instanceName, chatwootRequest);
        Console.WriteLine("Chatwoot configurado");

        // Testar conexão Chatwoot
        var chatwootTest = await _client.Chatwoot.TestConnectionAsync(instanceName);
        Console.WriteLine($"Teste Chatwoot: {chatwootTest.Success}");

        // Configurar WebSocket
        var websocketRequest = new SetWebSocketRequest
        {
            Enabled = true,
            Events = WebSocketEvents.AllEvents,
            ConnectionTimeout = 30,
            PingInterval = 25,
            AutoReconnect = true,
            MaxReconnectAttempts = 5,
            ReconnectInterval = 5
        };

        await _client.WebSocket.SetAsync(instanceName, websocketRequest);
        Console.WriteLine("WebSocket configurado");

        // Testar conexão WebSocket
        var wsTest = await _client.WebSocket.TestConnectionAsync(instanceName);
        Console.WriteLine($"Teste WebSocket: {wsTest.Success}");

        // Configurar SQS
        var sqsRequest = new SetSQSRequest
        {
            Enabled = true,
            AccessKeyId = "your-access-key",
            SecretAccessKey = "your-secret-key",
            Region = "us-east-1",
            QueueUrl = "https://sqs.us-east-1.amazonaws.com/123456789/my-queue",
            Events = SQSEvents.AllEvents,
            DelaySeconds = 0,
            VisibilityTimeoutSeconds = 30,
            MessageRetentionPeriod = 345600,
            MaxMessageSize = 262144,
            UseFifoQueue = false
        };

        await _client.SQS.SetAsync(instanceName, sqsRequest);
        Console.WriteLine("SQS configurado");

        // Testar conexão SQS
        var sqsTest = await _client.SQS.TestConnectionAsync(instanceName);
        Console.WriteLine($"Teste SQS: {sqsTest.Success}");

        // Configurar RabbitMQ
        var rabbitmqRequest = new SetRabbitMQRequest
        {
            Enabled = true,
            Uri = "amqp://user:password@localhost:5672",
            Exchange = "evolution-exchange",
            ExchangeType = "topic",
            ExchangeDurable = true,
            Events = RabbitMQEvents.AllEvents,
            DefaultRoutingKey = "evolution.events",
            QueueDurable = true,
            ConnectionTimeout = 30,
            HeartbeatInterval = 60,
            AutoReconnect = true,
            MaxReconnectAttempts = 5,
            ReconnectInterval = 5
        };

        await _client.RabbitMQ.SetAsync(instanceName, rabbitmqRequest);
        Console.WriteLine("RabbitMQ configurado");

        // Testar conexão RabbitMQ
        var rabbitTest = await _client.RabbitMQ.TestConnectionAsync(instanceName);
        Console.WriteLine($"Teste RabbitMQ: {rabbitTest.Success}");
    }

    #endregion

    /// <summary>
    /// Exemplo completo de configuração de múltiplas integrações
    /// </summary>
    public async Task CompleteIntegrationSetup()
    {
        var instanceName = "production-instance";

        try
        {
            // Configurar TypeBot para atendimento inicial
            await TypeBotExample();

            // Configurar OpenAI para respostas inteligentes
            await OpenAIExample();

            // Configurar Chatwoot para gestão de conversas
            await SimpleIntegrationsExample();

            Console.WriteLine("Todas as integrações foram configuradas com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao configurar integrações: {ex.Message}");
        }
    }

    /// <summary>
    /// Exemplo avançado de monitoramento e estatísticas das integrações
    /// </summary>
    public async Task MonitoringExample()
    {
        var instanceName = "my-instance";

        try
        {
            // Obter estatísticas do Chatwoot
            var chatwootStats = await _client.Chatwoot.GetStatsAsync(instanceName);
            Console.WriteLine($"Chatwoot - Conversas abertas: {chatwootStats.Stats?.OpenConversations}");
            Console.WriteLine($"Chatwoot - Total de mensagens: {chatwootStats.Stats?.MessagesSent + chatwootStats.Stats?.MessagesReceived}");

            // Obter estatísticas do WebSocket
            var wsStats = await _client.WebSocket.GetStatsAsync(instanceName);
            Console.WriteLine($"WebSocket - Status: {wsStats.ConnectionStatus?.Status}");
            Console.WriteLine($"WebSocket - Mensagens enviadas: {wsStats.Stats?.MessagesSent}");
            Console.WriteLine($"WebSocket - Uptime atual: {wsStats.Stats?.CurrentUptime} segundos");

            // Obter estatísticas do SQS
            var sqsStats = await _client.SQS.GetStatsAsync(instanceName);
            Console.WriteLine($"SQS - Mensagens na fila: {sqsStats.Stats?.ApproximateNumberOfMessages}");
            Console.WriteLine($"SQS - Taxa de sucesso: {sqsStats.Stats?.SuccessRate:F2}%");

            // Obter estatísticas do RabbitMQ
            var rabbitStats = await _client.RabbitMQ.GetStatsAsync(instanceName);
            Console.WriteLine($"RabbitMQ - Mensagens publicadas: {rabbitStats.Stats?.MessagesPublished}");
            Console.WriteLine($"RabbitMQ - Taxa de publicação: {rabbitStats.Stats?.PublishRate:F2} msg/s");

            // Sincronizar dados do Chatwoot
            await _client.Chatwoot.SyncContactsAsync(instanceName);
            await _client.Chatwoot.SyncMessagesAsync(instanceName, 3); // Últimos 3 dias

            // Testar conectividade
            await TestAllConnections(instanceName);

            Console.WriteLine("Monitoramento concluído com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro no monitoramento: {ex.Message}");
        }
    }

    /// <summary>
    /// Testa a conectividade de todas as integrações
    /// </summary>
    private async Task TestAllConnections(string instanceName)
    {
        Console.WriteLine("Testando conectividade das integrações...");

        // Testar Chatwoot
        var chatwootTest = await _client.Chatwoot.TestConnectionAsync(instanceName);
        Console.WriteLine($"✓ Chatwoot: {(chatwootTest.Success ? "Conectado" : "Falha")}");

        // Testar WebSocket
        var wsTest = await _client.WebSocket.TestConnectionAsync(instanceName);
        Console.WriteLine($"✓ WebSocket: {(wsTest.Success ? "Conectado" : "Falha")}");

        // Ping WebSocket
        var wsPing = await _client.WebSocket.PingAsync(instanceName);
        Console.WriteLine($"✓ WebSocket Ping: {(wsPing.Success ? "OK" : "Falha")}");

        // Testar SQS
        var sqsTest = await _client.SQS.TestConnectionAsync(instanceName);
        Console.WriteLine($"✓ SQS: {(sqsTest.Success ? "Conectado" : "Falha")}");

        // Testar RabbitMQ
        var rabbitTest = await _client.RabbitMQ.TestConnectionAsync(instanceName);
        Console.WriteLine($"✓ RabbitMQ: {(rabbitTest.Success ? "Conectado" : "Falha")}");

        // Verificar exchange e fila do RabbitMQ
        var exchangeCheck = await _client.RabbitMQ.CheckExchangeAsync(instanceName);
        Console.WriteLine($"✓ RabbitMQ Exchange: {(exchangeCheck.Success ? "OK" : "Falha")}");

        var queueCheck = await _client.RabbitMQ.CheckQueueAsync(instanceName);
        Console.WriteLine($"✓ RabbitMQ Queue: {(queueCheck.Success ? "OK" : "Falha")}");
    }

    /// <summary>
    /// Exemplo de envio de mensagens de teste
    /// </summary>
    public async Task SendTestMessages()
    {
        var instanceName = "my-instance";

        try
        {
            // Enviar mensagem de teste para SQS
            await _client.SQS.SendTestMessageAsync(instanceName, "Mensagem de teste SQS");
            Console.WriteLine("Mensagem de teste enviada para SQS");

            // Publicar mensagem de teste no RabbitMQ
            await _client.RabbitMQ.PublishTestMessageAsync(instanceName, "Mensagem de teste RabbitMQ", "test.routing.key");
            Console.WriteLine("Mensagem de teste publicada no RabbitMQ");

            Console.WriteLine("Mensagens de teste enviadas com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar mensagens de teste: {ex.Message}");
        }
    }
}
