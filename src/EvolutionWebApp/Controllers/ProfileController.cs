using Evolution.Client.CSharp;
using Evolution.Client.CSharp.Models.Profile;
using Microsoft.AspNetCore.Mvc;

namespace EvolutionWebApp.Controllers;

/// <summary>
/// Controller para operações de configurações de perfil.
/// </summary>
public class ProfileController : Controller
{
    private readonly EvolutionApiClient _evolutionClient;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="ProfileController"/>.
    /// </summary>
    /// <param name="evolutionClient">O cliente da API Evolution.</param>
    public ProfileController(EvolutionApiClient evolutionClient)
    {
        _evolutionClient = evolutionClient ?? throw new ArgumentNullException(nameof(evolutionClient));
    }

    /// <summary>
    /// Exibe a página principal de configurações de perfil.
    /// </summary>
    /// <returns>A view de configurações de perfil.</returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Exibe a página para buscar perfil de negócio.
    /// </summary>
    /// <returns>A view para buscar perfil de negócio.</returns>
    public IActionResult FetchBusinessProfile()
    {
        return View();
    }

    /// <summary>
    /// Processa a busca de perfil de negócio.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição de busca de perfil de negócio.</param>
    /// <returns>A view com o resultado da busca.</returns>
    [HttpPost]
    public async Task<IActionResult> FetchBusinessProfile(string instanceName, FetchBusinessProfileRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
        {
            ModelState.AddModelError("", "Nome da instância é obrigatório.");
            return View(request);
        }

        try
        {
            var response = await _evolutionClient.Profile.FetchBusinessProfileAsync(instanceName, request);
            ViewBag.Response = response;
            ViewBag.Success = true;
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.Success = false;
        }

        return View(request);
    }

    /// <summary>
    /// Exibe a página para buscar perfil de usuário.
    /// </summary>
    /// <returns>A view para buscar perfil de usuário.</returns>
    public IActionResult FetchProfile()
    {
        return View();
    }

    /// <summary>
    /// Processa a busca de perfil de usuário.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição de busca de perfil.</param>
    /// <returns>A view com o resultado da busca.</returns>
    [HttpPost]
    public async Task<IActionResult> FetchProfile(string instanceName, FetchProfileRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
        {
            ModelState.AddModelError("", "Nome da instância é obrigatório.");
            return View(request);
        }

        try
        {
            var response = await _evolutionClient.Profile.FetchProfileAsync(instanceName, request);
            ViewBag.Response = response;
            ViewBag.Success = true;
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.Success = false;
        }

        return View(request);
    }

    /// <summary>
    /// Exibe a página para atualizar nome do perfil.
    /// </summary>
    /// <returns>A view para atualizar nome do perfil.</returns>
    public IActionResult UpdateProfileName()
    {
        return View();
    }

    /// <summary>
    /// Processa a atualização do nome do perfil.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição de atualização do nome.</param>
    /// <returns>A view com o resultado da atualização.</returns>
    [HttpPost]
    public async Task<IActionResult> UpdateProfileName(string instanceName, UpdateProfileNameRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
        {
            ModelState.AddModelError("", "Nome da instância é obrigatório.");
            return View(request);
        }

        try
        {
            var response = await _evolutionClient.Profile.UpdateProfileNameAsync(instanceName, request);
            ViewBag.Response = response;
            ViewBag.Success = true;
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.Success = false;
        }

        return View(request);
    }

    /// <summary>
    /// Exibe a página para atualizar status do perfil.
    /// </summary>
    /// <returns>A view para atualizar status do perfil.</returns>
    public IActionResult UpdateProfileStatus()
    {
        return View();
    }

    /// <summary>
    /// Processa a atualização do status do perfil.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição de atualização do status.</param>
    /// <returns>A view com o resultado da atualização.</returns>
    [HttpPost]
    public async Task<IActionResult> UpdateProfileStatus(string instanceName, UpdateProfileStatusRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
        {
            ModelState.AddModelError("", "Nome da instância é obrigatório.");
            return View(request);
        }

        try
        {
            var response = await _evolutionClient.Profile.UpdateProfileStatusAsync(instanceName, request);
            ViewBag.Response = response;
            ViewBag.Success = true;
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.Success = false;
        }

        return View(request);
    }

    /// <summary>
    /// Exibe a página para atualizar foto do perfil.
    /// </summary>
    /// <returns>A view para atualizar foto do perfil.</returns>
    public IActionResult UpdateProfilePicture()
    {
        return View();
    }

    /// <summary>
    /// Processa a atualização da foto do perfil.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição de atualização da foto.</param>
    /// <returns>A view com o resultado da atualização.</returns>
    [HttpPost]
    public async Task<IActionResult> UpdateProfilePicture(string instanceName, UpdateProfilePictureRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
        {
            ModelState.AddModelError("", "Nome da instância é obrigatório.");
            return View(request);
        }

        try
        {
            var response = await _evolutionClient.Profile.UpdateProfilePictureAsync(instanceName, request);
            ViewBag.Response = response;
            ViewBag.Success = true;
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.Success = false;
        }

        return View(request);
    }

    /// <summary>
    /// Exibe a página para remover foto do perfil.
    /// </summary>
    /// <returns>A view para remover foto do perfil.</returns>
    public IActionResult RemoveProfilePicture()
    {
        return View();
    }

    /// <summary>
    /// Processa a remoção da foto do perfil.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <returns>A view com o resultado da remoção.</returns>
    [HttpPost]
    public async Task<IActionResult> RemoveProfilePicture(string instanceName)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
        {
            ModelState.AddModelError("", "Nome da instância é obrigatório.");
            return View();
        }

        try
        {
            var response = await _evolutionClient.Profile.RemoveProfilePictureAsync(instanceName);
            ViewBag.Response = response;
            ViewBag.Success = true;
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.Success = false;
        }

        return View();
    }

    /// <summary>
    /// Exibe a página para buscar configurações de privacidade.
    /// </summary>
    /// <returns>A view para buscar configurações de privacidade.</returns>
    public IActionResult FetchPrivacySettings()
    {
        return View();
    }

    /// <summary>
    /// Processa a busca de configurações de privacidade.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <returns>A view com o resultado da busca.</returns>
    [HttpPost]
    public async Task<IActionResult> FetchPrivacySettings(string instanceName)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
        {
            ModelState.AddModelError("", "Nome da instância é obrigatório.");
            return View();
        }

        try
        {
            var response = await _evolutionClient.Profile.FetchPrivacySettingsAsync(instanceName);
            ViewBag.Response = response;
            ViewBag.Success = true;
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.Success = false;
        }

        return View();
    }

    /// <summary>
    /// Exibe a página para atualizar configurações de privacidade.
    /// </summary>
    /// <returns>A view para atualizar configurações de privacidade.</returns>
    public IActionResult UpdatePrivacySettings()
    {
        return View();
    }

    /// <summary>
    /// Processa a atualização das configurações de privacidade.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição de atualização das configurações.</param>
    /// <returns>A view com o resultado da atualização.</returns>
    [HttpPost]
    public async Task<IActionResult> UpdatePrivacySettings(string instanceName, UpdatePrivacySettingsRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
        {
            ModelState.AddModelError("", "Nome da instância é obrigatório.");
            return View(request);
        }

        try
        {
            var response = await _evolutionClient.Profile.UpdatePrivacySettingsAsync(instanceName, request);
            ViewBag.Response = response;
            ViewBag.Success = true;
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.Success = false;
        }

        return View(request);
    }
}