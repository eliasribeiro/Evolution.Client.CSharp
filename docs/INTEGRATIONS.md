# Integrações Evolution Client

Este documento descreve as integrações disponíveis no Evolution Client para C#, incluindo configuração, uso e exemplos práticos.

## Integrações Disponíveis

### 1. 🤖 **Chatwoot** - Gestão de Conversas
Integração completa com Chatwoot para gestão de conversas e atendimento ao cliente.

**Recursos:**
- Configuração de conta e autenticação
- Sincronização de contatos e mensagens
- Gestão de conversas e caixas de entrada
- Estatísticas e monitoramento
- Teste de conectividade

**Exemplo de uso:**
```csharp
var chatwootRequest = new SetChatwootRequest
{
    Enabled = true,
    AccountId = "your-account-id",
    Token = "your-chatwoot-token",
    Url = "https://chatwoot.example.com",
    NameInbox = "WhatsApp Evolution",
    ImportContacts = true,
    ImportMessages = true,
    DaysLimitImportMessages = 7
};

await client.Chatwoot.SetAsync("instance-name", chatwootRequest);

// Testar conexão
var test = await client.Chatwoot.TestConnectionAsync("instance-name");

// Obter estatísticas
var stats = await client.Chatwoot.GetStatsAsync("instance-name");
```

### 2. 🔌 **WebSocket** - Comunicação em Tempo Real
Configuração de WebSocket para eventos em tempo real.

**Recursos:**
- Configuração de eventos personalizados
- Reconexão automática
- Monitoramento de latência
- Estatísticas de conexão
- Controle de ping/pong

**Eventos disponíveis:**
- `message.received` - Mensagem recebida
- `message.sent` - Mensagem enviada
- `message.status` - Status da mensagem
- `presence.update` - Atualização de presença
- `call.received` - Chamada recebida
- `instance.connect` - Instância conectada
- `instance.disconnect` - Instância desconectada
- `qrcode.updated` - QR Code atualizado
- `contact.update` - Contato atualizado
- `group.update` - Grupo atualizado

**Exemplo de uso:**
```csharp
var wsRequest = new SetWebSocketRequest
{
    Enabled = true,
    Events = WebSocketEvents.AllEvents, // ou eventos específicos
    ConnectionTimeout = 30,
    PingInterval = 25,
    AutoReconnect = true,
    MaxReconnectAttempts = 5
};

await client.WebSocket.SetAsync("instance-name", wsRequest);

// Testar conexão
var ping = await client.WebSocket.PingAsync("instance-name");
```

### 3. ☁️ **AWS SQS** - Fila de Mensagens
Integração com Amazon Simple Queue Service para processamento assíncrono.

**Recursos:**
- Configuração de filas Standard e FIFO
- Controle de TTL e retenção
- Atributos personalizados
- Monitoramento de fila
- Purga de mensagens

**Exemplo de uso:**
```csharp
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
    MessageRetentionPeriod = 345600, // 4 dias
    UseFifoQueue = false
};

await client.SQS.SetAsync("instance-name", sqsRequest);

// Enviar mensagem de teste
await client.SQS.SendTestMessageAsync("instance-name", "Teste SQS");

// Obter estatísticas
var stats = await client.SQS.GetStatsAsync("instance-name");
```

### 4. 🐰 **RabbitMQ** - Message Broker
Integração com RabbitMQ para mensageria robusta.

**Recursos:**
- Configuração de exchanges e filas
- Suporte a diferentes tipos de exchange (direct, topic, fanout, headers)
- Dead Letter Queues
- Controle de durabilidade
- Monitoramento de performance

