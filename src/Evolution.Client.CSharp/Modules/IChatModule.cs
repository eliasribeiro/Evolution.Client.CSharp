namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Interface para operações relacionadas a chat
/// </summary>
public interface IChatModule
{
    /// <summary>
    /// Verifica se números existem no WhatsApp
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Números para verificar</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista com status dos números</returns>
    Task<IEnumerable<WhatsAppNumberCheckResult>> CheckWhatsAppNumbersAsync(
        string instanceName,
        CheckWhatsAppNumbersRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Marca mensagens como lidas
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados para marcar como lida</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da operação</returns>
    Task<ChatOperationResponse> MarkAsReadAsync(
        string instanceName,
        MarkAsReadChatRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Marca mensagens como não lidas
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados para marcar como não lida</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da operação</returns>
    Task<ChatOperationResponse> MarkAsUnreadAsync(
        string instanceName,
        MarkAsUnreadChatRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Arquiva ou desarquiva um chat
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados para arquivar</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da operação</returns>
    Task<ChatOperationResponse> ArchiveChatAsync(
        string instanceName,
        ArchiveChatRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deleta uma mensagem para todos
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados da mensagem para deletar</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da operação</returns>
    Task<ChatOperationResponse> DeleteMessageForEveryoneAsync(
        string instanceName,
        DeleteMessageForEveryoneRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza o texto de uma mensagem
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados para atualizar a mensagem</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da operação</returns>
    Task<ChatOperationResponse> UpdateMessageAsync(
        string instanceName,
        UpdateMessageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Envia presença (digitando, gravando, etc.)
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados da presença</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da operação</returns>
    Task<ChatOperationResponse> SendPresenceAsync(
        string instanceName,
        SendPresenceRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Atualiza status de bloqueio de um contato
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados do bloqueio</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resposta da operação</returns>
    Task<ChatOperationResponse> UpdateBlockStatusAsync(
        string instanceName,
        UpdateBlockStatusRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca URL da foto do perfil
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados do contato</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>URL da foto do perfil</returns>
    Task<ProfilePictureResponse> FetchProfilePictureUrlAsync(
        string instanceName,
        FetchProfilePictureRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Obtém dados de mídia em base64
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados da mensagem</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Dados em base64</returns>
    Task<Base64Response> GetBase64Async(
        string instanceName,
        GetBase64Request request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca contatos
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Parâmetros de busca</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista de contatos</returns>
    Task<IEnumerable<ContactInfo>> FindContactsAsync(
        string instanceName,
        FindContactsChatRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca mensagens
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Parâmetros de busca</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista de mensagens</returns>
    Task<IEnumerable<Message>> FindMessagesAsync(
        string instanceName,
        FindMessagesChatRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca mensagens de status
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Parâmetros de busca</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista de mensagens de status</returns>
    Task<IEnumerable<Message>> FindStatusMessagesAsync(
        string instanceName,
        FindStatusMessageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca chats
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Parâmetros de busca</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista de chats</returns>
    Task<IEnumerable<ChatInfo>> FindChatsAsync(
        string instanceName,
        FindChatsRequest request,
        CancellationToken cancellationToken = default);
}
