using Evolution.Client.CSharp.Interfaces;

namespace Evolution.Client.CSharp;

/// <summary>
/// Cliente principal para a API Evolution.
/// </summary>
public class EvolutionApiClient : IEvolutionApiClient
{
    /// <summary>
    /// Obtém o serviço de informações da API Evolution.
    /// </summary>
    public IEvolutionInformationService Information { get; }
    
    /// <summary>
    /// Obtém o serviço de instâncias da API Evolution.
    /// </summary>
    public IEvolutionInstanceService Instance { get; }

    /// <summary>
    /// Obtém o serviço de chat da API Evolution.
    /// </summary>
    public IEvolutionChatService Chat { get; }

    /// <summary>
    /// Obtém o serviço de mensagens da API Evolution.
    /// </summary>
    public IEvolutionMessageService Message { get; }

    /// <summary>
    /// Obtém o serviço de perfil da API Evolution.
    /// </summary>
    public IEvolutionProfileService Profile { get; }

    /// <summary>
    /// Obtém o serviço de webhook da API Evolution.
    /// </summary>
    public IEvolutionWebhookService Webhook { get; }

    /// <summary>
    /// Obtém o serviço de configurações da API Evolution.
    /// </summary>
    public IEvolutionSettingsService Settings { get; }

    /// <summary>
    /// Obtém o serviço de grupos da API Evolution.
    /// </summary>
    public IEvolutionGroupService GroupService { get; }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="EvolutionApiClient"/>.
    /// </summary>
    /// <param name="informationService">O serviço de informações da API.</param>
    /// <param name="instanceService">O serviço de instâncias da API.</param>
    /// <param name="chatService">O serviço de chat da API.</param>
    /// <param name="messageService">O serviço de mensagens da API.</param>
    /// <param name="profileService">O serviço de perfil da API.</param>
    /// <param name="webhookService">O serviço de webhook da API.</param>
    /// <param name="settingsService">O serviço de configurações da API.</param>
    /// <param name="groupService">O serviço de grupos da API.</param>
    public EvolutionApiClient(
        IEvolutionInformationService informationService,
        IEvolutionInstanceService instanceService,
        IEvolutionChatService chatService,
        IEvolutionMessageService messageService,
        IEvolutionProfileService profileService,
        IEvolutionWebhookService webhookService,
        IEvolutionSettingsService settingsService,
        IEvolutionGroupService groupService)
    {
        Information = informationService ?? throw new ArgumentNullException(nameof(informationService));
        Instance = instanceService ?? throw new ArgumentNullException(nameof(instanceService));
        Chat = chatService ?? throw new ArgumentNullException(nameof(chatService));
        Message = messageService ?? throw new ArgumentNullException(nameof(messageService));
        Profile = profileService ?? throw new ArgumentNullException(nameof(profileService));
        Webhook = webhookService ?? throw new ArgumentNullException(nameof(webhookService));
        Settings = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
        GroupService = groupService ?? throw new ArgumentNullException(nameof(groupService));
    }
}