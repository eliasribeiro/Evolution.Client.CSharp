using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Chat;

/// <summary>
/// Representa a resposta da obtenção do base64 de uma mensagem de mídia.
/// </summary>
public class GetBase64FromMediaMessageResponse
{
    /// <summary>
    /// Tipo da mídia (ex: imageMessage, videoMessage, audioMessage).
    /// </summary>
    [JsonPropertyName("mediaType")]
    public string MediaType { get; set; } = string.Empty;

    /// <summary>
    /// Nome do arquivo.
    /// </summary>
    [JsonPropertyName("fileName")]
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Informações sobre o tamanho da mídia.
    /// </summary>
    [JsonPropertyName("size")]
    public MediaSize Size { get; set; } = new();

    /// <summary>
    /// Tipo MIME da mídia.
    /// </summary>
    [JsonPropertyName("mimetype")]
    public string MimeType { get; set; } = string.Empty;

    /// <summary>
    /// Conteúdo da mídia em base64.
    /// </summary>
    [JsonPropertyName("base64")]
    public string Base64 { get; set; } = string.Empty;

    /// <summary>
    /// Buffer da mídia (geralmente null quando base64 está presente).
    /// </summary>
    [JsonPropertyName("buffer")]
    public object? Buffer { get; set; }
}

/// <summary>
/// Representa as informações de tamanho da mídia.
/// </summary>
public class MediaSize
{
    /// <summary>
    /// Tamanho do arquivo em bytes.
    /// </summary>
    [JsonPropertyName("fileLength")]
    public string FileLength { get; set; } = string.Empty;

    /// <summary>
    /// Altura da imagem/vídeo (se aplicável).
    /// </summary>
    [JsonPropertyName("height")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Height { get; set; }

    /// <summary>
    /// Largura da imagem/vídeo (se aplicável).
    /// </summary>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Width { get; set; }
}