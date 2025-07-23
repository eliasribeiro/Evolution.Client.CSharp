using Evolution.Client.CSharp;
using Evolution.Client.CSharp.Models.Message;
using EvolutionWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EvolutionWebApp.Controllers;

/// <summary>
/// Controller para operações relacionadas a mensagens.
/// </summary>
public class MessageController : Controller
{
    private readonly EvolutionApiClient _evolutionClient;
    private readonly ILogger<MessageController> _logger;

    /// <summary>
    /// Inicializa uma nova instância do controller de mensagens.
    /// </summary>
    /// <param name="evolutionClient">Cliente da API Evolution.</param>
    /// <param name="logger">Logger para registrar operações.</param>
    public MessageController(EvolutionApiClient evolutionClient, ILogger<MessageController> logger)
    {
        _evolutionClient = evolutionClient ?? throw new ArgumentNullException(nameof(evolutionClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Exibe a página principal de mensagens.
    /// </summary>
    /// <returns>A view da página principal de mensagens.</returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Exibe a página de Enviar Mensagem de Texto.
    /// </summary>
    /// <returns>A view da página de Enviar Mensagem de Texto.</returns>
    public IActionResult SendText()
    {
        return View(new SendTextMessageViewModel());
    }

    /// <summary>
    /// Envia uma mensagem de texto usando formulário ASP.NET MVC tradicional.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="number">Número do destinatário.</param>
    /// <param name="text">Texto da mensagem.</param>
    /// <param name="delay">Atraso em milissegundos (opcional).</param>
    /// <param name="linkPreview">Indica se deve exibir preview de links.</param>
    /// <param name="mentionsEveryOne">Indica se deve mencionar todos.</param>
    /// <param name="mentioned">Lista de usuários mencionados (separados por vírgula).</param>
    /// <param name="quotedMessageId">ID da mensagem citada (opcional).</param>
    /// <param name="quotedMessageText">Texto da mensagem citada (opcional).</param>
    /// <returns>A view com o resultado do envio.</returns>
    [HttpPost]
    public async Task<IActionResult> SendText(
        string instanceName, 
        string number, 
        string text, 
        int? delay = null,
        bool linkPreview = false,
        bool mentionsEveryOne = false,
        string? mentioned = null,
        string? quotedMessageId = null,
        string? quotedMessageText = null)
    {
        var viewModel = new SendTextMessageViewModel();
        
        try
        {
            // Preserva os valores do formulário
            ViewBag.InstanceName = instanceName;
            ViewBag.Number = number;
            ViewBag.Text = text;
            ViewBag.Delay = delay;
            ViewBag.LinkPreview = linkPreview;
            ViewBag.MentionsEveryOne = mentionsEveryOne;
            ViewBag.Mentioned = mentioned;
            ViewBag.QuotedMessageId = quotedMessageId;
            ViewBag.QuotedMessageText = quotedMessageText;

            if (string.IsNullOrWhiteSpace(instanceName))
            {
                TempData["ErrorMessage"] = "Nome da instância é obrigatório.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(number))
            {
                TempData["ErrorMessage"] = "Número do destinatário é obrigatório.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                TempData["ErrorMessage"] = "Texto da mensagem é obrigatório.";
                return View(viewModel);
            }

            var request = new SendTextRequest
            {
                Number = number.Trim(),
                Text = text.Trim(),
                Delay = delay,
                LinkPreview = linkPreview,
                MentionsEveryOne = mentionsEveryOne
            };

            // Processa usuários mencionados
            if (!string.IsNullOrWhiteSpace(mentioned))
            {
                request.Mentioned = mentioned.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(m => m.Trim())
                    .Where(m => !string.IsNullOrEmpty(m))
                    .ToList();
            }

            // Processa mensagem citada
            if (!string.IsNullOrWhiteSpace(quotedMessageId))
            {
                request.Quoted = new QuotedMessage
                {
                    Key = new QuotedMessageKey { Id = quotedMessageId.Trim() },
                    Message = new QuotedMessageContent 
                    { 
                        Conversation = quotedMessageText?.Trim() ?? string.Empty 
                    }
                };
            }

            _logger.LogInformation("Enviando mensagem de texto. Instância: {InstanceName}, Destinatário: {Number}", 
                instanceName, number);

            var result = await _evolutionClient.Message.SendTextAsync(instanceName, request);

            // Mapeia o resultado para o ViewModel
            viewModel.Result = new SendTextResult
            {
                MessageId = result.Key.Id,
                RemoteJid = result.Key.RemoteJid,
                FromMe = result.Key.FromMe,
                MessageText = result.Message.ExtendedTextMessage?.Text ?? result.Message.Conversation ?? text,
                MessageTimestamp = result.MessageTimestamp,
                Status = result.Status
            };

            viewModel.InstanceName = instanceName;
            viewModel.Number = number;
            viewModel.Text = text;
            viewModel.Delay = delay;
            viewModel.LinkPreview = linkPreview;
            viewModel.MentionsEveryOne = mentionsEveryOne;
            viewModel.Mentioned = mentioned;
            viewModel.QuotedMessageId = quotedMessageId;
            viewModel.QuotedMessageText = quotedMessageText;

            _logger.LogInformation("Mensagem de texto enviada com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                instanceName, result.Key.Id, result.Status);

            TempData["SuccessMessage"] = $"Mensagem enviada com sucesso! ID: {result.Key.Id}, Status: {result.Status}";
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância não encontrada ou número inválido: {InstanceName}, {Number}", instanceName, number);
            TempData["ErrorMessage"] = $"Instância '{instanceName}' não encontrada ou número '{number}' inválido.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar mensagem de texto. Instância: {InstanceName}, Destinatário: {Number}", 
                instanceName, number);
            TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
        }

        return View(viewModel);
    }
}
