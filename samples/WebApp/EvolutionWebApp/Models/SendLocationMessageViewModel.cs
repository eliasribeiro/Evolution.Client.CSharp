using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.Models;

public class SendLocationMessageViewModel
{
    public string? InstanceName { get; set; }
    public string? Number { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int? Delay { get; set; }
    public bool LinkPreview { get; set; }
    public bool MentionsEveryOne { get; set; }
    public string? Mentioned { get; set; }
    public string? QuotedMessageId { get; set; }
    public string? QuotedMessageText { get; set; }
    public SendLocationResult? Result { get; set; }
}

public class SendLocationResult
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