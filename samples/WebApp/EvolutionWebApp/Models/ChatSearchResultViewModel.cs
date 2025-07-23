using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.Models;

public class ChatSearchResultViewModel
{
    public List<ChatResult>? Chats { get; set; }
    public int TotalCount { get; set; }
    public string? InstanceName { get; set; }
    public string? SearchId { get; set; }
    public string? SearchRemoteJid { get; set; }
    public string? SearchPushName { get; set; }
}

public class ChatResult
{
    public string? Id { get; set; }
    public string? RemoteJid { get; set; }
    public string? PushName { get; set; }
    public string? ProfilePicUrl { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime WindowStart { get; set; }
    public DateTime WindowExpires { get; set; }
    public bool WindowActive { get; set; }
    public string? ChatType { get; set; } // Individual ou Grupo
    public bool IsGroup => RemoteJid?.Contains("@g.us") == true;
}
