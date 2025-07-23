using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.Models;

public class ProfilePicUrlViewModel
{
    public string? InstanceName { get; set; }
    public string? Number { get; set; }
    public ProfilePicUrlResult? Result { get; set; }
}

public class ProfilePicUrlResult
{
    public string? Wuid { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public bool HasProfilePicture => !string.IsNullOrEmpty(ProfilePictureUrl);
}
