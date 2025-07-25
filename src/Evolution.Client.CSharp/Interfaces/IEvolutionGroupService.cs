using Evolution.Client.CSharp.Models.Group;

namespace Evolution.Client.CSharp.Interfaces;

/// <summary>
/// Interface para o serviço de grupos da API Evolution.
/// </summary>
public interface IEvolutionGroupService
{
    /// <summary>
    /// Cria um novo grupo no WhatsApp.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os dados do grupo a ser criado.</param>
    /// <returns>A resposta com os dados do grupo criado.</returns>
    Task<CreateGroupResponse> CreateGroupAsync(string instanceName, CreateGroupRequest request);

    /// <summary>
    /// Atualiza a foto de um grupo.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="groupJid">O JID do grupo.</param>
    /// <param name="request">A requisição contendo a nova imagem do grupo.</param>
    /// <returns>A resposta da operação.</returns>
    Task<UpdateGroupPictureResponse> UpdateGroupPictureAsync(string instanceName, string groupJid, UpdateGroupPictureRequest request);

    /// <summary>
    /// Atualiza o assunto (nome) de um grupo.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="groupJid">O JID do grupo.</param>
    /// <param name="request">A requisição contendo o novo assunto do grupo.</param>
    /// <returns>A resposta da operação.</returns>
    Task<UpdateGroupSubjectResponse> UpdateGroupSubjectAsync(string instanceName, string groupJid, UpdateGroupSubjectRequest request);

    /// <summary>
    /// Atualiza a descrição de um grupo.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="groupJid">O JID do grupo.</param>
    /// <param name="request">A requisição contendo a nova descrição do grupo.</param>
    /// <returns>A resposta da operação.</returns>
    Task<UpdateGroupDescriptionResponse> UpdateGroupDescriptionAsync(string instanceName, string groupJid, UpdateGroupDescriptionRequest request);

    /// <summary>
    /// Obtém o código de convite de um grupo.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="groupJid">O JID do grupo.</param>
    /// <returns>A resposta contendo o código de convite do grupo.</returns>
    Task<FetchInviteCodeResponse> FetchInviteCodeAsync(string instanceName, string groupJid);

    /// <summary>
    /// Revoga o código de convite de um grupo.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="groupJid">O JID do grupo.</param>
    /// <returns>A resposta da operação.</returns>
    Task<RevokeInviteCodeResponse> RevokeInviteCodeAsync(string instanceName, string groupJid);

    /// <summary>
    /// Envia um convite de grupo para números específicos.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="groupJid">O JID do grupo.</param>
    /// <param name="request">A requisição contendo os números para enviar o convite.</param>
    /// <returns>A resposta da operação.</returns>
    Task<SendGroupInviteResponse> SendGroupInviteAsync(string instanceName, string groupJid, SendGroupInviteRequest request);

    /// <summary>
    /// Find a group by its invite code
    /// </summary>
    /// <param name="instanceName">Name of the instance</param>
    /// <param name="inviteCode">Group invite code</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Group information</returns>
    Task<FindGroupByInviteCodeResponse> FindGroupByInviteCodeAsync(string instanceName, string inviteCode, CancellationToken cancellationToken = default);

    /// <summary>
    /// Find a group by its JID
    /// </summary>
    /// <param name="instanceName">Name of the instance</param>
    /// <param name="groupJid">Group JID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Group information with participants</returns>
    Task<FindGroupByJidResponse> FindGroupByJidAsync(string instanceName, string groupJid, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetch all groups
    /// </summary>
    /// <param name="instanceName">Name of the instance</param>
    /// <param name="getParticipants">Whether to include participants in the response</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of all groups</returns>
    Task<List<FetchAllGroupsResponse>> FetchAllGroupsAsync(string instanceName, bool getParticipants = true, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Busca grupos com base nos parâmetros fornecidos
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Parâmetros da busca</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista de grupos encontrados</returns>
    Task<List<FetchAllGroupsResponse>> FindGroupsAsync(string instanceName, FindGroupsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Find participants of a group
    /// </summary>
    /// <param name="instanceName">Name of the instance</param>
    /// <param name="groupJid">Group JID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of group participants</returns>
    Task<FindParticipantsResponse> FindParticipantsAsync(string instanceName, string groupJid, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update group participants (add, remove, promote, demote)
    /// </summary>
    /// <param name="instanceName">Name of the instance</param>
    /// <param name="groupJid">Group JID</param>
    /// <param name="request">Update participant request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Update result</returns>
    Task<UpdateParticipantResponse> UpdateParticipantAsync(string instanceName, string groupJid, UpdateParticipantRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update group settings (announcement, locked, etc.)
    /// </summary>
    /// <param name="instanceName">Name of the instance</param>
    /// <param name="groupJid">Group JID</param>
    /// <param name="request">Update setting request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Update result</returns>
    Task<UpdateGroupSettingResponse> UpdateGroupSettingAsync(string instanceName, string groupJid, UpdateGroupSettingRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Toggle ephemeral messages in group
    /// </summary>
    /// <param name="instanceName">Name of the instance</param>
    /// <param name="groupJid">Group JID</param>
    /// <param name="request">Toggle ephemeral request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Toggle result</returns>
    Task<ToggleEphemeralResponse> ToggleEphemeralAsync(string instanceName, string groupJid, ToggleEphemeralRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Leave a group
    /// </summary>
    /// <param name="instanceName">Name of the instance</param>
    /// <param name="groupJid">Group JID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Leave result</returns>
    Task<LeaveGroupResponse> LeaveGroupAsync(string instanceName, string groupJid, CancellationToken cancellationToken = default);
}