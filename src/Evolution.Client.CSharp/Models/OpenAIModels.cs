namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Requisição para criar bot OpenAI
/// </summary>
public class CreateOpenAIBotRequest
{
    /// <summary>
    /// Indica se o bot está habilitado
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// ID das credenciais OpenAI
    /// </summary>
    public string OpenaiCredsId { get; set; } = string.Empty;

    /// <summary>
    /// Tipo do bot
    /// </summary>
    public string BotType { get; set; } = string.Empty;

    /// <summary>
    /// ID do assistente
    /// </summary>
    public string AssistantId { get; set; } = string.Empty;

    /// <summary>
    /// URL da função
    /// </summary>
    public string FunctionUrl { get; set; } = string.Empty;

    /// <summary>
    /// Modelo a ser usado
    /// </summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Mensagens do sistema
    /// </summary>
    public string[] SystemMessages { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Mensagens do assistente
    /// </summary>
    public string[] AssistantMessages { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Mensagens do usuário
    /// </summary>
    public string[] UserMessages { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Máximo de tokens
    /// </summary>
    public int MaxTokens { get; set; }

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
/// Requisição para atualizar bot OpenAI
/// </summary>
public class UpdateOpenAIBotRequest : CreateOpenAIBotRequest
{
}

/// <summary>
/// Requisição para configurar credenciais OpenAI
/// </summary>
public class SetOpenAICredsRequest
{
    /// <summary>
    /// Nome das credenciais
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Chave da API OpenAI
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// ID da organização (opcional)
    /// </summary>
    public string? OrganizationId { get; set; }
}

/// <summary>
/// Requisição para configurar settings OpenAI
/// </summary>
public class OpenAISettingsRequest
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
/// Requisição para alterar status
/// </summary>
public class ChangeOpenAIStatusRequest
{
    /// <summary>
    /// Número do contato
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Status (opened/closed/paused)
    /// </summary>
    public string Status { get; set; } = string.Empty;
}

/// <summary>
/// Resposta do bot OpenAI
/// </summary>
public class OpenAIBotResponse
{
    /// <summary>
    /// Dados do bot
    /// </summary>
    public OpenAIBotData? Bot { get; set; }
}

/// <summary>
/// Dados do bot OpenAI
/// </summary>
public class OpenAIBotData
{
    /// <summary>
    /// Nome da instância
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Configuração do bot
    /// </summary>
    public OpenAIBotConfig? Bot { get; set; }
}

/// <summary>
/// Configuração do bot OpenAI
/// </summary>
public class OpenAIBotConfig
{
    /// <summary>
    /// Indica se está habilitado
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// ID das credenciais OpenAI
    /// </summary>
    public string? OpenaiCredsId { get; set; }

    /// <summary>
    /// Tipo do bot
    /// </summary>
    public string? BotType { get; set; }

    /// <summary>
    /// ID do assistente
    /// </summary>
    public string? AssistantId { get; set; }

    /// <summary>
    /// URL da função
    /// </summary>
    public string? FunctionUrl { get; set; }

    /// <summary>
    /// Modelo
    /// </summary>
    public string? Model { get; set; }

    /// <summary>
    /// Mensagens do sistema
    /// </summary>
    public string[]? SystemMessages { get; set; }

    /// <summary>
    /// Mensagens do assistente
    /// </summary>
    public string[]? AssistantMessages { get; set; }

    /// <summary>
    /// Mensagens do usuário
    /// </summary>
    public string[]? UserMessages { get; set; }

    /// <summary>
    /// Máximo de tokens
    /// </summary>
    public int? MaxTokens { get; set; }

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
/// Resposta da lista de bots OpenAI
/// </summary>
public class OpenAIBotListResponse
{
    /// <summary>
    /// Lista de bots
    /// </summary>
    public OpenAIBotData[]? Bots { get; set; }
}

/// <summary>
/// Resposta das credenciais OpenAI
/// </summary>
public class OpenAICredsResponse
{
    /// <summary>
    /// Dados das credenciais
    /// </summary>
    public OpenAICredsData? Creds { get; set; }
}

/// <summary>
/// Dados das credenciais OpenAI
/// </summary>
public class OpenAICredsData
{
    /// <summary>
    /// ID das credenciais
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Nome das credenciais
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Chave da API (mascarada)
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// ID da organização
    /// </summary>
    public string? OrganizationId { get; set; }
}

/// <summary>
/// Resposta das configurações OpenAI
/// </summary>
public class OpenAISettingsResponse
{
    /// <summary>
    /// Configurações
    /// </summary>
    public OpenAISettingsData? Settings { get; set; }
}

/// <summary>
/// Dados das configurações OpenAI
/// </summary>
public class OpenAISettingsData
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
/// Resposta da sessão OpenAI
/// </summary>
public class OpenAISessionResponse
{
    /// <summary>
    /// Dados da sessão
    /// </summary>
    public OpenAISessionData? Session { get; set; }
}

/// <summary>
/// Dados da sessão OpenAI
/// </summary>
public class OpenAISessionData
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
