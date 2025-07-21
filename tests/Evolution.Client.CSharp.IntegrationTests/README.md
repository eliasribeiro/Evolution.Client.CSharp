# Testes de Integração - Evolution.Client.CSharp

Este projeto contém testes de integração para o SDK Evolution.Client.CSharp, que consome a API Evolution.

## Configuração

Os testes de integração requerem uma configuração adequada para se conectar à API Evolution. Siga os passos abaixo para configurar o ambiente de testes:

### Arquivos de Configuração

1. **appsettings.json**: Contém configurações padrão e é versionado no repositório.
2. **appsettings.Development.json**: Contém configurações específicas para desenvolvimento, incluindo a URL da API e a chave de API. Este arquivo **não é versionado** no repositório por conter informações sensíveis.

### Configurando o ambiente

1. Crie um arquivo `appsettings.Development.json` na raiz do projeto de testes de integração com o seguinte conteúdo:

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

2. Substitua `"http://sua-api-url:8080/"` pela URL da sua instância da API Evolution.
3. Substitua `"SUA-API-KEY"` pela sua chave de API Evolution.

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

## Observações

- Os testes de integração dependem de uma instância em execução da API Evolution.
- Certifique-se de que a API esteja acessível a partir do ambiente onde os testes serão executados.
- As informações sensíveis (URL da API e chave de API) não devem ser versionadas no repositório.