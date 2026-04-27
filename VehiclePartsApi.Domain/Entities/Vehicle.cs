namespace VehiclePartsApi.Domain.Entities;

public class Vehicle
{
    public int Id { get; set; }
    public int CustomerProfileId { get; set; }
    public string VehicleNumber { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public CustomerProfile? CustomerProfile { get; set; }
}