using Evolution.Client;
using Evolution.Client.Modules;

namespace Evolution.Client.Examples;

/// <summary>
/// Exemplos de uso das funcionalidades de envio de mensagens
/// </summary>
public class MessageExamples
{
    private readonly EvolutionClient _client;

    public MessageExamples()
    {
        var options = new EvolutionClientOptions
        {
            BaseUrl = "https://your-evolution-api-url.com",
            ApiKey = "your-api-key"
        };
        _client = new EvolutionClient(options);
    }

    /// <summary>
    /// Exemplo de envio de mensagem de texto
    /// </summary>
    public async Task SendTextMessageExample()
    {
        var request = new SendTextMessageRequest
        {
            Number = "5511999999999",
            Text = "Olá! Esta é uma mensagem de texto.",
            LinkPreview = true,
            Delay = 1000 // 1 segundo de delay
        };

        var response = await _client.Messages.SendTextAsync("instance-name", request);
        Console.WriteLine($"Mensagem enviada com ID: {response.MessageId}");
    }

    /// <summary>
    /// Exemplo de envio de mídia
    /// </summary>
    public async Task SendMediaMessageExample()
    {
        var request = new SendMediaMessageRequest
        {
            Number = "5511999999999",
            MediaType = "image",
            MimeType = "image/jpeg",
            Media = "https://example.com/image.jpg",
            Caption = "Confira esta imagem!",
            FileName = "exemplo.jpg"
        };

        var response = await _client.Messages.SendMediaAsync("instance-name", request);
        Console.WriteLine($"Mídia enviada com ID: {response.MessageId}");
    }

    /// <summary>
    /// Exemplo de envio de áudio
    /// </summary>
    public async Task SendAudioMessageExample()
    {
        var request = new SendAudioMessageRequest
        {
            Number = "5511999999999",
            Audio = "https://example.com/audio.mp3"
        };

        var response = await _client.Messages.SendAudioAsync("instance-name", request);
        Console.WriteLine($"Áudio enviado com ID: {response.MessageId}");
    }

    /// <summary>
    /// Exemplo de envio de localização
    /// </summary>
    public async Task SendLocationMessageExample()
    {
        var request = new SendLocationMessageRequest
        {
            Number = "5511999999999",
            Latitude = -23.5505,
            Longitude = -46.6333,
            Name = "São Paulo",
            Address = "São Paulo, SP, Brasil"
        };

        var response = await _client.Messages.SendLocationAsync("instance-name", request);
        Console.WriteLine($"Localização enviada com ID: {response.MessageId}");
    }

    /// <summary>
    /// Exemplo de envio de status
    /// </summary>
    public async Task SendStatusMessageExample()
    {
        var request = new SendStatusMessageRequest
        {
            Type = "text",
            Content = "Meu status personalizado!",
            BackgroundColor = "#FF5722",
            FontColor = "#FFFFFF"
        };

        var response = await _client.Messages.SendStatusAsync("instance-name", request);
        Console.WriteLine($"Status enviado com ID: {response.MessageId}");
    }

    /// <summary>
    /// Exemplo de envio de sticker
    /// </summary>
    public async Task SendStickerMessageExample()
    {
        var request = new SendStickerMessageRequest
        {
            Number = "5511999999999",
            Sticker = "https://example.com/sticker.webp"
        };

        var response = await _client.Messages.SendStickerAsync("instance-name", request);
        Console.WriteLine($"Sticker enviado com ID: {response.MessageId}");
    }

    /// <summary>
    /// Exemplo de envio de contato
    /// </summary>
    public async Task SendContactMessageExample()
    {
        var request = new SendContactMessageRequest
        {
            Number = "5511999999999",
            Contacts = new[]
            {
                new MessageContactInfo
                {
                    FullName = "João Silva",
                    Organization = "Empresa XYZ",
                    PhoneNumbers = new[]
                    {
                        new PhoneNumber { Number = "5511888888888", Type = "MOBILE" },
                        new PhoneNumber { Number = "1133334444", Type = "WORK" }
                    },
                    Emails = new[]
                    {
                        new EmailAddress { Email = "joao@empresa.com", Type = "WORK" }
                    }
                }
            }
        };

        var response = await _client.Messages.SendContactAsync("instance-name", request);
        Console.WriteLine($"Contato enviado com ID: {response.MessageId}");
    }

