using Microsoft.AspNetCore.Mvc;
using Evolution.Client.CSharp;
using EvolutionWebApp.Models;

namespace EvolutionWebApp.Controllers;

/// <summary>
/// Controlador para gerenciar instâncias da API Evolution.
/// </summary>
public class InstancesController : Controller
{
    private readonly ILogger<InstancesController> _logger;
    private readonly EvolutionApiClient _evolutionClient;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="InstancesController"/>.
    /// </summary>
    /// <param name="logger">O logger para registrar informações.</param>
    /// <param name="evolutionClient">O cliente da API Evolution.</param>
    public InstancesController(ILogger<InstancesController> logger, EvolutionApiClient evolutionClient)
    {
        _logger = logger;
        _evolutionClient = evolutionClient;
    }

    /// <summary>
    /// Exibe a lista de instâncias disponíveis.
    /// </summary>
    /// <returns>A view com a lista de instâncias.</returns>
    public async Task<IActionResult> Index()
    {
        try
        {
            var instances = await _evolutionClient.Instance.FetchInstancesAsync();
            return View(instances);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter instâncias da API Evolution");
            TempData["ErrorMessage"] = $"Erro ao obter instâncias: {ex.Message}";
            return View(new Evolution.Client.CSharp.Models.Instance.InstancesResponse());
        }
    }

    /// <summary>
    /// Exibe os detalhes de uma instância específica.
    /// </summary>
    /// <param name="id">O ID da instância.</param>
    /// <returns>A view com os detalhes da instância.</returns>
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("ID da instância não fornecido");
        }

        try
        {
            var instances = await _evolutionClient.Instance.FetchInstancesAsync();
            var instance = instances.FirstOrDefault(i => i.Instance?.InstanceId == id);

            if (instance == null)
            {
                return NotFound($"Instância com ID {id} não encontrada");
            }

            return View(instance);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter detalhes da instância {InstanceId}", id);
            TempData["ErrorMessage"] = $"Erro ao obter detalhes da instância: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }
}