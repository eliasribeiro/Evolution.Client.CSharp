namespace Evolution.Client.Modules;

/// <summary>
/// Requisição para configuração de settings
/// </summary>
public class SetSettingsRequest
{
    /// <summary>
    /// Rejeitar chamadas automaticamente
    /// </summary>
    public bool? RejectCall { get; set; }

    /// <summary>
    /// Mensagem para chamadas rejeitadas
    /// </summary>
    public string? MsgCall { get; set; }

    /// <summary>
    /// Ignorar grupos
    /// </summary>
    public bool? GroupsIgnore { get; set; }

    /// <summary>
    /// Sempre online
    /// </summary>
    public bool? AlwaysOnline { get; set; }

    /// <summary>
    /// Ler mensagens automaticamente
    /// </summary>
    public bool? ReadMessages { get; set; }

    /// <summary>
    /// Ler status automaticamente
    /// </summary>
    public bool? ReadStatus { get; set; }

    /// <summary>
    /// Sincronizar histórico completo
    /// </summary>
    public bool? SyncFullHistory { get; set; }
}

/// <summary>
/// Resposta da configuração de settings
/// </summary>
public class SetSettingsResponse
{
    /// <summary>
    /// Dados das configurações
    /// </summary>
    public SettingsData? Settings { get; set; }
}

/// <summary>
/// Dados das configurações
/// </summary>
public class SettingsData
{
    /// <summary>
    /// Nome da instância
    /// </summary>
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// Configurações da instância
    /// </summary>
    public SettingsConfig? Settings { get; set; }
}

/// <summary>
/// Configurações da instância
/// </summary>
public class SettingsConfig
{
    /// <summary>
    /// Rejeitar chamadas
    /// </summary>
    public bool RejectCall { get; set; }

    /// <summary>
    /// Mensagem para chamadas
    /// </summary>
    public string? MsgCall { get; set; }

    /// <summary>
    /// Ignorar grupos
    /// </summary>
    public bool GroupsIgnore { get; set; }

    /// <summary>
    /// Sempre online
    /// </summary>
    public bool AlwaysOnline { get; set; }

    /// <summary>
    /// Ler mensagens
    /// </summary>
    public bool ReadMessages { get; set; }

    /// <summary>
    /// Ler status
    /// </summary>
    public bool ReadStatus { get; set; }

    /// <summary>
    /// Sincronizar histórico completo
    /// </summary>
    public bool SyncFullHistory { get; set; }
}
