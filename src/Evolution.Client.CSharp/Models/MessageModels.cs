namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Requisição para envio de mensagem de texto
/// </summary>
public class SendTextMessageRequest
{
    /// <summary>
    /// Número do destinatário
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Texto da mensagem
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Delay antes do envio (em segundos)
    /// </summary>
    public int? Delay { get; set; }

    /// <summary>
    /// Preview de link
    /// </summary>
    public bool? LinkPreview { get; set; }

    /// <summary>
    /// Mencionar todos
    /// </summary>
    public bool? MentionsEveryOne { get; set; }

    /// <summary>
    /// Usuários mencionados
    /// </summary>
    public string[]? Mentioned { get; set; }

    /// <summary>
    /// Mensagem citada
    /// </summary>
    public QuotedMessage? Quoted { get; set; }
}

/// <summary>
/// Requisição para envio de mensagem de mídia
/// </summary>
public class SendMediaMessageRequest
{
    /// <summary>
    /// Número do destinatário
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Tipo da mídia (image, video, document, etc.)
    /// </summary>
    public string MediaType { get; set; } = string.Empty;

    /// <summary>
    /// Tipo MIME
    /// </summary>
    public string? MimeType { get; set; }

    /// <summary>
    /// Legenda da mídia
    /// </summary>
    public string? Caption { get; set; }

    /// <summary>
    /// URL ou base64 da mídia
    /// </summary>
    public string Media { get; set; } = string.Empty;

    /// <summary>
    /// Nome do arquivo
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// Delay antes do envio (em segundos)
    /// </summary>
    public int? Delay { get; set; }

    /// <summary>
    /// Preview de link
    /// </summary>
    public bool? LinkPreview { get; set; }

    /// <summary>
    /// Mencionar todos
    /// </summary>
    public bool? MentionsEveryOne { get; set; }

    /// <summary>
    /// Usuários mencionados
    /// </summary>
    public string[]? Mentioned { get; set; }

    /// <summary>
    /// Mensagem citada
    /// </summary>
    public QuotedMessage? Quoted { get; set; }
}

/// <summary>
/// Requisição para envio de mensagem de áudio
/// </summary>
public class SendAudioMessageRequest
{
    /// <summary>
    /// Número do destinatário
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// URL ou base64 do áudio
    /// </summary>
    public string Audio { get; set; } = string.Empty;

    /// <summary>
    /// Delay antes do envio (em segundos)
    /// </summary>
    public int? Delay { get; set; }

    /// <summary>
    /// Mensagem citada
    /// </summary>
    public QuotedMessage? Quoted { get; set; }
}

/// <summary>
/// Requisição para envio de localização
/// </summary>
public class SendLocationMessageRequest
{
    /// <summary>
    /// Número do destinatário
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Latitude
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Longitude
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Nome do local
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Endereço
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Delay antes do envio (em segundos)
    /// </summary>
    public int? Delay { get; set; }

    /// <summary>
    /// Mensagem citada
    /// </summary>
    public QuotedMessage? Quoted { get; set; }
}

/// <summary>
/// Resposta do envio de mensagem
/// </summary>
public class SendMessageResponse
{
    /// <summary>
    /// ID da mensagem
    /// </summary>
    public string MessageId { get; set; } = string.Empty;

    /// <summary>
    /// Status do envio
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp do envio
    /// </summary>
    public long Timestamp { get; set; }
}

/// <summary>
/// Requisição para busca de mensagens
/// </summary>
public class FindMessagesRequest
{
    /// <summary>
    /// Número do contato
    /// </summary>
    public string? Number { get; set; }

    /// <summary>
    /// Data de início
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Data de fim
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Limite de resultados
    /// </summary>
    public int? Limit { get; set; }
}

/// <summary>
/// Mensagem
/// </summary>
public class Message
{
    /// <summary>
    /// ID da mensagem
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Número do remetente
    /// </summary>
    public string From { get; set; } = string.Empty;

    /// <summary>
    /// Número do destinatário
    /// </summary>
    public string To { get; set; } = string.Empty;

    /// <summary>
    /// Conteúdo da mensagem
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Tipo da mensagem
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    /// Status da mensagem
    /// </summary>
    public string Status { get; set; } = string.Empty;
}

