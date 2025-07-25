using System.Text.Json.Serialization;

namespace Evolution.Client.CSharp.Models.Group;

public class FetchAllGroupsResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("subject")]
    public string Subject { get; set; } = string.Empty;

    [JsonPropertyName("subjectOwner")]
    public string SubjectOwner { get; set; } = string.Empty;

    [JsonPropertyName("subjectTime")]
    public long SubjectTime { get; set; }

    [JsonPropertyName("pictureUrl")]
    public string? PictureUrl { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("creation")]
    public long Creation { get; set; }

    [JsonPropertyName("owner")]
    public string Owner { get; set; } = string.Empty;

    [JsonPropertyName("desc")]
    public string? Description { get; set; }

    [JsonPropertyName("descId")]
    public string? DescriptionId { get; set; }

    [JsonPropertyName("restrict")]
    public bool Restrict { get; set; }

    [JsonPropertyName("announce")]
    public bool Announce { get; set; }

    [JsonPropertyName("participants")]
    public List<GroupParticipant> Participants { get; set; } = new();
}