using Microsoft.AspNetCore.Mvc;

namespace Evolution.Client.CSharp.WebApp.Controllers;

/// <summary>
/// Controller para a página inicial.
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// Página inicial da aplicação.
    /// </summary>
    /// <returns>View da página inicial</returns>
    public IActionResult Index()
    {
        return View();
    }
}