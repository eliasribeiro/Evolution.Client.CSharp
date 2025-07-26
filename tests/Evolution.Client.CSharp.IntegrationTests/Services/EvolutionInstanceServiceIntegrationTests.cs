using Evolution.Client.CSharp.IntegrationTests.Infrastructure;
using Evolution.Client.CSharp.Models.Instance;
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
    /// Testa a criação de uma nova instância na API Evolution.
    /// </summary>
    [Fact]
    public async Task CreateInstanceAsync_WithValidRequest_ReturnsCreatedInstance()
    {
        // Arrange
        var instanceName = $"test-instance-{DateTime.Now:yyyyMMdd-HHmmss}";
        var request = new CreateInstanceRequest
        {
            InstanceName = instanceName,
            Token = string.Empty, // Deixar vazio para gerar automaticamente
            Number = "5511999999999", // Número de teste válido
            QrCode = true,
            Integration = WhatsAppIntegration.WhatsAppBaileys
        };

        Logger.LogInformation($"Criando instância de teste: {instanceName}");

        try
        {
            // Act
            var result = await _client.Instance.CreateInstanceAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Instance);
            Assert.Equal(instanceName, result.Instance.InstanceName);
            
            Logger.LogInformation("Instância criada com sucesso: {InstanceName}, ID: {InstanceId}, Status: {Status}", 
                result.Instance.InstanceName, result.Instance.InstanceId, result.Instance.Status);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"Erro ao criar instância: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Testa a criação de uma instância com nome inválido.
    /// </summary>
    [Fact]
    public async Task CreateInstanceAsync_WithInvalidInstanceName_ThrowsArgumentException()
    {
        // Arrange
        var request = new CreateInstanceRequest
        {
            InstanceName = string.Empty, // Nome inválido
            QrCode = true,
            Integration = WhatsAppIntegration.WhatsAppBaileys
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => _client.Instance.CreateInstanceAsync(request));
    }

    /// <summary>
    /// Testa se o método FetchInstancesAsync retorna instâncias válidas da API.
    /// </summary>
    [Fact]
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
            Assert.NotNull(instance.Name);
            Assert.NotNull(instance.Id);
            Assert.NotNull(instance.ConnectionStatus);

            Logger.LogInformation($"Instância: {instance.Name}");
            Logger.LogInformation($"ID: {instance.Id}");
            Logger.LogInformation($"Status: {instance.ConnectionStatus}");
            Logger.LogInformation($"Proprietário: {instance.OwnerJid}");
            Logger.LogInformation($"Integração: {instance.Integration}");
            Logger.LogInformation("---");
        }
    }

    /// <summary>
    /// Testa o ciclo completo: criar instância, buscar instâncias e verificar se a criada está na lista.
    /// </summary>
    [Fact]
    public async Task CreateAndFetchInstances_FullCycle_Success()
    {
        // Arrange
        var instanceName = $"integration-test-{DateTime.Now:yyyyMMdd-HHmmss}";
        var createRequest = new CreateInstanceRequest
        {
            InstanceName = instanceName,
            Token = string.Empty,
            Number = "5511999999999", // Número de teste válido
            QrCode = true,
            Integration = WhatsAppIntegration.WhatsAppBaileys
        };

        Logger.LogInformation($"Iniciando teste de ciclo completo com instância: {instanceName}");

        try
        {
            // Act 1: Criar instância
            var createResult = await _client.Instance.CreateInstanceAsync(createRequest);
            Assert.NotNull(createResult);
            Assert.NotNull(createResult.Instance);
            
            Logger.LogInformation($"Instância criada: {createResult.Instance.InstanceName}");

            // Act 2: Buscar todas as instâncias
            var fetchResult = await _client.Instance.FetchInstancesAsync();
            Assert.NotNull(fetchResult);

            // Assert: Verificar se a instância criada está na lista
            var createdInstance = fetchResult.FirstOrDefault(i => 
                i.Name == instanceName);
            
            Assert.NotNull(createdInstance);
            Assert.Equal(instanceName, createdInstance.Name);
            
            Logger.LogInformation($"Instância encontrada na lista de instâncias: {createdInstance.Name}");
            Logger.LogInformation($"Status da instância: {createdInstance.ConnectionStatus}");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"Erro no teste de ciclo completo: {ex.Message}");
            throw;
        }
    }
}