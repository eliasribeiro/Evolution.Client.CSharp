namespace Evolution.Client.CSharp.Modules;

/// <summary>
/// Interface para operações relacionadas a mensagens
/// </summary>
public interface IMessagesModule
{
    /// <summary>
    /// Envia uma mensagem de texto
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados da mensagem</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado do envio</returns>
    Task<SendMessageResponse> SendTextAsync(
        string instanceName,
        SendTextMessageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Envia uma mensagem de mídia
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados da mensagem de mídia</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado do envio</returns>
    Task<SendMessageResponse> SendMediaAsync(
        string instanceName,
        SendMediaMessageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Envia uma mensagem de áudio
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados da mensagem de áudio</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado do envio</returns>
    Task<SendMessageResponse> SendAudioAsync(
        string instanceName,
        SendAudioMessageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Envia uma mensagem de localização
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados da localização</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado do envio</returns>
    Task<SendMessageResponse> SendLocationAsync(
        string instanceName,
        SendLocationMessageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Envia uma mensagem de status
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados do status</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado do envio</returns>
    Task<SendMessageResponse> SendStatusAsync(
        string instanceName,
        SendStatusMessageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Envia um sticker
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados do sticker</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado do envio</returns>
    Task<SendMessageResponse> SendStickerAsync(
        string instanceName,
        SendStickerMessageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Envia um contato
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados do contato</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado do envio</returns>
    Task<SendMessageResponse> SendContactAsync(
        string instanceName,
        SendContactMessageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Envia uma reação
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados da reação</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado do envio</returns>
    Task<SendMessageResponse> SendReactionAsync(
        string instanceName,
        SendReactionMessageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Envia uma enquete
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados da enquete</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado do envio</returns>
    Task<SendMessageResponse> SendPollAsync(
        string instanceName,
        SendPollMessageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Envia uma lista
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados da lista</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado do envio</returns>
    Task<SendMessageResponse> SendListAsync(
        string instanceName,
        SendListMessageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Envia botões
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados dos botões</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Resultado do envio</returns>
    Task<SendMessageResponse> SendButtonAsync(
        string instanceName,
        SendButtonMessageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca mensagens
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Parâmetros de busca</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    /// <returns>Lista de mensagens</returns>
    Task<IEnumerable<Message>> FindAsync(
        string instanceName,
        FindMessagesRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Marca mensagens como lidas
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="request">Dados para marcar como lida</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task MarkAsReadAsync(
        string instanceName,
        MarkAsReadRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deleta uma mensagem
    /// </summary>
    /// <param name="instanceName">Nome da instância</param>
    /// <param name="messageId">ID da mensagem</param>
    /// <param name="cancellationToken">Token de cancelamento</param>
    Task DeleteAsync(
        string instanceName,
        string messageId,
        CancellationToken cancellationToken = default);
}
