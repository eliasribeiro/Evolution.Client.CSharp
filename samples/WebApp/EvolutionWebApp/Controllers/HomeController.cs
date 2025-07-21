using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EvolutionWebApp.Models;
using Evolution.Client.CSharp;

namespace EvolutionWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly EvolutionApiClient _evolutionClient;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="HomeController"/>.
    /// </summary>
    /// <param name="logger">O logger para registrar informações.</param>
    /// <param name="evolutionClient">O cliente da API Evolution.</param>
    public HomeController(ILogger<HomeController> logger, EvolutionApiClient evolutionClient)
    {
        _logger = logger;
        _evolutionClient = evolutionClient;
    }

    /// <summary>
    /// Exibe a página inicial com informações da API Evolution.
    /// </summary>
    /// <returns>A view com as informações da API.</returns>
    public async Task<IActionResult> Index()
    {
        var viewModel = new EvolutionViewModel();
        
        try
        {
            // Obtém informações da API
            viewModel.ApiInformation = await _evolutionClient.Information.GetInformationAsync();
            
            // Obtém instâncias disponíveis (pode falhar se a API não estiver configurada)
            try
            {
                viewModel.Instances = await _evolutionClient.Instance.FetchInstancesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Não foi possível obter as instâncias. A API pode não estar configurada corretamente.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter informações da API Evolution");
            viewModel.ErrorMessage = $"Erro ao conectar com a API Evolution: {ex.Message}";
        }
        
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
