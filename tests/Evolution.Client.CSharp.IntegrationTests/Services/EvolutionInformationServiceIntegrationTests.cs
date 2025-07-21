using Evolution.Client.CSharp.IntegrationTests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Evolution.Client.CSharp.IntegrationTests.Services;

/// <summary>
/// Testes de integração para o serviço de informações da API Evolution.
/// </summary>
public class EvolutionInformationServiceIntegrationTests : IntegrationTestBase
{
    private readonly EvolutionApiClient _client;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="EvolutionInformationServiceIntegrationTests"/>.
    /// </summary>
    public EvolutionInformationServiceIntegrationTests() : base()
    {
        _client = ServiceProvider.GetRequiredService<EvolutionApiClient>();
    }

    /// <summary>
    /// Testa se o método GetInformationAsync retorna informações válidas da API.
    /// Este teste é ignorado por padrão, pois requer uma instância da API Evolution em execução.
    /// </summary>
    [Fact(Skip = "Requer uma instância da API Evolution em execução")]
    public async Task GetInformationAsync_ReturnsValidInformation()
    {
        // Act
        var result = await _client.Information.GetInformationAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Message);
        Assert.NotNull(result.Version);
        Assert.NotNull(result.Swagger);
        Assert.NotNull(result.Manager);
        Assert.NotNull(result.Documentation);

        // Log das informações obtidas
        Logger.LogInformation("Informações da API obtidas com sucesso:");
        Logger.LogInformation($"Status: {result.Status}");
        Logger.LogInformation($"Mensagem: {result.Message}");
        Logger.LogInformation($"Versão: {result.Version}");
        Logger.LogInformation($"Swagger: {result.Swagger}");
        Logger.LogInformation($"Manager: {result.Manager}");
        Logger.LogInformation($"Documentação: {result.Documentation}");
    }
}