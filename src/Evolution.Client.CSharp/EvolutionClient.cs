using Evolution.Client.CSharp.Configuration;
using Evolution.Client.CSharp.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Evolution.Client.CSharp;

/// <summary>
/// Classe estática para criar instâncias do cliente da API Evolution.
/// </summary>
public static class EvolutionClient
{
    /// <summary>
    /// Cria uma nova instância do cliente da API Evolution com configuração básica.
    /// </summary>
    /// <param name="baseUrl">A URL base da API Evolution.</param>
    /// <param name="apiKey">A chave de API para autenticação.</param>
    /// <returns>Uma instância configurada do cliente da API Evolution.</returns>
    public static EvolutionApiClient Create(string baseUrl, string apiKey)
    {
        return Create(baseUrl, apiKey, null);
    }

    /// <summary>
    /// Cria uma nova instância do cliente da API Evolution com configuração avançada.
    /// </summary>
    /// <param name="baseUrl">A URL base da API Evolution.</param>
    /// <param name="apiKey">A chave de API para autenticação.</param>
    /// <param name="configureOptions">Uma ação para configurar opções adicionais.</param>
    /// <returns>Uma instância configurada do cliente da API Evolution.</returns>
    public static EvolutionApiClient Create(string baseUrl, string apiKey, Action<EvolutionApiOptions>? configureOptions)
    {
        // Cria um novo contêiner de serviços
        var services = new ServiceCollection();

        // Configura as opções da API
        services.AddEvolutionApi(options =>
        {
            options.BaseUrl = baseUrl;
            options.ApiKey = apiKey;

            // Aplica configurações adicionais, se fornecidas
            configureOptions?.Invoke(options);
        });

        // Adiciona serviços de logging
        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Information);
        });

        // Constrói o provedor de serviços
        var serviceProvider = services.BuildServiceProvider();

        // Resolve e retorna o cliente
        return serviceProvider.GetRequiredService<EvolutionApiClient>();
    }
}