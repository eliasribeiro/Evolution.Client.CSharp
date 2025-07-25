using Evolution.Client.CSharp.Configuration;
using Evolution.Client.CSharp.Interfaces;
using Evolution.Client.CSharp.Models.Group;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Evolution.Client.CSharp.Services;

/// <summary>
/// Implementação do serviço de grupos da API Evolution.
/// </summary>
public class EvolutionGroupService : IEvolutionGroupService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    /// <summary>
    /// Inicializa uma nova instância do serviço de grupos.
    /// </summary>
    /// <param name="httpClient">Cliente HTTP configurado.</param>
    /// <param name="options">As opções de configuração da API.</param>
    public EvolutionGroupService(HttpClient httpClient, IOptions<EvolutionApiOptions> options)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        
        var apiOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));

        // Configura o cliente HTTP
        _httpClient.BaseAddress = new Uri(apiOptions.BaseUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(apiOptions.TimeoutSeconds);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // Adiciona o cabeçalho de autenticação se a chave de API estiver definida
        if (!string.IsNullOrEmpty(apiOptions.ApiKey))
        {
            _httpClient.DefaultRequestHeaders.Add("apikey", apiOptions.ApiKey);
        }

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
    }

    /// <inheritdoc />
    public async Task<CreateGroupResponse> CreateGroupAsync(string instanceName, CreateGroupRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância não pode ser vazio.", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"group/create/{instanceName}", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<CreateGroupResponse>(responseContent, _jsonOptions) ?? new CreateGroupResponse();
    }

    /// <inheritdoc />
    public async Task<UpdateGroupPictureResponse> UpdateGroupPictureAsync(string instanceName, string groupJid, UpdateGroupPictureRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância não pode ser vazio.", nameof(instanceName));

        if (string.IsNullOrWhiteSpace(groupJid))
            throw new ArgumentException("JID do grupo não pode ser vazio.", nameof(groupJid));

        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"group/updateGroupPicture/{instanceName}?groupJid={Uri.EscapeDataString(groupJid)}", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UpdateGroupPictureResponse>(responseContent, _jsonOptions) ?? new UpdateGroupPictureResponse();
    }

    /// <inheritdoc />
    public async Task<UpdateGroupSubjectResponse> UpdateGroupSubjectAsync(string instanceName, string groupJid, UpdateGroupSubjectRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância não pode ser vazio.", nameof(instanceName));

        if (string.IsNullOrWhiteSpace(groupJid))
            throw new ArgumentException("JID do grupo não pode ser vazio.", nameof(groupJid));

        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"group/updateGroupSubject/{instanceName}?groupJid={Uri.EscapeDataString(groupJid)}", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UpdateGroupSubjectResponse>(responseContent, _jsonOptions) ?? new UpdateGroupSubjectResponse();
    }

    /// <inheritdoc />
    public async Task<UpdateGroupDescriptionResponse> UpdateGroupDescriptionAsync(string instanceName, string groupJid, UpdateGroupDescriptionRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância não pode ser vazio.", nameof(instanceName));

        if (string.IsNullOrWhiteSpace(groupJid))
            throw new ArgumentException("JID do grupo não pode ser vazio.", nameof(groupJid));

        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"group/updateGroupDescription/{instanceName}?groupJid={Uri.EscapeDataString(groupJid)}", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UpdateGroupDescriptionResponse>(responseContent, _jsonOptions) ?? new UpdateGroupDescriptionResponse();
    }

    /// <inheritdoc />
    public async Task<FetchInviteCodeResponse> FetchInviteCodeAsync(string instanceName, string groupJid)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância não pode ser vazio.", nameof(instanceName));

        if (string.IsNullOrWhiteSpace(groupJid))
            throw new ArgumentException("JID do grupo não pode ser vazio.", nameof(groupJid));

        var response = await _httpClient.GetAsync($"group/inviteCode/{instanceName}?groupJid={Uri.EscapeDataString(groupJid)}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<FetchInviteCodeResponse>(responseContent, _jsonOptions) ?? new FetchInviteCodeResponse();
    }

    /// <inheritdoc />
    public async Task<RevokeInviteCodeResponse> RevokeInviteCodeAsync(string instanceName, string groupJid)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância não pode ser vazio.", nameof(instanceName));

        if (string.IsNullOrWhiteSpace(groupJid))
            throw new ArgumentException("JID do grupo não pode ser vazio.", nameof(groupJid));

        var response = await _httpClient.PostAsync($"group/revokeInviteCode/{instanceName}?groupJid={Uri.EscapeDataString(groupJid)}", null);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<RevokeInviteCodeResponse>(responseContent, _jsonOptions) ?? new RevokeInviteCodeResponse();
    }

    /// <inheritdoc />
    public async Task<SendGroupInviteResponse> SendGroupInviteAsync(string instanceName, string groupJid, SendGroupInviteRequest request)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância não pode ser vazio.", nameof(instanceName));

        if (string.IsNullOrWhiteSpace(groupJid))
            throw new ArgumentException("JID do grupo não pode ser vazio.", nameof(groupJid));

        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"group/sendInvite/{instanceName}?groupJid={Uri.EscapeDataString(groupJid)}", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SendGroupInviteResponse>(responseContent, _jsonOptions) ?? new SendGroupInviteResponse();
    }

    /// <inheritdoc />
    public async Task<FindGroupByInviteCodeResponse> FindGroupByInviteCodeAsync(string instanceName, string inviteCode, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância não pode ser vazio.", nameof(instanceName));

        if (string.IsNullOrWhiteSpace(inviteCode))
            throw new ArgumentException("Código de convite não pode ser vazio.", nameof(inviteCode));

        var response = await _httpClient.GetAsync($"group/findGroupByInviteCode/{instanceName}?inviteCode={Uri.EscapeDataString(inviteCode)}", cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<FindGroupByInviteCodeResponse>(responseContent, _jsonOptions) ?? new FindGroupByInviteCodeResponse();
    }

    /// <inheritdoc />
    public async Task<FindGroupByJidResponse> FindGroupByJidAsync(string instanceName, string groupJid, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância não pode ser vazio.", nameof(instanceName));

        if (string.IsNullOrWhiteSpace(groupJid))
            throw new ArgumentException("JID do grupo não pode ser vazio.", nameof(groupJid));

        var response = await _httpClient.GetAsync($"group/findGroupByJid/{instanceName}?groupJid={Uri.EscapeDataString(groupJid)}", cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<FindGroupByJidResponse>(responseContent, _jsonOptions) ?? new FindGroupByJidResponse();
    }

    /// <inheritdoc />
    public async Task<List<FetchAllGroupsResponse>> FetchAllGroupsAsync(string instanceName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância não pode ser vazio.", nameof(instanceName));

        var response = await _httpClient.GetAsync($"group/fetchAllGroups/{instanceName}", cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<FetchAllGroupsResponse>>(responseContent, _jsonOptions) ?? new List<FetchAllGroupsResponse>();
    }

    /// <inheritdoc />
    public async Task<FindParticipantsResponse> FindParticipantsAsync(string instanceName, string groupJid, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância não pode ser vazio.", nameof(instanceName));

        if (string.IsNullOrWhiteSpace(groupJid))
            throw new ArgumentException("JID do grupo não pode ser vazio.", nameof(groupJid));

        var response = await _httpClient.GetAsync($"group/findParticipants/{instanceName}?groupJid={Uri.EscapeDataString(groupJid)}", cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<FindParticipantsResponse>(responseContent, _jsonOptions) ?? new FindParticipantsResponse();
    }

    /// <inheritdoc />
    public async Task<UpdateParticipantResponse> UpdateParticipantAsync(string instanceName, string groupJid, UpdateParticipantRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância não pode ser vazio.", nameof(instanceName));

        if (string.IsNullOrWhiteSpace(groupJid))
            throw new ArgumentException("JID do grupo não pode ser vazio.", nameof(groupJid));

        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"group/updateParticipant/{instanceName}?groupJid={Uri.EscapeDataString(groupJid)}", content, cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<UpdateParticipantResponse>(responseContent, _jsonOptions) ?? new UpdateParticipantResponse();
    }

    /// <inheritdoc />
    public async Task<UpdateGroupSettingResponse> UpdateGroupSettingAsync(string instanceName, string groupJid, UpdateGroupSettingRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância não pode ser vazio.", nameof(instanceName));

        if (string.IsNullOrWhiteSpace(groupJid))
            throw new ArgumentException("JID do grupo não pode ser vazio.", nameof(groupJid));

        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"group/updateSetting/{instanceName}?groupJid={Uri.EscapeDataString(groupJid)}", content, cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<UpdateGroupSettingResponse>(responseContent, _jsonOptions) ?? new UpdateGroupSettingResponse();
    }

    /// <inheritdoc />
    public async Task<ToggleEphemeralResponse> ToggleEphemeralAsync(string instanceName, string groupJid, ToggleEphemeralRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância não pode ser vazio.", nameof(instanceName));

        if (string.IsNullOrWhiteSpace(groupJid))
            throw new ArgumentException("JID do grupo não pode ser vazio.", nameof(groupJid));

        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"group/toggleEphemeral/{instanceName}?groupJid={Uri.EscapeDataString(groupJid)}", content, cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<ToggleEphemeralResponse>(responseContent, _jsonOptions) ?? new ToggleEphemeralResponse();
    }

    /// <inheritdoc />
    public async Task<LeaveGroupResponse> LeaveGroupAsync(string instanceName, string groupJid, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância não pode ser vazio.", nameof(instanceName));

        if (string.IsNullOrWhiteSpace(groupJid))
            throw new ArgumentException("JID do grupo não pode ser vazio.", nameof(groupJid));

        var response = await _httpClient.DeleteAsync($"group/leaveGroup/{instanceName}?groupJid={Uri.EscapeDataString(groupJid)}", cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<LeaveGroupResponse>(responseContent, _jsonOptions) ?? new LeaveGroupResponse();
    }

    /// <inheritdoc />
    public async Task<List<FetchAllGroupsResponse>> FindGroupsAsync(string instanceName, FindGroupsRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância não pode ser vazio.", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var url = $"group/fetchAllGroups/{instanceName}?getParticipants={request.GetParticipants}";
        var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<FetchAllGroupsResponse>>(responseContent, _jsonOptions) ?? new List<FetchAllGroupsResponse>();
    }
}