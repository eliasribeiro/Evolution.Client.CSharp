using Evolution.Client.CSharp.IntegrationTests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Evolution.Client.CSharp.IntegrationTests.Examples;

/// <summary>
/// Exemplos de testes de integração para demonstrar o uso do SDK.
/// </summary>
public class ExampleTests : IntegrationTestBase
{
    private readonly EvolutionApiClient _client;
    private readonly ILogger<ExampleTests> _logger;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="ExampleTests"/>.
    /// </summary>
    public ExampleTests() : base()
    {
        _client = ServiceProvider.GetRequiredService<EvolutionApiClient>();
        _logger = ServiceProvider.GetRequiredService<ILogger<ExampleTests>>();
    }

    /// <summary>
    /// Exemplo de teste que verifica se a API está acessível e retorna informações válidas.
    /// Este teste é ignorado por padrão, pois requer uma instância da API Evolution em execução.
    /// </summary>
    [Fact(Skip = "Requer uma instância da API Evolution em execução")]
    public async Task VerificarConexaoComApi()
    {
        // Act
        _logger.LogInformation("Iniciando teste de conexão com a API Evolution");
        var result = await _client.Information.GetInformationAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        
        _logger.LogInformation("Conexão com a API Evolution estabelecida com sucesso");
        _logger.LogInformation($"Versão da API: {result.Version}");
        _logger.LogInformation($"URL do Swagger: {result.Swagger}");
    }

    /// <summary>
    /// Exemplo de teste que demonstra como lidar com falhas de conexão.
    /// Este teste é ignorado por padrão, pois requer uma configuração específica para falhar.
    /// </summary>
    [Fact(Skip = "Este teste é apenas um exemplo e requer configuração específica para falhar")]
    public async Task LidarComFalhaDeConexao()
    {
        // Arrange
        _logger.LogInformation("Iniciando teste de falha de conexão");

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await _client.Information.GetInformationAsync();
        });

        _logger.LogInformation("Teste de falha de conexão concluído com sucesso");
    }
}