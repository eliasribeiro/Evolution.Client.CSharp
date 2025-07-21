# Testes de Integração

Este documento descreve como configurar e executar os testes de integração para o SDK Evolution.Client.CSharp.

## Configuração

Os testes de integração requerem uma configuração adequada para se conectar à API Evolution. Siga os passos abaixo para configurar o ambiente de testes:

### Pré-requisitos

- .NET 9.0 SDK ou superior
- Acesso a uma instância da API Evolution
- Chave de API válida

### Arquivos de Configuração

Os testes de integração utilizam dois arquivos de configuração:

1. **appsettings.json**: Contém configurações padrão e é versionado no repositório.
2. **appsettings.Development.json**: Contém configurações específicas para desenvolvimento, incluindo a URL da API e a chave de API. Este arquivo **não é versionado** no repositório por conter informações sensíveis.

### Configurando o ambiente

1. Adicione o projeto de testes de integração à solução executando o script `add-integration-tests.sh`:

```bash
./add-integration-tests.sh
```

2. Verifique se o arquivo `appsettings.Development.json` na pasta `tests/Evolution.Client.CSharp.IntegrationTests/` contém as configurações corretas:

```json
{
  "EvolutionApi": {
    "BaseUrl": "http://sua-api-url:8080/",
    "ApiKey": "SUA-API-KEY",
    "TimeoutSeconds": 30
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Information",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

3. Substitua `"http://sua-api-url:8080/"` pela URL da sua instância da API Evolution.
4. Substitua `"SUA-API-KEY"` pela sua chave de API Evolution.

## Executando os Testes

Você pode executar os testes de integração usando o Visual Studio, Visual Studio Code ou a linha de comando.

### Usando a linha de comando

```bash
# Navegue até a pasta do projeto de testes de integração
cd tests/Evolution.Client.CSharp.IntegrationTests

# Execute os testes
dotnet test
```

### Usando o Visual Studio

1. Abra a solução no Visual Studio
2. No Gerenciador de Testes, selecione os testes de integração
3. Clique em "Executar"

## Estrutura do Projeto

- **Infrastructure/**: Contém classes base e utilitários para os testes de integração
  - **IntegrationTestBase.cs**: Classe base que configura os serviços necessários para os testes
- **Services/**: Contém testes para os serviços do SDK
  - **EvolutionInformationServiceIntegrationTests.cs**: Testes para o serviço de informações
- **Examples/**: Contém exemplos de testes de integração
  - **ExampleTests.cs**: Exemplos de testes para demonstrar o uso do SDK

## Observações de Segurança

- **NUNCA** versione o arquivo `appsettings.Development.json` no GitHub, pois ele contém informações sensíveis.
- O arquivo `.gitignore` já está configurado para ignorar este arquivo.
- Para ambientes de CI/CD, configure as variáveis de ambiente correspondentes em vez de usar o arquivo de configuração.

## Adicionando Novos Testes de Integração

Para adicionar novos testes de integração:

1. Crie uma nova classe de teste na pasta `Services/` ou em uma pasta apropriada
2. Herde da classe `IntegrationTestBase` para aproveitar a configuração comum
3. Implemente os métodos de teste usando o padrão AAA (Arrange, Act, Assert)

Exemplo:

```csharp
public class NovosTestesIntegracao : IntegrationTestBase
{
    private readonly EvolutionApiClient _client;

    public NovosTestesIntegracao() : base()
    {
        _client = ServiceProvider.GetRequiredService<EvolutionApiClient>();
    }

    [Fact]
    public async Task MeuNovoTeste()
    {
        // Arrange
        // ...

        // Act
        var resultado = await _client.AlgumServico.AlgumMetodoAsync();

        // Assert
        Assert.NotNull(resultado);
        // ...
    }
}
```