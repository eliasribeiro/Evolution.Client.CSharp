using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Group;

public class LeaveGroupResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}