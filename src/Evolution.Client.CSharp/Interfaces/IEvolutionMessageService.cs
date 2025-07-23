using Evolution.Client.CSharp.Models.Message;

namespace Evolution.Client.CSharp.Interfaces;

/// <summary>
/// Interface para operações relacionadas a mensagens na API Evolution.
/// </summary>
public interface IEvolutionMessageService
{
    /// <summary>
    /// Envia uma mensagem de texto para um destinatário.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os dados da mensagem.</param>
    /// <returns>A resposta com informações da mensagem enviada.</returns>
    Task<SendTextResponse> SendTextAsync(string instanceName, SendTextRequest request);
}
