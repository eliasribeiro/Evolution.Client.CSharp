using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Message;

/// <summary>
/// Representa a requisição para enviar um status.
/// </summary>
public class SendStatusRequest
{
    /// <summary>
    /// Tipo do status.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Conteúdo do status.
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Legenda opcional para imagem ou vídeo.
    /// </summary>
    [JsonPropertyName("caption")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Caption { get; set; }

    /// <summary>
    /// Cor de fundo (exemplo: #008000).
    /// </summary>
    [JsonPropertyName("backgroundColor")]
    public string BackgroundColor { get; set; } = string.Empty;

    /// <summary>
    /// Fonte do texto.
    /// 1 = SERIF, 2 = NORICAN_REGULAR, 3 = BRYNDAN_WRITE, 4 = BEBASNEUE_REGULAR, 5 = OSWALD_HEAVY
    /// </summary>
    [JsonPropertyName("font")]
    public int Font { get; set; }

    /// <summary>
    /// Indica se deve enviar para todos os contatos ou apenas para a lista especificada.
    /// </summary>
    [JsonPropertyName("allContacts")]
    public bool AllContacts { get; set; }

    /// <summary>
    /// Lista de números para enviar o status (quando AllContacts = false).
    /// </summary>
    [JsonPropertyName("statusJidList")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? StatusJidList { get; set; }
}

/// <summary>
/// Enumeração para os tipos de status disponíveis.
/// </summary>
public static class StatusType
{
    /// <summary>
    /// Status de texto.
    /// </summary>
    public const string Text = "text";

    /// <summary>
    /// Status de imagem.
    /// </summary>
    public const string Image = "image";

    /// <summary>
    /// Status de áudio.
    /// </summary>
    public const string Audio = "audio";
}

/// <summary>
/// Enumeração para as fontes disponíveis.
/// </summary>
public static class StatusFont
{
    /// <summary>
    /// Fonte SERIF.
    /// </summary>
    public const int Serif = 1;

    /// <summary>
    /// Fonte NORICAN_REGULAR.
    /// </summary>
    public const int NoricanRegular = 2;

    /// <summary>
    /// Fonte BRYNDAN_WRITE.
    /// </summary>
    public const int BryndanWrite = 3;

    /// <summary>
    /// Fonte BEBASNEUE_REGULAR.
    /// </summary>
    public const int BebasneueRegular = 4;

    /// <summary>
    /// Fonte OSWALD_HEAVY.
    /// </summary>
    public const int OswaldHeavy = 5;
}