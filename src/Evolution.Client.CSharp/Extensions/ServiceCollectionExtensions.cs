using Evolution.Client.CSharp.Configuration;
using Evolution.Client.CSharp.Interfaces;
using Evolution.Client.CSharp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Evolution.Client.CSharp.Extensions;

/// <summary>
/// Extensões para configurar os serviços da API Evolution no contêiner de injeção de dependência.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adiciona os serviços da API Evolution ao contêiner de injeção de dependência.
    /// </summary>
    /// <param name="services">A coleção de serviços.</param>
    /// <param name="configureOptions">Uma ação para configurar as opções da API.</param>
    /// <returns>A coleção de serviços para encadeamento.</returns>
    public static IServiceCollection AddEvolutionApi(this IServiceCollection services, Action<EvolutionApiOptions> configureOptions)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configureOptions == null)
        {
            throw new ArgumentNullException(nameof(configureOptions));
        }

        // Configura as opções da API
        services.Configure(configureOptions);

        // Adiciona os clientes HTTP
        services.AddHttpClient<IEvolutionInformationService, EvolutionInformationService>();
        services.AddHttpClient<IEvolutionInstanceService, EvolutionInstanceService>();
        services.AddHttpClient<IEvolutionChatService, EvolutionChatService>();

        // Registra os serviços
        services.TryAddSingleton<EvolutionApiClient>();
        services.TryAddSingleton<IEvolutionInformationService, EvolutionInformationService>();
        services.TryAddSingleton<IEvolutionInstanceService, EvolutionInstanceService>();
        services.TryAddSingleton<IEvolutionChatService, EvolutionChatService>();

        return services;
    }
}