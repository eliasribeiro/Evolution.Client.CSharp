using System.ComponentModel.DataAnnotations;

namespace Evolution.Client.CSharp.WebApp.Models;

/// <summary>
/// ViewModel para atualizar a foto do grupo.
/// </summary>
public class UpdateGroupPictureViewModel
{
    /// <summary>
    /// Nome da instância.
    /// </summary>
    [Required(ErrorMessage = "Nome da instância é obrigatório")]
    [Display(Name = "Nome da Instância")]
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// ID do grupo.
    /// </summary>
    [Required(ErrorMessage = "ID do grupo é obrigatório")]
    [Display(Name = "ID do Grupo")]
    public string GroupJid { get; set; } = string.Empty;

    /// <summary>
    /// Imagem da foto do grupo (Base64).
    /// </summary>
    [Required(ErrorMessage = "Imagem é obrigatória")]
    [Display(Name = "Imagem (Base64)")]
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Indica se houve erro na operação.
    /// </summary>
    public bool HasError { get; set; }

    /// <summary>
    /// Mensagem de erro, se houver.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Resultado da operação.
    /// </summary>
    public string? Result { get; set; }
}

/// <summary>
/// ViewModel para atualizar o assunto do grupo.
/// </summary>
public class UpdateGroupSubjectViewModel
{
    /// <summary>
    /// Nome da instância.
    /// </summary>
    [Required(ErrorMessage = "Nome da instância é obrigatório")]
    [Display(Name = "Nome da Instância")]
    public string InstanceName { get; set; } = string.Empty;

    /// <summary>
    /// ID do grupo.
    /// </summary>
    [Required(ErrorMessage = "ID do grupo é obrigatório")]
    [Display(Name = "ID do Grupo")]
    public string GroupJid { get; set; } = string.Empty;

    /// <summary>
    /// Novo assunto do grupo.
    /// </summary>
    [Required(ErrorMessage = "Assunto é obrigatório")]
    [Display(Name = "Assunto")]
    [StringLength(100, ErrorMessage = "O assunto deve ter no máximo 100 caracteres")]
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// Indica se houve erro na operação.
    /// </summary>
    public bool HasError { get; set; }

    /// <summary>
    /// Mensagem de erro, se houver.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Resultado da operação.
    /// </summary>
    public string? Result { get; set; }
}

/// <summary>
/// Representa um resultado de grupo.
/// </summary>
public class GroupResult
{
    /// <summary>
    /// ID do grupo.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Assunto do grupo.
    /// </summary>
    public string? Subject { get; set; }

    /// <summary>
    /// Proprietário do assunto.
    /// </summary>
    public string? SubjectOwner { get; set; }

    /// <summary>
    /// Tempo do assunto.
    /// </summary>
    public long SubjectTime { get; set; }

    /// <summary>
    /// Data de criação.
    /// </summary>
    public long Creation { get; set; }

    /// <summary>
    /// Proprietário do grupo.
    /// </summary>
    public string? Owner { get; set; }

    /// <summary>
    /// Descrição do grupo.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Proprietário da descrição.
    /// </summary>
    public string? DescriptionOwner { get; set; }

    /// <summary>
    /// Tempo da descrição.
    /// </summary>
    public long DescriptionTime { get; set; }

    /// <summary>
    /// Lista de participantes.
    /// </summary>
    public List<GroupParticipant> Participants { get; set; } = new List<GroupParticipant>();

    /// <summary>
    /// Tamanho do grupo.
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// Indica se é um grupo de anúncios.
    /// </summary>
    public bool Announce { get; set; }

    /// <summary>
    /// Indica se é restrito.
    /// </summary>
    public bool Restrict { get; set; }
}

/// <summary>
/// Representa um participante do grupo.
/// </summary>
public class GroupParticipant
{
    /// <summary>
    /// ID do participante.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Indica se é administrador.
    /// </summary>
    public string? Admin { get; set; }
}