namespace Evolution.Client.Modules;

/// <summary>
/// Requisição para configurar Chatwoot
/// </summary>
public class SetChatwootRequest
{
    /// <summary>
    /// Indica se está habilitado
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// ID da conta Chatwoot
    /// </summary>
    public string AccountId { get; set; } = string.Empty;

    /// <summary>
    /// Token de acesso do Chatwoot
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// URL do Chatwoot
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Assinar mensagens
    /// </summary>
    public bool SignMsg { get; set; } = true;

    /// <summary>
    /// Reabrir conversa automaticamente
    /// </summary>
    public bool ReopenConversation { get; set; } = true;

    /// <summary>
    /// Manter conversa como pendente
    /// </summary>
    public bool ConversationPending { get; set; } = true;

    /// <summary>
    /// Nome da caixa de entrada
    /// </summary>
    public string NameInbox { get; set; } = string.Empty;

    /// <summary>
    /// Mesclar contatos do Brasil
    /// </summary>
    public bool MergeBrazilContacts { get; set; } = true;

    /// <summary>
    /// Importar contatos automaticamente
    /// </summary>
    public bool ImportContacts { get; set; } = true;

    /// <summary>
    /// Importar mensagens automaticamente
    /// </summary>
    public bool ImportMessages { get; set; } = true;

    /// <summary>
    /// Limite de dias para importar mensagens
    /// </summary>
    public int DaysLimitImportMessages { get; set; } = 7;

    /// <summary>
    /// Delimitador de assinatura
    /// </summary>
    public string SignDelimiter { get; set; } = "\n";

    /// <summary>
    /// Criar conversa automaticamente
    /// </summary>
    public bool AutoCreate { get; set; } = true;

    /// <summary>
    /// Nome da organização
    /// </summary>
    public string Organization { get; set; } = string.Empty;

    /// <summary>
    /// URL do logo
    /// </summary>
    public string Logo { get; set; } = string.Empty;

    /// <summary>
    /// JIDs a ignorar
    /// </summary>
    public string[] IgnoreJids { get; set; } = Array.Empty<string>();
}

/// <summary>
/// Resposta da configuração do Chatwoot
/// </summary>
public class ChatwootResponse
{
    /// <summary>
    /// Dados do Chatwoot configurado
    /// </summary>
    public ChatwootData? Chatwoot { get; set; }

    /// <summary>
    /// Mensagem de status
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Indica se a operação foi bem-sucedida
    /// </summary>
    public bool Success { get; set; }
}

/// <summary>
/// Dados do Chatwoot
/// </summary>
public class ChatwootData
{
    /// <summary>
    /// Nome da instância
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Configuração do Chatwoot
    /// </summary>
    public ChatwootIntegrationConfig? Chatwoot { get; set; }

    /// <summary>
    /// Data de criação
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Data de atualização
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Configuração detalhada do Chatwoot para integração
/// </summary>
public class ChatwootIntegrationConfig
{
    /// <summary>
    /// Indica se está habilitado
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// ID da conta (mascarado para segurança)
    /// </summary>
    public string? AccountId { get; set; }

    /// <summary>
    /// Token de acesso (mascarado para segurança)
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// URL do Chatwoot
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Assinar mensagens
    /// </summary>
    public bool? SignMsg { get; set; }

    /// <summary>
    /// Reabrir conversa
    /// </summary>
    public bool? ReopenConversation { get; set; }

    /// <summary>
    /// Conversa pendente
    /// </summary>
    public bool? ConversationPending { get; set; }

    /// <summary>
    /// Nome da caixa de entrada
    /// </summary>
    public string? NameInbox { get; set; }

    /// <summary>
    /// Mesclar contatos do Brasil
    /// </summary>
    public bool? MergeBrazilContacts { get; set; }

    /// <summary>
    /// Importar contatos
    /// </summary>
    public bool? ImportContacts { get; set; }

    /// <summary>
    /// Importar mensagens
    /// </summary>
    public bool? ImportMessages { get; set; }

    /// <summary>
    /// Limite de dias para importar mensagens
    /// </summary>
    public int? DaysLimitImportMessages { get; set; }

    /// <summary>
    /// Delimitador de assinatura
    /// </summary>
    public string? SignDelimiter { get; set; }

    /// <summary>
    /// Criar automaticamente
    /// </summary>
    public bool? AutoCreate { get; set; }

    /// <summary>
    /// Organização
    /// </summary>
    public string? Organization { get; set; }

    /// <summary>
    /// Logo
    /// </summary>
    public string? Logo { get; set; }

    /// <summary>
    /// JIDs ignorados
    /// </summary>
    public string[]? IgnoreJids { get; set; }
}

/// <summary>
/// Estatísticas do Chatwoot
/// </summary>
public class ChatwootStats
{
    /// <summary>
    /// Total de conversas
    /// </summary>
    public int TotalConversations { get; set; }

    /// <summary>
    /// Conversas abertas
    /// </summary>
    public int OpenConversations { get; set; }

    /// <summary>
    /// Conversas resolvidas
    /// </summary>
    public int ResolvedConversations { get; set; }

    /// <summary>
    /// Conversas pendentes
    /// </summary>
    public int PendingConversations { get; set; }

    /// <summary>
    /// Total de mensagens enviadas
    /// </summary>
    public int MessagesSent { get; set; }

    /// <summary>
    /// Total de mensagens recebidas
    /// </summary>
    public int MessagesReceived { get; set; }

    /// <summary>
    /// Última sincronização
    /// </summary>
    public DateTime? LastSync { get; set; }
}

/// <summary>
/// Resposta das estatísticas do Chatwoot
/// </summary>
public class ChatwootStatsResponse
{
    /// <summary>
    /// Estatísticas do Chatwoot
    /// </summary>
    public ChatwootStats? Stats { get; set; }

    /// <summary>
    /// Nome da instância
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;
}
