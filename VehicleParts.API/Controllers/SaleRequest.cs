using System.Collections.Generic;

public class SaleRequest
{
    public int CustomerId { get; set; }
    public List<SaleItem> Items { get; set; }
}

public class SaleItem
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}