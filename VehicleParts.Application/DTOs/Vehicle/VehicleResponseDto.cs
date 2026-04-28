namespace VehicleParts.Application.DTOs.Vehicle;

public class VehicleResponseDto
{
    public int Id { get; set; }
    public string RegistrationNumber { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Color { get; set; } = string.Empty;
    public string VinNumber { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
}
