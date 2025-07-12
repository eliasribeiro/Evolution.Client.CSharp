namespace Evolution.Client.Modules;

/// <summary>
/// Requisição para criar/configurar TypeBot
/// </summary>
public class CreateTypeBotRequest
{
    /// <summary>
    /// Indica se o TypeBot está habilitado
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// URL do TypeBot
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// ID do TypeBot
    /// </summary>
    public string Typebot { get; set; } = string.Empty;

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
}

/// <summary>
/// Requisição para atualizar TypeBot
/// </summary>
public class UpdateTypeBotRequest : CreateTypeBotRequest
{
}

/// <summary>
/// Requisição para iniciar TypeBot
/// </summary>
public class StartTypeBotRequest
{
    /// <summary>
    /// Número do contato
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Variáveis para o TypeBot
    /// </summary>
    public Dictionary<string, object>? Variables { get; set; }
}

/// <summary>
/// Requisição para configurar settings do TypeBot
/// </summary>
public class TypeBotSettingsRequest
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
public class ChangeSessionStatusRequest
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
/// Resposta da criação/configuração do TypeBot
/// </summary>
public class TypeBotResponse
{
    /// <summary>
    /// Dados do TypeBot
    /// </summary>
    public TypeBotData? TypeBot { get; set; }
}

/// <summary>
/// Dados do TypeBot
/// </summary>
public class TypeBotData
{
    /// <summary>
    /// Nome da instância
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Configuração do TypeBot
    /// </summary>
    public TypeBotConfig? TypeBot { get; set; }
}

/// <summary>
/// Configuração do TypeBot
/// </summary>
public class TypeBotConfig
{
    /// <summary>
    /// Indica se está habilitado
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// URL do TypeBot
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// ID do TypeBot
    /// </summary>
    public string? Typebot { get; set; }

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
}

/// <summary>
/// Resposta da busca de TypeBots
/// </summary>
public class TypeBotListResponse
{
    /// <summary>
    /// Lista de TypeBots
    /// </summary>
    public TypeBotData[]? TypeBots { get; set; }
}

/// <summary>
/// Resposta da sessão do TypeBot
/// </summary>
public class TypeBotSessionResponse
{
    /// <summary>
    /// Dados da sessão
    /// </summary>
    public TypeBotSessionData? Session { get; set; }
}

/// <summary>
    /// Dados da sessão do TypeBot
/// </summary>
public class TypeBotSessionData
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

    /// <summary>
    /// Variáveis da sessão
    /// </summary>
    public Dictionary<string, object>? Variables { get; set; }
}

/// <summary>
/// Resposta das configurações do TypeBot
/// </summary>
public class TypeBotSettingsResponse
{
    /// <summary>
    /// Configurações do TypeBot
    /// </summary>
    public TypeBotSettingsData? Settings { get; set; }
}

/// <summary>
/// Dados das configurações do TypeBot
/// </summary>
public class TypeBotSettingsData
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