    /// <summary>
    /// Exemplo de envio de reação
    /// </summary>
    public async Task SendReactionMessageExample()
    {
        var request = new SendReactionMessageRequest
        {
            Number = "5511999999999",
            Key = new MessageKey
            {
                Id = "BAE5F5A632EAE722", // ID da mensagem para reagir
                FromMe = false
            },
            Reaction = "👍"
        };

        var response = await _client.Messages.SendReactionAsync("instance-name", request);
        Console.WriteLine($"Reação enviada com ID: {response.MessageId}");
    }

    /// <summary>
    /// Exemplo de envio de enquete
    /// </summary>
    public async Task SendPollMessageExample()
    {
        var request = new SendPollMessageRequest
        {
            Number = "5511999999999",
            Name = "Qual sua cor favorita?",
            Options = new[]
            {
                new PollOption { OptionName = "Azul" },
                new PollOption { OptionName = "Vermelho" },
                new PollOption { OptionName = "Verde" },
                new PollOption { OptionName = "Amarelo" }
            },
            SelectableOptionsCount = 1
        };

        var response = await _client.Messages.SendPollAsync("instance-name", request);
        Console.WriteLine($"Enquete enviada com ID: {response.MessageId}");
    }

    /// <summary>
    /// Exemplo de envio de lista
    /// </summary>
    public async Task SendListMessageExample()
    {
        var request = new SendListMessageRequest
        {
            Number = "5511999999999",
            Title = "Menu do Restaurante",
            Description = "Escolha uma opção do nosso menu",
            ButtonText = "Ver Menu",
            Footer = "Restaurante XYZ",
            Sections = new[]
            {
                new ListSection
                {
                    Title = "Pratos Principais",
                    Rows = new[]
                    {
                        new ListItem { Id = "1", Title = "Pizza Margherita", Description = "Pizza com molho de tomate, mussarela e manjericão" },
                        new ListItem { Id = "2", Title = "Hambúrguer Artesanal", Description = "Hambúrguer com carne 180g, queijo e salada" }
                    }
                },
                new ListSection
                {
                    Title = "Bebidas",
                    Rows = new[]
                    {
                        new ListItem { Id = "3", Title = "Refrigerante", Description = "Coca-Cola, Pepsi ou Guaraná" },
                        new ListItem { Id = "4", Title = "Suco Natural", Description = "Laranja, limão ou maracujá" }
                    }
                }
            }
        };

        var response = await _client.Messages.SendListAsync("instance-name", request);
        Console.WriteLine($"Lista enviada com ID: {response.MessageId}");
    }

    /// <summary>
    /// Exemplo de envio de botões
    /// </summary>
    public async Task SendButtonMessageExample()
    {
        var request = new SendButtonMessageRequest
        {
            Number = "5511999999999",
            Title = "Confirmação de Pedido",
            Description = "Deseja confirmar seu pedido?",
            Footer = "Restaurante XYZ",
            Buttons = new[]
            {
                new MessageButton { Id = "confirm", Title = "Confirmar", DisplayText = "✅ Sim, confirmar" },
                new MessageButton { Id = "cancel", Title = "Cancelar", DisplayText = "❌ Não, cancelar" },
                new MessageButton { Id = "modify", Title = "Modificar", DisplayText = "✏️ Modificar pedido" }
            }
        };

        var response = await _client.Messages.SendButtonAsync("instance-name", request);
        Console.WriteLine($"Botões enviados com ID: {response.MessageId}");
    }
}
