using Evolution.Client.CSharp.Models.Group;

namespace EvolutionWebApp.ViewModels;

public class FetchAllGroupsViewModel
{
    public string InstanceName { get; set; } = string.Empty;
    public List<FetchAllGroupsResponse>? Groups { get; set; }
    public string? ErrorMessage { get; set; }
    public bool IsSuccess => Groups != null && string.IsNullOrEmpty(ErrorMessage);
}