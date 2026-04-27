namespace VehiclePartsApi.Application.DTOs.Staff;

public class CustomerSearchResultDto
{
    public int ProfileId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<VehicleBriefDto> Vehicles { get; set; } = new();
}

public class VehicleBriefDto
{
    public string VehicleNumber { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
}