using Evolution.Client.CSharp.Models.Instance.Create;
using Evolution.Client.CSharp.Models.Instance.FetchInstances;
using Evolution.Client.CSharp.Samples.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Evolution.Client.CSharp.Samples.Controllers;
[ValidateCookies]
[Route("[controller]")]
public class InstanceController : BaseController
{
    // GET
    public async Task<IActionResult> Index()
    {
        ResponseFetchInstances instances = await GetEvolutionClient().Instances.FetchInstance();
        return View(instances);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(RequestCreateInstance model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await GetEvolutionClient().Instances.CreateInstance(model);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var instance = await GetEvolutionClient().Instances.ConnectionStatus(id);
        if (instance is not null && instance.Instance is not null && instance.Instance.State == "open")
        {
            await GetEvolutionClient().Instances.LogoutInstance(id);
        }
        await GetEvolutionClient().Instances.DeleteInstance(id);

        return RedirectToAction(nameof(Index));
    }
}
