using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.Models;

public class ContactSearchResultViewModel
{
    public List<ContactResult>? Contacts { get; set; }
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}

public class ContactResult
{
    public string? Id { get; set; }
    public string? PushName { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? RemoteJid { get; set; }
    public string? Name { get; set; }
    public string? NotifyName { get; set; }
    public string? VerifiedName { get; set; }
    public string? Status { get; set; }
    public bool IsGroup { get; set; }
    public bool IsContact { get; set; }
    public bool IsMyContact { get; set; }
    public bool IsWAContact { get; set; }
    public bool IsBlocked { get; set; }
    public DateTime? LastSeen { get; set; }
    public string? About { get; set; }
    public List<string>? Labels { get; set; }
}