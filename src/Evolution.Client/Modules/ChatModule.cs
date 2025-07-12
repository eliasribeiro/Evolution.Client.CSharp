using Evolution.Client.Core.Http;

namespace Evolution.Client.Modules;

/// <summary>
/// Implementação do módulo de chat
/// </summary>
internal class ChatModule : IChatModule
{
    private readonly IHttpService _httpService;

    public ChatModule(IHttpService httpService)
    {
        _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
    }

    public async Task<IEnumerable<WhatsAppNumberCheckResult>> CheckWhatsAppNumbersAsync(
        string instanceName,
        CheckWhatsAppNumbersRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<CheckWhatsAppNumbersRequest, IEnumerable<WhatsAppNumberCheckResult>>(
            $"chat/whatsappNumbers/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<ChatOperationResponse> MarkAsReadAsync(
        string instanceName,
        MarkAsReadChatRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<MarkAsReadChatRequest, ChatOperationResponse>(
            $"chat/markMessageAsRead/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<ChatOperationResponse> MarkAsUnreadAsync(
        string instanceName,
        MarkAsUnreadChatRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<MarkAsUnreadChatRequest, ChatOperationResponse>(
            $"chat/markMessageAsUnread/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<ChatOperationResponse> ArchiveChatAsync(
        string instanceName,
        ArchiveChatRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<ArchiveChatRequest, ChatOperationResponse>(
            $"chat/archiveChat/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<ChatOperationResponse> DeleteMessageForEveryoneAsync(
        string instanceName,
        DeleteMessageForEveryoneRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<DeleteMessageForEveryoneRequest, ChatOperationResponse>(
            $"chat/deleteMessageForEveryone/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<ChatOperationResponse> UpdateMessageAsync(
        string instanceName,
        UpdateMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<UpdateMessageRequest, ChatOperationResponse>(
            $"chat/updateMessage/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<ChatOperationResponse> SendPresenceAsync(
        string instanceName,
        SendPresenceRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<SendPresenceRequest, ChatOperationResponse>(
            $"chat/sendPresence/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<ChatOperationResponse> UpdateBlockStatusAsync(
        string instanceName,
        UpdateBlockStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<UpdateBlockStatusRequest, ChatOperationResponse>(
            $"chat/updateBlockStatus/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<ProfilePictureResponse> FetchProfilePictureUrlAsync(
        string instanceName,
        FetchProfilePictureRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<FetchProfilePictureRequest, ProfilePictureResponse>(
            $"chat/fetchProfilePicture/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<Base64Response> GetBase64Async(
        string instanceName,
        GetBase64Request request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<GetBase64Request, Base64Response>(
            $"chat/getBase64FromMediaMessage/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<IEnumerable<ContactInfo>> FindContactsAsync(
        string instanceName,
        FindContactsChatRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<FindContactsChatRequest, IEnumerable<ContactInfo>>(
            $"chat/findContacts/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<IEnumerable<Message>> FindMessagesAsync(
        string instanceName,
        FindMessagesChatRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<FindMessagesChatRequest, IEnumerable<Message>>(
            $"chat/findMessages/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<IEnumerable<Message>> FindStatusMessagesAsync(
        string instanceName,
        FindStatusMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<FindStatusMessageRequest, IEnumerable<Message>>(
            $"chat/findStatusMessage/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<IEnumerable<ChatInfo>> FindChatsAsync(
        string instanceName,
        FindChatsRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<FindChatsRequest, IEnumerable<ChatInfo>>(
            $"chat/findChats/{instanceName}",
            request,
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
