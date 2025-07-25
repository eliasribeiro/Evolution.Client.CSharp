using Evolution.Client.CSharp.Models.Group;

namespace EvolutionWebApp.ViewModels;

public class UpdateParticipantViewModel
{
    public string InstanceName { get; set; } = string.Empty;
    public string GroupJid { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string Participants { get; set; } = string.Empty; // Textarea for multiple participants
    public UpdateParticipantResponse? Result { get; set; }
    public string? ErrorMessage { get; set; }
    public bool IsSuccess => Result != null && string.IsNullOrEmpty(ErrorMessage);
}