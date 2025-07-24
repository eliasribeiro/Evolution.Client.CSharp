using Microsoft.AspNetCore.Mvc;
using Evolution.Client.CSharp;
using Evolution.Client.CSharp.Models.Chat;
using EvolutionWebApp.Models;
using System.Net;

namespace EvolutionWebApp.Controllers;

/// <summary>
/// Controlador para gerenciar funcionalidades de chat da API Evolution.
/// </summary>
public class ChatController : Controller
{
    private readonly ILogger<ChatController> _logger;
    private readonly EvolutionApiClient _evolutionClient;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="ChatController"/>.
    /// </summary>
    /// <param name="logger">O logger para registrar informações.</param>
    /// <param name="evolutionClient">O cliente da API Evolution.</param>
    public ChatController(ILogger<ChatController> logger, EvolutionApiClient evolutionClient)
    {
        _logger = logger;
        _evolutionClient = evolutionClient;
    }

    /// <summary>
    /// Exibe a página principal do Chat.
    /// </summary>
    /// <returns>A view da página principal do Chat.</returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Exibe a página de Verificar WhatsApp.
    /// </summary>
    /// <returns>A view da página de Verificar WhatsApp.</returns>
    public IActionResult CheckWhatsApp()
    {
        return View();
    }

    /// <summary>
    /// Exibe a página de Buscar Contatos.
    /// </summary>
    /// <returns>A view da página de Buscar Contatos.</returns>
    public IActionResult FindContacts()
    {
        return View();
    }

    /// <summary>
    /// Exibe a página de Buscar Mensagens.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="searchRemoteJid">Remote JID para buscar mensagens (opcional).</param>
    /// <param name="page">Número da página para paginação (opcional).</param>
    /// <param name="offset">Offset para paginação (opcional).</param>
    /// <returns>A view da página de Buscar Mensagens.</returns>
    public async Task<IActionResult> FindMessages(string? instanceName = null, string? searchRemoteJid = null, int? page = null, int? offset = null)
    {
        // Se não há parâmetros, apenas exibe a página vazia
        if (string.IsNullOrWhiteSpace(instanceName))
        {
            return View();
        }

        // Se há parâmetros, executa a busca (mesmo código do POST)
        return await ExecuteFindMessages(instanceName, searchRemoteJid, page, offset);
    }

    /// <summary>
    /// Exibe a página de Buscar Chats.
    /// </summary>
    /// <returns>A view da página de Buscar Chats.</returns>
    public IActionResult FindChats()
    {
        return View(new ChatSearchResultViewModel());
    }

    /// <summary>
    /// Exibe a página de Buscar URL da Foto de Perfil.
    /// </summary>
    /// <returns>A view da página de Buscar URL da Foto de Perfil.</returns>
    public IActionResult FetchProfilePicUrl()
    {
        return View(new ProfilePicUrlViewModel());
    }

    /// <summary>
    /// Exibe a página de Deletar Mensagem para Todos.
    /// </summary>
    /// <returns>A view da página de Deletar Mensagem para Todos.</returns>
    public IActionResult DeleteMessageForEveryone()
    {
        return View(new DeleteMessageViewModel());
    }

