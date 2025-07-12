namespace Evolution.Client.Modules;

/// <summary>
/// Requisição para buscar perfil de negócio
/// </summary>
public class FetchBusinessProfileRequest
{
    /// <summary>
    /// Número do contato
    /// </summary>
    public string Number { get; set; } = string.Empty;
}

/// <summary>
/// Requisição para buscar perfil
/// </summary>
public class FetchProfileRequest
{
    /// <summary>
    /// Número do contato
    /// </summary>
    public string Number { get; set; } = string.Empty;
}

/// <summary>
/// Requisição para atualizar nome do perfil
/// </summary>
public class UpdateProfileNameRequest
{
    /// <summary>
    /// Novo nome do perfil
    /// </summary>
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Requisição para atualizar status do perfil
/// </summary>
public class UpdateProfileStatusRequest
{
    /// <summary>
    /// Novo status do perfil
    /// </summary>
    public string Status { get; set; } = string.Empty;
}

/// <summary>
/// Requisição para atualizar foto do perfil
/// </summary>
public class UpdateProfilePictureRequest
{
    /// <summary>
    /// URL da nova foto do perfil
    /// </summary>
    public string Picture { get; set; } = string.Empty;
}

/// <summary>
/// Requisição para atualizar configurações de privacidade
/// </summary>
public class UpdatePrivacySettingsRequest
{
    /// <summary>
    /// Configuração de quem pode ver a última vez online
    /// </summary>
    public string? ReadReceipts { get; set; }

    /// <summary>
    /// Configuração de foto do perfil
    /// </summary>
    public string? Profile { get; set; }

    /// <summary>
    /// Configuração de status
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Configuração de última vez online
    /// </summary>
    public string? Online { get; set; }

    /// <summary>
    /// Configuração de grupos
    /// </summary>
    public string? GroupAdd { get; set; }

    /// <summary>
    /// Configuração de chamadas
    /// </summary>
    public string? CallAdd { get; set; }
}

/// <summary>
/// Perfil de negócio
/// </summary>
public class BusinessProfile
{
    /// <summary>
    /// ID do perfil
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Nome do negócio
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Categoria do negócio
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Email do negócio
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Descrição do negócio
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Endereço do negócio
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Website do negócio
    /// </summary>
    public string? Website { get; set; }
}

/// <summary>
/// Perfil do usuário
/// </summary>
public class UserProfile
{
    /// <summary>
    /// Nome do usuário
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Status do usuário
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// URL da foto do perfil
    /// </summary>
    public string? PictureUrl { get; set; }

    /// <summary>
    /// Número do usuário
    /// </summary>
    public string? Number { get; set; }
}

/// <summary>
/// Configurações de privacidade
/// </summary>
public class PrivacySettings
{
    /// <summary>
    /// Configuração de confirmação de leitura
    /// </summary>
    public string? ReadReceipts { get; set; }

    /// <summary>
    /// Configuração de foto do perfil
    /// </summary>
    public string? Profile { get; set; }

    /// <summary>
    /// Configuração de status
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Configuração de última vez online
    /// </summary>
    public string? Online { get; set; }

    /// <summary>
    /// Configuração de grupos
    /// </summary>
    public string? GroupAdd { get; set; }

    /// <summary>
    /// Configuração de chamadas
    /// </summary>
    public string? CallAdd { get; set; }
}

/// <summary>
/// Resposta de atualização de perfil
/// </summary>
public class UpdateProfileResponse
{
    /// <summary>
    /// Indica se a operação foi bem-sucedida
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Mensagem de resposta
    /// </summary>
    public string? Message { get; set; }
}
