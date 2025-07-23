using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Chat;

/// <summary>
/// Representa um item da resposta do endpoint /chat/whatsappNumbers/{instance} da API Evolution.
/// </summary>
public class CheckWhatsAppResponseItem
{
    /// <summary>
    /// Obtém ou define se o número existe no WhatsApp.
    /// </summary>
    [JsonPropertyName("exists")]
    public bool Exists { get; set; }

    /// <summary>
    /// Obtém ou define o JID (identificador único) do WhatsApp.
    /// </summary>
    [JsonPropertyName("jid")]
    public string Jid { get; set; } = string.Empty;

    /// <summary>
    /// Obtém ou define o nome do contato (apenas quando exists = true).
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Obtém ou define o número de telefone.
    /// </summary>
    [JsonPropertyName("number")]
    public string Number { get; set; } = string.Empty;
}

/// <summary>
/// Representa a resposta do endpoint /chat/whatsappNumbers/{instance} da API Evolution.
/// </summary>
public class CheckWhatsAppResponse : List<CheckWhatsAppResponseItem>
{
}