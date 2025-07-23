using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Chat;

/// <summary>
/// Representa a requisição para obter o base64 de uma mensagem de mídia.
/// </summary>
public class GetBase64FromMediaMessageRequest
{
    /// <summary>
    /// Informações da mensagem de mídia.
    /// </summary>
    [JsonPropertyName("message")]
    public MediaMessageInfo Message { get; set; } = new();

    /// <summary>
    /// Indica se deve converter vídeo para MP4 (apenas para vídeos).
    /// </summary>
    [JsonPropertyName("convertToMp4")]
    public bool ConvertToMp4 { get; set; }
}

/// <summary>
/// Representa as informações da mensagem de mídia.
/// </summary>
public class MediaMessageInfo
{
    /// <summary>
    /// Chave da mensagem.
    /// </summary>
    [JsonPropertyName("key")]
    public MediaMessageKey Key { get; set; } = new();
}

/// <summary>
/// Representa a chave de uma mensagem de mídia.
/// </summary>
public class MediaMessageKey
{
    /// <summary>
    /// ID da mensagem.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
}