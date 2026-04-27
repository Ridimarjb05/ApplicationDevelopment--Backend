using VehiclePartsApi.Application.DTOs.Staff;

namespace VehiclePartsApi.Application.Interfaces;

public interface IStaffService
{
    Task<List<CustomerSearchResultDto>> SearchCustomersAsync(string type, string value);
    Task<List<CustomerSearchResultDto>> GetRecentRegistrationsAsync(int count = 5);
    Task<DashboardStatsDto> GetDashboardStatsAsync();
}