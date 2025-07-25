using Evolution.Client.CSharp.Interfaces;
using Evolution.Client.CSharp.Models.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Evolution.Client.CSharp.WebApp.Controllers;

/// <summary>
/// Controller for Settings operations
/// </summary>
public class SettingsController : Controller
{
    private readonly IEvolutionSettingsService _settingsService;

    /// <summary>
    /// Initializes a new instance of the SettingsController
    /// </summary>
    /// <param name="settingsService">Settings service</param>
    public SettingsController(IEvolutionSettingsService settingsService)
    {
        _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
    }

    /// <summary>
    /// Display settings management page
    /// </summary>
    /// <returns>Settings index view</returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Display set settings page
    /// </summary>
    /// <returns>Set settings view</returns>
    public IActionResult SetSettings()
    {
        return View(new SetSettingsRequest());
    }

    /// <summary>
    /// Set settings configuration
    /// </summary>
    /// <param name="instanceName">Instance name</param>
    /// <param name="request">Settings configuration request</param>
    /// <returns>Set settings view with result</returns>
    [HttpPost]
    public async Task<IActionResult> SetSettings(string instanceName, SetSettingsRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(instanceName))
            {
                ViewBag.Error = "Nome da instância é obrigatório.";
                ViewBag.Success = false;
                return View(request);
            }

            var response = await _settingsService.SetSettingsAsync(instanceName, request);
            ViewBag.Response = response;
            ViewBag.Success = true;
            return View(request);
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.Success = false;
            return View(request);
        }
    }

    /// <summary>
    /// Display find settings page
    /// </summary>
    /// <returns>Find settings view</returns>
    public IActionResult FindSettings()
    {
        return View();
    }

    /// <summary>
    /// Find settings configuration
    /// </summary>
    /// <param name="instanceName">Instance name</param>
    /// <returns>Find settings view with result</returns>
    [HttpPost]
    public async Task<IActionResult> FindSettings(string instanceName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(instanceName))
            {
                ViewBag.Error = "Nome da instância é obrigatório.";
                ViewBag.Success = false;
                return View();
            }

            var response = await _settingsService.FindSettingsAsync(instanceName);
            ViewBag.Response = response;
            ViewBag.Success = true;
            return View();
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.Success = false;
            return View();
        }
    }
}