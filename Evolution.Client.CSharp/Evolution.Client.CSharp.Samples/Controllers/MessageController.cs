using Microsoft.AspNetCore.Mvc;

namespace Evolution.Client.CSharp.Samples.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
