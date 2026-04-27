using VehiclePartsApi.Application.DTOs.Staff;
using VehiclePartsApi.Application.Interfaces;
using VehiclePartsApi.Domain.Interfaces;

namespace VehiclePartsApi.Application.Services;

public class StaffService : IStaffService
{
    private readonly ICustomerRepository _customerRepo;
    private readonly IVehicleRepository _vehicleRepo;

    public StaffService(ICustomerRepository customerRepo, IVehicleRepository vehicleRepo)
    {
        _customerRepo = customerRepo;
        _vehicleRepo = vehicleRepo;
    }

    public async Task<List<CustomerSearchResultDto>> SearchCustomersAsync(string type, string value)
    {
        var customers = await _customerRepo.SearchAsync(type, value, 30);
        return MapToDto(customers);
    }

    public async Task<List<CustomerSearchResultDto>> GetRecentRegistrationsAsync(int count = 5)
    {
        var customers = await _customerRepo.SearchAsync("recent", "", count); 
        return MapToDto(customers);
    }

    public async Task<DashboardStatsDto> GetDashboardStatsAsync()
    {
        return new DashboardStatsDto
        {
            TotalCustomers = await _customerRepo.GetTotalCountAsync(),
            TotalVehicles = await _vehicleRepo.GetTotalCountAsync(),
            RecentInvoicesCount = 0, // Not implemented yet
            SystemStatus = "Operational"
        };
    }

    private List<CustomerSearchResultDto> MapToDto(List<VehiclePartsApi.Domain.Entities.CustomerProfile> customers)
    {
        return customers.Select(c => new CustomerSearchResultDto
        {
            ProfileId = c.Id,
            FullName = c.FullName,
            Phone = c.Phone,
            Email = c.Email,
            Vehicles = c.Vehicles?.Select(v => new VehicleBriefDto
            {
                VehicleNumber = v.VehicleNumber,
                Make = v.Make,
                Model = v.Model
            }).ToList() ?? new()
        }).ToList();
    }
}