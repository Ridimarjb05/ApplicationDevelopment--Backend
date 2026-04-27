namespace VehicleParts.Application.DTOs;

public class CreateAppointmentDto
{
    public int CustomerId { get; set; }
    public int VehicleId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string ServiceType { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}
