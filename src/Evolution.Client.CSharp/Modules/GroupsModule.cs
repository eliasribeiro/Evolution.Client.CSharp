using Evolution.Client.CSharp.Core.Http;

namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Implementação do módulo de grupos
/// </summary>
internal class GroupsModule : IGroupsModule
{
    private readonly IHttpService _httpService;

    public GroupsModule(IHttpService httpService)
    {
        _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
    }

    public async Task<GroupInfo> CreateAsync(
        string instanceName,
        CreateGroupRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<CreateGroupRequest, GroupInfo>(
            $"group/create/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<GroupOperationResponse> UpdateGroupPictureAsync(
        string instanceName,
        string groupJid,
        UpdateGroupPictureRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateGroupJid(groupJid);
        ValidateRequest(request);

        return await _httpService.PostAsync<UpdateGroupPictureRequest, GroupOperationResponse>(
            $"group/updateGroupPicture/{instanceName}?groupJid={groupJid}",
            request,
            cancellationToken);
    }

    public async Task<GroupOperationResponse> UpdateGroupSubjectAsync(
        string instanceName,
        string groupJid,
        UpdateGroupSubjectRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateGroupJid(groupJid);
        ValidateRequest(request);

        return await _httpService.PostAsync<UpdateGroupSubjectRequest, GroupOperationResponse>(
            $"group/updateGroupSubject/{instanceName}?groupJid={groupJid}",
            request,
            cancellationToken);
    }

    public async Task<GroupOperationResponse> UpdateGroupDescriptionAsync(
        string instanceName,
        string groupJid,
        UpdateGroupDescriptionRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateGroupJid(groupJid);
        ValidateRequest(request);

        return await _httpService.PostAsync<UpdateGroupDescriptionRequest, GroupOperationResponse>(
            $"group/updateGroupDescription/{instanceName}?groupJid={groupJid}",
            request,
            cancellationToken);
    }

    public async Task<GroupInviteCodeResponse> FetchInviteCodeAsync(
        string instanceName,
        string groupJid,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateGroupJid(groupJid);

        return await _httpService.GetAsync<GroupInviteCodeResponse>(
            $"group/fetchInviteCode/{instanceName}?groupJid={groupJid}",
            cancellationToken);
    }

    public async Task<GroupInviteCodeResponse> RevokeInviteCodeAsync(
        string instanceName,
        string groupJid,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateGroupJid(groupJid);

        return await _httpService.PostAsync<object, GroupInviteCodeResponse>(
            $"group/revokeInviteCode/{instanceName}?groupJid={groupJid}",
            new { },
            cancellationToken);
    }

    public async Task<GroupOperationResponse> SendGroupInviteAsync(
        string instanceName,
        string groupJid,
        SendGroupInviteRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateGroupJid(groupJid);
        ValidateRequest(request);

        return await _httpService.PostAsync<SendGroupInviteRequest, GroupOperationResponse>(
            $"group/sendInvite/{instanceName}?groupJid={groupJid}",
            request,
            cancellationToken);
    }

    public async Task<GroupByInviteCodeResponse> FindGroupByInviteCodeAsync(
        string instanceName,
        string inviteCode,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        if (string.IsNullOrWhiteSpace(inviteCode))
            throw new ArgumentException("Código de convite é obrigatório", nameof(inviteCode));

        return await _httpService.GetAsync<GroupByInviteCodeResponse>(
            $"group/findGroupByInviteCode/{instanceName}?inviteCode={inviteCode}",
            cancellationToken);
    }

    public async Task<GroupInfo> FindGroupByJidAsync(
        string instanceName,
        string groupJid,
        bool getParticipants = false,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateGroupJid(groupJid);

        return await _httpService.GetAsync<GroupInfo>(
            $"group/findGroupByJid/{instanceName}?groupJid={groupJid}&getParticipants={getParticipants}",
            cancellationToken);
    }

    public async Task<IEnumerable<GroupInfo>> FetchAllGroupsAsync(
        string instanceName,
        bool getParticipants = false,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<IEnumerable<GroupInfo>>(
            $"group/fetchAllGroups/{instanceName}?getParticipants={getParticipants}",
            cancellationToken);
    }

    public async Task<IEnumerable<GroupParticipant>> FindParticipantsAsync(
        string instanceName,
        string groupJid,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateGroupJid(groupJid);

        return await _httpService.GetAsync<IEnumerable<GroupParticipant>>(
            $"group/findParticipants/{instanceName}?groupJid={groupJid}",
            cancellationToken);
    }

    public async Task<GroupInfo> GetAsync(
        string instanceName,
        string groupId,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateGroupId(groupId);

        return await _httpService.GetAsync<GroupInfo>(
            $"group/findGroupInfos/{instanceName}?groupJid={groupId}",
            cancellationToken);
    }

    public async Task<GroupOperationResponse> UpdateParticipantAsync(
        string instanceName,
        string groupJid,
        UpdateParticipantRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateGroupJid(groupJid);
        ValidateRequest(request);

        return await _httpService.PostAsync<UpdateParticipantRequest, GroupOperationResponse>(
            $"group/updateParticipant/{instanceName}?groupJid={groupJid}",
            request,
            cancellationToken);
    }

    public async Task<GroupOperationResponse> UpdateSettingAsync(
        string instanceName,
        string groupJid,
        UpdateGroupSettingRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateGroupJid(groupJid);
        ValidateRequest(request);

        return await _httpService.PostAsync<UpdateGroupSettingRequest, GroupOperationResponse>(
            $"group/updateSetting/{instanceName}?groupJid={groupJid}",
            request,
            cancellationToken);
    }

    public async Task<GroupOperationResponse> ToggleEphemeralAsync(
        string instanceName,
        string groupJid,
        ToggleEphemeralRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateGroupJid(groupJid);
        ValidateRequest(request);

        return await _httpService.PostAsync<ToggleEphemeralRequest, GroupOperationResponse>(
            $"group/toggleEphemeral/{instanceName}?groupJid={groupJid}",
            request,
            cancellationToken);
    }

    public async Task<GroupOperationResponse> LeaveGroupAsync(
        string instanceName,
        string groupJid,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateGroupJid(groupJid);

        return await _httpService.DeleteAsync<GroupOperationResponse>(
            $"group/leaveGroup/{instanceName}?groupJid={groupJid}",
            cancellationToken);
    }

    public async Task<IEnumerable<GroupInfo>> ListAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<IEnumerable<GroupInfo>>(
            $"group/findGroupInfos/{instanceName}",
            cancellationToken);
    }

