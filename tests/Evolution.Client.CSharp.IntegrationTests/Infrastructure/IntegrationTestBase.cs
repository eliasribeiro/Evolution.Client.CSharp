using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Evolution.Client.CSharp.IntegrationTests.Infrastructure;

/// <summary>
/// Classe base para testes de integração que configura os serviços necessários.
/// </summary>
public abstract class IntegrationTestBase : IDisposable
{
    protected readonly IServiceProvider ServiceProvider;
    protected readonly IConfiguration Configuration;
    protected readonly ILogger Logger;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="IntegrationTestBase"/>.
    /// </summary>
    protected IntegrationTestBase()
    {
        // Configurar a configuração
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables();

        Configuration = configBuilder.Build();

        // Configurar os serviços
        var services = new ServiceCollection();
        ConfigureServices(services);

        ServiceProvider = services.BuildServiceProvider();
        Logger = ServiceProvider.GetRequiredService<ILogger<IntegrationTestBase>>();
        
        Logger.LogInformation("Teste de integração inicializado");
    }

    /// <summary>
    /// Configura os serviços necessários para os testes de integração.
    /// </summary>
    /// <param name="services">A coleção de serviços a ser configurada.</param>
    protected virtual void ConfigureServices(IServiceCollection services)
    {
        // Adicionar logging
        services.AddLogging(builder =>
        {
            builder.AddConfiguration(Configuration.GetSection("Logging"));
            builder.AddConsole();
        });

        // Adicionar o cliente da API Evolution
        services.AddEvolutionApi(options =>
        {
            options.BaseUrl = Configuration["EvolutionApi:BaseUrl"] ?? "http://localhost:8080/";
            options.ApiKey = Configuration["EvolutionApi:ApiKey"] ?? string.Empty;
            options.TimeoutSeconds = int.Parse(Configuration["EvolutionApi:TimeoutSeconds"] ?? "30");
        });
    }

    /// <summary>
    /// Libera os recursos utilizados pelo teste.
    /// </summary>
    public virtual void Dispose()
    {
        if (ServiceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}