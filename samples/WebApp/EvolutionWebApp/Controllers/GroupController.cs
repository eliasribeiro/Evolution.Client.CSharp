using Evolution.Client.CSharp;
using Evolution.Client.CSharp.Interfaces;
using Evolution.Client.CSharp.Models.Group;
using EvolutionWebApp.Models;
using EvolutionWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EvolutionWebApp.Controllers;

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

    #region Create Group

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
            }
            else
            {
                model.Result = null;
                model.ErrorMessage = "Resposta nula recebida do serviço";
            }

            _logger.LogInformation("Grupo criado com sucesso para a instância {InstanceName}", model.InstanceName);
        }
        catch (Exception ex)
        {
            model.Result = null;
            model.ErrorMessage = ex.Message;
            _logger.LogError(ex, "Erro ao criar grupo para a instância {InstanceName}", model.InstanceName);
        }

        return View(model);
    }

    #endregion

    #region Update Group Picture

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

            _logger.LogInformation("Foto do grupo atualizada com sucesso para a instância {InstanceName}", model.InstanceName);
        }
        catch (Exception ex)
        {
            model.Result = $"Erro: {ex.Message}";
            model.HasError = true;
            _logger.LogError(ex, "Erro ao atualizar foto do grupo para a instância {InstanceName}", model.InstanceName);
        }

        return View(model);
    }

    #endregion

    #region Update Group Subject

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

            _logger.LogInformation("Assunto do grupo atualizado com sucesso para a instância {InstanceName}", model.InstanceName);
        }
        catch (Exception ex)
        {
            model.Result = $"Erro: {ex.Message}";
            model.HasError = true;
            _logger.LogError(ex, "Erro ao atualizar assunto do grupo para a instância {InstanceName}", model.InstanceName);
        }

        return View(model);
    }

    #endregion

    #region Update Group Description

    /// <summary>
    /// Exibe a página para atualizar descrição do grupo.
    /// </summary>
    [HttpGet]
    public IActionResult UpdateGroupDescription()
    {
        return View(new UpdateGroupDescriptionViewModel());
    }

    /// <summary>
    /// Atualiza a descrição do grupo.
    /// </summary>
    /// <param name="model">Dados para atualização da descrição.</param>
    [HttpPost]
    public async Task<IActionResult> UpdateGroupDescription(UpdateGroupDescriptionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var request = new UpdateGroupDescriptionRequest
            {
                Description = model.Description
            };

            var response = await _evolutionClient.GroupService.UpdateGroupDescriptionAsync(model.InstanceName, model.GroupJid, request);

            model.Result = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            model.HasError = false;

            _logger.LogInformation("Descrição do grupo atualizada com sucesso para a instância {InstanceName}", model.InstanceName);
        }
        catch (Exception ex)
        {
            model.Result = $"Erro: {ex.Message}";
            model.HasError = true;
            _logger.LogError(ex, "Erro ao atualizar descrição do grupo para a instância {InstanceName}", model.InstanceName);
        }

        return View(model);
    }

    #endregion

    #region Fetch Invite Code

    /// <summary>
    /// Exibe a página para buscar código de convite.
    /// </summary>
    [HttpGet]
    public IActionResult FetchInviteCode()
    {
        return View(new FetchInviteCodeViewModel());
    }

    /// <summary>
    /// Busca o código de convite do grupo.
    /// </summary>
    /// <param name="model">Dados para buscar o código de convite.</param>
    [HttpPost]
    public async Task<IActionResult> FetchInviteCode(FetchInviteCodeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var response = await _evolutionClient.GroupService.FetchInviteCodeAsync(model.InstanceName, model.GroupJid);

            model.InviteCode = response.InviteCode;
            model.InviteUrl = response.InviteUrl;
            model.Result = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            model.HasError = false;

            _logger.LogInformation("Código de convite obtido com sucesso para a instância {InstanceName}", model.InstanceName);
        }
        catch (Exception ex)
        {
            model.Result = $"Erro: {ex.Message}";
            model.HasError = true;
            _logger.LogError(ex, "Erro ao buscar código de convite para a instância {InstanceName}", model.InstanceName);
        }

        return View(model);
    }

    #endregion

    #region Revoke Invite Code

    /// <summary>
    /// Exibe a página para revogar código de convite.
    /// </summary>
    [HttpGet]
    public IActionResult RevokeInviteCode()
    {
        return View(new RevokeInviteCodeViewModel());
    }

    /// <summary>
    /// Revoga o código de convite do grupo.
    /// </summary>
    /// <param name="model">Dados para revogar o código de convite.</param>
    [HttpPost]
    public async Task<IActionResult> RevokeInviteCode(RevokeInviteCodeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var response = await _evolutionClient.GroupService.RevokeInviteCodeAsync(model.InstanceName, model.GroupJid);

            model.NewInviteCode = response.InviteCode;
            model.Result = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            model.HasError = false;

            _logger.LogInformation("Código de convite revogado com sucesso para a instância {InstanceName}", model.InstanceName);
        }
        catch (Exception ex)
        {
            model.Result = $"Erro: {ex.Message}";
            model.HasError = true;
            _logger.LogError(ex, "Erro ao revogar código de convite para a instância {InstanceName}", model.InstanceName);
        }

        return View(model);
    }

    #endregion

    #region Send Group Invite

    /// <summary>
    /// Exibe a página para enviar convite de grupo.
    /// </summary>
    [HttpGet]
    public IActionResult SendGroupInvite()
    {
        return View(new SendGroupInviteViewModel());
    }

    /// <summary>
    /// Envia convite do grupo para números específicos.
    /// </summary>
    /// <param name="model">Dados para enviar o convite.</param>
    [HttpPost]
    public async Task<IActionResult> SendGroupInvite(SendGroupInviteViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var numbers = model.Numbers
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(n => n.Trim())
                .Where(n => !string.IsNullOrEmpty(n))
                .ToList();

            var request = new SendGroupInviteRequest
            {
                Numbers = numbers,
                Text = model.Text
            };

            var response = await _evolutionClient.GroupService.SendGroupInviteAsync(model.InstanceName, model.GroupJid, request);

            model.Result = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            model.HasError = false;

            _logger.LogInformation("Convites enviados com sucesso para a instância {InstanceName}", model.InstanceName);
        }
        catch (Exception ex)
        {
            model.Result = $"Erro: {ex.Message}";
            model.HasError = true;
            _logger.LogError(ex, "Erro ao enviar convites para a instância {InstanceName}", model.InstanceName);
        }

        return View(model);
    }

    #endregion

    #region Find Group By Invite Code

    /// <summary>
    /// Exibe a página para buscar grupo por código de convite.
    /// </summary>
    [HttpGet]
    public IActionResult FindGroupByInviteCode()
    {
        return View(new FindGroupByInviteCodeViewModel());
    }

    /// <summary>
    /// Busca grupo pelo código de convite.
    /// </summary>
    /// <param name="model">Dados para buscar o grupo.</param>
    [HttpPost]
    public async Task<IActionResult> FindGroupByInviteCode(FindGroupByInviteCodeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var response = await _evolutionClient.GroupService.FindGroupByInviteCodeAsync(model.InstanceName, model.InviteCode);

            model.GroupInfo = new GroupInfoViewModel
            {
                Id = response.Id,
                Subject = response.Subject,
                Description = response.Description,
                InviteCode = response.InviteCode,
                InviteUrl = response.InviteUrl,
                Size = response.Size,
                Owner = response.Owner,
                CreationDate = response.Creation.HasValue ? DateTimeOffset.FromUnixTimeSeconds(response.Creation.Value).DateTime : null
            };

            model.Result = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            model.HasError = false;

            _logger.LogInformation("Grupo encontrado com sucesso para a instância {InstanceName}", model.InstanceName);
        }
        catch (Exception ex)
        {
            model.Result = $"Erro: {ex.Message}";
            model.HasError = true;
            _logger.LogError(ex, "Erro ao buscar grupo para a instância {InstanceName}", model.InstanceName);
        }

        return View(model);
    }

    #endregion

    #region Find Group By Jid

    /// <summary>
    /// Exibe a página para buscar grupo por JID.
    /// </summary>
    [HttpGet]
    public IActionResult FindGroupByJid()
    {
        return View(new FindGroupByJidViewModel());
    }

    /// <summary>
    /// Busca grupo pelo JID.
    /// </summary>
    /// <param name="model">Dados para buscar o grupo.</param>
    [HttpPost]
    public async Task<IActionResult> FindGroupByJid(FindGroupByJidViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var response = await _evolutionClient.GroupService.FindGroupByJidAsync(model.InstanceName, model.GroupJid);

            model.Result = response;
            model.ErrorMessage = null;

            _logger.LogInformation("Grupo encontrado com sucesso para a instância {InstanceName}", model.InstanceName);
        }
        catch (Exception ex)
        {
            model.Result = null;
            model.ErrorMessage = ex.Message;
            _logger.LogError(ex, "Erro ao buscar grupo para a instância {InstanceName}", model.InstanceName);
        }

        return View(model);
    }

    #endregion

    #region Fetch All Groups

    /// <summary>
    /// Exibe a página para buscar todos os grupos.
    /// </summary>
    [HttpGet]
    public IActionResult FetchAllGroups()
    {
        return View(new FetchAllGroupsViewModel());
    }

    /// <summary>
    /// Busca todos os grupos.
    /// </summary>
    /// <param name="model">Dados para buscar os grupos.</param>
    [HttpPost]
    public async Task<IActionResult> FetchAllGroups(FetchAllGroupsViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var response = await _evolutionClient.GroupService.FetchAllGroupsAsync(model.InstanceName, getParticipants: true);

            model.Groups = response;
            model.ErrorMessage = null;

            _logger.LogInformation("Grupos obtidos com sucesso para a instância {InstanceName}", model.InstanceName);
        }
        catch (Exception ex)
        {
            model.Groups = null;
            model.ErrorMessage = ex.Message;
            _logger.LogError(ex, "Erro ao buscar grupos para a instância {InstanceName}", model.InstanceName);
        }

        return View(model);
    }

    #endregion

    #region Find Participants

    /// <summary>
    /// Exibe a página para buscar participantes do grupo.
    /// </summary>
    [HttpGet]
    public IActionResult FindParticipants()
    {
        return View(new FindParticipantsViewModel());
    }

    /// <summary>
    /// Busca participantes do grupo.
    /// </summary>
    /// <param name="model">Dados para buscar os participantes.</param>
    [HttpPost]
    public async Task<IActionResult> FindParticipants(FindParticipantsViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var response = await _evolutionClient.GroupService.FindParticipantsAsync(model.InstanceName, model.GroupJid);

            model.Result = response;
            model.ErrorMessage = null;

            _logger.LogInformation("Participantes obtidos com sucesso para a instância {InstanceName}", model.InstanceName);
        }
        catch (Exception ex)
        {
            model.Result = null;
            model.ErrorMessage = ex.Message;
            _logger.LogError(ex, "Erro ao buscar participantes para a instância {InstanceName}", model.InstanceName);
        }

        return View(model);
    }

    #endregion

    #region Update Participant

    /// <summary>
    /// Exibe a página para atualizar participantes do grupo.
    /// </summary>
    [HttpGet]
    public IActionResult UpdateParticipant()
    {
        return View(new UpdateParticipantViewModel());
    }

    /// <summary>
    /// Atualiza participantes do grupo.
    /// </summary>
    /// <param name="model">Dados para atualizar os participantes.</param>
    [HttpPost]
    public async Task<IActionResult> UpdateParticipant(UpdateParticipantViewModel model)
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

            var request = new UpdateParticipantRequest
            {
                Action = model.Action,
                Participants = participants
            };

            var response = await _evolutionClient.GroupService.UpdateParticipantAsync(model.InstanceName, model.GroupJid, request);

            model.Result = response;
            model.ErrorMessage = null;

            _logger.LogInformation("Participantes atualizados com sucesso para a instância {InstanceName}", model.InstanceName);
        }
        catch (Exception ex)
        {
            model.Result = null;
            model.ErrorMessage = ex.Message;
            _logger.LogError(ex, "Erro ao atualizar participantes para a instância {InstanceName}", model.InstanceName);
        }

        return View(model);
    }

    #endregion

    #region Update Group Setting

    /// <summary>
    /// Exibe a página para atualizar configurações do grupo.
    /// </summary>
    [HttpGet]
    public IActionResult UpdateGroupSetting()
    {
        return View(new UpdateGroupSettingViewModel());
    }

    /// <summary>
    /// Atualiza configurações do grupo.
    /// </summary>
    /// <param name="model">Dados para atualizar as configurações.</param>
    [HttpPost]
    public async Task<IActionResult> UpdateGroupSetting(UpdateGroupSettingViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var request = new UpdateGroupSettingRequest
            {
                Action = model.Action
            };

            var response = await _evolutionClient.GroupService.UpdateGroupSettingAsync(model.InstanceName, model.GroupJid, request);

            model.Result = response;
            model.ErrorMessage = null;

            _logger.LogInformation("Configurações do grupo atualizadas com sucesso para a instância {InstanceName}", model.InstanceName);
        }
        catch (Exception ex)
        {
            model.Result = null;
            model.ErrorMessage = ex.Message;
            _logger.LogError(ex, "Erro ao atualizar configurações do grupo para a instância {InstanceName}", model.InstanceName);
        }

        return View(model);
    }

    #endregion

    #region Toggle Ephemeral

    /// <summary>
    /// Exibe a página para alternar mensagens efêmeras.
    /// </summary>
    [HttpGet]
    public IActionResult ToggleEphemeral()
    {
        return View(new ToggleEphemeralViewModel());
    }

    /// <summary>
    /// Alterna mensagens efêmeras do grupo.
    /// </summary>
    /// <param name="model">Dados para alternar mensagens efêmeras.</param>
    [HttpPost]
    public async Task<IActionResult> ToggleEphemeral(ToggleEphemeralViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var request = new ToggleEphemeralRequest
            {
                Expiration = model.Expiration
            };

            var response = await _evolutionClient.GroupService.ToggleEphemeralAsync(model.InstanceName, model.GroupJid, request);

            model.Result = response;
            model.ErrorMessage = null;

            _logger.LogInformation("Mensagens efêmeras alternadas com sucesso para a instância {InstanceName}", model.InstanceName);
        }
        catch (Exception ex)
        {
            model.Result = null;
            model.ErrorMessage = ex.Message;
            _logger.LogError(ex, "Erro ao alternar mensagens efêmeras para a instância {InstanceName}", model.InstanceName);
        }

        return View(model);
    }

    #endregion

    #region Leave Group

    /// <summary>
    /// Exibe a página para sair do grupo.
    /// </summary>
    [HttpGet]
    public IActionResult LeaveGroup()
    {
        return View(new LeaveGroupViewModel());
    }

    /// <summary>
    /// Sai do grupo.
    /// </summary>
    /// <param name="model">Dados para sair do grupo.</param>
    [HttpPost]
    public async Task<IActionResult> LeaveGroup(LeaveGroupViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var response = await _evolutionClient.GroupService.LeaveGroupAsync(model.InstanceName, model.GroupJid);

            model.Result = response;
            model.ErrorMessage = null;

            _logger.LogInformation("Saída do grupo realizada com sucesso para a instância {InstanceName}", model.InstanceName);
        }
        catch (Exception ex)
        {
            model.Result = null;
            model.ErrorMessage = ex.Message;
            _logger.LogError(ex, "Erro ao sair do grupo para a instância {InstanceName}", model.InstanceName);
        }

        return View(model);
    }

    #endregion
}