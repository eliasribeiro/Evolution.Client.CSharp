namespace Evolution.Client.Modules;

/// <summary>
/// Interface para operações relacionadas a grupos
/// </summary>
public interface IGroupsModule
{
    /// <summary>
    /// Cria um novo grupo
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados para criação do grupo</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Informações do grupo criado</returns>
    Task<GroupInfo> CreateAsync(
        string instanceName,
        CreateGroupRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém informações de um grupo
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="groupId">ID do grupo</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Informações do grupo</returns>
    Task<GroupInfo> GetAsync(
        string instanceName,
        string groupId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lista todos os grupos
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista de grupos</returns>
    Task<IEnumerable<GroupInfo>> ListAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adiciona participantes ao grupo
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="groupId">ID do grupo</param>
    /// <param name="request">Dados dos participantes</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task AddParticipantsAsync(
        string instanceName,
        string groupId,
        AddParticipantsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove participantes do grupo
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="groupId">ID do grupo</param>
    /// <param name="request">Dados dos participantes</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task RemoveParticipantsAsync(
        string instanceName,
        string groupId,
        RemoveParticipantsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza a foto do grupo
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="groupJid">JID do grupo</param>
    /// <param name="request">Dados da nova foto</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task<GroupOperationResponse> UpdateGroupPictureAsync(
        string instanceName,
        string groupJid,
        UpdateGroupPictureRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza o assunto do grupo
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="groupJid">JID do grupo</param>
    /// <param name="request">Dados do novo assunto</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task<GroupOperationResponse> UpdateGroupSubjectAsync(
        string instanceName,
        string groupJid,
        UpdateGroupSubjectRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza a descrição do grupo
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="groupJid">JID do grupo</param>
    /// <param name="request">Dados da nova descrição</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task<GroupOperationResponse> UpdateGroupDescriptionAsync(
        string instanceName,
        string groupJid,
        UpdateGroupDescriptionRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém o código de convite do grupo
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="groupJid">JID do grupo</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task<GroupInviteCodeResponse> FetchInviteCodeAsync(
        string instanceName,
        string groupJid,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Revoga o código de convite do grupo
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="groupJid">JID do grupo</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task<GroupInviteCodeResponse> RevokeInviteCodeAsync(
        string instanceName,
        string groupJid,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Envia convite do grupo para números específicos
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="groupJid">JID do grupo</param>
    /// <param name="request">Dados do convite</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task<GroupOperationResponse> SendGroupInviteAsync(
        string instanceName,
        string groupJid,
        SendGroupInviteRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca grupo por código de convite
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="inviteCode">Código de convite</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task<GroupByInviteCodeResponse> FindGroupByInviteCodeAsync(
        string instanceName,
        string inviteCode,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca grupo por JID
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="groupJid">JID do grupo</param>
    /// <param name="getParticipants">Se deve incluir participantes</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task<GroupInfo> FindGroupByJidAsync(
        string instanceName,
        string groupJid,
        bool getParticipants = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca todos os grupos
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="getParticipants">Se deve incluir participantes</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task<IEnumerable<GroupInfo>> FetchAllGroupsAsync(
        string instanceName,
        bool getParticipants = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca participantes do grupo
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="groupJid">JID do grupo</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task<IEnumerable<GroupParticipant>> FindParticipantsAsync(
        string instanceName,
        string groupJid,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza participantes do grupo
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="groupJid">JID do grupo</param>
    /// <param name="request">Dados da atualização</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task<GroupOperationResponse> UpdateParticipantAsync(
        string instanceName,
        string groupJid,
        UpdateParticipantRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza configurações do grupo
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="groupJid">JID do grupo</param>
    /// <param name="request">Dados da configuração</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task<GroupOperationResponse> UpdateSettingAsync(
        string instanceName,
        string groupJid,
        UpdateGroupSettingRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Alterna mensagens efêmeras no grupo
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="groupJid">JID do grupo</param>
    /// <param name="request">Dados da configuração efêmera</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task<GroupOperationResponse> ToggleEphemeralAsync(
        string instanceName,
        string groupJid,
        ToggleEphemeralRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sai do grupo
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="groupJid">JID do grupo</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task<GroupOperationResponse> LeaveGroupAsync(
        string instanceName,
        string groupJid,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza informações do grupo (mantido para compatibilidade)
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="groupId">ID do grupo</param>
    /// <param name="request">Novos dados do grupo</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task UpdateAsync(
        string instanceName,
        string groupId,
        UpdateGroupRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sai do grupo (mantido para compatibilidade)
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="groupId">ID do grupo</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task LeaveAsync(
        string instanceName,
        string groupId,
        CancellationToken cancellationToken = default);
}
