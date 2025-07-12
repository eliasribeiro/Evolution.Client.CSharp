using Evolution.Client.Core.Http;

namespace Evolution.Client.Modules;

/// <summary>
/// Implementação do módulo de contatos
/// </summary>
internal class ContactsModule : IContactsModule
{
    private readonly IHttpService _httpService;

    public ContactsModule(IHttpService httpService)
    {
        _httpService = httpService ?? throw new ArgumentNullException(nameof(httpService));
    }

    public async Task<ContactInfo> GetAsync(
        string instanceName,
        string contactId,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateContactId(contactId);

        return await _httpService.GetAsync<ContactInfo>(
            $"chat/findContacts/{instanceName}?number={contactId}",
            cancellationToken);
    }

    public async Task<IEnumerable<ContactInfo>> ListAsync(
        string instanceName,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);

        return await _httpService.GetAsync<IEnumerable<ContactInfo>>(
            $"chat/findContacts/{instanceName}",
            cancellationToken);
    }

    public async Task<IEnumerable<ContactInfo>> FindAsync(
        string instanceName,
        FindContactsRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateRequest(request);

        return await _httpService.PostAsync<FindContactsRequest, IEnumerable<ContactInfo>>(
            $"chat/findContacts/{instanceName}",
            request,
            cancellationToken);
    }

    public async Task<WhatsAppNumberStatus> CheckWhatsAppAsync(
        string instanceName,
        string phoneNumber,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Número de telefone é obrigatório", nameof(phoneNumber));

        return await _httpService.GetAsync<WhatsAppNumberStatus>(
            $"chat/whatsappNumbers/{instanceName}?numbers={phoneNumber}",
            cancellationToken);
    }

    public async Task<ProfilePictureInfo> GetProfilePictureAsync(
        string instanceName,
        string contactId,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateContactId(contactId);

        return await _httpService.GetAsync<ProfilePictureInfo>(
            $"chat/fetchProfilePictureUrl/{instanceName}?number={contactId}",
            cancellationToken);
    }

    public async Task BlockAsync(
        string instanceName,
        string contactId,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateContactId(contactId);

        await _httpService.PutAsync(
            $"chat/updateContactInfo/{instanceName}",
            new { number = contactId, action = "block" },
            cancellationToken);
    }

    public async Task UnblockAsync(
        string instanceName,
        string contactId,
        CancellationToken cancellationToken = default)
    {
        ValidateInstanceName(instanceName);
        ValidateContactId(contactId);

        await _httpService.PutAsync(
            $"chat/updateContactInfo/{instanceName}",
            new { number = contactId, action = "unblock" },
            cancellationToken);
    }

    private static void ValidateInstanceName(string instanceName)
    {
        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância é obrigatório", nameof(instanceName));
    }

    private static void ValidateContactId(string contactId)
    {
        if (string.IsNullOrWhiteSpace(contactId))
            throw new ArgumentException("ID do contato é obrigatório", nameof(contactId));
    }

    private static void ValidateRequest<T>(T request) where T : class
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
    }
}
