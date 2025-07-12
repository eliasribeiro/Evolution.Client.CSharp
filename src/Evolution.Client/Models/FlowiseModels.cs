namespace Evolution.Client.Modules;

/// <summary>
/// Requisição para criar bot Flowise
/// </summary>
public class CreateFlowiseBotRequest
{
    /// <summary>
    /// Indica se o bot está habilitado
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Descrição do bot
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// URL da API Flowise
    /// </summary>
    public string ApiUrl { get; set; } = string.Empty;

    /// <summary>
    /// Chave da API Flowise
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// ID do fluxo
    /// </summary>
    public string FlowId { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de trigger
    /// </summary>
    public string TriggerType { get; set; } = string.Empty;

    /// <summary>
    /// Operador do trigger
    /// </summary>
    public string TriggerOperator { get; set; } = string.Empty;

    /// <summary>
    /// Valor do trigger
    /// </summary>
    public string TriggerValue { get; set; } = string.Empty;

    /// <summary>
    /// Tempo de expiração em minutos
    /// </summary>
    public int Expire { get; set; }

    /// <summary>
    /// Palavra-chave para finalizar
    /// </summary>
    public string KeywordFinish { get; set; } = string.Empty;

    /// <summary>
    /// Delay da mensagem em milissegundos
    /// </summary>
    public int DelayMessage { get; set; }

    /// <summary>
    /// Mensagem para comando desconhecido
    /// </summary>
    public string UnknownMessage { get; set; } = string.Empty;

    /// <summary>
    /// Escutar mensagens próprias
    /// </summary>
    public bool ListeningFromMe { get; set; }

    /// <summary>
    /// Parar bot de mensagens próprias
    /// </summary>
    public bool StopBotFromMe { get; set; }

    /// <summary>
    /// Manter conversa aberta
    /// </summary>
    public bool KeepOpen { get; set; }

    /// <summary>
    /// Tempo de debounce em milissegundos
    /// </summary>
    public int DebounceTime { get; set; }

    /// <summary>
    /// JIDs a ignorar
    /// </summary>
    public string[] IgnoreJids { get; set; } = Array.Empty<string>();
}

/// <summary>
/// Requisição para atualizar bot Flowise
/// </summary>
public class UpdateFlowiseBotRequest : CreateFlowiseBotRequest
{
}

/// <summary>
/// Requisição para configurar settings do Flowise
/// </summary>
public class FlowiseSettingsRequest
{
    /// <summary>
    /// Indica se está habilitado
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Descrição das configurações
    /// </summary>
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// Requisição para alterar status da sessão
/// </summary>
public class ChangeFlowiseSessionStatusRequest
{
    /// <summary>
    /// Número do contato
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Status da sessão (opened/closed/paused)
    /// </summary>
    public string Status { get; set; } = string.Empty;
}

/// <summary>
/// Resposta do bot Flowise
/// </summary>
public class FlowiseBotResponse
{
    /// <summary>
    /// Dados do bot
    /// </summary>
    public FlowiseBotData? Bot { get; set; }
}

/// <summary>
/// Dados do bot Flowise
/// </summary>
public class FlowiseBotData
{
    /// <summary>
    /// Nome da instância
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Configuração do bot
    /// </summary>
    public FlowiseBotConfig? Bot { get; set; }
}

/// <summary>
/// Configuração do bot Flowise
/// </summary>
public class FlowiseBotConfig
{
    /// <summary>
    /// Indica se está habilitado
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Descrição do bot
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// URL da API Flowise
    /// </summary>
    public string? ApiUrl { get; set; }

    /// <summary>
    /// Chave da API (mascarada)
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// ID do fluxo
    /// </summary>
    public string? FlowId { get; set; }

    /// <summary>
    /// Tipo de trigger
    /// </summary>
    public string? TriggerType { get; set; }

    /// <summary>
    /// Operador do trigger
    /// </summary>
    public string? TriggerOperator { get; set; }

    /// <summary>
    /// Valor do trigger
    /// </summary>
    public string? TriggerValue { get; set; }

    /// <summary>
    /// Tempo de expiração
    /// </summary>
    public int? Expire { get; set; }

    /// <summary>
    /// Palavra-chave para finalizar
    /// </summary>
    public string? KeywordFinish { get; set; }

    /// <summary>
    /// Delay da mensagem
    /// </summary>
    public int? DelayMessage { get; set; }

    /// <summary>
    /// Mensagem para comando desconhecido
    /// </summary>
    public string? UnknownMessage { get; set; }

    /// <summary>
    /// Escutar mensagens próprias
    /// </summary>
    public bool? ListeningFromMe { get; set; }

    /// <summary>
    /// Parar bot de mensagens próprias
    /// </summary>
    public bool? StopBotFromMe { get; set; }

    /// <summary>
    /// Manter conversa aberta
    /// </summary>
    public bool? KeepOpen { get; set; }

    /// <summary>
    /// Tempo de debounce
    /// </summary>
    public int? DebounceTime { get; set; }

    /// <summary>
    /// JIDs a ignorar
    /// </summary>
    public string[]? IgnoreJids { get; set; }
}

/// <summary>
/// Resposta da lista de bots Flowise
/// </summary>
public class FlowiseBotListResponse
{
    /// <summary>
    /// Lista de bots
    /// </summary>
    public FlowiseBotData[]? Bots { get; set; }
}

/// <summary>
/// Resposta das configurações do Flowise
/// </summary>
public class FlowiseSettingsResponse
{
    /// <summary>
    /// Configurações
    /// </summary>
    public FlowiseSettingsData? Settings { get; set; }
}

/// <summary>
/// Dados das configurações do Flowise
/// </summary>
public class FlowiseSettingsData
{
    /// <summary>
    /// Indica se está habilitado
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// Descrição
    /// </summary>
    public string? Description { get; set; }
}

/// <summary>
/// Resposta das sessões do Flowise
/// </summary>
public class FlowiseSessionListResponse
{
    /// <summary>
    /// Lista de sessões
    /// </summary>
    public FlowiseSessionData[]? Sessions { get; set; }
}

/// <summary>
/// Dados da sessão do Flowise
/// </summary>
public class FlowiseSessionData
{
    /// <summary>
    /// Número do contato
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Status da sessão
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Data de criação
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Data de atualização
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
