using VehicleParts.Application.DTOs.Customer;

namespace VehicleParts.Application.Interface.IServices;

public interface ICustomerService
{
    Task<IEnumerable<CustomerResponseDto>> GetAllCustomersAsync();
    Task<CustomerResponseDto?> GetCustomerByIdAsync(int id);
    Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerDto dto);
    Task<CustomerResponseDto?> UpdateCustomerAsync(int id, UpdateCustomerDto dto);
    Task<bool> DeleteCustomerAsync(int id);
}
