namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Requisição para verificar se números existem no WhatsApp
/// </summary>
public class CheckWhatsAppNumbersRequest
{
    /// <summary>
    /// Lista de números para verificar
    /// </summary>
    public string[] Numbers { get; set; } = Array.Empty<string>();
}

/// <summary>
/// Resposta da verificação de números no WhatsApp
/// </summary>
public class WhatsAppNumberCheckResult
{
    /// <summary>
    /// Indica se o número existe no WhatsApp
    /// </summary>
    public bool Exists { get; set; }

    /// <summary>
    /// JID do WhatsApp
    /// </summary>
    public string? Jid { get; set; }

    /// <summary>
    /// Número verificado
    /// </summary>
    public string Number { get; set; } = string.Empty;
}

/// <summary>
/// Requisição para marcar mensagens como lidas
/// </summary>
public class MarkAsReadChatRequest
{
    /// <summary>
    /// JID remoto do chat
    /// </summary>
    public string RemoteJid { get; set; } = string.Empty;

    /// <summary>
    /// Indica se deve marcar todas as mensagens como lidas
    /// </summary>
    public bool? ReadMessages { get; set; }
}

/// <summary>
/// Requisição para marcar mensagens como não lidas
/// </summary>
public class MarkAsUnreadChatRequest
{
    /// <summary>
    /// JID remoto do chat
    /// </summary>
    public string RemoteJid { get; set; } = string.Empty;
}

/// <summary>
/// Requisição para arquivar chat
/// </summary>
public class ArchiveChatRequest
{
    /// <summary>
    /// JID remoto do chat
    /// </summary>
    public string RemoteJid { get; set; } = string.Empty;

    /// <summary>
    /// Indica se deve arquivar (true) ou desarquivar (false)
    /// </summary>
    public bool Archive { get; set; } = true;
}

/// <summary>
/// Requisição para deletar mensagem para todos
/// </summary>
public class DeleteMessageForEveryoneRequest
{
    /// <summary>
    /// Chave da mensagem
    /// </summary>
    public MessageKey Key { get; set; } = new();
}

/// <summary>
/// Requisição para atualizar mensagem
/// </summary>
public class UpdateMessageRequest
{
    /// <summary>
    /// Número do destinatário
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Novo texto da mensagem
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Chave da mensagem a ser atualizada
    /// </summary>
    public MessageKey Key { get; set; } = new();
}

/// <summary>
/// Requisição para enviar presença
/// </summary>
public class SendPresenceRequest
{
    /// <summary>
    /// Número do destinatário
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de presença (available, unavailable, composing, recording, paused)
    /// </summary>
    public string Presence { get; set; } = string.Empty;

    /// <summary>
    /// Delay em milissegundos (opcional)
    /// </summary>
    public int? Delay { get; set; }
}

/// <summary>
/// Requisição para atualizar status de bloqueio
/// </summary>
public class UpdateBlockStatusRequest
{
    /// <summary>
    /// Número do contato
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Status de bloqueio (block ou unblock)
    /// </summary>
    public string Status { get; set; } = string.Empty;
}

/// <summary>
/// Requisição para buscar URL da foto do perfil
/// </summary>
public class FetchProfilePictureRequest
{
    /// <summary>
    /// Número do contato
    /// </summary>
    public string Number { get; set; } = string.Empty;
}

/// <summary>
/// Resposta com URL da foto do perfil
/// </summary>
public class ProfilePictureResponse
{
    /// <summary>
    /// URL da foto do perfil
    /// </summary>
    public string? ProfilePictureUrl { get; set; }
}

/// <summary>
/// Requisição para obter base64 de mídia
/// </summary>
public class GetBase64Request
{
    /// <summary>
    /// Chave da mensagem
    /// </summary>
    public MessageKey Key { get; set; } = new();

    /// <summary>
    /// Indica se deve converter para base64
    /// </summary>
    public bool ConvertToMp4 { get; set; } = false;
}

/// <summary>
/// Resposta com dados em base64
/// </summary>
public class Base64Response
{
    /// <summary>
    /// Dados em base64
    /// </summary>
    public string? Base64 { get; set; }

    /// <summary>
    /// Tipo MIME
    /// </summary>
    public string? MimeType { get; set; }

    /// <summary>
    /// Nome do arquivo
    /// </summary>
    public string? FileName { get; set; }
}

/// <summary>
/// Requisição para buscar contatos
/// </summary>
public class FindContactsChatRequest
{
    /// <summary>
    /// Termo de busca (opcional)
    /// </summary>
    public string? Where { get; set; }
}

/// <summary>
/// Requisição para buscar mensagens
/// </summary>
public class FindMessagesChatRequest
{
    /// <summary>
    /// Filtros de busca
    /// </summary>
    public MessageSearchWhere? Where { get; set; }

    /// <summary>
    /// Limite de resultados
    /// </summary>
    public int? Limit { get; set; }
}

/// <summary>
/// Filtros para busca de mensagens
/// </summary>
public class MessageSearchWhere
{
    /// <summary>
    /// JID remoto
    /// </summary>
    public string? RemoteJid { get; set; }

    /// <summary>
    /// Indica se é de mim
    /// </summary>
    public bool? FromMe { get; set; }

    /// <summary>
    /// ID da mensagem
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Status da mensagem
    /// </summary>
    public string? Status { get; set; }
}

/// <summary>
/// Requisição para buscar mensagens de status
/// </summary>
public class FindStatusMessageRequest
{
    /// <summary>
    /// Filtros de busca (opcional)
    /// </summary>
    public StatusMessageWhere? Where { get; set; }

    /// <summary>
    /// Limite de resultados
    /// </summary>
    public int? Limit { get; set; }
}

/// <summary>
/// Filtros para busca de mensagens de status
/// </summary>
public class StatusMessageWhere
{
    /// <summary>
    /// JID do proprietário
    /// </summary>
    public string? Owner { get; set; }

    /// <summary>
    /// Status da mensagem
    /// </summary>
    public string? Status { get; set; }
}

/// <summary>
/// Requisição para buscar chats
/// </summary>
public class FindChatsRequest
{
    /// <summary>
    /// Filtros de busca (opcional)
    /// </summary>
    public ChatSearchWhere? Where { get; set; }

    /// <summary>
    /// Limite de resultados
    /// </summary>
    public int? Limit { get; set; }
}

/// <summary>
/// Filtros para busca de chats
/// </summary>
public class ChatSearchWhere
{
    /// <summary>
    /// JID remoto
    /// </summary>
    public string? RemoteJid { get; set; }

    /// <summary>
    /// Indica se está arquivado
    /// </summary>
    public bool? Archived { get; set; }
}

/// <summary>
/// Informações do chat
/// </summary>
public class ChatInfo
{
    /// <summary>
    /// JID remoto
    /// </summary>
    public string RemoteJid { get; set; } = string.Empty;

    /// <summary>
    /// Nome do chat
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Indica se está arquivado
    /// </summary>
    public bool Archived { get; set; }

    /// <summary>
    /// Timestamp da última mensagem
    /// </summary>
    public long? LastMessageTimestamp { get; set; }

    /// <summary>
    /// Número de mensagens não lidas
    /// </summary>
    public int UnreadCount { get; set; }
}

/// <summary>
/// Resposta genérica de sucesso
/// </summary>
public class ChatOperationResponse
{
    /// <summary>
    /// Indica se a operação foi bem-sucedida
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Mensagem de resposta
    /// </summary>
    public string? Message { get; set; }
}
