namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Interface para operações relacionadas a contatos
/// </summary>
public interface IContactsModule
{
    /// <summary>
    /// Obtém informações de um contato
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="contactId">ID do contato</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Informações do contato</returns>
    Task<ContactInfo> GetAsync(
        string instanceName,
        string contactId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lista todos os contatos
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista de contatos</returns>
    Task<IEnumerable<ContactInfo>> ListAsync(
        string instanceName,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca contatos
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Parâmetros de busca</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista de contatos encontrados</returns>
    Task<IEnumerable<ContactInfo>> FindAsync(
        string instanceName,
        FindContactsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifica se um número está no WhatsApp
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="phoneNumber">Número de telefone</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Status do número no WhatsApp</returns>
    Task<WhatsAppNumberStatus> CheckWhatsAppAsync(
        string instanceName,
        string phoneNumber,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém foto do perfil de um contato
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="contactId">ID do contato</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>URL da foto do perfil</returns>
    Task<ProfilePictureInfo> GetProfilePictureAsync(
        string instanceName,
        string contactId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Bloqueia um contato
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="contactId">ID do contato</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task BlockAsync(
        string instanceName,
        string contactId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Desbloqueia um contato
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="contactId">ID do contato</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task UnblockAsync(
        string instanceName,
        string contactId,
        CancellationToken cancellationToken = default);
}
