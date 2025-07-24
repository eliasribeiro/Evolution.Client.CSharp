using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message;

/// <summary>
/// Representa a requisição para enviar localização.
/// </summary>
public class SendLocationRequest
{
    /// <summary>
    /// Número do telefone do destinatário com código do país.
    /// </summary>
    [JsonPropertyName("number")]
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Nome da cidade.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Endereço da localização.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Latitude da localização.
    /// </summary>
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    /// <summary>
    /// Longitude da localização.
    /// </summary>
    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

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