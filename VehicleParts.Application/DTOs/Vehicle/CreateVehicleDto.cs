using System.ComponentModel.DataAnnotations;

namespace VehicleParts.Application.DTOs.Vehicle;

public class CreateVehicleDto
{
    [Required, MaxLength(20)]
    public string RegistrationNumber { get; set; } = string.Empty;

    [Required, MaxLength(60)]
    public string Make { get; set; } = string.Empty;

    [Required, MaxLength(60)]
    public string Model { get; set; } = string.Empty;

    [Range(1900, 2100)]
    public int Year { get; set; }

    [MaxLength(30)]
    public string Color { get; set; } = string.Empty;

    [MaxLength(17)]
    public string VinNumber { get; set; } = string.Empty;
}
