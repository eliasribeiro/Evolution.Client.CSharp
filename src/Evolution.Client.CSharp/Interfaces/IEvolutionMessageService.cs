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

    /// <summary>
    /// Envia um status para os contatos.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os dados do status.</param>
    /// <returns>A resposta com informações do status enviado.</returns>
    Task<SendStatusResponse> SendStatusAsync(string instanceName, SendStatusRequest request);

    /// <summary>
    /// Envia mídia (imagem, vídeo ou documento) para um destinatário.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os dados da mídia.</param>
    /// <returns>A resposta com informações da mídia enviada.</returns>
    Task<SendMediaResponse> SendMediaAsync(string instanceName, SendMediaRequest request);

    /// <summary>
    /// Envia áudio para um destinatário.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os dados do áudio.</param>
    /// <returns>A resposta com informações do áudio enviado.</returns>
    Task<SendAudioResponse> SendAudioAsync(string instanceName, SendAudioRequest request);

    /// <summary>
    /// Envia sticker para um destinatário.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os dados do sticker.</param>
    /// <returns>A resposta com informações do sticker enviado.</returns>
    Task<SendStickerResponse> SendStickerAsync(string instanceName, SendStickerRequest request);

    /// <summary>
    /// Envia localização para um destinatário.
    /// </summary>
    /// <param name="instanceName">O nome da instância.</param>
    /// <param name="request">A requisição contendo os dados da localização.</param>
    /// <returns>A resposta com informações da localização enviada.</returns>
    Task<SendLocationResponse> SendLocationAsync(string instanceName, SendLocationRequest request);
}
