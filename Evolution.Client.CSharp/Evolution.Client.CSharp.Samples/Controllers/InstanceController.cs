using Evolution.Client.CSharp.Samples.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Evolution.Client.CSharp.Samples.Controllers;
[ValidateCookies]
public class InstanceController : Controller
{
    
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    
}