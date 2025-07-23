using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message;

/// <summary>
/// Representa a requisição para enviar uma mensagem de texto.
/// </summary>
public class SendTextRequest
{
    /// <summary>
    /// Número do telefone do destinatário com código do país.
    /// </summary>
    [JsonPropertyName("number")]
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Texto da mensagem a ser enviada.
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Atraso em milissegundos antes de enviar a mensagem (opcional).
    /// </summary>
    [JsonPropertyName("delay")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Delay { get; set; }

    /// <summary>
    /// Indica se deve exibir preview de links (opcional).
    /// </summary>
    [JsonPropertyName("linkPreview")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? LinkPreview { get; set; }

    /// <summary>
    /// Indica se deve mencionar todos os participantes do grupo (opcional).
    /// </summary>
    [JsonPropertyName("mentionsEveryOne")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? MentionsEveryOne { get; set; }

    /// <summary>
    /// Lista de JIDs dos usuários mencionados (opcional).
    /// </summary>
    [JsonPropertyName("mentioned")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Mentioned { get; set; }

    /// <summary>
    /// Mensagem citada/respondida (opcional).
    /// </summary>
    [JsonPropertyName("quoted")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public QuotedMessage? Quoted { get; set; }
}

/// <summary>
/// Representa uma mensagem citada/respondida.
/// </summary>
public class QuotedMessage
{
    /// <summary>
    /// Chave da mensagem citada.
    /// </summary>
    [JsonPropertyName("key")]
    public QuotedMessageKey Key { get; set; } = new();

    /// <summary>
    /// Conteúdo da mensagem citada.
    /// </summary>
    [JsonPropertyName("message")]
    public QuotedMessageContent Message { get; set; } = new();
}

/// <summary>
/// Representa a chave de uma mensagem citada.
/// </summary>
public class QuotedMessageKey
{
    /// <summary>
    /// ID da mensagem citada.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
}

/// <summary>
/// Representa o conteúdo de uma mensagem citada.
/// </summary>
public class QuotedMessageContent
{
    /// <summary>
    /// Texto da mensagem citada.
    /// </summary>
    [JsonPropertyName("conversation")]
    public string Conversation { get; set; } = string.Empty;
}
