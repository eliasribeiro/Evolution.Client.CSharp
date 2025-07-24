using Microsoft.AspNetCore.Mvc;
using Evolution.Client.CSharp.WebApp.Models;
using Evolution.Client.CSharp.Models.Message;
using Evolution.Client.CSharp.Interfaces;
using System.Text.Json;

namespace Evolution.Client.CSharp.WebApp.Controllers;

/// <summary>
/// Controller para operações relacionadas a mensagens.
/// </summary>
public class MessageController : Controller
{
    private readonly IEvolutionApiClient _evolutionClient;
    private readonly ILogger<MessageController> _logger;

    /// <summary>
    /// Inicializa uma nova instância do controller de mensagens.
    /// </summary>
    /// <param name="evolutionClient">Cliente da API Evolution.</param>
    /// <param name="logger">Logger para registrar operações.</param>
    public MessageController(IEvolutionApiClient evolutionClient, ILogger<MessageController> logger)
    {
        _evolutionClient = evolutionClient ?? throw new ArgumentNullException(nameof(evolutionClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Send Text

    /// <summary>
    /// Exibe a página para envio de mensagens de texto.
    /// </summary>
    /// <returns>A view para envio de mensagens de texto.</returns>
    [HttpGet]
    public IActionResult SendText()
    {
        return View(new SendTextMessageViewModel());
    }

    /// <summary>
    /// Processa o envio de uma mensagem de texto.
    /// </summary>
    /// <param name="model">O modelo com os dados da mensagem.</param>
    /// <returns>A view com o resultado da operação.</returns>
    [HttpPost]
    public async Task<IActionResult> SendText(SendTextMessageViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var request = new SendTextRequest
            {
                Number = model.Number,
                Text = model.Text,
                Delay = model.Delay,
                LinkPreview = model.LinkPreview
            };

            // Processar usuários mencionados
            if (!string.IsNullOrWhiteSpace(model.MentionedText))
            {
                request.Mentioned = model.MentionedText
                    .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                    .Select(m => m.Trim())
                    .Where(m => !string.IsNullOrWhiteSpace(m))
                    .ToList();
            }

            if (model.MentionsEveryOne.HasValue)
            {
                request.MentionsEveryOne = model.MentionsEveryOne.Value;
            }

            // Processar mensagem citada
            if (!string.IsNullOrWhiteSpace(model.QuotedMessageId))
            {
                request.Quoted = new QuotedMessage
                {
                    Key = new QuotedMessageKey
                    {
                        Id = model.QuotedMessageId
                    }
                };
            }

            var response = await _evolutionClient.Message.SendTextAsync(model.InstanceName, request);

            model.Result = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            model.HasError = false;

            _logger.LogInformation("Mensagem de texto enviada com sucesso. Instância: {InstanceName}, Destinatário: {Number}", 
                model.InstanceName, model.Number);
        }
        catch (Exception ex)
        {
            model.Result = $"Erro ao enviar mensagem: {ex.Message}";
            model.HasError = true;

            _logger.LogError(ex, "Erro ao enviar mensagem de texto. Instância: {InstanceName}, Destinatário: {Number}", 
                model.InstanceName, model.Number);
        }

        return View(model);
    }

    #endregion

    #region Send Status

    /// <summary>
    /// Exibe a página para envio de status.
    /// </summary>
    /// <returns>A view para envio de status.</returns>
    [HttpGet]
    public IActionResult SendStatus()
    {
        return View(new SendStatusViewModel());
    }

    /// <summary>
    /// Processa o envio de um status.
    /// </summary>
    /// <param name="model">O modelo com os dados do status.</param>
    /// <returns>A view com o resultado da operação.</returns>
    [HttpPost]
    public async Task<IActionResult> SendStatus(SendStatusViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var request = new SendStatusRequest
            {
                Type = model.Type,
                Content = model.Content,
                Caption = model.Caption,
                BackgroundColor = model.BackgroundColor,
                Font = model.Font,
                AllContacts = model.AllContacts
            };

            // Processar lista de contatos
            if (!model.AllContacts && !string.IsNullOrWhiteSpace(model.StatusJidListText))
            {
                request.StatusJidList = model.StatusJidListText
                    .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.Trim())
                    .Where(c => !string.IsNullOrWhiteSpace(c))
                    .ToList();
            }

            var response = await _evolutionClient.Message.SendStatusAsync(model.InstanceName, request);

            model.Result = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            model.HasError = false;

            _logger.LogInformation("Status enviado com sucesso. Instância: {InstanceName}, Tipo: {Type}", 
                model.InstanceName, model.Type);
        }
        catch (Exception ex)
        {
            model.Result = $"Erro ao enviar status: {ex.Message}";
            model.HasError = true;

            _logger.LogError(ex, "Erro ao enviar status. Instância: {InstanceName}, Tipo: {Type}", 
                model.InstanceName, model.Type);
        }

        return View(model);
    }

    #endregion

    #region Send Media

    /// <summary>
    /// Exibe a página para envio de mídia.
    /// </summary>
    /// <returns>A view para envio de mídia.</returns>
    [HttpGet]
    public IActionResult SendMedia()
    {
        return View(new SendMediaViewModel());
    }

    /// <summary>
    /// Processa o envio de mídia.
    /// </summary>
    /// <param name="model">O modelo com os dados da mídia.</param>
    /// <returns>A view com o resultado da operação.</returns>
    [HttpPost]
    public async Task<IActionResult> SendMedia(SendMediaViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var request = new SendMediaRequest
            {
                Number = model.Number,
                MediaType = model.MediaType,
                MimeType = model.MimeType,
                Caption = model.Caption,
                Media = model.Media,
                FileName = model.FileName,
                Delay = model.Delay,
                LinkPreview = model.LinkPreview,
                MentionsEveryOne = model.MentionsEveryOne
            };

            // Processar usuários mencionados
            if (!string.IsNullOrWhiteSpace(model.MentionedText))
            {
                request.Mentioned = model.MentionedText
                    .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                    .Select(m => m.Trim())
                    .Where(m => !string.IsNullOrWhiteSpace(m))
                    .ToList();
            }

            // Processar mensagem citada
            if (!string.IsNullOrWhiteSpace(model.QuotedMessageId))
            {
                request.Quoted = new QuotedMessage
                {
                    Key = new QuotedMessageKey
                    {
                        Id = model.QuotedMessageId
                    }
                };
            }

            var response = await _evolutionClient.Message.SendMediaAsync(model.InstanceName, request);

            model.Result = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            model.HasError = false;

            _logger.LogInformation("Mídia enviada com sucesso. Instância: {InstanceName}, Destinatário: {Number}, Tipo: {MediaType}", 
                model.InstanceName, model.Number, model.MediaType);
        }
        catch (Exception ex)
        {
            model.Result = $"Erro ao enviar mídia: {ex.Message}";
            model.HasError = true;

            _logger.LogError(ex, "Erro ao enviar mídia. Instância: {InstanceName}, Destinatário: {Number}, Tipo: {MediaType}", 
                model.InstanceName, model.Number, model.MediaType);
        }

        return View(model);
    }

    #endregion
}