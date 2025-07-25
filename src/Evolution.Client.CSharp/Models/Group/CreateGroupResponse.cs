using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Group;

/// <summary>
/// Representa a resposta da criação de um grupo.
/// </summary>
public class CreateGroupResponse
{
    /// <summary>
    /// ID do grupo criado.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Assunto (nome) do grupo.
    /// </summary>
    [JsonPropertyName("subject")]
    public string? Subject { get; set; }

    /// <summary>
    /// Proprietário do assunto do grupo.
    /// </summary>
    [JsonPropertyName("subjectOwner")]
    public string? SubjectOwner { get; set; }

    /// <summary>
    /// Timestamp do assunto do grupo.
    /// </summary>
    [JsonPropertyName("subjectTime")]
    public long SubjectTime { get; set; }

    /// <summary>
    /// Tamanho do grupo (número de participantes).
    /// </summary>
    [JsonPropertyName("size")]
    public int Size { get; set; }

    /// <summary>
    /// Timestamp de criação do grupo.
    /// </summary>
    [JsonPropertyName("creation")]
    public long Creation { get; set; }

    /// <summary>
    /// Proprietário do grupo.
    /// </summary>
    [JsonPropertyName("owner")]
    public string? Owner { get; set; }

    /// <summary>
    /// Descrição do grupo.
    /// </summary>
    [JsonPropertyName("desc")]
    public string? Description { get; set; }

    /// <summary>
    /// ID da descrição do grupo.
    /// </summary>
    [JsonPropertyName("descId")]
    public string? DescriptionId { get; set; }

    /// <summary>
    /// Indica se o grupo é restrito.
    /// </summary>
    [JsonPropertyName("restrict")]
    public bool Restrict { get; set; }

    /// <summary>
    /// Indica se o grupo é apenas para anúncios.
    /// </summary>
    [JsonPropertyName("announce")]
    public bool Announce { get; set; }

    /// <summary>
    /// Indica se é uma comunidade.
    /// </summary>
    [JsonPropertyName("isCommunity")]
    public bool IsCommunity { get; set; }

    /// <summary>
    /// Indica se é um anúncio de comunidade.
    /// </summary>
    [JsonPropertyName("isCommunityAnnounce")]
    public bool IsCommunityAnnounce { get; set; }

    /// <summary>
    /// Indica se requer aprovação para entrar.
    /// </summary>
    [JsonPropertyName("joinApprovalMode")]
    public bool JoinApprovalMode { get; set; }

    /// <summary>
    /// Modo de adição de membros.
    /// </summary>
    [JsonPropertyName("memberAddMode")]
    public bool MemberAddMode { get; set; }

    /// <summary>
    /// Lista de participantes do grupo.
    /// </summary>
    [JsonPropertyName("participants")]
    public List<GroupParticipant>? Participants { get; set; }

    /// <summary>
    /// JID do grupo criado (propriedade adicional para compatibilidade).
    /// </summary>
    [JsonIgnore]
    public string? GroupJid => Id;
}

/// <summary>
/// Representa um participante do grupo.
/// </summary>
public class GroupParticipant
{
    /// <summary>
    /// JID do participante.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Tipo de administrador do participante.
    /// </summary>
    [JsonPropertyName("admin")]
    public string? Admin { get; set; }

    /// <summary>
    /// Indica se o participante é administrador (propriedade adicional para compatibilidade).
    /// </summary>
    [JsonIgnore]
    public bool IsAdmin => !string.IsNullOrEmpty(Admin);

    /// <summary>
    /// Indica se o participante é super administrador.
    /// </summary>
    [JsonIgnore]
    public bool IsSuperAdmin => Admin == "superadmin";
}