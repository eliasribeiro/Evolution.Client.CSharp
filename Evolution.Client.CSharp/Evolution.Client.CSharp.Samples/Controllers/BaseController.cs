using Microsoft.AspNetCore.Mvc;

namespace Evolution.Client.CSharp.Samples.Controllers
{
    public class BaseController : Controller
    {
        protected EvolutionClient client;
        protected EvolutionClient GetEvolutionClient()
        {
            if(client == null)
            {
                client = new EvolutionClient(Request.Cookies["ServerUrl"], Request.Cookies["ApiKey"]);
            }
            return client;
        }
    }
}
