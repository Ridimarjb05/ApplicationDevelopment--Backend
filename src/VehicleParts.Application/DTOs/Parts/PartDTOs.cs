namespace VehicleParts.Application.DTOs.Parts;

// Request DTOs 

/// Payload for POST /api/admin/parts
public class CreatePartDto
{
    public string   Name         { get; set; } = string.Empty;
    public string   SKU          { get; set; } = string.Empty;
    public string   Category     { get; set; } = string.Empty;
    public string?  Brand        { get; set; }
    public string?  Description  { get; set; }
    public decimal  UnitPrice    { get; set; }
    public int      StockQty     { get; set; }
    public int      ReorderLevel { get; set; } = 10;
}

/// <summary>Payload for PUT /api/admin/parts/{id} — SKU is intentionally excluded (immutable).</summary>
public class UpdatePartDto
{
    public string?  Name         { get; set; }
    public string?  Category     { get; set; }
    public string?  Brand        { get; set; }
    public string?  Description  { get; set; }
    public decimal? UnitPrice    { get; set; }
    public int?     ReorderLevel { get; set; }
}

/// <summary>Payload for POST /api/admin/parts/{id}/stock-in</summary>
public class StockInDto
{
    /// <summary>Number of units to add — must be greater than zero.</summary>
    public int    Quantity { get; set; }
    public string Reason   { get; set; } = "Purchase";
}

/// <summary>Query parameters for GET /api/admin/parts</summary>
public class PartQueryDto
{
    public string? Search   { get; set; }
    public string? SortBy   { get; set; }  // name | price | stock
    public int     Page     { get; set; } = 1;
    public int     PageSize { get; set; } = 10;
}

// ── Response DTOs ────────────────────────────────────────────────────────────

/// <summary>Part data returned by list and detail endpoints.</summary>
public class PartResponseDto
{
    public Guid     Id           { get; set; }
    public string   Name         { get; set; } = string.Empty;
    public string   SKU          { get; set; } = string.Empty;
    public string   Category     { get; set; } = string.Empty;
    public string?  Brand        { get; set; }
    public string?  Description  { get; set; }
    public decimal  UnitPrice    { get; set; }
    public int      StockQty     { get; set; }
    public int      ReorderLevel { get; set; }
    public bool     IsLowStock   { get; set; }   // StockQty < ReorderLevel
    public DateTime CreatedAt    { get; set; }
    public DateTime UpdatedAt    { get; set; }
}
