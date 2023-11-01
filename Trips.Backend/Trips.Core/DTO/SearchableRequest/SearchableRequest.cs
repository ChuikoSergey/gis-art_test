namespace Trips.Core.DTO.SearchableRequest;

public class SearchableRequest
{
    public string? OrderBy { get; set; }
    public bool AscendingOrder { get; set; }
    public string? SearchBy { get; set; }
}
