using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message;

/// <summary>
/// Representa uma requisição para enviar botões.
/// </summary>
public class SendButtonRequest
{
    /// <summary>
    /// Número para receber a mensagem (com código do país).
    /// </summary>
    [JsonPropertyName("number")]
    public required string Number { get; set; }

    /// <summary>
    /// Título dos botões.
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    /// <summary>
    /// Descrição dos botões.
    /// </summary>
    [JsonPropertyName("description")]
    public required string Description { get; set; }

    /// <summary>
    /// Texto do rodapé.
    /// </summary>
    [JsonPropertyName("footer")]
    public required string Footer { get; set; }

    /// <summary>
    /// Lista de botões.
    /// </summary>
    [JsonPropertyName("buttons")]
    public required List<ButtonInfo> Buttons { get; set; }

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

/// <summary>
/// Representa as informações de um botão.
/// </summary>
public class ButtonInfo
{
    /// <summary>
    /// Tipo do botão.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    /// <summary>
    /// Texto exibido no botão.
    /// </summary>
    [JsonPropertyName("displayText")]
    public required string DisplayText { get; set; }

    /// <summary>
    /// ID do botão.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }
}