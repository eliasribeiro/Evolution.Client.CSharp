using Evolution.Client.CSharp.Models.Group;

namespace EvolutionWebApp.ViewModels;

public class FindParticipantsViewModel
{
    public string InstanceName { get; set; } = string.Empty;
    public string GroupJid { get; set; } = string.Empty;
    public FindParticipantsResponse? Result { get; set; }
    public string? ErrorMessage { get; set; }
    public bool IsSuccess => Result != null && string.IsNullOrEmpty(ErrorMessage);
}