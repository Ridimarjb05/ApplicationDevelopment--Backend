namespace VehicleParts.Domain.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalAmount { get; set; }

        public DateTime Date { get; set; }
    }
}