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
}