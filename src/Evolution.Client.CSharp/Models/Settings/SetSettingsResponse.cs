using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Settings;

/// <summary>
/// Response model for setting instance settings
/// </summary>
public class SetSettingsResponse
{
    /// <summary>
    /// Settings configuration object
    /// </summary>
    [JsonPropertyName("settings")]
    public SettingsInfo Settings { get; set; } = new();
}

/// <summary>
/// Settings information
/// </summary>
public class SettingsInfo
{
    /// <summary>
    /// Indicates whether to reject incoming calls
    /// </summary>
    [JsonPropertyName("reject_call")]
    public bool RejectCall { get; set; }

    /// <summary>
    /// Message to be sent when a call is rejected automatically
    /// </summary>
    [JsonPropertyName("msg_call")]
    public string MsgCall { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether to ignore group messages
    /// </summary>
    [JsonPropertyName("groups_ignore")]
    public bool GroupsIgnore { get; set; }

    /// <summary>
    /// Indicates whether to always keep the instance online
    /// </summary>
    [JsonPropertyName("always_online")]
    public bool AlwaysOnline { get; set; }

    /// <summary>
    /// Indicates whether to mark messages as read
    /// </summary>
    [JsonPropertyName("read_messages")]
    public bool ReadMessages { get; set; }

    /// <summary>
    /// Indicates whether to read status updates
    /// </summary>
    [JsonPropertyName("read_status")]
    public bool ReadStatus { get; set; }

    /// <summary>
    /// Indicates whether to synchronize full message history
    /// </summary>
    [JsonPropertyName("sync_full_history")]
    public bool SyncFullHistory { get; set; }
}