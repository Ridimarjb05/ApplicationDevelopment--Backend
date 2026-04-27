using System.ComponentModel.DataAnnotations;

namespace VehiclePartsApi.Application.DTOs.Customer;

public class VehicleDto
{
    [Required]
    public string VehicleNumber { get; set; } = string.Empty;
    
    [Required]
    public string Make { get; set; } = string.Empty;
    
    [Required]
    public string Model { get; set; } = string.Empty;
    
    [Required]
    public int Year { get; set; }
}