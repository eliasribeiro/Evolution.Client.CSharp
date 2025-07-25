using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Group;

public class FindParticipantsResponse
{
    [JsonPropertyName("participants")]
    public List<GroupParticipant> Participants { get; set; } = new();
}