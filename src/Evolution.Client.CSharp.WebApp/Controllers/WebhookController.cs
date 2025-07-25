using Evolution.Client.CSharp.Interfaces;
using Evolution.Client.CSharp.Models.Webhook;
using Microsoft.AspNetCore.Mvc;

namespace Evolution.Client.CSharp.WebApp.Controllers;

/// <summary>
/// Controller for Webhook operations
/// </summary>
public class WebhookController : Controller
{
    private readonly IEvolutionWebhookService _webhookService;

    /// <summary>
    /// Initializes a new instance of the WebhookController
    /// </summary>
    /// <param name="webhookService">Webhook service</param>
    public WebhookController(IEvolutionWebhookService webhookService)
    {
        _webhookService = webhookService ?? throw new ArgumentNullException(nameof(webhookService));
    }

    /// <summary>
    /// Display webhook management page
    /// </summary>
    /// <returns>Webhook index view</returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Display set webhook page
    /// </summary>
    /// <returns>Set webhook view</returns>
    public IActionResult SetWebhook()
    {
        return View(new SetWebhookRequest());
    }

    /// <summary>
    /// Set webhook configuration
    /// </summary>
    /// <param name="instanceName">Instance name</param>
    /// <param name="request">Webhook configuration request</param>
    /// <returns>Set webhook view with result</returns>
    [HttpPost]
    public async Task<IActionResult> SetWebhook(string instanceName, SetWebhookRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(instanceName))
            {
                ViewBag.Error = "Nome da instância é obrigatório.";
                ViewBag.Success = false;
                return View(request);
            }

            var response = await _webhookService.SetWebhookAsync(instanceName, request);
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
    /// Display find webhook page
    /// </summary>
    /// <returns>Find webhook view</returns>
    public IActionResult FindWebhook()
    {
        return View();
    }

    /// <summary>
    /// Find webhook configuration
    /// </summary>
    /// <param name="instanceName">Instance name</param>
    /// <returns>Find webhook view with result</returns>
    [HttpPost]
    public async Task<IActionResult> FindWebhook(string instanceName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(instanceName))
            {
                ViewBag.Error = "Nome da instância é obrigatório.";
                ViewBag.Success = false;
                return View();
            }

            var response = await _webhookService.FindWebhookAsync(instanceName);
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