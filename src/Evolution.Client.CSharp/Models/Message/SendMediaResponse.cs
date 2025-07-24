using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message;

/// <summary>
/// Representa a resposta do envio de mídia.
/// </summary>
public class SendMediaResponse
{
    /// <summary>
    /// Chave da mensagem de mídia enviada.
    /// </summary>
    [JsonPropertyName("key")]
    public MessageKey Key { get; set; } = new();

    /// <summary>
    /// Conteúdo da mensagem de mídia enviada.
    /// </summary>
    [JsonPropertyName("message")]
    public SentMediaContent Message { get; set; } = new();

    /// <summary>
    /// Timestamp da mensagem de mídia.
    /// </summary>
    [JsonPropertyName("messageTimestamp")]
    public string MessageTimestamp { get; set; } = string.Empty;

    /// <summary>
    /// Status da mensagem.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Participante no chat para quem a mensagem foi enviada.
    /// </summary>
    [JsonPropertyName("participant")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Participant { get; set; }
}

/// <summary>
/// Representa o conteúdo de uma mensagem de mídia enviada.
/// </summary>
public class SentMediaContent
{
    /// <summary>
    /// Mensagem de imagem.
    /// </summary>
    [JsonPropertyName("imageMessage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MediaMessage? ImageMessage { get; set; }

    /// <summary>
    /// Mensagem de vídeo.
    /// </summary>
    [JsonPropertyName("videoMessage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MediaMessage? VideoMessage { get; set; }

    /// <summary>
    /// Mensagem de documento.
    /// </summary>
    [JsonPropertyName("documentMessage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DocumentMessage? DocumentMessage { get; set; }
}

/// <summary>
/// Representa uma mensagem de mídia (imagem ou vídeo).
/// </summary>
public class MediaMessage
{
    /// <summary>
    /// URL da mídia.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Tipo MIME da mídia.
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
    /// Tamanho do arquivo em bytes.
    /// </summary>
    [JsonPropertyName("fileLength")]
    public string FileLength { get; set; } = string.Empty;

    /// <summary>
    /// Altura da mídia (para imagens e vídeos).
    /// </summary>
    [JsonPropertyName("height")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Height { get; set; }

    /// <summary>
    /// Largura da mídia (para imagens e vídeos).
    /// </summary>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Width { get; set; }

    /// <summary>
    /// Duração do vídeo em segundos.
    /// </summary>
    [JsonPropertyName("seconds")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Seconds { get; set; }
}

/// <summary>
/// Representa uma mensagem de documento.
/// </summary>
public class DocumentMessage
{
    /// <summary>
    /// URL do documento.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Tipo MIME do documento.
    /// </summary>
    [JsonPropertyName("mimetype")]
    public string MimeType { get; set; } = string.Empty;

    /// <summary>
    /// Nome do arquivo.
    /// </summary>
    [JsonPropertyName("fileName")]
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Legenda do documento.
    /// </summary>
    [JsonPropertyName("caption")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Caption { get; set; }

    /// <summary>
    /// Tamanho do arquivo em bytes.
    /// </summary>
    [JsonPropertyName("fileLength")]
    public string FileLength { get; set; } = string.Empty;
}