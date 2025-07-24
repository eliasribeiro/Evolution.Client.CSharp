using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message;

/// <summary>
/// Representa uma requisição para enviar lista.
/// </summary>
public class SendListRequest
{
    /// <summary>
    /// Número para receber a mensagem (com código do país).
    /// </summary>
    [JsonPropertyName("number")]
    public required string Number { get; set; }

    /// <summary>
    /// Título da lista.
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    /// <summary>
    /// Descrição da lista.
    /// </summary>
    [JsonPropertyName("description")]
    public required string Description { get; set; }

    /// <summary>
    /// Texto do botão.
    /// </summary>
    [JsonPropertyName("buttonText")]
    public required string ButtonText { get; set; }

    /// <summary>
    /// Texto do rodapé.
    /// </summary>
    [JsonPropertyName("footerText")]
    public required string FooterText { get; set; }

    /// <summary>
    /// Valores da lista.
    /// </summary>
    [JsonPropertyName("values")]
    public required List<ListValue> Values { get; set; }

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
/// Representa um valor da lista.
/// </summary>
public class ListValue
{
    /// <summary>
    /// Título da seção.
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    /// <summary>
    /// Linhas da seção.
    /// </summary>
    [JsonPropertyName("rows")]
    public required List<ListRow> Rows { get; set; }
}

/// <summary>
/// Representa uma linha da lista.
/// </summary>
public class ListRow
{
    /// <summary>
    /// Título da linha.
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    /// <summary>
    /// Descrição da linha.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// ID da linha.
    /// </summary>
    [JsonPropertyName("rowId")]
    public required string RowId { get; set; }
}