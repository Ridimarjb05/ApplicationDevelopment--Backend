using VehicleParts.Domain.Common;

namespace VehicleParts.Domain.Entities;


/// Records every stock movement (purchases / adjustments) for a part.

public class StockTransaction : BaseEntity
{
    public Guid   PartId    { get; set; }
    public Part   Part      { get; set; } = null!;

    /// Number of units added to stock (always positive for purchase).
    public int    Quantity  { get; set; }

    /// Human-readable reason, e.g. "Purchase", "Adjustment".
    public string Reason    { get; set; } = "Purchase";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
