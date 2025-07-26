using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.Models;

public class SendAudioMessageViewModel
{
    public string? InstanceName { get; set; }
    public string? Number { get; set; }
    public string? Audio { get; set; }
    public int? Delay { get; set; }
    public bool? LinkPreview { get; set; }
    public bool? MentionsEveryOne { get; set; }
    public string? Mentioned { get; set; }
    public string? QuotedMessageId { get; set; }
    public string? QuotedMessageText { get; set; }
    public SendAudioResult? Result { get; set; }
}

public class SendAudioResult
{
    public string? MessageId { get; set; }
    public string? RemoteJid { get; set; }
    public bool FromMe { get; set; }
    public string? MessageTimestamp { get; set; }
    public string? Status { get; set; }
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
