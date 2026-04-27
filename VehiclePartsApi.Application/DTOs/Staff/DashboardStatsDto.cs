namespace VehiclePartsApi.Application.DTOs.Staff;

public class DashboardStatsDto
{
    public int TotalCustomers { get; set; }
    public int TotalVehicles { get; set; }
    public int RecentInvoicesCount { get; set; }
    public string SystemStatus { get; set; } = "Active";
}
