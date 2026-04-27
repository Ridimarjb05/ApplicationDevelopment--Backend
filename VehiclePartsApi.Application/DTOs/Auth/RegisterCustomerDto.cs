using System.ComponentModel.DataAnnotations;

namespace VehiclePartsApi.Application.DTOs.Auth;

public class RegisterCustomerDto
{
    [Required]
    public string FullName { get; set; } = string.Empty;
    
    [Required]
    public string Phone { get; set; } = string.Empty;
    
    [Required]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string VehicleNumber { get; set; } = string.Empty;
    
    public string? Make { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
}