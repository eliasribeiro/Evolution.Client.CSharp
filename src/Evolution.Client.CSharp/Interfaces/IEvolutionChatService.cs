using Evolution.Client.CSharp.Models.Chat;

namespace Evolution.Client.CSharp.Interfaces;

/// <summary>
/// Interface para o serviço de chat da API Evolution.
/// </summary>
public interface IEvolutionChatService
{
    /// <summary>
    /// Verifica se os números fornecidos existem no WhatsApp.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os números a serem verificados.</param>
    /// <returns>Uma lista com informações sobre cada número verificado.</returns>
    Task<CheckWhatsAppResponse> CheckWhatsAppNumbersAsync(string instanceName, CheckWhatsAppRequest request);

    /// <summary>
    /// Busca contatos da instância especificada.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os critérios de busca (opcional).</param>
    /// <returns>Uma lista de contatos encontrados.</returns>
    Task<FindContactsResponse> FindContactsAsync(string instanceName, FindContactsRequest? request = null);

    /// <summary>
    /// Busca mensagens da instância especificada.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os critérios de busca (opcional).</param>
    /// <returns>Uma lista de mensagens encontradas com informações de paginação.</returns>
    Task<FindMessagesResponse> FindMessagesAsync(string instanceName, FindMessagesRequest? request = null);

    /// <summary>
    /// Busca chats da instância especificada.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os critérios de busca (opcional).</param>
    /// <returns>Uma lista de chats encontrados.</returns>
    Task<FindChatsResponse> FindChatsAsync(string instanceName, FindChatsRequest? request = null);

    /// <summary>
    /// Busca a URL da foto de perfil de um número do WhatsApp.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo o número do WhatsApp.</param>
    /// <returns>A URL da foto de perfil do usuário.</returns>
    Task<FetchProfilePicUrlResponse> FetchProfilePicUrlAsync(string instanceName, FetchProfilePicUrlRequest request);

    /// <summary>
    /// Obtém o base64 de uma mensagem de mídia.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo as informações da mensagem de mídia.</param>
    /// <returns>Os dados da mídia em base64.</returns>
    Task<GetBase64FromMediaMessageResponse> GetBase64FromMediaMessageAsync(string instanceName, GetBase64FromMediaMessageRequest request);
}