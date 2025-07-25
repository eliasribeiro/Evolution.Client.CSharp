namespace Evolution.Client.CSharp.Interfaces;

/// <summary>
/// Interface para o cliente principal da API Evolution.
/// </summary>
public interface IEvolutionApiClient
{
    /// <summary>
    /// Obtém o serviço de informações da API Evolution.
    /// </summary>
    IEvolutionInformationService Information { get; }
    
    /// <summary>
    /// Obtém o serviço de instâncias da API Evolution.
    /// </summary>
    IEvolutionInstanceService Instance { get; }

    /// <summary>
    /// Obtém o serviço de chat da API Evolution.
    /// </summary>
    IEvolutionChatService Chat { get; }

    /// <summary>
    /// Obtém o serviço de mensagens da API Evolution.
    /// </summary>
    IEvolutionMessageService Message { get; }

    /// <summary>
    /// Obtém o serviço de perfil da API Evolution.
    /// </summary>
    IEvolutionProfileService Profile { get; }

    /// <summary>
    /// Obtém o serviço de webhook da API Evolution.
    /// </summary>
    IEvolutionWebhookService Webhook { get; }

    /// <summary>
    /// Obtém o serviço de configurações da API Evolution.
    /// </summary>
    IEvolutionSettingsService Settings { get; }

    /// <summary>
    /// Obtém o serviço de grupos da API Evolution.
    /// </summary>
    IEvolutionGroupService GroupService { get; }
}