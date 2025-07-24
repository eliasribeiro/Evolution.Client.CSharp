using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Profile;

/// <summary>
/// Representa a resposta da busca de configurações de privacidade.
/// </summary>
public class FetchPrivacySettingsResponse
{
    /// <summary>
    /// Configuração de confirmação de leitura.
    /// Valores possíveis: "all", "contacts", "none"
    /// </summary>
    [JsonPropertyName("readReceipts")]
    public string? ReadReceipts { get; set; }

    /// <summary>
    /// Configuração de visualização do perfil.
    /// Valores possíveis: "all", "contacts", "none"
    /// </summary>
    [JsonPropertyName("profile")]
    public string? Profile { get; set; }

    /// <summary>
    /// Configuração de visualização do status.
    /// Valores possíveis: "all", "contacts", "none"
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Configuração de visualização do status online.
    /// Valores possíveis: "all", "contacts", "none"
    /// </summary>
    [JsonPropertyName("online")]
    public string? Online { get; set; }

    /// <summary>
    /// Configuração de adição a grupos.
    /// Valores possíveis: "all", "contacts", "none"
    /// </summary>
    [JsonPropertyName("groupAdd")]
    public string? GroupAdd { get; set; }

    /// <summary>
    /// Configuração de chamadas.
    /// Valores possíveis: "all", "contacts", "none"
    /// </summary>
    [JsonPropertyName("callAdd")]
    public string? CallAdd { get; set; }
}