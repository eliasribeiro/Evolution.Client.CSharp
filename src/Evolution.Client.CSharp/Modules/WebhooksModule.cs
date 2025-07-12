using Evolution.Client.CSharp.Core.Http;

namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Implementação do módulo de webhooks
/// </summary>
internal class WebhooksModule : IWebhooksModule
{
    private readonly IHttpService _httpService;

    public WebhooksModule(IHttpService httpService)
    {
        _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
    }

    public async Task<SetWebhookResponse> SetAsync(
        string instanceName,
        SetWebhookRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<SetWebhookRequest, SetWebhookResponse>(
            $"webhook/set/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<WebhookConfig> GetAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<WebhookConfig>(
            $"webhook/find/{instanceName}",
            cancellationToken);
    }

    private static void ValidateInstanceName(string instanceName)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância é obrigatório", nameof(instanceName));
    }

    private static void ValidateRequest<T>(T request) where T : class
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
    }
}
