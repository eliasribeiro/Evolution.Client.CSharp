using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Group;

public class UpdateParticipantRequest
{
    [JsonPropertyName("action")]
    public string Action { get; set; } = string.Empty;

    [JsonPropertyName("participants")]
    public List<string> Participants { get; set; } = new();
}

public class UpdateParticipantResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}