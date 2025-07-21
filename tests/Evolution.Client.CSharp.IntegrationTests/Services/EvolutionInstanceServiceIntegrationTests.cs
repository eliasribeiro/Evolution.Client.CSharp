using Evolution.Client.CSharp.IntegrationTests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Evolution.Client.CSharp.IntegrationTests.Services;

/// <summary>
/// Testes de integração para o serviço de instâncias da API Evolution.
/// </summary>
public class EvolutionInstanceServiceIntegrationTests : IntegrationTestBase
{
    private readonly EvolutionApiClient _client;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="EvolutionInstanceServiceIntegrationTests"/>.
    /// </summary>
    public EvolutionInstanceServiceIntegrationTests() : base()
    {
        _client = ServiceProvider.GetRequiredService<EvolutionApiClient>();
    }

    /// <summary>
    /// Testa se o método FetchInstancesAsync retorna instâncias válidas da API.
    /// Este teste é ignorado por padrão, pois requer uma instância da API Evolution em execução.
    /// </summary>
    [Fact(Skip = "Requer uma instância da API Evolution em execução")]
    public async Task FetchInstancesAsync_ReturnsValidInstances()
    {
        // Act
        var result = await _client.Instance.FetchInstancesAsync();

        // Assert
        Assert.NotNull(result);
        
        // Log das instâncias obtidas
        Logger.LogInformation($"Instâncias obtidas com sucesso. Total: {result.Count}");

        foreach (var instance in result)
        {
            Assert.NotNull(instance.Instance);
            Assert.NotNull(instance.Instance.InstanceName);
            Assert.NotNull(instance.Instance.InstanceId);
            Assert.NotNull(instance.Instance.Status);

            Logger.LogInformation($"Instância: {instance.Instance.InstanceName}");
            Logger.LogInformation($"ID: {instance.Instance.InstanceId}");
            Logger.LogInformation($"Status: {instance.Instance.Status}");
            Logger.LogInformation($"Proprietário: {instance.Instance.Owner}");
            Logger.LogInformation($"URL do servidor: {instance.Instance.ServerUrl}");
            Logger.LogInformation("---");
        }
    }
}