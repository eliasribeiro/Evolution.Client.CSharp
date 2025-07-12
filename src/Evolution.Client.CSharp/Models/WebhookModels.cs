namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Requisição para configuração de webhook
/// </summary>
public class SetWebhookRequest
{
    /// <summary>
    /// Indica se o webhook está habilitado
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// URL do webhook
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Enviar webhook por eventos
    /// </summary>
    public bool WebhookByEvents { get; set; } = true;

    /// <summary>
    /// Enviar dados em base64
    /// </summary>
    public bool WebhookBase64 { get; set; } = true;

    /// <summary>
    /// Eventos a serem enviados
    /// </summary>
    public string[] Events { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Headers customizados
    /// </summary>
    public Dictionary<string, string>? Headers { get; set; }
}

/// <summary>
/// Resposta da configuração de webhook
/// </summary>
public class SetWebhookResponse
{
    /// <summary>
    /// Dados do webhook
    /// </summary>
    public WebhookData? Webhook { get; set; }
}

/// <summary>
/// Dados do webhook
/// </summary>
public class WebhookData
{
    /// <summary>
    /// Nome da instância
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Configuração do webhook
    /// </summary>
    public WebhookConfig? Webhook { get; set; }
}

/// <summary>
/// Configuração do webhook
/// </summary>
public class WebhookConfig
{
    /// <summary>
    /// URL do webhook
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Eventos configurados
    /// </summary>
    public string[]? Events { get; set; }

    /// <summary>
    /// Indica se está habilitado
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Enviar por eventos
    /// </summary>
    public bool? WebhookByEvents { get; set; }

    /// <summary>
    /// Enviar em base64
    /// </summary>
    public bool? WebhookBase64 { get; set; }

    /// <summary>
    /// Headers customizados
    /// </summary>
    public Dictionary<string, string>? Headers { get; set; }
}
