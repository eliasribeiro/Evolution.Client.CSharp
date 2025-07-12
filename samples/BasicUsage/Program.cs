using Evolution.Client;
using Evolution.Client.CSharp.Modules;

// Exemplo básico de uso do Evolution.Client.CSharp.CSharp

Console.WriteLine("=== Evolution.Client.CSharp.CSharp - Exemplo Básico ===");

// Configuração do cliente
var baseUrl = "https://api.evolution.com";
var apiKey = "your-api-key-here";

// Criar cliente com configurações padrão
var client = EvolutionClient.Create(baseUrl, apiKey);

// Ou criar cliente com configurações customizadas
var clientWithOptions = EvolutionClient.Create(baseUrl, apiKey, options =>
{
    options.Timeout = TimeSpan.FromMinutes(2);
    options.MaxRetryAttempts = 5;
    options.LogHttpRequests = true;
});

try
{
    Console.WriteLine("Cliente criado com sucesso!");
    Console.WriteLine($"Módulos disponíveis:");
    Console.WriteLine($"- Instances: {client.Instances != null}");
    Console.WriteLine($"- Messages: {client.Messages != null}");
    Console.WriteLine($"- Webhooks: {client.Webhooks != null}");
    Console.WriteLine($"- Groups: {client.Groups != null}");
    Console.WriteLine($"- Contacts: {client.Contacts != null}");
    Console.WriteLine($"- Settings: {client.Settings != null}");
    Console.WriteLine($"- Profile: {client.Profile != null}");

    // Exemplo de obter informações da API (comentado para não fazer requisições reais)
    /*
    var apiInfo = await client.GetInformationAsync();
    Console.WriteLine($"API Status: {apiInfo.Status}");
    Console.WriteLine($"API Version: {apiInfo.Version}");
    Console.WriteLine($"API Message: {apiInfo.Message}");
    */

    // Exemplo de criação de instância (comentado para não fazer requisições reais)
    /*
    var createInstanceRequest = new CreateInstanceRequest
    {
        InstanceName = "minha-instancia",
        Number = "5511999999999",
        Qrcode = true,
        Integration = "WHATSAPP-BAILEYS",
        RejectCall = false,
        AlwaysOnline = true,
        ReadMessages = true,
        Webhook = new WebhookSettings
        {
            Url = "https://meusite.com/webhook",
            ByEvents = true,
            Events = new[] { "MESSAGE_RECEIVED", "CONNECTION_UPDATE" }
        }
    };

    var instanceResponse = await client.Instances.CreateAsync(createInstanceRequest);
    Console.WriteLine($"Instância criada: {instanceResponse.Instance?.InstanceName}");

    // Conectar instância
    var connectResponse = await client.Instances.ConnectAsync("minha-instancia");
    Console.WriteLine($"QR Code: {connectResponse.Base64}");

    // Verificar status de conexão
    var connectionStatus = await client.Instances.GetConnectionStatusAsync("minha-instancia");
    Console.WriteLine($"Status da conexão: {connectionStatus.State}");

    // Definir presença
    var setPresenceRequest = new SetPresenceRequest
    {
        Presence = "available"
    };
    var presenceResponse = await client.Instances.SetPresenceAsync("minha-instancia", setPresenceRequest);
    Console.WriteLine($"Presença definida: {presenceResponse.Presence}");

    // Exemplo de envio de mensagem
    var sendMessageRequest = new SendTextMessageRequest
    {
        Number = "5511888888888",
        Text = "Olá! Esta é uma mensagem de teste do Evolution.Client.CSharp.CSharp"
    };

    var messageResponse = await client.Messages.SendTextAsync("minha-instancia", sendMessageRequest);
    Console.WriteLine($"Mensagem enviada: {messageResponse.MessageId}");

    // Configurar webhook
    var webhookRequest = new SetWebhookRequest
    {
        Enabled = true,
        Url = "https://meusite.com/webhook",
        WebhookByEvents = true,
        WebhookBase64 = true,
        Events = new[] { "MESSAGE_RECEIVED", "CONNECTION_UPDATE", "QRCODE_UPDATED" }
    };
    var webhookResponse = await client.Webhooks.SetAsync("minha-instancia", webhookRequest);
    Console.WriteLine($"Webhook configurado para: {webhookResponse.Webhook?.Webhook?.Url}");

    // Configurar settings da instância
    var settingsRequest = new SetSettingsRequest
    {
        RejectCall = true,
        MsgCall = "Chamada rejeitada automaticamente",
        GroupsIgnore = false,
        AlwaysOnline = true,
        ReadMessages = true,
        ReadStatus = false,
        SyncFullHistory = true
    };
    var settingsResponse = await client.Settings.SetAsync("minha-instancia", settingsRequest);
    Console.WriteLine($"Settings configurados para instância: {settingsResponse.Settings?.InstanceName}");

    // Atualizar nome do perfil
    var updateNameRequest = new UpdateProfileNameRequest
    {
        Name = "Meu Bot WhatsApp"
    };
    var nameResponse = await client.Profile.UpdateProfileNameAsync("minha-instancia", updateNameRequest);
    Console.WriteLine($"Nome do perfil atualizado: {nameResponse.Success}");

    // Atualizar status do perfil
    var updateStatusRequest = new UpdateProfileStatusRequest
    {
        Status = "Disponível 24/7 para atendimento"
    };
    var statusResponse = await client.Profile.UpdateProfileStatusAsync("minha-instancia", updateStatusRequest);
    Console.WriteLine($"Status do perfil atualizado: {statusResponse.Success}");

    // Buscar configurações de privacidade
    var privacySettings = await client.Profile.FetchPrivacySettingsAsync("minha-instancia");
    Console.WriteLine($"Configurações de privacidade: {privacySettings.ReadReceipts}");

    // Atualizar configurações de privacidade
    var updatePrivacyRequest = new UpdatePrivacySettingsRequest
    {
        ReadReceipts = "all",
        Profile = "contacts",
        Status = "contacts",
        Online = "all",
        GroupAdd = "contacts",
        CallAdd = "all"
    };
    var privacyResponse = await client.Profile.UpdatePrivacySettingsAsync("minha-instancia", updatePrivacyRequest);
    Console.WriteLine($"Configurações de privacidade atualizadas: {privacyResponse.Success}");
    */

    Console.WriteLine("\nPara usar este exemplo:");
    Console.WriteLine("1. Configure sua URL base e API key");
    Console.WriteLine("2. Descomente os exemplos de uso");
    Console.WriteLine("3. Execute o programa");
}
catch (Exception ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
}
finally
{
    // Liberar recursos
    client.Dispose();
    clientWithOptions.Dispose();
}

Console.WriteLine("\nPressione qualquer tecla para sair...");
Console.ReadKey();