/// <summary>
/// Requisição para envio de status
/// </summary>
public class SendStatusMessageRequest
{
    /// <summary>
    /// Tipo de status (text, image, video, audio)
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Conteúdo do status (texto ou URL da mídia)
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Legenda (para mídia)
    /// </summary>
    public string? Caption { get; set; }

    /// <summary>
    /// Cor de fundo (para texto)
    /// </summary>
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// Cor da fonte (para texto)
    /// </summary>
    public string? FontColor { get; set; }

    /// <summary>
    /// Delay antes do envio (em segundos)
    /// </summary>
    public int? Delay { get; set; }
}

/// <summary>
/// Requisição para envio de sticker
/// </summary>
public class SendStickerMessageRequest
{
    /// <summary>
    /// Número do destinatário
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// URL ou base64 do sticker
    /// </summary>
    public string Sticker { get; set; } = string.Empty;

    /// <summary>
    /// Delay antes do envio (em segundos)
    /// </summary>
    public int? Delay { get; set; }

    /// <summary>
    /// Mensagem citada
    /// </summary>
    public QuotedMessage? Quoted { get; set; }
}

/// <summary>
/// Requisição para envio de contato
/// </summary>
public class SendContactMessageRequest
{
    /// <summary>
    /// Número do destinatário
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Lista de contatos
    /// </summary>
    public MessageContactInfo[] Contacts { get; set; } = Array.Empty<MessageContactInfo>();

    /// <summary>
    /// Delay antes do envio (em segundos)
    /// </summary>
    public int? Delay { get; set; }

    /// <summary>
    /// Mensagem citada
    /// </summary>
    public QuotedMessage? Quoted { get; set; }
}

/// <summary>
/// Informações do contato para mensagem
/// </summary>
public class MessageContactInfo
{
    /// <summary>
    /// Nome completo
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Organização
    /// </summary>
    public string? Organization { get; set; }

    /// <summary>
    /// Números de telefone
    /// </summary>
    public PhoneNumber[] PhoneNumbers { get; set; } = Array.Empty<PhoneNumber>();

    /// <summary>
    /// Endereços de email
    /// </summary>
    public EmailAddress[] Emails { get; set; } = Array.Empty<EmailAddress>();

    /// <summary>
    /// URLs
    /// </summary>
    public ContactUrl[] Urls { get; set; } = Array.Empty<ContactUrl>();
}

/// <summary>
/// Número de telefone do contato
/// </summary>
public class PhoneNumber
{
    /// <summary>
    /// Número
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Tipo (HOME, WORK, MOBILE, etc.)
    /// </summary>
    public string Type { get; set; } = string.Empty;
}

/// <summary>
/// Endereço de email do contato
/// </summary>
public class EmailAddress
{
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Tipo (HOME, WORK, etc.)
    /// </summary>
    public string Type { get; set; } = string.Empty;
}

/// <summary>
/// URL do contato
/// </summary>
public class ContactUrl
{
    /// <summary>
    /// URL
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Tipo (HOME, WORK, etc.)
    /// </summary>
    public string Type { get; set; } = string.Empty;
}

/// <summary>
/// Requisição para envio de reação
/// </summary>
public class SendReactionMessageRequest
{
    /// <summary>
    /// Número do destinatário
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Chave da mensagem para reagir
    /// </summary>
    public MessageKey Key { get; set; } = new();

    /// <summary>
    /// Emoji da reação
    /// </summary>
    public string Reaction { get; set; } = string.Empty;

    /// <summary>
    /// Delay antes do envio (em segundos)
    /// </summary>
    public int? Delay { get; set; }
}

/// <summary>
/// Chave da mensagem
/// </summary>
public class MessageKey
{
    /// <summary>
    /// ID da mensagem
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// JID remoto
    /// </summary>
    public string? RemoteJid { get; set; }

    /// <summary>
    /// Se é de mim
    /// </summary>
    public bool? FromMe { get; set; }
}

