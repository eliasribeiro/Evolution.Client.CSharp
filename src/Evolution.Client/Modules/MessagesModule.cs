using Evolution.Client.Core.Http;

namespace Evolution.Client.Modules;

/// <summary>
/// Implementação do módulo de mensagens
/// </summary>
internal class MessagesModule : IMessagesModule
{
    private readonly IHttpService _httpService;

    public MessagesModule(IHttpService httpService)
    {
        _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
    }

    public async Task<SendMessageResponse> SendTextAsync(
        string instanceName,
        SendTextMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<SendTextMessageRequest, SendMessageResponse>(
            $"message/sendText/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<SendMessageResponse> SendMediaAsync(
        string instanceName,
        SendMediaMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<SendMediaMessageRequest, SendMessageResponse>(
            $"message/sendMedia/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<SendMessageResponse> SendAudioAsync(
        string instanceName,
        SendAudioMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<SendAudioMessageRequest, SendMessageResponse>(
            $"message/sendAudio/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<SendMessageResponse> SendLocationAsync(
        string instanceName,
        SendLocationMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<SendLocationMessageRequest, SendMessageResponse>(
            $"message/sendLocation/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<SendMessageResponse> SendStatusAsync(
        string instanceName,
        SendStatusMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<SendStatusMessageRequest, SendMessageResponse>(
            $"message/sendStatus/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<SendMessageResponse> SendStickerAsync(
        string instanceName,
        SendStickerMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<SendStickerMessageRequest, SendMessageResponse>(
            $"message/sendSticker/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<SendMessageResponse> SendContactAsync(
        string instanceName,
        SendContactMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<SendContactMessageRequest, SendMessageResponse>(
            $"message/sendContact/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<SendMessageResponse> SendReactionAsync(
        string instanceName,
        SendReactionMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<SendReactionMessageRequest, SendMessageResponse>(
            $"message/sendReaction/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<SendMessageResponse> SendPollAsync(
        string instanceName,
        SendPollMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<SendPollMessageRequest, SendMessageResponse>(
            $"message/sendPoll/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<SendMessageResponse> SendListAsync(
        string instanceName,
        SendListMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<SendListMessageRequest, SendMessageResponse>(
            $"message/sendList/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<SendMessageResponse> SendButtonAsync(
        string instanceName,
        SendButtonMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<SendButtonMessageRequest, SendMessageResponse>(
            $"message/sendButtons/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<IEnumerable<Message>> FindAsync(
        string instanceName,
        FindMessagesRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<FindMessagesRequest, IEnumerable<Message>>(
            $"chat/findMessages/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task MarkAsReadAsync(
        string instanceName,
        MarkAsReadRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        await _httpService.PutAsync(
            $"chat/markMessageAsRead/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task DeleteAsync(
        string instanceName,
        string messageId,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        
        if (string.IsNullOrWhiteSpace(messageId))
            throw new ArgumentException("ID da mensagem é obrigatório", nameof(messageId));

        await _httpService.DeleteAsync(
            $"message/delete/{instanceName}/{messageId}",
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
