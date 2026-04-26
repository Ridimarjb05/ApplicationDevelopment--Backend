namespace VehicleParts.Application.Common;


/// Wraps a page of results with pagination metadata.

/// <typeparam name="T">Item type.</typeparam>
public class PagedResult<T>
{
    public IEnumerable<T> Items      { get; set; } = Enumerable.Empty<T>();
    public int            TotalCount { get; set; }
    public int            Page       { get; set; }
    public int            PageSize   { get; set; }
    public int            TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
