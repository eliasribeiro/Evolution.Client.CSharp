using Evolution.Client;
using Evolution.Client.Modules;

namespace Evolution.Client.Examples;

/// <summary>
/// Exemplos de uso das funcionalidades do Chat Controller
/// </summary>
public class ChatExamples
{
    private readonly EvolutionClient _client;

    public ChatExamples()
    {
        var options = new EvolutionClientOptions
        {
            BaseUrl = "https://your-evolution-api-url.com",
            ApiKey = "your-api-key"
        };
        _client = new EvolutionClient(options);
    }

    /// <summary>
    /// Exemplo de verificação se números existem no WhatsApp
    /// </summary>
    public async Task CheckWhatsAppNumbersExample()
    {
        var request = new CheckWhatsAppNumbersRequest
        {
            Numbers = new[] { "5511999999999", "5511888888888", "5511777777777" }
        };

        var results = await _client.Chat.CheckWhatsAppNumbersAsync("instance-name", request);
        
        foreach (var result in results)
        {
            Console.WriteLine($"Número: {result.Number}");
            Console.WriteLine($"Existe no WhatsApp: {result.Exists}");
            if (result.Exists)
            {
                Console.WriteLine($"JID: {result.Jid}");
            }
            Console.WriteLine("---");
        }
    }

    /// <summary>
    /// Exemplo de marcar mensagens como lidas
    /// </summary>
    public async Task MarkAsReadExample()
    {
        var request = new MarkAsReadChatRequest
        {
            RemoteJid = "5511999999999@s.whatsapp.net",
            ReadMessages = true
        };

        var response = await _client.Chat.MarkAsReadAsync("instance-name", request);
        Console.WriteLine($"Operação bem-sucedida: {response.Success}");
        Console.WriteLine($"Mensagem: {response.Message}");
    }

    /// <summary>
    /// Exemplo de marcar mensagens como não lidas
    /// </summary>
    public async Task MarkAsUnreadExample()
    {
        var request = new MarkAsUnreadChatRequest
        {
            RemoteJid = "5511999999999@s.whatsapp.net"
        };

        var response = await _client.Chat.MarkAsUnreadAsync("instance-name", request);
        Console.WriteLine($"Operação bem-sucedida: {response.Success}");
    }

    /// <summary>
    /// Exemplo de arquivar chat
    /// </summary>
    public async Task ArchiveChatExample()
    {
        var request = new ArchiveChatRequest
        {
            RemoteJid = "5511999999999@s.whatsapp.net",
            Archive = true // true para arquivar, false para desarquivar
        };

        var response = await _client.Chat.ArchiveChatAsync("instance-name", request);
        Console.WriteLine($"Chat arquivado: {response.Success}");
    }

    /// <summary>
    /// Exemplo de deletar mensagem para todos
    /// </summary>
    public async Task DeleteMessageForEveryoneExample()
    {
        var request = new DeleteMessageForEveryoneRequest
        {
            Key = new MessageKey
            {
                Id = "BAE5F5A632EAE722", // ID da mensagem
                RemoteJid = "5511999999999@s.whatsapp.net",
                FromMe = true
            }
        };

        var response = await _client.Chat.DeleteMessageForEveryoneAsync("instance-name", request);
        Console.WriteLine($"Mensagem deletada: {response.Success}");
    }

    /// <summary>
    /// Exemplo de atualizar mensagem
    /// </summary>
    public async Task UpdateMessageExample()
    {
        var request = new UpdateMessageRequest
        {
            Number = "5511999999999",
            Text = "Texto atualizado da mensagem",
            Key = new MessageKey
            {
                Id = "BAE5F5A632EAE722",
                RemoteJid = "5511999999999@s.whatsapp.net",
                FromMe = true
            }
        };

        var response = await _client.Chat.UpdateMessageAsync("instance-name", request);
        Console.WriteLine($"Mensagem atualizada: {response.Success}");
    }

    /// <summary>
    /// Exemplo de enviar presença (digitando, gravando, etc.)
    /// </summary>
    public async Task SendPresenceExample()
    {
        var request = new SendPresenceRequest
        {
            Number = "5511999999999",
            Presence = "composing", // composing, recording, paused, available, unavailable
            Delay = 3000 // 3 segundos
        };

        var response = await _client.Chat.SendPresenceAsync("instance-name", request);
        Console.WriteLine($"Presença enviada: {response.Success}");
    }

