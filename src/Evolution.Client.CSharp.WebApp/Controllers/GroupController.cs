using Evolution.Client.CSharp;
using Evolution.Client.CSharp.Models.Group;
using Evolution.Client.CSharp.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Evolution.Client.CSharp.WebApp.Controllers;

/// <summary>
/// Controller para operações de grupo.
/// </summary>
public class GroupController : Controller
{
    private readonly ILogger<GroupController> _logger;
    private readonly EvolutionApiClient _evolutionClient;

    /// <summary>
    /// Inicializa uma nova instância do controller de grupos.
    /// </summary>
    /// <param name="logger">Logger para registrar eventos.</param>
    /// <param name="evolutionClient">Cliente da API Evolution.</param>
    public GroupController(ILogger<GroupController> logger, EvolutionApiClient evolutionClient)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _evolutionClient = evolutionClient ?? throw new ArgumentNullException(nameof(evolutionClient));
    }

    /// <summary>
    /// Exibe a página principal de grupos.
    /// </summary>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Exibe a página para criar grupo.
    /// </summary>
    [HttpGet]
    public IActionResult CreateGroup()
    {
        return View(new CreateGroupViewModel());
    }

    /// <summary>
    /// Cria um novo grupo.
    /// </summary>
    /// <param name="model">Dados para criação do grupo.</param>
    [HttpPost]
    public async Task<IActionResult> CreateGroup(CreateGroupViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var participants = model.Participants
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim())
                .Where(p => !string.IsNullOrEmpty(p))
                .ToList();

            var request = new CreateGroupRequest
            {
                Subject = model.Subject,
                Description = model.Description ?? string.Empty,
                Participants = participants
            };

            var response = await _evolutionClient.GroupService.CreateGroupAsync(model.InstanceName, request);

            if (response != null)
            {
                model.Result = response;
                model.ErrorMessage = null;
                TempData["SuccessMessage"] = "Grupo criado com sucesso!";
            }
            else
            {
                model.Result = null;
                model.ErrorMessage = "Resposta nula recebida do serviço";
                TempData["ErrorMessage"] = "Erro ao criar grupo.";
            }

            _logger.LogInformation("Grupo criado com sucesso para a instância {InstanceName}", model.InstanceName);
        }
        catch (Exception ex)
        {
            model.Result = null;
            model.ErrorMessage = ex.Message;
            TempData["ErrorMessage"] = $"Erro ao criar grupo: {ex.Message}";
            _logger.LogError(ex, "Erro ao criar grupo para a instância {InstanceName}", model.InstanceName);
        }

        return View(model);
    }

    /// <summary>
    /// Exibe a página para buscar grupos.
    /// </summary>
    [HttpGet]
    public IActionResult FindGroups()
    {
        return View(new GroupSearchResultViewModel());
    }

    /// <summary>
    /// Busca grupos da instância especificada.
    /// </summary>
    /// <param name="instanceName">Nome da instância.</param>
    /// <param name="getParticipants">Se deve incluir participantes.</param>
    /// <returns>View com os resultados da busca.</returns>
    [HttpPost]
    public async Task<IActionResult> FindGroups(string instanceName, bool getParticipants = false)
    {
        var viewModel = new GroupSearchResultViewModel();

        try
        {
            ViewBag.InstanceName = instanceName;
            ViewBag.GetParticipants = getParticipants;

            if (string.IsNullOrWhiteSpace(instanceName))
            {
                TempData["ErrorMessage"] = "Nome da instância é obrigatório.";
                return View(viewModel);
            }

            var request = new FindGroupsRequest
            {
                GetParticipants = getParticipants
            };

            _logger.LogInformation("Buscando grupos para a instância: {InstanceName}", instanceName);

            var result = await _evolutionClient.GroupService.FindGroupsAsync(instanceName, request);

            viewModel.Groups = result.Select(g => new GroupResult
            {
                Id = g.Id,
                Subject = g.Subject,
                SubjectOwner = g.SubjectOwner,
                SubjectTime = g.SubjectTime,
                Creation = g.Creation,
                Owner = g.Owner,
                Description = g.Description,
                DescriptionOwner = g.DescriptionId ?? string.Empty, // Usando DescriptionId como DescriptionOwner
                DescriptionTime = 0, // Valor padrão para DescriptionTime
                Participants = g.Participants?.Select(p => new Evolution.Client.CSharp.WebApp.Models.GroupParticipant
                {
                    Id = p.Id,
                    Admin = p.Admin?.ToString() ?? "false"
                }).ToList() ?? new List<Evolution.Client.CSharp.WebApp.Models.GroupParticipant>(),
                Size = g.Size,
                Announce = g.Announce,
                Restrict = g.Restrict
            }).ToList();

            viewModel.TotalCount = result.Count;
            viewModel.InstanceName = instanceName;
            viewModel.GetParticipants = getParticipants;

            _logger.LogInformation("Busca de grupos concluída com sucesso para a instância: {InstanceName}. Grupos encontrados: {Count}",
                instanceName, result.Count);

            TempData["SuccessMessage"] = $"Busca concluída! {result.Count} grupo(s) encontrado(s).";
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância não encontrada: {InstanceName}", instanceName);
            TempData["ErrorMessage"] = $"Instância '{instanceName}' não encontrada.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar grupos para a instância: {InstanceName}", instanceName);
            TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
        }

        return View(viewModel);
    }

    /// <summary>
    /// Exibe a página para atualizar foto do grupo.
    /// </summary>
    [HttpGet]
    public IActionResult UpdateGroupPicture()
    {
        return View(new UpdateGroupPictureViewModel());
    }

    /// <summary>
    /// Atualiza a foto do grupo.
    /// </summary>
    /// <param name="model">Dados para atualização da foto.</param>
    [HttpPost]
    public async Task<IActionResult> UpdateGroupPicture(UpdateGroupPictureViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var request = new UpdateGroupPictureRequest
            {
                Image = model.Image
            };

            var response = await _evolutionClient.GroupService.UpdateGroupPictureAsync(model.InstanceName, model.GroupJid, request);

            model.Result = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            model.HasError = false;
            TempData["SuccessMessage"] = "Foto do grupo atualizada com sucesso!";

            _logger.LogInformation("Foto do grupo atualizada com sucesso para a instância {InstanceName}", model.InstanceName);
        }
        catch (Exception ex)
        {
            model.Result = $"Erro: {ex.Message}";
            model.HasError = true;
            TempData["ErrorMessage"] = $"Erro ao atualizar foto do grupo: {ex.Message}";
            _logger.LogError(ex, "Erro ao atualizar foto do grupo para a instância {InstanceName}", model.InstanceName);
        }

        return View(model);
    }

    /// <summary>
    /// Exibe a página para atualizar assunto do grupo.
    /// </summary>
    [HttpGet]
    public IActionResult UpdateGroupSubject()
    {
        return View(new UpdateGroupSubjectViewModel());
    }

    /// <summary>
    /// Atualiza o assunto do grupo.
    /// </summary>
    /// <param name="model">Dados para atualização do assunto.</param>
    [HttpPost]
    public async Task<IActionResult> UpdateGroupSubject(UpdateGroupSubjectViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var request = new UpdateGroupSubjectRequest
            {
                Subject = model.Subject
            };

            var response = await _evolutionClient.GroupService.UpdateGroupSubjectAsync(model.InstanceName, model.GroupJid, request);

            model.Result = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            model.HasError = false;
            TempData["SuccessMessage"] = "Assunto do grupo atualizado com sucesso!";

            _logger.LogInformation("Assunto do grupo atualizado com sucesso para a instância {InstanceName}", model.InstanceName);
        }
        catch (Exception ex)
        {
            model.Result = $"Erro: {ex.Message}";
            model.HasError = true;
            TempData["ErrorMessage"] = $"Erro ao atualizar assunto do grupo: {ex.Message}";
            _logger.LogError(ex, "Erro ao atualizar assunto do grupo para a instância {InstanceName}", model.InstanceName);
        }

        return View(model);
    }
}