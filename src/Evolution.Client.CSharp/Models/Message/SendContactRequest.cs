using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message;

/// <summary>
/// Representa uma requisição para enviar contatos.
/// </summary>
public class SendContactRequest
{
    /// <summary>
    /// Número para receber a mensagem (com código do país).
    /// </summary>
    [JsonPropertyName("number")]
    public required string Number { get; set; }

    /// <summary>
    /// Lista de contatos a serem enviados.
    /// </summary>
    [JsonPropertyName("contact")]
    public required List<ContactInfo> Contact { get; set; }

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
/// Representa as informações de um contato.
/// </summary>
public class ContactInfo
{
    /// <summary>
    /// Nome completo do contato.
    /// </summary>
    [JsonPropertyName("fullName")]
    public required string FullName { get; set; }

    /// <summary>
    /// Organização do contato.
    /// </summary>
    [JsonPropertyName("organization")]
    public string? Organization { get; set; }

    /// <summary>
    /// Lista de números de telefone do contato.
    /// </summary>
    [JsonPropertyName("phoneNumber")]
    public required List<PhoneNumber> PhoneNumber { get; set; }

    /// <summary>
    /// Lista de emails do contato.
    /// </summary>
    [JsonPropertyName("email")]
    public List<EmailInfo>? Email { get; set; }

    /// <summary>
    /// Lista de URLs do contato.
    /// </summary>
    [JsonPropertyName("url")]
    public List<UrlInfo>? Url { get; set; }
}

/// <summary>
/// Representa um número de telefone do contato.
/// </summary>
public class PhoneNumber
{
    /// <summary>
    /// Número de telefone.
    /// </summary>
    [JsonPropertyName("phoneNumber")]
    public required string Number { get; set; }

    /// <summary>
    /// Tipo do número (ex: "Mobile", "Home", "Work").
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}

/// <summary>
/// Representa um email do contato.
/// </summary>
public class EmailInfo
{
    /// <summary>
    /// Endereço de email.
    /// </summary>
    [JsonPropertyName("email")]
    public required string Email { get; set; }

    /// <summary>
    /// Tipo do email (ex: "Personal", "Work").
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}

/// <summary>
/// Representa uma URL do contato.
/// </summary>
public class UrlInfo
{
    /// <summary>
    /// URL.
    /// </summary>
    [JsonPropertyName("url")]
    public required string Url { get; set; }

    /// <summary>
    /// Tipo da URL (ex: "Website", "Social").
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}