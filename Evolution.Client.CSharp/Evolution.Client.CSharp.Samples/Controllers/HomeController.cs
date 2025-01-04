using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Evolution.Client.CSharp.Samples.Models;

namespace Evolution.Client.CSharp.Samples.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (Request.Cookies["ServerUrl"] is not null && Request.Cookies["ApiKey"] is not null)
        {
            var serv = new ConfigServer();
            serv.ServerUrl = Request.Cookies["ServerUrl"];
            serv.ApiKey = Request.Cookies["ApiKey"];

            return View(serv);
        }
        return View();
    }
    [HttpPost]
    public IActionResult Index(ConfigServer configServer)
    {
        // Salva a URL do servidor no cookie
        CookieOptions cookieOptions = new CookieOptions
        {
            IsEssential = true,
            Expires = DateTime.Now.AddDays(30), // O cookie expira em 30 dias
            HttpOnly = true,                   // Torna o cookie acessível apenas pelo servidor
            Secure = true                      // Garante que o cookie só será enviado via HTTPS
        };

        // Adiciona a URL do servidor ao cookie
        Response.Cookies.Append("ServerUrl", configServer.ServerUrl, cookieOptions);

        // Adiciona a chave da API ao cookie
        Response.Cookies.Append("ApiKey", configServer.ApiKey, cookieOptions);

        // Exibe uma mensagem de confirmação
        TempData["Message"] = "As configurações foram salvas com sucesso!";
        return RedirectToAction("Index", configServer);
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