using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.Models;

public class DeleteMessageViewModel
{
    public string? InstanceName { get; set; }
    public string? MessageId { get; set; }
    public string? RemoteJid { get; set; }
    public bool FromMe { get; set; }
    public string? Participant { get; set; }
    public DeleteMessageResult? Result { get; set; }
}

public class DeleteMessageResult
{
    public string? MessageId { get; set; }
    public string? RemoteJid { get; set; }
    public bool FromMe { get; set; }
    public string? MessageTimestamp { get; set; }
    public string? Status { get; set; }
    public string? ProtocolType { get; set; }
    public bool IsSuccess => !string.IsNullOrEmpty(Status);
    public DateTime? TimestampAsDateTime
    {
        get
        {
            if (long.TryParse(MessageTimestamp, out var timestamp))
            {
                return DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
            }
            return null;
        }
    }
}
