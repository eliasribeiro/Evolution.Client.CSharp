using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message;

/// <summary>
/// Representa a requisição para enviar mídia.
/// </summary>
public class SendMediaRequest
{
    /// <summary>
    /// Número do telefone do destinatário com código do país.
    /// </summary>
    [JsonPropertyName("number")]
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Tipo da mídia (image, video, document).
    /// </summary>
    [JsonPropertyName("mediatype")]
    public string MediaType { get; set; } = string.Empty;

    /// <summary>
    /// Tipo MIME da mídia (ex: image/png).
    /// </summary>
    [JsonPropertyName("mimetype")]
    public string MimeType { get; set; } = string.Empty;

    /// <summary>
    /// Legenda da mídia.
    /// </summary>
    [JsonPropertyName("caption")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Caption { get; set; }

    /// <summary>
    /// URL ou base64 da mídia.
    /// </summary>
    [JsonPropertyName("media")]
    public string Media { get; set; } = string.Empty;

    /// <summary>
    /// Nome do arquivo.
    /// </summary>
    [JsonPropertyName("fileName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FileName { get; set; }

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

/// <summary>
/// Enumeração para os tipos de mídia disponíveis.
/// </summary>
public static class MediaType
{
    /// <summary>
    /// Imagem.
    /// </summary>
    public const string Image = "image";

    /// <summary>
    /// Vídeo.
    /// </summary>
    public const string Video = "video";

    /// <summary>
    /// Documento.
    /// </summary>
    public const string Document = "document";
}

/// <summary>
/// Enumeração para tipos MIME comuns.
/// </summary>
public static class MimeType
{
    // Imagens
    public const string ImagePng = "image/png";
    public const string ImageJpeg = "image/jpeg";
    public const string ImageGif = "image/gif";
    public const string ImageWebp = "image/webp";

    // Vídeos
    public const string VideoMp4 = "video/mp4";
    public const string VideoAvi = "video/avi";
    public const string VideoMov = "video/mov";

    // Documentos
    public const string ApplicationPdf = "application/pdf";
    public const string ApplicationDoc = "application/msword";
    public const string ApplicationDocx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
    public const string ApplicationXls = "application/vnd.ms-excel";
    public const string ApplicationXlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    public const string TextPlain = "text/plain";
}