/// <summary>
/// Requisição para envio de enquete
/// </summary>
public class SendPollMessageRequest
{
    /// <summary>
    /// Número do destinatário
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Nome da enquete
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Opções da enquete
    /// </summary>
    public PollOption[] Options { get; set; } = Array.Empty<PollOption>();

    /// <summary>
    /// Número de opções selecionáveis
    /// </summary>
    public int SelectableOptionsCount { get; set; } = 1;

    /// <summary>
    /// Delay antes do envio (em segundos)
    /// </summary>
    public int? Delay { get; set; }

    /// <summary>
    /// Mensagem citada
    /// </summary>
    public QuotedMessage? Quoted { get; set; }
}

/// <summary>
/// Opção da enquete
/// </summary>
public class PollOption
{
    /// <summary>
    /// Nome da opção
    /// </summary>
    public string OptionName { get; set; } = string.Empty;
}

/// <summary>
/// Requisição para envio de lista
/// </summary>
public class SendListMessageRequest
{
    /// <summary>
    /// Número do destinatário
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Título da lista
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Descrição da lista
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Texto do botão
    /// </summary>
    public string ButtonText { get; set; } = string.Empty;

    /// <summary>
    /// Rodapé
    /// </summary>
    public string? Footer { get; set; }

    /// <summary>
    /// Seções da lista
    /// </summary>
    public ListSection[] Sections { get; set; } = Array.Empty<ListSection>();

    /// <summary>
    /// Delay antes do envio (em segundos)
    /// </summary>
    public int? Delay { get; set; }

    /// <summary>
    /// Mensagem citada
    /// </summary>
    public QuotedMessage? Quoted { get; set; }
}

/// <summary>
/// Seção da lista
/// </summary>
public class ListSection
{
    /// <summary>
    /// Título da seção
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Itens da seção
    /// </summary>
    public ListItem[] Rows { get; set; } = Array.Empty<ListItem>();
}

/// <summary>
/// Item da lista
/// </summary>
public class ListItem
{
    /// <summary>
    /// ID do item
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Título do item
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Descrição do item
    /// </summary>
    public string? Description { get; set; }
}

/// <summary>
/// Requisição para envio de botões
/// </summary>
public class SendButtonMessageRequest
{
    /// <summary>
    /// Número do destinatário
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Título
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Descrição
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Rodapé
    /// </summary>
    public string? Footer { get; set; }

    /// <summary>
    /// Botões
    /// </summary>
    public MessageButton[] Buttons { get; set; } = Array.Empty<MessageButton>();

    /// <summary>
    /// Delay antes do envio (em segundos)
    /// </summary>
    public int? Delay { get; set; }

    /// <summary>
    /// Preview de link
    /// </summary>
    public bool? LinkPreview { get; set; }

    /// <summary>
    /// Mencionar todos
    /// </summary>
    public bool? MentionsEveryOne { get; set; }

    /// <summary>
    /// Usuários mencionados
    /// </summary>
    public string[]? Mentioned { get; set; }

    /// <summary>
    /// Mensagem citada
    /// </summary>
    public QuotedMessage? Quoted { get; set; }
}

/// <summary>
/// Botão da mensagem
/// </summary>
public class MessageButton
{
    /// <summary>
    /// Título do botão
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Texto exibido
    /// </summary>
    public string DisplayText { get; set; } = string.Empty;

    /// <summary>
    /// ID do botão
    /// </summary>
    public string Id { get; set; } = string.Empty;
}

/// <summary>
/// Mensagem citada
/// </summary>
public class QuotedMessage
{
    /// <summary>
    /// Chave da mensagem
    /// </summary>
    public MessageKey Key { get; set; } = new();

    /// <summary>
    /// Conteúdo da mensagem
    /// </summary>
    public QuotedMessageContent Message { get; set; } = new();
}

/// <summary>
/// Conteúdo da mensagem citada
/// </summary>
public class QuotedMessageContent
{
    /// <summary>
    /// Conversa
    /// </summary>
    public string? Conversation { get; set; }
}

/// <summary>
/// Requisição para marcar como lida
/// </summary>
public class MarkAsReadRequest
{
    /// <summary>
    /// Número do contato
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// IDs das mensagens
    /// </summary>
    public string[]? MessageIds { get; set; }
}
