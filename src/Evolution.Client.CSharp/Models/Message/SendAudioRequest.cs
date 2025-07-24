using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message;

/// <summary>
/// Representa a requisição para enviar áudio.
/// </summary>
public class SendAudioRequest
{
    /// <summary>
    /// Número do telefone do destinatário com código do país.
    /// </summary>
    [JsonPropertyName("number")]
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// URL ou base64 do áudio.
    /// </summary>
    [JsonPropertyName("audio")]
    public string Audio { get; set; } = string.Empty;

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