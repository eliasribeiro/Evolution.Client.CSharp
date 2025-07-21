# Endpoint Get Information

O endpoint `get-information` é usado para obter informações básicas sobre a API Evolution. Este é um endpoint simples que não requer autenticação e pode ser usado para verificar se a API está funcionando corretamente.

## Requisição

```http
GET /
```

## Resposta

```json
{
  "status": 200,
  "message": "Welcome to the Evolution API, it is working!",
  "version": "1.7.4",
  "swagger": "http://example.evolution-api.com/docs",
  "manager": "http://example.evolution-api.com/manager",
  "documentation": "https://doc.evolution-api.com"
}
```

## Propriedades da Resposta

| Propriedade    | Tipo   | Descrição                                    |
|----------------|--------|----------------------------------------------|
| status         | int    | O código de status HTTP da resposta          |
| message        | string | Mensagem descritiva sobre o estado da API    |
| version        | string | A versão atual da API                        |
| swagger        | string | URL para a documentação Swagger da API       |
| manager        | string | URL para o gerenciador da API                |
| documentation  | string | URL para a documentação detalhada da API     |

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
    options.BaseUrl = "http://localhost:8080";
    options.ApiKey = "sua-api-key";
});

// Construir o provedor de serviços
var serviceProvider = services.BuildServiceProvider();

// Obter o cliente da API
var client = serviceProvider.GetRequiredService<EvolutionApiClient>();

// Obter informações da API
var info = await client.Information.GetInformationAsync();

// Exibir as informações
Console.WriteLine($"Status: {info.Status}");
Console.WriteLine($"Mensagem: {info.Message}");
Console.WriteLine($"Versão: {info.Version}");
Console.WriteLine($"Swagger: {info.Swagger}");
Console.WriteLine($"Manager: {info.Manager}");
Console.WriteLine($"Documentação: {info.Documentation}");
```