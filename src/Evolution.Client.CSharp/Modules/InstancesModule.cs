using Evolution.Client.CSharp.Core.Http;

namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Implementação do módulo de instâncias
/// </summary>
internal class InstancesModule : IInstancesModule
{
    private readonly IHttpService _httpService;

    public InstancesModule(IHttpService httpService)
    {
        _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
    }

    public async Task<CreateInstanceResponse> CreateAsync(
        CreateInstanceRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.InstanceName))
            throw new ArgumentException("Nome da instância é obrigatório", nameof(request));

        return await _httpService.PostAsync<CreateInstanceRequest, CreateInstanceResponse>(
            "instance/create",
            request,
            cancellationToken);
    }

    public async Task<InstanceInfo> GetAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância é obrigatório", nameof(instanceName));

        return await _httpService.GetAsync<InstanceInfo>(
            $"instance/fetchInstances?instanceName={instanceName}",
            cancellationToken);
    }

    public async Task<IEnumerable<InstanceInfo>> ListAsync(
        CancellationToken cancellationToken = default)
    {
        return await _httpService.GetAsync<IEnumerable<InstanceInfo>>(
            "instance/fetchInstances",
            cancellationToken);
    }

    public async Task DeleteAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância é obrigatório", nameof(instanceName));

        await _httpService.DeleteAsync(
            $"instance/delete/{instanceName}",
            cancellationToken);
    }

    public async Task<ConnectInstanceResponse> ConnectAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância é obrigatório", nameof(instanceName));

        return await _httpService.GetAsync<ConnectInstanceResponse>(
            $"instance/connect/{instanceName}",
            cancellationToken);
    }

    public async Task DisconnectAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância é obrigatório", nameof(instanceName));

        await _httpService.DeleteAsync(
            $"instance/logout/{instanceName}",
            cancellationToken);
    }

    public async Task RestartAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância é obrigatório", nameof(instanceName));

        await _httpService.PutAsync(
            $"instance/restart/{instanceName}",
            new { },
            cancellationToken);
    }

    public async Task<ConnectionStatus> GetConnectionStatusAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância é obrigatório", nameof(instanceName));

        return await _httpService.GetAsync<ConnectionStatus>(
            $"instance/connectionState/{instanceName}",
            cancellationToken);
    }

    public async Task<SetPresenceResponse> SetPresenceAsync(
        string instanceName,
        SetPresenceRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância é obrigatório", nameof(instanceName));

        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.Presence))
            throw new ArgumentException("Presença é obrigatória", nameof(request));

        return await _httpService.PostAsync<SetPresenceRequest, SetPresenceResponse>(
            $"instance/setPresence/{instanceName}",
            request,
            cancellationToken);
    }
}
