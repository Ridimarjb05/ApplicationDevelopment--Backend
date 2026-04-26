using VehicleParts.Domain.Common;

namespace VehicleParts.Domain.Entities;

/// Represents a vehicle part in the inventory.

public class Part : BaseEntity
{
    public string Name          { get; set; } = string.Empty;

    /// Stock-Keeping Unit — unique identifier for the part
    public string SKU           { get; set; } = string.Empty;

    public string Category      { get; set; } = string.Empty;
    public string? Brand        { get; set; }
    public string? Description  { get; set; }

    /// Selling price per unit
    public decimal UnitPrice    { get; set; }

    /// <summary>Current stock quantity on hand.</summary>
    public int StockQty         { get; set; }

    ///Minimum stock level before a low-stock alert is triggered (default 10).
    public int ReorderLevel     { get; set; } = 10;

    /// Soft-delete flag — deleted parts are hidden but history is preserved.
    public bool IsDeleted       { get; set; } = false;

    public DateTime CreatedAt   { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt   { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
}
