using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using VehicleParts.Application.DTOs.Vehicle;

namespace VehicleParts.Application.DTOs.Customer;

public class CreateCustomerDto
{
    [Required, MaxLength(60)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(60)]
    public string LastName { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required, Phone, MaxLength(20)]
    public string Phone { get; set; } = string.Empty;

    [MaxLength(250)]
    public string Address { get; set; } = string.Empty;

    [JsonPropertyName("nic")]
    [Required, MaxLength(20)]
    public string NIC { get; set; } = string.Empty;

    // At least one vehicle must be registered with the customer
    [Required, MinLength(1)]
    public List<CreateVehicleDto> Vehicles { get; set; } = new();
}