    public async Task AddParticipantsAsync(
        string instanceName,
        string groupId,
        AddParticipantsRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateGroupId(groupId);
        ValidateRequest(request);

        await _httpService.PutAsync(
            $"group/updateParticipant/{instanceName}?groupJid={groupId}&action=add",
            request,
            cancellationToken);
    }

    public async Task RemoveParticipantsAsync(
        string instanceName,
        string groupId,
        RemoveParticipantsRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateGroupId(groupId);
        ValidateRequest(request);

        await _httpService.PutAsync(
            $"group/updateParticipant/{instanceName}?groupJid={groupId}&action=remove",
            request,
            cancellationToken);
    }

    public async Task UpdateAsync(
        string instanceName,
        string groupId,
        UpdateGroupRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateGroupId(groupId);
        ValidateRequest(request);

        await _httpService.PutAsync(
            $"group/updateGroupInfo/{instanceName}?groupJid={groupId}",
            request,
            cancellationToken);
    }

    public async Task LeaveAsync(
        string instanceName,
        string groupId,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateGroupId(groupId);

        await _httpService.DeleteAsync(
            $"group/leaveGroup/{instanceName}?groupJid={groupId}",
            cancellationToken);
    }

    private static void ValidateInstanceName(string instanceName)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância é obrigatório", nameof(instanceName));
    }

    private static void ValidateGroupId(string groupId)
    {
        if (string.IsNullOrWhiteSpace(groupId))
            throw new ArgumentException("ID do grupo é obrigatório", nameof(groupId));
    }

    private static void ValidateGroupJid(string groupJid)
    {
        if (string.IsNullOrWhiteSpace(groupJid))
            throw new ArgumentException("JID do grupo é obrigatório", nameof(groupJid));
    }

    private static void ValidateRequest<T>(T request) where T : class
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
    }
}
