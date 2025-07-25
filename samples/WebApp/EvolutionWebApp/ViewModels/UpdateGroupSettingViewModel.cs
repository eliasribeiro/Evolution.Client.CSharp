using Evolution.Client.CSharp.Models.Group;

namespace EvolutionWebApp.ViewModels;

public class UpdateGroupSettingViewModel
{
    public string InstanceName { get; set; } = string.Empty;
    public string GroupJid { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public UpdateGroupSettingResponse? Result { get; set; }
    public string? ErrorMessage { get; set; }
    public bool IsSuccess => Result != null && string.IsNullOrEmpty(ErrorMessage);
}