    /// <summary>
    /// Verifica se os números fornecidos existem no WhatsApp.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="numbers">Os números a serem verificados (separados por vírgula ou quebra de linha).</param>
    /// <returns>Um JSON com o resultado da verificação.</returns>
    [HttpPost]
    public async Task<IActionResult> CheckWhatsAppNumbers(string instanceName, string numbers)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(instanceName))
            {
                return Json(new { success = false, message = "Nome da instância é obrigatório." });
            }

            if (string.IsNullOrWhiteSpace(numbers))
            {
                return Json(new { success = false, message = "Pelo menos um número deve ser fornecido." });
            }

            // Processa os números (remove espaços, quebras de linha e separa por vírgula)
            var numberList = numbers
                .Split(new[] { ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => n.Trim())
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .ToList();

            if (numberList.Count == 0)
            {
                return Json(new { success = false, message = "Nenhum número válido foi fornecido." });
            }

            var request = new CheckWhatsAppRequest
            {
                Numbers = numberList
            };

            _logger.LogInformation("Verificando {Count} números do WhatsApp para a instância: {InstanceName}", 
                numberList.Count, instanceName);

            var result = await _evolutionClient.Chat.CheckWhatsAppNumbersAsync(instanceName, request);

            _logger.LogInformation("Verificação concluída com sucesso para a instância: {InstanceName}. Resultados: {Count}", 
                instanceName, result.Count);

            return Json(new { 
                success = true, 
                message = $"Verificação concluída! {result.Count} resultado(s) encontrado(s).",
                data = result
            });
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância não encontrada: {InstanceName}", instanceName);
            return Json(new { success = false, message = $"Instância '{instanceName}' não encontrada." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao verificar números do WhatsApp para a instância: {InstanceName}", instanceName);
            return Json(new { success = false, message = "Erro interno do servidor. Tente novamente." });
        }
    }

    /// <summary>
    /// Busca mensagens usando formulário ASP.NET MVC tradicional.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="searchRemoteJid">Remote JID para buscar mensagens (opcional).</param>
    /// <param name="page">Número da página para paginação (opcional).</param>
    /// <param name="offset">Offset para paginação (opcional).</param>
    /// <returns>A view com os resultados da busca.</returns>
    [HttpPost]
    [ActionName("FindMessages")]
    public async Task<IActionResult> FindMessagesPost(string instanceName, string? searchRemoteJid = null, int? page = null, int? offset = null)
    {
        return await ExecuteFindMessages(instanceName, searchRemoteJid, page, offset);
    }

    /// <summary>
    /// Executa a busca de mensagens (método comum para GET e POST).
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="searchRemoteJid">Remote JID para buscar mensagens (opcional).</param>
    /// <param name="page">Número da página para paginação (opcional).</param>
    /// <param name="offset">Offset para paginação (opcional).</param>
    /// <returns>A view com os resultados da busca.</returns>
    private async Task<IActionResult> ExecuteFindMessages(string instanceName, string? searchRemoteJid = null, int? page = null, int? offset = null)
    {
        var viewModel = new MessageSearchResultViewModel();
        
        try
        {
            // Preserva os valores do formulário
            ViewBag.InstanceName = instanceName;
            ViewBag.SearchRemoteJid = searchRemoteJid;
            ViewBag.Page = page;
            ViewBag.Offset = offset;

            if (string.IsNullOrWhiteSpace(instanceName))
            {
                TempData["ErrorMessage"] = "Nome da instância é obrigatório.";
                return View(viewModel);
            }

            FindMessagesRequest? request = null;

            // Se algum critério de busca foi fornecido, cria a requisição
            if (!string.IsNullOrWhiteSpace(searchRemoteJid) || page.HasValue || offset.HasValue)
            {
                request = new FindMessagesRequest();

                if (!string.IsNullOrWhiteSpace(searchRemoteJid))
                {
                    request.Where = new FindMessagesWhere
                    {
                        Key = new MessageKey
                        {
                            RemoteJid = searchRemoteJid.Trim()
                        }
                    };
                }

                if (page.HasValue)
                    request.Page = page.Value;

                if (offset.HasValue)
                    request.Offset = offset.Value;
            }

            _logger.LogInformation("Buscando mensagens para a instância: {InstanceName}", instanceName);

            var result = await _evolutionClient.Chat.FindMessagesAsync(instanceName, request);

            // Mapeia os resultados para o ViewModel
            viewModel.Messages = result.Messages.Records?.Select(m => new MessageResult
            {
                Key = m.Key?.Id,
                MessageId = m.Key?.Id, // Adicionando ID da mensagem
                PushName = m.PushName,
                Message = m.Message?.Conversation ?? m.Message?.MessageContextInfo?.MessageSecret,
                MessageType = m.MessageType,
                ChatId = m.Key?.RemoteJid,
                FromMe = m.Key?.FromMe ?? false,
                DateTime = m.MessageTimestamp > 0 ? DateTimeOffset.FromUnixTimeSeconds(m.MessageTimestamp).DateTime : DateTime.MinValue,
                Status = "N/A", // Status não disponível na API
                Source = m.Source
            }).ToList() ?? new List<MessageResult>();

            viewModel.TotalCount = result.Messages.Total;
            viewModel.CurrentPage = result.Messages.CurrentPage;
            viewModel.PageSize = 20; // Valor padrão

            _logger.LogInformation("Busca de mensagens concluída com sucesso para a instância: {InstanceName}. Total: {Total}, Página: {CurrentPage}/{Pages}", 
                instanceName, result.Messages.Total, result.Messages.CurrentPage, result.Messages.Pages);

            TempData["SuccessMessage"] = $"Busca concluída! {result.Messages.Total} mensagem(s) encontrada(s). Página {result.Messages.CurrentPage} de {result.Messages.Pages}.";
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância não encontrada: {InstanceName}", instanceName);
            TempData["ErrorMessage"] = $"Instância '{instanceName}' não encontrada.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar mensagens para a instância: {InstanceName}", instanceName);
            TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
        }

        return View(viewModel);
    }

    /// <summary>
    /// Deleta uma mensagem para todos os participantes da conversa.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="messageId">O ID da mensagem a ser deletada.</param>
    /// <param name="remoteJid">O JID remoto do chat.</param>
    /// <param name="fromMe">Indica se a mensagem foi enviada pelo proprietário da instância.</param>
    /// <param name="participant">O participante (usado apenas em grupos).</param>
    /// <returns>A view com o resultado da operação.</returns>
    [HttpPost]
    public IActionResult DeleteMessageForEveryone(string instanceName, string messageId, string remoteJid, bool fromMe = true, string? participant = null)
    {
        var viewModel = new DeleteMessageViewModel();

        try
        {
            // Preserva os valores do formulário
            ViewBag.InstanceName = instanceName;
            ViewBag.MessageId = messageId;
            ViewBag.RemoteJid = remoteJid;
            ViewBag.FromMe = fromMe;
            ViewBag.Participant = participant;

            if (string.IsNullOrWhiteSpace(instanceName))
            {
                TempData["ErrorMessage"] = "Nome da instância é obrigatório.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(messageId))
            {
                TempData["ErrorMessage"] = "ID da mensagem é obrigatório.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(remoteJid))
            {
                TempData["ErrorMessage"] = "Remote JID é obrigatório.";
                return View(viewModel);
            }

            _logger.LogInformation("Tentativa de deletar mensagem para todos na instância: {InstanceName}, ID da mensagem: {MessageId}, Remote JID: {RemoteJid}",
                instanceName, messageId, remoteJid);

            // NOTA: A funcionalidade de deletar mensagem ainda não está implementada no SDK
            TempData["ErrorMessage"] = "A funcionalidade de deletar mensagem ainda não está implementada no SDK Evolution Client.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao tentar deletar mensagem para todos na instância: {InstanceName}, ID da mensagem: {MessageId}", instanceName, messageId);
            TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
        }

        return View(viewModel);
    }

    /// <summary>
    /// Busca contatos usando formulário ASP.NET MVC tradicional.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="searchId">ID do contato para buscar (opcional).</param>
    /// <param name="searchRemoteJid">Remote JID do contato para buscar (opcional).</param>
    /// <param name="searchPushName">Push Name do contato para buscar (opcional).</param>
    /// <returns>A view com os resultados da busca.</returns>
    [HttpPost]
    public async Task<IActionResult> FindContacts(string instanceName, string? searchId = null, string? searchRemoteJid = null, string? searchPushName = null)
    {
        var viewModel = new ContactSearchResultViewModel();
        
        try
        {
            // Preserva os valores do formulário
            ViewBag.InstanceName = instanceName;
            ViewBag.SearchId = searchId;
            ViewBag.SearchRemoteJid = searchRemoteJid;
            ViewBag.SearchPushName = searchPushName;

            if (string.IsNullOrWhiteSpace(instanceName))
            {
                TempData["ErrorMessage"] = "Nome da instância é obrigatório.";
                return View(viewModel);
            }

            FindContactsRequest? request = null;

            // Se algum critério de busca foi fornecido, cria a requisição
            if (!string.IsNullOrWhiteSpace(searchId) || !string.IsNullOrWhiteSpace(searchRemoteJid) || !string.IsNullOrWhiteSpace(searchPushName))
            {
                request = new FindContactsRequest
                {
                    Where = new FindContactsWhere()
                };

                if (!string.IsNullOrWhiteSpace(searchId))
                    request.Where.Id = searchId.Trim();

                if (!string.IsNullOrWhiteSpace(searchRemoteJid))
                    request.Where.RemoteJid = searchRemoteJid.Trim();

                if (!string.IsNullOrWhiteSpace(searchPushName))
                    request.Where.PushName = searchPushName.Trim();
            }

            _logger.LogInformation("Buscando contatos para a instância: {InstanceName}", instanceName);

            var result = await _evolutionClient.Chat.FindContactsAsync(instanceName, request);

            // Mapeia os resultados para o ViewModel
            viewModel.Contacts = result.Select(c => new ContactResult
            {
                Id = c.Id,
                PushName = c.PushName,
                ProfilePictureUrl = c.ProfilePicUrl,
                RemoteJid = c.RemoteJid,
                Name = c.PushName, // Usando PushName como Name
                NotifyName = c.PushName, // Usando PushName como NotifyName
                VerifiedName = c.PushName, // Usando PushName como VerifiedName
                Status = "Ativo", // Status padrão
                IsGroup = false, // Propriedade não disponível na API
                IsContact = true, // Assumindo que todos são contatos
                IsMyContact = false, // Propriedade não disponível na API
                IsWAContact = true,  // Assumindo que todos os contatos retornados são do WhatsApp
                IsBlocked = false    // Propriedade não disponível na API
            }).ToList();

            viewModel.TotalCount = result.Count;
            viewModel.CurrentPage = 1;
            viewModel.PageSize = result.Count;

            _logger.LogInformation("Busca de contatos concluída com sucesso para a instância: {InstanceName}. Contatos encontrados: {Count}", 
                instanceName, result.Count);

            TempData["SuccessMessage"] = $"Busca concluída! {result.Count} contato(s) encontrado(s).";
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância não encontrada: {InstanceName}", instanceName);
            TempData["ErrorMessage"] = $"Instância '{instanceName}' não encontrada.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar contatos para a instância: {InstanceName}", instanceName);
            TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
        }

        return View(viewModel);
    }

    /// <summary>
    /// Busca chats usando formulário ASP.NET MVC tradicional.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="searchId">ID do chat para buscar (opcional).</param>
    /// <param name="searchRemoteJid">Remote JID do chat para buscar (opcional).</param>
    /// <param name="searchPushName">Push Name do chat para buscar (opcional).</param>
    /// <returns>A view com os resultados da busca.</returns>
    [HttpPost]
    public async Task<IActionResult> FindChats(string instanceName, string? searchId = null, string? searchRemoteJid = null, string? searchPushName = null)
    {
        var viewModel = new ChatSearchResultViewModel();

        try
        {
            // Preserva os valores do formulário
            ViewBag.InstanceName = instanceName;
            ViewBag.SearchId = searchId;
            ViewBag.SearchRemoteJid = searchRemoteJid;
            ViewBag.SearchPushName = searchPushName;

            if (string.IsNullOrWhiteSpace(instanceName))
            {
                TempData["ErrorMessage"] = "Nome da instância é obrigatório.";
                return View(viewModel);
            }

            FindChatsRequest? request = null;

            // Se algum critério de busca foi fornecido, cria a requisição
            if (!string.IsNullOrWhiteSpace(searchId) || !string.IsNullOrWhiteSpace(searchRemoteJid) || !string.IsNullOrWhiteSpace(searchPushName))
            {
                request = new FindChatsRequest
                {
                    Where = new FindChatsWhere()
                };

                if (!string.IsNullOrWhiteSpace(searchId))
                    request.Where.Id = searchId.Trim();

                if (!string.IsNullOrWhiteSpace(searchRemoteJid))
                    request.Where.RemoteJid = searchRemoteJid.Trim();

                if (!string.IsNullOrWhiteSpace(searchPushName))
                    request.Where.PushName = searchPushName.Trim();
            }

            _logger.LogInformation("Buscando chats para a instância: {InstanceName}", instanceName);

            var result = await _evolutionClient.Chat.FindChatsAsync(instanceName, request);

            // Mapeia os resultados para o ViewModel
            viewModel.Chats = result.Select(c => new ChatResult
            {
                Id = c.Id,
                RemoteJid = c.RemoteJid,
                PushName = c.PushName,
                ProfilePicUrl = c.ProfilePicUrl,
                UpdatedAt = c.UpdatedAt,
                WindowStart = c.WindowStart,
                WindowExpires = c.WindowExpires,
                WindowActive = c.WindowActive,
                ChatType = c.RemoteJid?.Contains("@g.us") == true ? "Grupo" : "Individual"
            }).ToList();

            viewModel.TotalCount = result.Count;
            viewModel.InstanceName = instanceName;
            viewModel.SearchId = searchId;
            viewModel.SearchRemoteJid = searchRemoteJid;
            viewModel.SearchPushName = searchPushName;

            _logger.LogInformation("Busca de chats concluída com sucesso para a instância: {InstanceName}. Chats encontrados: {Count}",
                instanceName, result.Count);

            TempData["SuccessMessage"] = $"Busca concluída! {result.Count} chat(s) encontrado(s).";
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância não encontrada: {InstanceName}", instanceName);
            TempData["ErrorMessage"] = $"Instância '{instanceName}' não encontrada.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar chats para a instância: {InstanceName}", instanceName);
            TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
        }

        return View(viewModel);
    }

    /// <summary>
    /// Verifica números do WhatsApp usando formulário ASP.NET MVC tradicional.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="numbers">Os números a serem verificados (separados por vírgula ou quebra de linha).</param>
    /// <returns>A view com os resultados da verificação.</returns>
    [HttpPost]
    public async Task<IActionResult> CheckWhatsApp(string instanceName, string numbers)
    {
        var viewModel = new WhatsAppCheckResultViewModel();
        
        try
        {
            // Preserva os valores do formulário
            ViewBag.InstanceName = instanceName;
            ViewBag.Numbers = numbers;

            if (string.IsNullOrWhiteSpace(instanceName))
            {
                TempData["ErrorMessage"] = "Nome da instância é obrigatório.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(numbers))
            {
                TempData["ErrorMessage"] = "Pelo menos um número deve ser fornecido.";
                return View(viewModel);
            }

            // Processa os números (remove espaços, quebras de linha e separa por vírgula)
            var numberList = numbers
                .Split(new[] { ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => n.Trim())
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .ToList();

            if (numberList.Count == 0)
            {
                TempData["ErrorMessage"] = "Nenhum número válido foi fornecido.";
                return View(viewModel);
            }

            var request = new CheckWhatsAppRequest
            {
                Numbers = numberList
            };

            _logger.LogInformation("Verificando {Count} números do WhatsApp para a instância: {InstanceName}", 
                numberList.Count, instanceName);

            var result = await _evolutionClient.Chat.CheckWhatsAppNumbersAsync(instanceName, request);

            // Mapeia os resultados para o ViewModel
            viewModel.Results = result.Select(r => new WhatsAppCheckResult
            {
                Number = r.Number,
                Exists = r.Exists,
                Jid = r.Jid
            }).ToList();

            viewModel.TotalCount = result.Count;
            viewModel.InstanceName = instanceName;
            viewModel.Numbers = numbers;

            _logger.LogInformation("Verificação concluída com sucesso para a instância: {InstanceName}. Resultados: {Count}", 
                instanceName, result.Count);

            TempData["SuccessMessage"] = $"Verificação concluída! {result.Count} resultado(s) encontrado(s).";
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância não encontrada: {InstanceName}", instanceName);
            TempData["ErrorMessage"] = $"Instância '{instanceName}' não encontrada.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao verificar números do WhatsApp para a instância: {InstanceName}", instanceName);
            TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
        }

        return View(viewModel);
    }

    /// <summary>
    /// Busca a URL da foto de perfil usando formulário ASP.NET MVC tradicional.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="number">Número do WhatsApp para buscar a foto de perfil.</param>
    /// <returns>A view com o resultado da busca.</returns>
    [HttpPost]
    public async Task<IActionResult> FetchProfilePicUrl(string instanceName, string number)
    {
        var viewModel = new ProfilePicUrlViewModel();

        try
        {
            // Preserva os valores do formulário
            ViewBag.InstanceName = instanceName;
            ViewBag.Number = number;

            if (string.IsNullOrWhiteSpace(instanceName))
            {
                TempData["ErrorMessage"] = "Nome da instância é obrigatório.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(number))
            {
                TempData["ErrorMessage"] = "Número do WhatsApp é obrigatório.";
                return View(viewModel);
            }

            var request = new FetchProfilePicUrlRequest
            {
                Number = number.Trim()
            };

            _logger.LogInformation("Buscando URL da foto de perfil para o número: {Number} na instância: {InstanceName}",
                number, instanceName);

            var result = await _evolutionClient.Chat.FetchProfilePicUrlAsync(instanceName, request);

            // Mapeia o resultado para o ViewModel
            viewModel.Result = new ProfilePicUrlResult
            {
                Wuid = result.Wuid,
                ProfilePictureUrl = result.ProfilePictureUrl
            };

            viewModel.InstanceName = instanceName;
            viewModel.Number = number;

            _logger.LogInformation("URL da foto de perfil obtida com sucesso para o número: {Number} na instância: {InstanceName}",
                number, instanceName);

            TempData["SuccessMessage"] = "URL da foto de perfil obtida com sucesso!";
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância não encontrada ou número inválido: {InstanceName}, {Number}", instanceName, number);
            TempData["ErrorMessage"] = $"Instância '{instanceName}' não encontrada ou número '{number}' não existe no WhatsApp.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar URL da foto de perfil para o número: {Number} na instância: {InstanceName}",
                number, instanceName);
            TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
        }

        return View(viewModel);
    }

    /// <summary>
    /// Busca contatos da instância especificada.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="searchId">ID do contato para buscar (opcional).</param>
    /// <param name="searchRemoteJid">Remote JID do contato para buscar (opcional).</param>
    /// <param name="searchPushName">Push Name do contato para buscar (opcional).</param>
    /// <returns>Um JSON com a lista de contatos encontrados.</returns>
    [HttpPost]
    public async Task<IActionResult> SearchContacts(string instanceName, string? searchId = null, string? searchRemoteJid = null, string? searchPushName = null)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(instanceName))
            {
                return Json(new { success = false, message = "Nome da instância é obrigatório." });
            }

            FindContactsRequest? request = null;

            // Se algum critério de busca foi fornecido, cria a requisição
            if (!string.IsNullOrWhiteSpace(searchId) || !string.IsNullOrWhiteSpace(searchRemoteJid) || !string.IsNullOrWhiteSpace(searchPushName))
            {
                request = new FindContactsRequest
                {
                    Where = new FindContactsWhere()
                };

                if (!string.IsNullOrWhiteSpace(searchId))
                    request.Where.Id = searchId.Trim();

                if (!string.IsNullOrWhiteSpace(searchRemoteJid))
                    request.Where.RemoteJid = searchRemoteJid.Trim();

                if (!string.IsNullOrWhiteSpace(searchPushName))
                    request.Where.PushName = searchPushName.Trim();
            }

            _logger.LogInformation("Buscando contatos para a instância: {InstanceName}", instanceName);

            var result = await _evolutionClient.Chat.FindContactsAsync(instanceName, request);

            _logger.LogInformation("Busca de contatos concluída com sucesso para a instância: {InstanceName}. Contatos encontrados: {Count}", 
                instanceName, result.Count);

            return Json(new { 
                success = true, 
                message = $"Busca concluída! {result.Count} contato(s) encontrado(s).",
                data = result
            });
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância não encontrada: {InstanceName}", instanceName);
            return Json(new { success = false, message = $"Instância '{instanceName}' não encontrada." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar contatos para a instância: {InstanceName}", instanceName);
            return Json(new { success = false, message = "Erro interno do servidor. Tente novamente." });
        }
    }

    /// <summary>
    /// Busca mensagens da instância especificada.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="searchRemoteJid">Remote JID para buscar mensagens (opcional).</param>
    /// <param name="page">Número da página para paginação (opcional).</param>
    /// <param name="offset">Offset para paginação (opcional).</param>
    /// <returns>Um JSON com a lista de mensagens encontradas.</returns>
    [HttpPost]
    public async Task<IActionResult> SearchMessages(string instanceName, string? searchRemoteJid = null, int? page = null, int? offset = null)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(instanceName))
            {
                return Json(new { success = false, message = "Nome da instância é obrigatório." });
            }

            FindMessagesRequest? request = null;

            // Se algum critério de busca foi fornecido, cria a requisição
            if (!string.IsNullOrWhiteSpace(searchRemoteJid) || page.HasValue || offset.HasValue)
            {
                request = new FindMessagesRequest();

                if (!string.IsNullOrWhiteSpace(searchRemoteJid))
                {
                    request.Where = new FindMessagesWhere
                    {
                        Key = new MessageKey
                        {
                            RemoteJid = searchRemoteJid.Trim()
                        }
                    };
                }

                if (page.HasValue)
                    request.Page = page.Value;

                if (offset.HasValue)
                    request.Offset = offset.Value;
            }

            _logger.LogInformation("Buscando mensagens para a instância: {InstanceName}", instanceName);

            var result = await _evolutionClient.Chat.FindMessagesAsync(instanceName, request);

            _logger.LogInformation("Busca de mensagens concluída com sucesso para a instância: {InstanceName}. Total: {Total}, Página: {CurrentPage}/{Pages}", 
                instanceName, result.Messages.Total, result.Messages.CurrentPage, result.Messages.Pages);

            return Json(new { 
                success = true, 
                message = $"Busca concluída! {result.Messages.Total} mensagem(s) encontrada(s). Página {result.Messages.CurrentPage} de {result.Messages.Pages}.",
                data = result
            });
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância não encontrada: {InstanceName}", instanceName);
            return Json(new { success = false, message = $"Instância '{instanceName}' não encontrada." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar mensagens para a instância: {InstanceName}", instanceName);
            return Json(new { success = false, message = "Erro interno do servidor. Tente novamente." });
        }
    }

    /// <summary>
    /// Exibe a página de Obter Base64 de Mensagem de Mídia.
    /// </summary>
    /// <returns>A view da página de Obter Base64 de Mensagem de Mídia.</returns>
    public IActionResult GetBase64FromMediaMessage()
    {
        return View();
    }

    /// <summary>
    /// Obtém o base64 de uma mensagem de mídia.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="messageId">O ID da mensagem de mídia.</param>
    /// <param name="convertToMp4">Indica se deve converter vídeo para MP4 (apenas para vídeos).</param>
    /// <returns>Um JSON com os dados da mídia em base64.</returns>
    [HttpPost]
    public async Task<IActionResult> GetBase64FromMediaMessage(string instanceName, string messageId, bool convertToMp4 = false)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(instanceName))
            {
                return Json(new { success = false, message = "Nome da instância é obrigatório." });
            }

            if (string.IsNullOrWhiteSpace(messageId))
            {
                return Json(new { success = false, message = "ID da mensagem é obrigatório." });
            }

            var request = new GetBase64FromMediaMessageRequest
            {
                Message = new MediaMessageInfo
                {
                    Key = new MediaMessageKey
                    {
                        Id = messageId.Trim()
                    }
                },
                ConvertToMp4 = convertToMp4
            };

            _logger.LogInformation("Obtendo base64 da mensagem de mídia para a instância: {InstanceName}, ID da mensagem: {MessageId}", 
                instanceName, messageId);

            var result = await _evolutionClient.Chat.GetBase64FromMediaMessageAsync(instanceName, request);

            _logger.LogInformation("Base64 da mensagem de mídia obtido com sucesso para a instância: {InstanceName}, ID da mensagem: {MessageId}, Tipo de mídia: {MediaType}", 
                instanceName, messageId, result.MediaType);

            return Json(new { 
                success = true, 
                message = $"Base64 obtido com sucesso! Tipo de mídia: {result.MediaType}, Arquivo: {result.FileName}",
                data = result
            });
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância ou mensagem não encontrada: {InstanceName}, ID da mensagem: {MessageId}", instanceName, messageId);
            return Json(new { success = false, message = $"Instância '{instanceName}' ou mensagem com ID '{messageId}' não encontrada." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter base64 da mensagem de mídia para a instância: {InstanceName}, ID da mensagem: {MessageId}", instanceName, messageId);
            return Json(new { success = false, message = "Erro interno do servidor. Tente novamente." });
        }
    }

    /// <summary>
    /// Exibe a página de Atualizar Mensagem.
    /// </summary>
    /// <returns>A view da página de Atualizar Mensagem.</returns>
    public IActionResult UpdateMessage()
    {
        return View();
    }

    /// <summary>
    /// Atualiza o conteúdo de uma mensagem enviada.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="number">O número do destinatário.</param>
    /// <param name="text">O novo texto da mensagem.</param>
    /// <param name="remoteJid">O JID remoto do chat.</param>
    /// <param name="messageId">O ID da mensagem a ser atualizada.</param>
    /// <param name="fromMe">Indica se a mensagem foi enviada pelo proprietário da instância.</param>
    /// <returns>Um JSON com as informações da mensagem atualizada.</returns>
    [HttpPost]
    public async Task<IActionResult> UpdateMessage(string instanceName, string number, string text, string remoteJid, string messageId, bool fromMe = true)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(instanceName))
            {
                return Json(new { success = false, message = "Nome da instância é obrigatório." });
            }

            if (string.IsNullOrWhiteSpace(number))
            {
                return Json(new { success = false, message = "Número do destinatário é obrigatório." });
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                return Json(new { success = false, message = "Novo texto da mensagem é obrigatório." });
            }

            if (string.IsNullOrWhiteSpace(remoteJid))
            {
                return Json(new { success = false, message = "Remote JID é obrigatório." });
            }

            if (string.IsNullOrWhiteSpace(messageId))
            {
                return Json(new { success = false, message = "ID da mensagem é obrigatório." });
            }

            var request = new UpdateMessageRequest
            {
                Number = number.Trim(),
                Text = text.Trim(),
                Key = new UpdateMessageKey
                {
                    RemoteJid = remoteJid.Trim(),
                    FromMe = fromMe,
                    Id = messageId.Trim()
                }
            };

            _logger.LogInformation("Atualizando mensagem para a instância: {InstanceName}, ID da mensagem: {MessageId}, Número: {Number}", 
                instanceName, messageId, number);

            var result = await _evolutionClient.Chat.UpdateMessageAsync(instanceName, request);

            _logger.LogInformation("Mensagem atualizada com sucesso para a instância: {InstanceName}, ID da mensagem: {MessageId}", 
                instanceName, messageId);

            return Json(new { 
                success = true, 
                message = $"Mensagem atualizada com sucesso! ID: {result.Key.Id}",
                data = result
            });
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância ou mensagem não encontrada: {InstanceName}, ID da mensagem: {MessageId}", instanceName, messageId);
            return Json(new { success = false, message = $"Instância '{instanceName}' ou mensagem com ID '{messageId}' não encontrada." });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Dados inválidos para atualização da mensagem: {InstanceName}, ID da mensagem: {MessageId}, Erro: {Error}", instanceName, messageId, ex.Message);
            return Json(new { success = false, message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar mensagem para a instância: {InstanceName}, ID da mensagem: {MessageId}", instanceName, messageId);
            return Json(new { success = false, message = "Erro interno do servidor. Tente novamente." });
        }
    }


}