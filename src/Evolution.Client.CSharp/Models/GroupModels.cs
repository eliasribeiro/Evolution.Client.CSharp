namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Requisição para criação de grupo
/// </summary>
public class CreateGroupRequest
{
    /// <summary>
    /// Nome/assunto do grupo
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Descrição do grupo
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Participantes iniciais
    /// </summary>
    public string[] Participants { get; set; } = Array.Empty<string>();
}

/// <summary>
/// Requisição para atualizar foto do grupo
/// </summary>
public class UpdateGroupPictureRequest
{
    /// <summary>
    /// Imagem em base64 ou URL
    /// </summary>
    public string Image { get; set; } = string.Empty;
}

/// <summary>
/// Requisição para atualizar assunto do grupo
/// </summary>
public class UpdateGroupSubjectRequest
{
    /// <summary>
    /// Novo assunto do grupo
    /// </summary>
    public string Subject { get; set; } = string.Empty;
}

/// <summary>
/// Requisição para atualizar descrição do grupo
/// </summary>
public class UpdateGroupDescriptionRequest
{
    /// <summary>
    /// Nova descrição do grupo
    /// </summary>
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// Requisição para enviar convite do grupo
/// </summary>
public class SendGroupInviteRequest
{
    /// <summary>
    /// Números para enviar o convite
    /// </summary>
    public string[] Numbers { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Texto do convite
    /// </summary>
    public string? Text { get; set; }
}

/// <summary>
/// Requisição para atualizar participantes do grupo
/// </summary>
public class UpdateParticipantRequest
{
    /// <summary>
    /// Ação a ser executada (add, remove, promote, demote)
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Lista de participantes
    /// </summary>
    public string[] Participants { get; set; } = Array.Empty<string>();
}

/// <summary>
/// Requisição para atualizar configurações do grupo
/// </summary>
public class UpdateGroupSettingRequest
{
    /// <summary>
    /// Configuração a ser alterada (announcement, locked, not_announcement, unlocked)
    /// </summary>
    public string Action { get; set; } = string.Empty;
}

/// <summary>
/// Requisição para alternar mensagens efêmeras
/// </summary>
public class ToggleEphemeralRequest
{
    /// <summary>
    /// Duração das mensagens efêmeras em segundos (0 para desabilitar)
    /// </summary>
    public int Expiration { get; set; }
}

/// <summary>
/// Informações do grupo
/// </summary>
public class GroupInfo
{
    /// <summary>
    /// ID do grupo
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Assunto/nome do grupo
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Proprietário do assunto
    /// </summary>
    public string? SubjectOwner { get; set; }

    /// <summary>
    /// Timestamp da última alteração do assunto
    /// </summary>
    public long? SubjectTime { get; set; }

    /// <summary>
    /// URL da foto do grupo
    /// </summary>
    public string? PictureUrl { get; set; }

    /// <summary>
    /// Tamanho do grupo (número de participantes)
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// Timestamp de criação do grupo
    /// </summary>
    public long Creation { get; set; }

    /// <summary>
    /// Proprietário do grupo
    /// </summary>
    public string Owner { get; set; } = string.Empty;

    /// <summary>
    /// Descrição do grupo
    /// </summary>
    public string? Desc { get; set; }

    /// <summary>
    /// ID da descrição
    /// </summary>
    public string? DescId { get; set; }

    /// <summary>
    /// Indica se o grupo está restrito
    /// </summary>
    public bool Restrict { get; set; }

    /// <summary>
    /// Indica se apenas admins podem enviar mensagens
    /// </summary>
    public bool Announce { get; set; }

    /// <summary>
    /// Participantes do grupo
    /// </summary>
    public GroupParticipant[]? Participants { get; set; }
}

/// <summary>
/// Participante do grupo
/// </summary>
public class GroupParticipant
{
    /// <summary>
    /// Número do participante
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Nome do participante
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Indica se é administrador
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// Data de entrada no grupo
    /// </summary>
    public DateTime? JoinedAt { get; set; }
}

/// <summary>
/// Requisição para adicionar participantes
/// </summary>
public class AddParticipantsRequest
{
    /// <summary>
    /// Números dos participantes
    /// </summary>
    public string[] Participants { get; set; } = Array.Empty<string>();
}

/// <summary>
/// Requisição para remover participantes
/// </summary>
public class RemoveParticipantsRequest
{
    /// <summary>
    /// Números dos participantes
    /// </summary>
    public string[] Participants { get; set; } = Array.Empty<string>();
}

/// <summary>
/// Resposta do código de convite do grupo
/// </summary>
public class GroupInviteCodeResponse
{
    /// <summary>
    /// Código de convite
    /// </summary>
    public string InviteCode { get; set; } = string.Empty;

    /// <summary>
    /// URL de convite
    /// </summary>
    public string InviteUrl { get; set; } = string.Empty;
}

/// <summary>
/// Resposta de operação do grupo
/// </summary>
public class GroupOperationResponse
{
    /// <summary>
    /// Indica se a operação foi bem-sucedida
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Mensagem da resposta
    /// </summary>
    public string? Message { get; set; }
}

/// <summary>
/// Resposta de busca de grupo por código de convite
/// </summary>
public class GroupByInviteCodeResponse
{
    /// <summary>
    /// Informações do grupo encontrado
    /// </summary>
    public GroupInfo? Group { get; set; }
}

/// <summary>
/// Requisição para atualizar grupo (mantido para compatibilidade)
/// </summary>
public class UpdateGroupRequest
{
    /// <summary>
    /// Novo nome do grupo
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Nova descrição do grupo
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// URL da nova foto do grupo
    /// </summary>
    public string? PictureUrl { get; set; }
}
