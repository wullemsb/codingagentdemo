#nullable enable

namespace DataContract.Models;

public class EventsFilter
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string? Location { get; set; }
}