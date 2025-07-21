# Evolution API Demo - Aplicação Web ASP.NET Core

Este projeto é uma demonstração de como utilizar o SDK Evolution.Client.CSharp em uma aplicação web ASP.NET Core. A aplicação fornece uma interface gráfica para visualizar informações da API Evolution e gerenciar instâncias.

## Funcionalidades

- **Dashboard**: Exibe informações gerais da API Evolution e lista as instâncias disponíveis.
- **Gerenciamento de Instâncias**: Permite visualizar detalhes de cada instância.

## Pré-requisitos

- .NET 8.0 SDK ou superior
- Uma instância da API Evolution em execução
- Chave de API válida para a API Evolution

## Configuração

1. Abra o arquivo `appsettings.json` e configure as opções da API Evolution:

```json
"EvolutionApi": {
  "BaseUrl": "https://sua-api-evolution.com",
  "ApiKey": "sua-chave-api",
  "TimeoutSeconds": 30
}
```

## Executando o Projeto

1. Navegue até o diretório do projeto:

```bash
cd samples/WebApp/EvolutionWebApp
```

2. Execute o projeto:

```bash
dotnet run
```

3. Abra um navegador e acesse `https://localhost:5001` ou `http://localhost:5000`.

## Estrutura do Projeto

- **Controllers/**
  - `HomeController.cs`: Controlador principal que exibe o dashboard.
  - `InstancesController.cs`: Controlador para gerenciar instâncias.

- **Models/**
  - `EvolutionViewModel.cs`: Modelo de visualização para o dashboard.

- **Views/**
  - `Home/Index.cshtml`: View do dashboard principal.
  - `Instances/Index.cshtml`: View da lista de instâncias.
  - `Instances/Details.cshtml`: View de detalhes de uma instância.

## Como o SDK é Utilizado

### Configuração do SDK

O SDK é configurado no arquivo `Program.cs` utilizando o método de extensão `AddEvolutionApi`:

```csharp
builder.Services.AddEvolutionApi(options =>
{
    options.BaseUrl = builder.Configuration["EvolutionApi:BaseUrl"];
    options.ApiKey = builder.Configuration["EvolutionApi:ApiKey"];
    options.TimeoutSeconds = int.Parse(builder.Configuration["EvolutionApi:TimeoutSeconds"] ?? "30");
});
```

### Injeção de Dependência

O cliente da API Evolution é injetado nos controladores:

```csharp
public HomeController(ILogger<HomeController> logger, EvolutionApiClient evolutionClient)
{
    _logger = logger;
    _evolutionClient = evolutionClient;
}
```

### Uso dos Serviços

Exemplo de como obter informações da API:

```csharp
var apiInfo = await _evolutionClient.Information.GetApiInformationAsync();
```

Exemplo de como obter instâncias:

```csharp
var instances = await _evolutionClient.Instance.FetchInstancesAsync();
```

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou enviar pull requests para melhorar este exemplo.

## Licença

Este projeto está licenciado sob a mesma licença do SDK Evolution.Client.CSharp.