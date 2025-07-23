using System.ComponentModel.DataAnnotations;

namespace EvolutionWebApp.Models;

public class MessageSearchResultViewModel
{
    public List<MessageResult>? Messages { get; set; }
    public int TotalCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}

public class MessageResult
{
    public string? Key { get; set; }
    public string? PushName { get; set; }
    public string? Message { get; set; }
    public string? MessageType { get; set; }
    public string? ChatId { get; set; }
    public bool FromMe { get; set; }
    public DateTime DateTime { get; set; }
    public string? Status { get; set; }
    public string? Source { get; set; }
    public MessageContent? MessageContent { get; set; }
}

public class MessageContent
{
    public string? Text { get; set; }
    public string? Caption { get; set; }
    public string? MimeType { get; set; }
    public string? FileName { get; set; }
    public long? FileLength { get; set; }
    public string? Url { get; set; }
    public LocationContent? Location { get; set; }
    public ContactContent? Contact { get; set; }
    public List<ContactContent>? Contacts { get; set; }
    public PollContent? Poll { get; set; }
    public ListContent? List { get; set; }
    public ButtonContent? Buttons { get; set; }
}

public class LocationContent
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
}

public class ContactContent
{
    public string? FullName { get; set; }
    public string? WaId { get; set; }
    public string? DisplayName { get; set; }
    public List<PhoneNumber>? PhoneNumbers { get; set; }
}

public class PhoneNumber
{
    public string? Phone { get; set; }
    public string? Type { get; set; }
    public string? WaId { get; set; }
}

public class PollContent
{
    public string? Name { get; set; }
    public List<PollOption>? Options { get; set; }
    public bool SelectableCount { get; set; }
}

public class PollOption
{
    public string? Name { get; set; }
    public int Count { get; set; }
}

public class ListContent
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ButtonText { get; set; }
    public string? FooterText { get; set; }
    public List<ListSection>? Sections { get; set; }
}

public class ListSection
{
    public string? Title { get; set; }
    public List<ListRow>? Rows { get; set; }
}

public class ListRow
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? RowId { get; set; }
}

public class ButtonContent
{
    public string? Text { get; set; }
    public List<MessageButton>? Buttons { get; set; }
}

public class MessageButton
{
    public string? DisplayText { get; set; }
    public string? Id { get; set; }
    public string? Type { get; set; }
}