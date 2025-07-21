# Evolution.Client.CSharp - Documentação

Bem-vindo à documentação do SDK Evolution.Client.CSharp, uma biblioteca para consumir a API EvolutionAPI em aplicações .NET.

## Visão Geral

O Evolution.Client.CSharp é um SDK completo para interagir com a API EvolutionAPI, que fornece integração com mensagens do WhatsApp. Este SDK foi desenvolvido seguindo as melhores práticas do .NET 9.0, com foco em facilidade de uso, desempenho e robustez.

## Características

- Implementado com .NET 9.0
- Utiliza System.Text.Json para serialização/desserialização
- Suporte para injeção de dependência
- Logging integrado
- Configuração flexível
- Tratamento de erros robusto
- Totalmente assíncrono

## Instalação

Você pode adicionar o pacote ao seu projeto usando o NuGet Package Manager:

```bash
dotnet add package Evolution.Client.CSharp
```

Ou adicionar uma referência ao projeto diretamente na sua solução.

## Configuração

Para começar a usar o SDK, você precisa configurar o cliente da API. Isso pode ser feito de duas maneiras:

### Usando Injeção de Dependência

```csharp
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

// Obter o cliente da API
var client = serviceProvider.GetRequiredService<EvolutionApiClient>();
```

## Endpoints Disponíveis

Atualmente, o SDK suporta os seguintes endpoints:

- [Get Information](endpoints/get-information.md) - Obtém informações básicas sobre a API
- [Fetch Instances](endpoints/fetch-instances.md) - Obtém todas as instâncias disponíveis

## Exemplos

Veja o projeto de exemplo em `samples/BasicUsage` para um exemplo completo de como usar o SDK.

## Contribuindo

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests no repositório do projeto.

## Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo LICENSE para detalhes.