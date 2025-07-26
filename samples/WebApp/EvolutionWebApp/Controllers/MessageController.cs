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
                MessageTimestamp = result.MessageTimestamp.ToString(),
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

    /// <summary>
    /// Exibe a página de Enviar Áudio.
    /// </summary>
    /// <returns>A view da página de Enviar Áudio.</returns>
    public IActionResult SendAudio()
    {
        return View(new SendAudioMessageViewModel());
    }

    /// <summary>
    /// Envia um áudio usando formulário ASP.NET MVC tradicional.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="number">Número do destinatário.</param>
    /// <param name="audio">URL ou base64 do áudio.</param>
    /// <param name="delay">Atraso em milissegundos (opcional).</param>
    /// <param name="linkPreview">Indica se deve exibir preview de links.</param>
    /// <param name="mentionsEveryOne">Indica se deve mencionar todos.</param>
    /// <param name="mentioned">Lista de usuários mencionados (separados por vírgula).</param>
    /// <param name="quotedMessageId">ID da mensagem citada (opcional).</param>
    /// <param name="quotedMessageText">Texto da mensagem citada (opcional).</param>
    /// <returns>A view com o resultado do envio.</returns>
    [HttpPost]
    public async Task<IActionResult> SendAudio(
        string instanceName, 
        string number, 
        string audio, 
        int? delay = null,
        bool? linkPreview = null,
        bool? mentionsEveryOne = null,
        string? mentioned = null,
        string? quotedMessageId = null,
        string? quotedMessageText = null)
    {
        var viewModel = new SendAudioMessageViewModel();
        
        try
        {
            // Preserva os valores do formulário
            ViewBag.InstanceName = instanceName;
            ViewBag.Number = number;
            ViewBag.Audio = audio;
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

            if (string.IsNullOrWhiteSpace(audio))
            {
                TempData["ErrorMessage"] = "URL ou base64 do áudio é obrigatório.";
                return View(viewModel);
            }

            var request = new SendAudioRequest
            {
                Number = number.Trim(),
                Audio = audio.Trim(),
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

            _logger.LogInformation("Enviando áudio. Instância: {InstanceName}, Destinatário: {Number}", 
                instanceName, number);

            var result = await _evolutionClient.Message.SendAudioAsync(instanceName, request);

            // Mapeia o resultado para o ViewModel
            viewModel.Result = new SendAudioResult
            {
                MessageId = result.Key.Id,
                RemoteJid = result.Key.RemoteJid,
                FromMe = result.Key.FromMe,
                MessageTimestamp = result.MessageTimestamp.ToString(),
                Status = result.Status
            };

            viewModel.InstanceName = instanceName;
            viewModel.Number = number;
            viewModel.Audio = audio;
            viewModel.Delay = delay;
            viewModel.LinkPreview = linkPreview;
            viewModel.MentionsEveryOne = mentionsEveryOne;
            viewModel.Mentioned = mentioned;
            viewModel.QuotedMessageId = quotedMessageId;
            viewModel.QuotedMessageText = quotedMessageText;

            _logger.LogInformation("Áudio enviado com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                instanceName, result.Key.Id, result.Status);

            TempData["SuccessMessage"] = $"Áudio enviado com sucesso! ID: {result.Key.Id}, Status: {result.Status}";
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância não encontrada ou número inválido: {InstanceName}, {Number}", instanceName, number);
            TempData["ErrorMessage"] = $"Instância '{instanceName}' não encontrada ou número '{number}' inválido.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar áudio. Instância: {InstanceName}, Destinatário: {Number}", 
                instanceName, number);
            TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
        }

        return View(viewModel);
    }

    /// <summary>
    /// Exibe a página de Enviar Sticker.
    /// </summary>
    /// <returns>A view da página de Enviar Sticker.</returns>
    public IActionResult SendSticker()
    {
        return View(new SendStickerMessageViewModel());
    }

    /// <summary>
    /// Envia um sticker usando formulário ASP.NET MVC tradicional.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="number">Número do destinatário.</param>
    /// <param name="sticker">URL ou base64 do sticker.</param>
    /// <param name="delay">Atraso em milissegundos (opcional).</param>
    /// <param name="linkPreview">Indica se deve exibir preview de links.</param>
    /// <param name="mentionsEveryOne">Indica se deve mencionar todos.</param>
    /// <param name="mentioned">Lista de usuários mencionados (separados por vírgula).</param>
    /// <param name="quotedMessageId">ID da mensagem citada (opcional).</param>
    /// <param name="quotedMessageText">Texto da mensagem citada (opcional).</param>
    /// <returns>A view com o resultado do envio.</returns>
    [HttpPost]
    public async Task<IActionResult> SendSticker(
        string instanceName, 
        string number, 
        string sticker, 
        int? delay = null,
        bool linkPreview = false,
        bool mentionsEveryOne = false,
        string? mentioned = null,
        string? quotedMessageId = null,
        string? quotedMessageText = null)
    {
        var viewModel = new SendStickerMessageViewModel();
        
        try
        {
            // Preserva os valores do formulário
            ViewBag.InstanceName = instanceName;
            ViewBag.Number = number;
            ViewBag.Sticker = sticker;
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

            if (string.IsNullOrWhiteSpace(sticker))
            {
                TempData["ErrorMessage"] = "URL ou base64 do sticker é obrigatório.";
                return View(viewModel);
            }

            var request = new SendStickerRequest
            {
                Number = number.Trim(),
                Sticker = sticker.Trim(),
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

            _logger.LogInformation("Enviando sticker. Instância: {InstanceName}, Destinatário: {Number}", 
                instanceName, number);

            var result = await _evolutionClient.Message.SendStickerAsync(instanceName, request);

            // Mapeia o resultado para o ViewModel
            viewModel.Result = new SendStickerResult
            {
                MessageId = result.Key.Id,
                RemoteJid = result.Key.RemoteJid,
                FromMe = result.Key.FromMe,
                MessageTimestamp = result.MessageTimestamp.ToString(),
                Status = result.Status
            };

            viewModel.InstanceName = instanceName;
            viewModel.Number = number;
            viewModel.Sticker = sticker;
            viewModel.Delay = delay;
            viewModel.LinkPreview = linkPreview;
            viewModel.MentionsEveryOne = mentionsEveryOne;
            viewModel.Mentioned = mentioned;
            viewModel.QuotedMessageId = quotedMessageId;
            viewModel.QuotedMessageText = quotedMessageText;

            _logger.LogInformation("Sticker enviado com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                instanceName, result.Key.Id, result.Status);

            TempData["SuccessMessage"] = $"Sticker enviado com sucesso! ID: {result.Key.Id}, Status: {result.Status}";
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância não encontrada ou número inválido: {InstanceName}, {Number}", instanceName, number);
            TempData["ErrorMessage"] = $"Instância '{instanceName}' não encontrada ou número '{number}' inválido.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar sticker. Instância: {InstanceName}, Destinatário: {Number}", 
                instanceName, number);
            TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
        }

        return View(viewModel);
    }

    /// <summary>
    /// Exibe a página de Enviar Localização.
    /// </summary>
    /// <returns>A view da página de Enviar Localização.</returns>
    public IActionResult SendLocation()
    {
        return View(new SendLocationMessageViewModel());
    }

    /// <summary>
    /// Envia uma localização usando formulário ASP.NET MVC tradicional.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="number">Número do destinatário.</param>
    /// <param name="name">Nome da localização.</param>
    /// <param name="address">Endereço da localização.</param>
    /// <param name="latitude">Latitude da localização.</param>
    /// <param name="longitude">Longitude da localização.</param>
    /// <param name="delay">Atraso em milissegundos (opcional).</param>
    /// <param name="linkPreview">Indica se deve exibir preview de links.</param>
    /// <param name="mentionsEveryOne">Indica se deve mencionar todos.</param>
    /// <param name="mentioned">Lista de usuários mencionados (separados por vírgula).</param>
    /// <param name="quotedMessageId">ID da mensagem citada (opcional).</param>
    /// <param name="quotedMessageText">Texto da mensagem citada (opcional).</param>
    /// <returns>A view com o resultado do envio.</returns>
    [HttpPost]
    public async Task<IActionResult> SendLocation(
        string instanceName, 
        string number, 
        string name,
        string address,
        double latitude,
        double longitude,
        int? delay = null,
        bool linkPreview = false,
        bool mentionsEveryOne = false,
        string? mentioned = null,
        string? quotedMessageId = null,
        string? quotedMessageText = null)
    {
        var viewModel = new SendLocationMessageViewModel();
        
        try
        {
            // Preserva os valores do formulário
            ViewBag.InstanceName = instanceName;
            ViewBag.Number = number;
            ViewBag.Name = name;
            ViewBag.Address = address;
            ViewBag.Latitude = latitude;
            ViewBag.Longitude = longitude;
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

            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["ErrorMessage"] = "Nome da localização é obrigatório.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                TempData["ErrorMessage"] = "Endereço da localização é obrigatório.";
                return View(viewModel);
            }

            if (latitude == 0)
            {
                TempData["ErrorMessage"] = "Latitude da localização é obrigatória.";
                return View(viewModel);
            }

            if (longitude == 0)
            {
                TempData["ErrorMessage"] = "Longitude da localização é obrigatória.";
                return View(viewModel);
            }

            var request = new SendLocationRequest
            {
                Number = number.Trim(),
                Name = name.Trim(),
                Address = address.Trim(),
                Latitude = latitude,
                Longitude = longitude,
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

            _logger.LogInformation("Enviando localização. Instância: {InstanceName}, Destinatário: {Number}, Local: {Name}", 
                instanceName, number, name);

            var result = await _evolutionClient.Message.SendLocationAsync(instanceName, request);

            // Mapeia o resultado para o ViewModel
            viewModel.Result = new SendLocationResult
            {
                MessageId = result.Key.Id,
                RemoteJid = result.Key.RemoteJid,
                FromMe = result.Key.FromMe,
                MessageTimestamp = result.MessageTimestamp.ToString(),
                Status = result.Status
            };

            viewModel.InstanceName = instanceName;
            viewModel.Number = number;
            viewModel.Name = name;
            viewModel.Address = address;
            viewModel.Latitude = latitude;
            viewModel.Longitude = longitude;
            viewModel.Delay = delay;
            viewModel.LinkPreview = linkPreview;
            viewModel.MentionsEveryOne = mentionsEveryOne;
            viewModel.Mentioned = mentioned;
            viewModel.QuotedMessageId = quotedMessageId;
            viewModel.QuotedMessageText = quotedMessageText;

            _logger.LogInformation("Localização enviada com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                instanceName, result.Key.Id, result.Status);

            TempData["SuccessMessage"] = $"Localização enviada com sucesso! ID: {result.Key.Id}, Status: {result.Status}";
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância não encontrada ou número inválido: {InstanceName}, {Number}", instanceName, number);
            TempData["ErrorMessage"] = $"Instância '{instanceName}' não encontrada ou número '{number}' inválido.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar localização. Instância: {InstanceName}, Destinatário: {Number}, Local: {Name}", 
                instanceName, number, name);
            TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
        }

        return View(viewModel);
    }

    /// <summary>
    /// Exibe a página de Enviar Contato.
    /// </summary>
    /// <returns>A view da página de Enviar Contato.</returns>
    public IActionResult SendContact()
    {
        return View(new SendContactMessageViewModel());
    }

    /// <summary>
    /// Envia um contato usando formulário ASP.NET MVC tradicional.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> SendContact(
        string instanceName, 
        string number, 
        string fullName,
        string? organization,
        string phoneNumber,
        string? secondaryPhoneNumber,
        string? email,
        string? secondaryEmail,
        string? url,
        int? delay = null,
        bool linkPreview = false,
        bool mentionsEveryOne = false,
        string? mentioned = null,
        string? quotedMessageId = null,
        string? quotedMessageText = null)
    {
        var viewModel = new SendContactMessageViewModel();
        
        try
        {
            // Preserva os valores do formulário
            ViewBag.InstanceName = instanceName;
            ViewBag.Number = number;
            ViewBag.FullName = fullName;
            ViewBag.Organization = organization;
            ViewBag.PhoneNumber = phoneNumber;
            ViewBag.SecondaryPhoneNumber = secondaryPhoneNumber;
            ViewBag.Email = email;
            ViewBag.SecondaryEmail = secondaryEmail;
            ViewBag.Url = url;
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

            if (string.IsNullOrWhiteSpace(fullName))
            {
                TempData["ErrorMessage"] = "Nome completo é obrigatório.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                TempData["ErrorMessage"] = "Telefone é obrigatório.";
                return View(viewModel);
            }

            var contact = new ContactInfo
            {
                FullName = fullName.Trim(),
                Organization = organization?.Trim(),
                PhoneNumber = new List<Evolution.Client.CSharp.Models.Message.PhoneNumber> { new Evolution.Client.CSharp.Models.Message.PhoneNumber { Number = phoneNumber.Trim() } },
                Email = new List<EmailInfo>(),
                Url = new List<UrlInfo>()
            };

            if (!string.IsNullOrWhiteSpace(secondaryPhoneNumber))
            {
                contact.PhoneNumber.Add(new Evolution.Client.CSharp.Models.Message.PhoneNumber { Number = secondaryPhoneNumber.Trim() });
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                contact.Email.Add(new EmailInfo { Email = email.Trim() });
            }

            if (!string.IsNullOrWhiteSpace(secondaryEmail))
            {
                contact.Email.Add(new EmailInfo { Email = secondaryEmail.Trim() });
            }

            if (!string.IsNullOrWhiteSpace(url))
            {
                contact.Url.Add(new UrlInfo { Url = url.Trim() });
            }

            var request = new SendContactRequest
            {
                Number = number.Trim(),
                Contact = new List<ContactInfo> { contact },
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

            _logger.LogInformation("Enviando contato. Instância: {InstanceName}, Destinatário: {Number}, Contato: {FullName}", 
                instanceName, number, fullName);

            var result = await _evolutionClient.Message.SendContactAsync(instanceName, request);

            // Mapeia o resultado para o ViewModel
            viewModel.Result = new SendContactResult
            {
                MessageId = result.Key.Id,
                RemoteJid = result.Key.RemoteJid,
                FromMe = result.Key.FromMe,
                MessageTimestamp = long.TryParse(result.MessageTimestamp, out var contactTimestamp) ? contactTimestamp : 0,
                Status = result.Status
            };

            viewModel.InstanceName = instanceName;
            viewModel.Number = number;
            viewModel.FullName = fullName;
            viewModel.Organization = organization;
            viewModel.PhoneNumber = phoneNumber;
            viewModel.SecondaryPhoneNumber = secondaryPhoneNumber;
            viewModel.Email = email;
            viewModel.SecondaryEmail = secondaryEmail;
            viewModel.Url = url;
            viewModel.Delay = delay;
            viewModel.LinkPreview = linkPreview;
            viewModel.MentionsEveryOne = mentionsEveryOne;
            viewModel.Mentioned = mentioned;
            viewModel.QuotedMessageId = quotedMessageId;
            viewModel.QuotedMessageText = quotedMessageText;

            _logger.LogInformation("Contato enviado com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                instanceName, result.Key.Id, result.Status);

            TempData["SuccessMessage"] = $"Contato enviado com sucesso! ID: {result.Key.Id}, Status: {result.Status}";
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância não encontrada ou número inválido: {InstanceName}, {Number}", instanceName, number);
            TempData["ErrorMessage"] = $"Instância '{instanceName}' não encontrada ou número '{number}' inválido.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar contato. Instância: {InstanceName}, Destinatário: {Number}, Contato: {FullName}", 
                instanceName, number, fullName);
            TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
        }

        return View(viewModel);
    }

    /// <summary>
    /// Exibe a página de Enviar Reação.
    /// </summary>
    /// <returns>A view da página de Enviar Reação.</returns>
    public IActionResult SendReaction()
    {
        return View(new SendReactionMessageViewModel());
    }

    /// <summary>
    /// Envia uma reação usando formulário ASP.NET MVC tradicional.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> SendReaction(
        string instanceName, 
        string remoteJid, 
        string? fromMe,
        string messageId,
        string reaction)
    {
        var viewModel = new SendReactionMessageViewModel();
        
        try
        {
            // Preserva os valores do formulário
            ViewBag.InstanceName = instanceName;
            ViewBag.RemoteJid = remoteJid;
            ViewBag.FromMe = fromMe;
            ViewBag.MessageId = messageId;
            ViewBag.Reaction = reaction;

            if (string.IsNullOrWhiteSpace(instanceName))
            {
                TempData["ErrorMessage"] = "Nome da instância é obrigatório.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(remoteJid))
            {
                TempData["ErrorMessage"] = "JID remoto é obrigatório.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(messageId))
            {
                TempData["ErrorMessage"] = "ID da mensagem é obrigatório.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(reaction))
            {
                TempData["ErrorMessage"] = "Reação é obrigatória.";
                return View(viewModel);
            }

            var request = new SendReactionRequest
            {
                Key = new ReactionMessageKey
                {
                    RemoteJid = remoteJid.Trim(),
                    FromMe = !string.IsNullOrWhiteSpace(fromMe) && (fromMe.Trim().ToLower() == "true" || fromMe.Trim() == "1"),
                    Id = messageId.Trim()
                },
                Reaction = reaction.Trim()
            };

            _logger.LogInformation("Enviando reação. Instância: {InstanceName}, Mensagem: {MessageId}, Reação: {Reaction}", 
                instanceName, messageId, reaction);

            var result = await _evolutionClient.Message.SendReactionAsync(instanceName, request);

            // Mapeia o resultado para o ViewModel
            viewModel.Result = new SendReactionResult
            {
                MessageId = result.Key.Id,
                RemoteJid = result.Key.RemoteJid,
                FromMe = result.Key.FromMe,
                MessageTimestamp = long.TryParse(result.MessageTimestamp, out var reactionTimestamp) ? reactionTimestamp : 0,
                Status = result.Status
            };

            viewModel.InstanceName = instanceName;
            viewModel.RemoteJid = remoteJid;
            viewModel.FromMe = fromMe;
            viewModel.MessageId = messageId;
            viewModel.Reaction = reaction;

            _logger.LogInformation("Reação enviada com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                instanceName, result.Key.Id, result.Status);

            TempData["SuccessMessage"] = $"Reação enviada com sucesso! ID: {result.Key.Id}, Status: {result.Status}";
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância não encontrada ou mensagem inválida: {InstanceName}, {MessageId}", instanceName, messageId);
            TempData["ErrorMessage"] = $"Instância '{instanceName}' não encontrada ou mensagem '{messageId}' inválida.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar reação. Instância: {InstanceName}, Mensagem: {MessageId}, Reação: {Reaction}", 
                instanceName, messageId, reaction);
            TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
        }

        return View(viewModel);
    }

    /// <summary>
    /// Exibe a página de Enviar Enquete.
    /// </summary>
    /// <returns>A view da página de Enviar Enquete.</returns>
    public IActionResult SendPoll()
    {
        return View(new SendPollMessageViewModel());
    }

    /// <summary>
    /// Envia uma enquete usando formulário ASP.NET MVC tradicional.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> SendPoll(
        string instanceName, 
        string number, 
        string name,
        int selectableCount,
        string values,
        int? delay = null,
        bool linkPreview = false,
        bool mentionsEveryOne = false,
        string? mentioned = null,
        string? quotedMessageId = null,
        string? quotedMessageText = null)
    {
        var viewModel = new SendPollMessageViewModel();
        
        try
        {
            // Preserva os valores do formulário
            ViewBag.InstanceName = instanceName;
            ViewBag.Number = number;
            ViewBag.Name = name;
            ViewBag.SelectableCount = selectableCount;
            ViewBag.Values = values;
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

            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["ErrorMessage"] = "Título da enquete é obrigatório.";
                return View(viewModel);
            }

            if (selectableCount <= 0)
            {
                TempData["ErrorMessage"] = "Número de opções selecionáveis deve ser maior que zero.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(values))
            {
                TempData["ErrorMessage"] = "Opções da enquete são obrigatórias.";
                return View(viewModel);
            }

            var pollValues = values.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(v => v.Trim())
                .Where(v => !string.IsNullOrEmpty(v))
                .ToList();

            if (pollValues.Count == 0)
            {
                TempData["ErrorMessage"] = "Pelo menos uma opção é obrigatória.";
                return View(viewModel);
            }

            var request = new SendPollRequest
            {
                Number = number.Trim(),
                Name = name.Trim(),
                SelectableCount = selectableCount,
                Values = pollValues,
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

            _logger.LogInformation("Enviando enquete. Instância: {InstanceName}, Destinatário: {Number}, Título: {Name}", 
                instanceName, number, name);

            var result = await _evolutionClient.Message.SendPollAsync(instanceName, request);

            // Mapeia o resultado para o ViewModel
            viewModel.Result = new SendPollResult
            {
                MessageId = result.Key.Id,
                RemoteJid = result.Key.RemoteJid,
                FromMe = result.Key.FromMe,
                MessageTimestamp = long.TryParse(result.MessageTimestamp, out var pollTimestamp) ? pollTimestamp : 0,
                Status = result.Status
            };

            viewModel.InstanceName = instanceName;
            viewModel.Number = number;
            viewModel.Name = name;
            viewModel.SelectableCount = selectableCount;
            viewModel.Values = values;
            viewModel.Delay = delay;
            viewModel.LinkPreview = linkPreview;
            viewModel.MentionsEveryOne = mentionsEveryOne;
            viewModel.Mentioned = mentioned;
            viewModel.QuotedMessageId = quotedMessageId;
            viewModel.QuotedMessageText = quotedMessageText;

            _logger.LogInformation("Enquete enviada com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                instanceName, result.Key.Id, result.Status);

            TempData["SuccessMessage"] = $"Enquete enviada com sucesso! ID: {result.Key.Id}, Status: {result.Status}";
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância não encontrada ou número inválido: {InstanceName}, {Number}", instanceName, number);
            TempData["ErrorMessage"] = $"Instância '{instanceName}' não encontrada ou número '{number}' inválido.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar enquete. Instância: {InstanceName}, Destinatário: {Number}, Título: {Name}", 
                instanceName, number, name);
            TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
        }

        return View(viewModel);
    }

    /// <summary>
    /// Exibe a página de Enviar Lista.
    /// </summary>
    /// <returns>A view da página de Enviar Lista.</returns>
    public IActionResult SendList()
    {
        return View(new SendListMessageViewModel());
    }

    /// <summary>
    /// Envia uma lista usando formulário ASP.NET MVC tradicional.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> SendList(
        string instanceName, 
        string number, 
        string title,
        string description,
        string buttonText,
        string footerText,
        string sections,
        int? delay = null,
        bool linkPreview = false,
        bool mentionsEveryOne = false,
        string? mentioned = null,
        string? quotedMessageId = null,
        string? quotedMessageText = null)
    {
        var viewModel = new SendListMessageViewModel();
        
        try
        {
            // Preserva os valores do formulário
            ViewBag.InstanceName = instanceName;
            ViewBag.Number = number;
            ViewBag.Title = title;
            ViewBag.Description = description;
            ViewBag.ButtonText = buttonText;
            ViewBag.FooterText = footerText;
            ViewBag.Sections = sections;
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

            if (string.IsNullOrWhiteSpace(title))
            {
                TempData["ErrorMessage"] = "Título da lista é obrigatório.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                TempData["ErrorMessage"] = "Descrição da lista é obrigatória.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(buttonText))
            {
                TempData["ErrorMessage"] = "Texto do botão é obrigatório.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(footerText))
            {
                TempData["ErrorMessage"] = "Texto do rodapé é obrigatório.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(sections))
            {
                TempData["ErrorMessage"] = "Seções da lista são obrigatórias.";
                return View(viewModel);
            }

            // Parse das seções JSON
            List<ListValue> listValues;
            try
            {
                listValues = System.Text.Json.JsonSerializer.Deserialize<List<ListValue>>(sections) ?? new List<ListValue>();
            }
            catch (System.Text.Json.JsonException)
            {
                TempData["ErrorMessage"] = "Formato JSON das seções inválido.";
                return View(viewModel);
            }

            if (listValues.Count == 0)
            {
                TempData["ErrorMessage"] = "Pelo menos uma seção é obrigatória.";
                return View(viewModel);
            }

            var request = new SendListRequest
            {
                Number = number.Trim(),
                Title = title.Trim(),
                Description = description.Trim(),
                ButtonText = buttonText.Trim(),
                FooterText = footerText.Trim(),
                Values = listValues,
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

            _logger.LogInformation("Enviando lista. Instância: {InstanceName}, Destinatário: {Number}, Título: {Title}", 
                instanceName, number, title);

            var result = await _evolutionClient.Message.SendListAsync(instanceName, request);

            // Mapeia o resultado para o ViewModel
            viewModel.Result = new SendListResult
            {
                MessageId = result.Key.Id,
                RemoteJid = result.Key.RemoteJid,
                FromMe = result.Key.FromMe,
                MessageTimestamp = long.TryParse(result.MessageTimestamp, out var listTimestamp) ? listTimestamp : 0,
                Status = result.Status
            };

            viewModel.InstanceName = instanceName;
            viewModel.Number = number;
            viewModel.Title = title;
            viewModel.Description = description;
            viewModel.ButtonText = buttonText;
            viewModel.FooterText = footerText;
            viewModel.Sections = sections;
            viewModel.Delay = delay;
            viewModel.LinkPreview = linkPreview;
            viewModel.MentionsEveryOne = mentionsEveryOne;
            viewModel.Mentioned = mentioned;
            viewModel.QuotedMessageId = quotedMessageId;
            viewModel.QuotedMessageText = quotedMessageText;

            _logger.LogInformation("Lista enviada com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                instanceName, result.Key.Id, result.Status);

            TempData["SuccessMessage"] = $"Lista enviada com sucesso! ID: {result.Key.Id}, Status: {result.Status}";
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância não encontrada ou número inválido: {InstanceName}, {Number}", instanceName, number);
            TempData["ErrorMessage"] = $"Instância '{instanceName}' não encontrada ou número '{number}' inválido.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar lista. Instância: {InstanceName}, Destinatário: {Number}, Título: {Title}", 
                instanceName, number, title);
            TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
        }

        return View(viewModel);
    }

    /// <summary>
    /// Exibe a página de Enviar Botões.
    /// </summary>
    /// <returns>A view da página de Enviar Botões.</returns>
    public IActionResult SendButton()
    {
        return View(new SendButtonMessageViewModel());
    }

    /// <summary>
    /// Envia botões usando formulário ASP.NET MVC tradicional.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> SendButton(
        string instanceName, 
        string number, 
        string title,
        string description,
        string footer,
        string buttons,
        int? delay = null,
        bool linkPreview = false,
        bool mentionsEveryOne = false,
        string? mentioned = null,
        string? quotedMessageId = null,
        string? quotedMessageText = null)
    {
        var viewModel = new SendButtonMessageViewModel();
        
        try
        {
            // Preserva os valores do formulário
            ViewBag.InstanceName = instanceName;
            ViewBag.Number = number;
            ViewBag.Title = title;
            ViewBag.Description = description;
            ViewBag.Footer = footer;
            ViewBag.Buttons = buttons;
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

            if (string.IsNullOrWhiteSpace(title))
            {
                TempData["ErrorMessage"] = "Título é obrigatório.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                TempData["ErrorMessage"] = "Descrição é obrigatória.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(footer))
            {
                TempData["ErrorMessage"] = "Rodapé é obrigatório.";
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(buttons))
            {
                TempData["ErrorMessage"] = "Botões são obrigatórios.";
                return View(viewModel);
            }

            // Parse dos botões JSON
            List<ButtonInfo> buttonList;
            try
            {
                buttonList = System.Text.Json.JsonSerializer.Deserialize<List<ButtonInfo>>(buttons) ?? new List<ButtonInfo>();
            }
            catch (System.Text.Json.JsonException)
            {
                TempData["ErrorMessage"] = "Formato JSON dos botões inválido.";
                return View(viewModel);
            }

            if (buttonList.Count == 0)
            {
                TempData["ErrorMessage"] = "Pelo menos um botão é obrigatório.";
                return View(viewModel);
            }

            var request = new SendButtonRequest
            {
                Number = number.Trim(),
                Title = title.Trim(),
                Description = description.Trim(),
                Footer = footer.Trim(),
                Buttons = buttonList,
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

            _logger.LogInformation("Enviando botões. Instância: {InstanceName}, Destinatário: {Number}, Título: {Title}", 
                instanceName, number, title);

            var result = await _evolutionClient.Message.SendButtonAsync(instanceName, request);

            // Mapeia o resultado para o ViewModel
            viewModel.Result = new SendButtonResult
            {
                MessageId = result.Key.Id,
                RemoteJid = result.Key.RemoteJid,
                FromMe = result.Key.FromMe,
                MessageTimestamp = long.TryParse(result.MessageTimestamp, out var buttonTimestamp) ? buttonTimestamp : 0,
                Status = result.Status
            };

            viewModel.InstanceName = instanceName;
            viewModel.Number = number;
            viewModel.Title = title;
            viewModel.Description = description;
            viewModel.Footer = footer;
            viewModel.Buttons = buttons;
            viewModel.Delay = delay;
            viewModel.LinkPreview = linkPreview;
            viewModel.MentionsEveryOne = mentionsEveryOne;
            viewModel.Mentioned = mentioned;
            viewModel.QuotedMessageId = quotedMessageId;
            viewModel.QuotedMessageText = quotedMessageText;

            _logger.LogInformation("Botões enviados com sucesso. Instância: {InstanceName}, ID da mensagem: {MessageId}, Status: {Status}", 
                instanceName, result.Key.Id, result.Status);

            TempData["SuccessMessage"] = $"Botões enviados com sucesso! ID: {result.Key.Id}, Status: {result.Status}";
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("não encontrada"))
        {
            _logger.LogWarning("Instância não encontrada ou número inválido: {InstanceName}, {Number}", instanceName, number);
            TempData["ErrorMessage"] = $"Instância '{instanceName}' não encontrada ou número '{number}' inválido.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar botões. Instância: {InstanceName}, Destinatário: {Number}, Título: {Title}", 
                instanceName, number, title);
            TempData["ErrorMessage"] = "Erro interno do servidor. Tente novamente.";
        }

        return View(viewModel);
    }
}
