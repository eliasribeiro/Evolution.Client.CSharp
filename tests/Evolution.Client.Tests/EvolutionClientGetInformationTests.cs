using FluentAssertions;
using Evolution.Client.Core.Http;
using Evolution.Client.Models;
using NSubstitute;

namespace Evolution.Client.Tests;

public class EvolutionClientGetInformationTests
{
    [Fact]
    public async Task GetInformationAsync_ShouldReturnApiInformation()
    {
        // Arrange
        var httpClient = new HttpClient();
        var options = new EvolutionClientOptions
        {
            BaseUrl = "https://api.evolution.com",
            ApiKey = "test-api-key"
        };

        var expectedResponse = new ApiInformation
        {
            Status = 200,
            Message = "Welcome to the Evolution API",
            Version = "2.0.0",
            Swagger = "https://api.evolution.com/docs",
            Manager = "https://manager.evolution.com",
            Documentation = "https://doc.evolution-api.com"
        };

        // Criar um mock do HttpService
        var httpService = Substitute.For<IHttpService>();
        httpService.GetAsync<ApiInformation>("", Arg.Any<CancellationToken>())
            .Returns(expectedResponse);

        // Usar reflexão para injetar o mock (para fins de teste)
        var client = EvolutionClient.Create(httpClient, options);
        var httpServiceField = typeof(EvolutionClient).GetField("_httpService", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        httpServiceField?.SetValue(client, httpService);

        // Act
        var result = await client.GetInformationAsync();

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(200);
        result.Message.Should().Be("Welcome to the Evolution API");
        result.Version.Should().Be("2.0.0");
        result.Swagger.Should().Be("https://api.evolution.com/docs");
        result.Manager.Should().Be("https://manager.evolution.com");
        result.Documentation.Should().Be("https://doc.evolution-api.com");

        // Verificar se o método foi chamado corretamente
        await httpService.Received(1).GetAsync<ApiInformation>("", Arg.Any<CancellationToken>());

        // Cleanup
        client.Dispose();
    }

    [Fact]
    public async Task GetInformationAsync_WithCancellationToken_ShouldPassTokenToHttpService()
    {
        // Arrange
        var httpClient = new HttpClient();
        var options = new EvolutionClientOptions
        {
            BaseUrl = "https://api.evolution.com",
            ApiKey = "test-api-key"
        };

        var cancellationToken = new CancellationToken();
        var expectedResponse = new ApiInformation();

        var httpService = Substitute.For<IHttpService>();
        httpService.GetAsync<ApiInformation>("", cancellationToken)
            .Returns(expectedResponse);

        var client = EvolutionClient.Create(httpClient, options);
        var httpServiceField = typeof(EvolutionClient).GetField("_httpService", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        httpServiceField?.SetValue(client, httpService);

        // Act
        await client.GetInformationAsync(cancellationToken);

        // Assert
        await httpService.Received(1).GetAsync<ApiInformation>("", cancellationToken);

        // Cleanup
        client.Dispose();
    }
}
