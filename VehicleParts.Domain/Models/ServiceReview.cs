namespace VehicleParts.Domain.Models;

public class ServiceReview
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int AppointmentId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime ReviewedAt { get; set; } = DateTime.UtcNow;
}