namespace VehicleParts.Application.DTOs
{
    public class SaleRequestDto
    {
        public int CustomerId { get; set; }
        public List<SaleItemDto> Items { get; set; } = new();
    }

    public class SaleItemDto
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}