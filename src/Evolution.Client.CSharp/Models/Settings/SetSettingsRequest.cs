using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Settings;

/// <summary>
/// Request model for setting instance settings
/// </summary>
public class SetSettingsRequest
{
    /// <summary>
    /// Reject calls automatically
    /// </summary>
    [JsonPropertyName("rejectCall")]
    public bool RejectCall { get; set; }

    /// <summary>
    /// Message to be sent when a call is rejected automatically
    /// </summary>
    [JsonPropertyName("msgCall")]
    public string MsgCall { get; set; } = string.Empty;

    /// <summary>
    /// Ignore group messages
    /// </summary>
    [JsonPropertyName("groupsIgnore")]
    public bool GroupsIgnore { get; set; }

    /// <summary>
    /// Always show WhatsApp online
    /// </summary>
    [JsonPropertyName("alwaysOnline")]
    public bool AlwaysOnline { get; set; }

    /// <summary>
    /// Send read receipts
    /// </summary>
    [JsonPropertyName("readMessages")]
    public bool ReadMessages { get; set; }

    /// <summary>
    /// See message status
    /// </summary>
    [JsonPropertyName("readStatus")]
    public bool ReadStatus { get; set; }

    /// <summary>
    /// Synchronize full WhatsApp history with EvolutionAPI
    /// </summary>
    [JsonPropertyName("syncFullHistory")]
    public bool SyncFullHistory { get; set; }
}