**Exemplo de uso:**
```csharp
var rabbitRequest = new SetRabbitMQRequest
{
    Enabled = true,
    Uri = "amqp://user:password@localhost:5672",
    Exchange = "evolution-exchange",
    ExchangeType = "topic",
    ExchangeDurable = true,
    Events = RabbitMQEvents.AllEvents,
    DefaultRoutingKey = "evolution.events",
    QueueDurable = true,
    AutoReconnect = true,
    MaxReconnectAttempts = 5
};

await client.RabbitMQ.SetAsync("instance-name", rabbitRequest);

// Publicar mensagem de teste
await client.RabbitMQ.PublishTestMessageAsync("instance-name", "Teste RabbitMQ", "test.key");

// Verificar exchange e fila
var exchangeCheck = await client.RabbitMQ.CheckExchangeAsync("instance-name");
var queueCheck = await client.RabbitMQ.CheckQueueAsync("instance-name");
```

## Monitoramento e Estatísticas

Todas as integrações fornecem estatísticas detalhadas e monitoramento:

```csharp
// Chatwoot
var chatwootStats = await client.Chatwoot.GetStatsAsync("instance-name");
Console.WriteLine($"Conversas abertas: {chatwootStats.Stats?.OpenConversations}");

// WebSocket
var wsStats = await client.WebSocket.GetStatsAsync("instance-name");
Console.WriteLine($"Uptime: {wsStats.Stats?.CurrentUptime} segundos");

// SQS
var sqsStats = await client.SQS.GetStatsAsync("instance-name");
Console.WriteLine($"Taxa de sucesso: {sqsStats.Stats?.SuccessRate:F2}%");

// RabbitMQ
var rabbitStats = await client.RabbitMQ.GetStatsAsync("instance-name");
Console.WriteLine($"Taxa de publicação: {rabbitStats.Stats?.PublishRate:F2} msg/s");
```

## Teste de Conectividade

Todas as integrações suportam teste de conectividade:

```csharp
// Testar todas as conexões
var chatwootTest = await client.Chatwoot.TestConnectionAsync("instance-name");
var wsTest = await client.WebSocket.TestConnectionAsync("instance-name");
var sqsTest = await client.SQS.TestConnectionAsync("instance-name");
var rabbitTest = await client.RabbitMQ.TestConnectionAsync("instance-name");

Console.WriteLine($"Chatwoot: {(chatwootTest.Success ? "✓" : "✗")}");
Console.WriteLine($"WebSocket: {(wsTest.Success ? "✓" : "✗")}");
Console.WriteLine($"SQS: {(sqsTest.Success ? "✓" : "✗")}");
Console.WriteLine($"RabbitMQ: {(rabbitTest.Success ? "✓" : "✗")}");
```

## Validações e Segurança

O SDK inclui validações abrangentes:

- **URLs válidas** para Chatwoot e WebSocket
- **Credenciais AWS** para SQS
- **URIs AMQP** para RabbitMQ
- **Eventos válidos** para todas as integrações
- **Timeouts e limites** apropriados
- **Mascaramento de credenciais** nas respostas

## Tratamento de Erros

Todas as operações incluem tratamento de erros robusto:

```csharp
try
{
    await client.Chatwoot.SetAsync("instance-name", request);
}
catch (ArgumentException ex)
{
    // Erro de validação de parâmetros
    Console.WriteLine($"Erro de validação: {ex.Message}");
}
catch (HttpRequestException ex)
{
    // Erro de comunicação HTTP
    Console.WriteLine($"Erro de rede: {ex.Message}");
}
catch (Exception ex)
{
    // Outros erros
    Console.WriteLine($"Erro geral: {ex.Message}");
}
```

## Exemplos Completos

Consulte o arquivo `examples/IntegrationExamples.cs` para exemplos completos de uso, incluindo:

- Configuração básica de cada integração
- Monitoramento e estatísticas
- Teste de conectividade
- Envio de mensagens de teste
- Sincronização de dados

## Suporte

Para mais informações sobre as APIs, consulte a documentação oficial da Evolution API:
- [Documentação Evolution API](https://doc.evolution-api.com/)
- [Integrações](https://doc.evolution-api.com/v2/api-reference/integrations/)
