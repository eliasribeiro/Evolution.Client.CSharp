# Endpoint Fetch Instances

## Descrição

O endpoint `fetch-instances` permite obter todas as instâncias disponíveis na API Evolution.

## Endpoint

```
GET /instance/fetchInstances
```

## Resposta

A resposta é um array de objetos, cada um contendo informações sobre uma instância:

```json
[
  {
    "instance": {
      "instanceName": "example-name",
      "instanceId": "421a4121-a3d9-40cc-a8db-c3a1df353126",
      "owner": "553198296801@s.whatsapp.net",
      "profileName": "Guilherme Gomes",
      "profilePictureUrl": null,
      "profileStatus": "This is the profile status.",
      "status": "open",
      "serverUrl": "https://example.evolution-api.com",
      "apikey": "B3844804-481D-47A4-B69C-F14B4206EB56",
      "integration": {
        "integration": "WHATSAPP-BAILEYS",
        "webhook_wa_business": "https://example.evolution-api.com/webhook/whatsapp/db5e11d3-ded5-4d91-b3fb-48272688f206"
      }
    }
  },
  {
    "instance": {
      "instanceName": "teste-docs",
      "instanceId": "af6c5b7c-ee27-4f94-9ea8-192393746ddd",
      "status": "close",
      "serverUrl": "https://example.evolution-api.com",
      "apikey": "123456",
      "integration": {
        "token": "123456",
        "webhook_wa_business": "https://example.evolution-api.com/webhook/whatsapp/teste-docs"
      }
    }
  }
]
```

## Exemplo de Uso no SDK

```csharp
using Evolution.Client.CSharp;
using Evolution.Client.CSharp.Extensions;
using Microsoft.Extensions.DependencyInjection;

// Configurar os serviços
var services = new ServiceCollection();

// Adicionar os serviços da API Evolution
services.AddEvolutionApi(options =>
{
    options.BaseUrl = "http://localhost:8080"; // Substitua pela URL da sua API
    options.ApiKey = "sua-api-key"; // Substitua pela sua chave de API
    options.TimeoutSeconds = 30;
});

// Construir o provedor de serviços
var serviceProvider = services.BuildServiceProvider();

// Obter o cliente da API Evolution
var client = serviceProvider.GetRequiredService<EvolutionApiClient>();

// Obter todas as instâncias disponíveis
var instances = await client.Instance.FetchInstancesAsync();

// Exibir as instâncias
foreach (var instance in instances)
{
    Console.WriteLine($"Nome da instância: {instance.Instance?.InstanceName}");
    Console.WriteLine($"ID da instância: {instance.Instance?.InstanceId}");
    Console.WriteLine($"Status: {instance.Instance?.Status}");
    Console.WriteLine();
}
```

## Observações

- Este endpoint requer autenticação com a chave de API.
- O status da instância pode ser `open`, `close`, `connecting`, entre outros.
- Algumas instâncias podem não ter todas as informações preenchidas, dependendo do seu estado.