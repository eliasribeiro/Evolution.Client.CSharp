using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message;

/// <summary>
/// Representa uma requisição para enviar enquete.
/// </summary>
public class SendPollRequest
{
    /// <summary>
    /// Número para receber a mensagem (com código do país).
    /// </summary>
    [JsonPropertyName("number")]
    public required string Number { get; set; }

    /// <summary>
    /// Texto principal da enquete.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Número de opções selecionáveis.
    /// </summary>
    [JsonPropertyName("selectableCount")]
    public required int SelectableCount { get; set; }

    /// <summary>
    /// Valores para as opções da enquete.
    /// </summary>
    [JsonPropertyName("values")]
    public required List<string> Values { get; set; }

    /// <summary>
    /// Tempo de presença em milissegundos antes de enviar a mensagem.
    /// </summary>
    [JsonPropertyName("delay")]
    public int? Delay { get; set; }

    /// <summary>
    /// Mostra uma prévia do site de destino se houver um link na mensagem.
    /// </summary>
    [JsonPropertyName("linkPreview")]
    public bool? LinkPreview { get; set; }

    /// <summary>
    /// Menciona todos quando a mensagem é enviada.
    /// </summary>
    [JsonPropertyName("mentionsEveryOne")]
    public bool? MentionsEveryOne { get; set; }

    /// <summary>
    /// Números para mencionar.
    /// </summary>
    [JsonPropertyName("mentioned")]
    public List<string>? Mentioned { get; set; }

    /// <summary>
    /// Informações da mensagem citada.
    /// </summary>
    [JsonPropertyName("quoted")]
    public QuotedMessage? Quoted { get; set; }
}