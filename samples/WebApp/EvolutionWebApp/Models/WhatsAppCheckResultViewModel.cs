using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.Models;

public class WhatsAppCheckResultViewModel
{
    public List<WhatsAppCheckResult>? Results { get; set; }
    public int TotalCount { get; set; }
    public string? InstanceName { get; set; }
    public string? Numbers { get; set; }
}

public class WhatsAppCheckResult
{
    public string? Number { get; set; }
    public bool Exists { get; set; }
    public string? Jid { get; set; }
    public string? Name { get; set; }
    public string? PictureUrl { get; set; }
    public string? Status { get; set; }
    public bool IsBusiness { get; set; }
    public DateTime? CheckedAt { get; set; }
}