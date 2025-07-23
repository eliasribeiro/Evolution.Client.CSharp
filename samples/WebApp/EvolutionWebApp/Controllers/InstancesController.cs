using Microsoft.AspNetCore.Mvc;
using Evolution.Client.CSharp;
using EvolutionWebApp.Models;
using System.Net;

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
            var instances = await _evolutionClient.Instance.FetchInstancesV2Async();
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
            var instance = instances.FirstOrDefault(i => i.Id == id);

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

    /// <summary>
    /// Exibe o formulário para criar uma nova instância.
    /// </summary>
    /// <returns>A view com o formulário de criação.</returns>
    public IActionResult Create()
    {
        return View(new CreateInstanceViewModel());
    }

    /// <summary>
    /// Processa a criação de uma nova instância.
    /// </summary>
    /// <param name="model">O modelo com os dados da instância a ser criada.</param>
    /// <returns>Redireciona para a lista de instâncias ou retorna a view com erros.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateInstanceViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            _logger.LogInformation("Tentando criar nova instância: {InstanceName}", model.InstanceName);

            var request = model.ToCreateInstanceRequest();
            var result = await _evolutionClient.Instance.CreateInstanceAsync(request);

            _logger.LogInformation("Instância criada com sucesso: {InstanceName} (ID: {InstanceId})", 
                result.Instance?.InstanceName, result.Instance?.InstanceId);

            TempData["SuccessMessage"] = $"Instância '{result.Instance?.InstanceName}' criada com sucesso! ID: {result.Instance?.InstanceId}";
            
            return RedirectToAction(nameof(Index));
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Erro HTTP ao criar instância: {InstanceName}", model.InstanceName);
            
            // Extrai a mensagem de erro mais específica
            var errorMessage = ex.Message;
            if (ex.Message.Contains("Erro ao criar instância:"))
            {
                errorMessage = ex.Message.Replace("Erro ao criar instância: ", "");
            }
            
            ModelState.AddModelError("", $"Erro ao criar instância: {errorMessage}");
            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao criar instância: {InstanceName}", model.InstanceName);
            ModelState.AddModelError("", $"Erro inesperado ao criar instância: {ex.Message}");
            return View(model);
        }
    }

    /// <summary>
    /// Conecta uma instância e exibe o QR code para escaneamento.
    /// </summary>
    /// <param name="instanceName">O nome da instância a ser conectada.</param>
    /// <returns>A view com o QR code para escaneamento.</returns>
    public async Task<IActionResult> Connect(string instanceName)
    {
        if (string.IsNullOrEmpty(instanceName))
        {
            return BadRequest("Nome da instância não fornecido");
        }

        try
        {
            _logger.LogInformation("Tentando conectar instância: {InstanceName}", instanceName);

            var connection = await _evolutionClient.Instance.ConnectInstanceAsync(instanceName);

            _logger.LogInformation("QR code gerado com sucesso para a instância: {InstanceName}", instanceName);

            return View(new ConnectInstanceViewModel
            {
                InstanceName = instanceName,
                QrCodeBase64 = connection.Base64 ?? string.Empty,
                PairingCode = connection.PairingCode,
                Count = connection.Count
            });
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogError(ex, "Instância não encontrada: {InstanceName}", instanceName);
            TempData["ErrorMessage"] = $"Instância '{instanceName}' não encontrada";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao conectar instância: {InstanceName}", instanceName);
            TempData["ErrorMessage"] = $"Erro ao conectar instância: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    /// <summary>
    /// Conecta uma instância via AJAX e retorna os dados de conexão em JSON.
    /// </summary>
    /// <param name="instanceName">O nome da instância a ser conectada.</param>
    /// <returns>JSON com os dados de conexão ou erro.</returns>
    [HttpPost]
    public async Task<IActionResult> ConnectAjax(string instanceName)
    {
        if (string.IsNullOrEmpty(instanceName))
        {
            return Json(new { success = false, message = "Nome da instância não fornecido" });
        }

        try
        {
            _logger.LogInformation("Tentando conectar instância via AJAX: {InstanceName}", instanceName);

            var connection = await _evolutionClient.Instance.ConnectInstanceAsync(instanceName);

            _logger.LogInformation("QR code gerado com sucesso para a instância: {InstanceName}", instanceName);

            return Json(new 
            { 
                success = true, 
                data = new
                {
                    instanceName = instanceName,
                    qrCodeBase64 = connection.Base64 ?? string.Empty,
                    pairingCode = connection.PairingCode,
                    count = connection.Count
                }
            });
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogError(ex, "Instância não encontrada: {InstanceName}", instanceName);
            return Json(new { success = false, message = $"Instância '{instanceName}' não encontrada" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao conectar instância: {InstanceName}", instanceName);
            return Json(new { success = false, message = $"Erro ao conectar instância: {ex.Message}" });
        }
    }
}
