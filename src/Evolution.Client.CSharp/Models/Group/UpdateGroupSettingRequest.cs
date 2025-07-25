using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Group;

public class UpdateGroupSettingRequest
{
    [JsonPropertyName("action")]
    public string Action { get; set; } = string.Empty;
}

public class UpdateGroupSettingResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}