    /// <summary>
    /// Exemplo de bloquear/desbloquear contato
    /// </summary>
    public async Task UpdateBlockStatusExample()
    {
        var request = new UpdateBlockStatusRequest
        {
            Number = "5511999999999",
            Status = "block" // "block" para bloquear, "unblock" para desbloquear
        };

        var response = await _client.Chat.UpdateBlockStatusAsync("instance-name", request);
        Console.WriteLine($"Status de bloqueio atualizado: {response.Success}");
    }

    /// <summary>
    /// Exemplo de buscar URL da foto do perfil
    /// </summary>
    public async Task FetchProfilePictureExample()
    {
        var request = new FetchProfilePictureRequest
        {
            Number = "5511999999999"
        };

        var response = await _client.Chat.FetchProfilePictureUrlAsync("instance-name", request);
        Console.WriteLine($"URL da foto do perfil: {response.ProfilePictureUrl}");
    }

    /// <summary>
    /// Exemplo de obter base64 de mídia
    /// </summary>
    public async Task GetBase64Example()
    {
        var request = new GetBase64Request
        {
            Key = new MessageKey
            {
                Id = "BAE5F5A632EAE722",
                RemoteJid = "5511999999999@s.whatsapp.net",
                FromMe = false
            },
            ConvertToMp4 = false
        };

        var response = await _client.Chat.GetBase64Async("instance-name", request);
        Console.WriteLine($"Tipo MIME: {response.MimeType}");
        Console.WriteLine($"Nome do arquivo: {response.FileName}");
        Console.WriteLine($"Base64 (primeiros 50 chars): {response.Base64?[..Math.Min(50, response.Base64?.Length ?? 0)]}...");
    }

    /// <summary>
    /// Exemplo de buscar contatos
    /// </summary>
    public async Task FindContactsExample()
    {
        var request = new FindContactsChatRequest
        {
            Where = "João" // Termo de busca opcional
        };

        var contacts = await _client.Chat.FindContactsAsync("instance-name", request);
        
        foreach (var contact in contacts)
        {
            Console.WriteLine($"Nome: {contact.Name}");
            Console.WriteLine($"Número: {contact.Number}");
            Console.WriteLine($"Status: {contact.Status}");
            Console.WriteLine("---");
        }
    }

    /// <summary>
    /// Exemplo de buscar mensagens
    /// </summary>
    public async Task FindMessagesExample()
    {
        var request = new FindMessagesChatRequest
        {
            Where = new MessageSearchWhere
            {
                RemoteJid = "5511999999999@s.whatsapp.net",
                FromMe = false
            },
            Limit = 10
        };

        var messages = await _client.Chat.FindMessagesAsync("instance-name", request);
        
        foreach (var message in messages)
        {
            Console.WriteLine($"ID: {message.Id}");
            Console.WriteLine($"De: {message.From}");
            Console.WriteLine($"Para: {message.To}");
            Console.WriteLine($"Texto: {message.Text}");
            Console.WriteLine($"Tipo: {message.Type}");
            Console.WriteLine("---");
        }
    }

    /// <summary>
    /// Exemplo de buscar chats
    /// </summary>
    public async Task FindChatsExample()
    {
        var request = new FindChatsRequest
        {
            Where = new ChatSearchWhere
            {
                Archived = false // Buscar apenas chats não arquivados
            },
            Limit = 20
        };

        var chats = await _client.Chat.FindChatsAsync("instance-name", request);
        
        foreach (var chat in chats)
        {
            Console.WriteLine($"JID: {chat.RemoteJid}");
            Console.WriteLine($"Nome: {chat.Name}");
            Console.WriteLine($"Arquivado: {chat.Archived}");
            Console.WriteLine($"Mensagens não lidas: {chat.UnreadCount}");
            Console.WriteLine("---");
        }
    }

    /// <summary>
    /// Exemplo de buscar mensagens de status
    /// </summary>
    public async Task FindStatusMessagesExample()
    {
        var request = new FindStatusMessageRequest
        {
            Where = new StatusMessageWhere
            {
                Owner = "5511999999999@s.whatsapp.net"
            },
            Limit = 5
        };

        var statusMessages = await _client.Chat.FindStatusMessagesAsync("instance-name", request);
        
        foreach (var message in statusMessages)
        {
            Console.WriteLine($"ID: {message.Id}");
            Console.WriteLine($"Tipo: {message.Type}");
            Console.WriteLine($"Status: {message.Status}");
            Console.WriteLine("---");
        }
    }
}
