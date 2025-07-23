using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Chat;

/// <summary>
/// Representa um contato retornado pela API Evolution.
/// </summary>
public class ContactItem
{
    /// <summary>
    /// ID único do contato.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// JID remoto do contato no WhatsApp.
    /// </summary>
    [JsonPropertyName("remoteJid")]
    public string RemoteJid { get; set; } = string.Empty;

    /// <summary>
    /// Nome de exibição do contato.
    /// </summary>
    [JsonPropertyName("pushName")]
    public string PushName { get; set; } = string.Empty;

    /// <summary>
    /// URL da foto de perfil do contato.
    /// </summary>
    [JsonPropertyName("profilePicUrl")]
    public string? ProfilePicUrl { get; set; }

    /// <summary>
    /// Data de criação do contato.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Data da última atualização do contato.
    /// </summary>
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// ID da instância à qual o contato pertence.
    /// </summary>
    [JsonPropertyName("instanceId")]
    public string InstanceId { get; set; } = string.Empty;
}

/// <summary>
/// Representa a resposta da busca de contatos.
/// </summary>
public class FindContactsResponse : List<ContactItem>
{
    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="FindContactsResponse"/>.
    /// </summary>
    public FindContactsResponse() : base()
    {
    }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="FindContactsResponse"/> com uma coleção de contatos.
    /// </summary>
    /// <param name="contacts">A coleção de contatos.</param>
    public FindContactsResponse(IEnumerable<ContactItem> contacts) : base(contacts)
    {
    }
}