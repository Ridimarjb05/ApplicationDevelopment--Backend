using System.Text.Json.Serialization;
using VehicleParts.Application.DTOs.Vehicle;

namespace VehicleParts.Application.DTOs.Customer;

public class CustomerResponseDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    [JsonPropertyName("nic")]
    public string NIC { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<VehicleResponseDto> Vehicles { get; set; } = new();
}
