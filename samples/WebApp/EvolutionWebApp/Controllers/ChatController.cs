using Microsoft.AspNetCore.Mvc;
using Evolution.Client.CSharp;
using Evolution.Client.CSharp.Models.Chat;
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
    /// Verifica se os números fornecidos existem no WhatsApp.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="numbers">Os números a serem verificados (separados por vírgula ou quebra de linha).</param>
    /// <returns>Um JSON com o resultado da verificação.</returns>
    [HttpPost]
    public async Task<IActionResult> CheckWhatsApp(string instanceName, string numbers)
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
    /// Busca contatos da instância especificada.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="searchId">ID do contato para buscar (opcional).</param>
    /// <param name="searchRemoteJid">Remote JID do contato para buscar (opcional).</param>
    /// <param name="searchPushName">Push Name do contato para buscar (opcional).</param>
    /// <returns>Um JSON com a lista de contatos encontrados.</returns>
    [HttpPost]
    public async Task<IActionResult> FindContacts(string instanceName, string? searchId = null, string? searchRemoteJid = null, string? searchPushName = null)
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
}