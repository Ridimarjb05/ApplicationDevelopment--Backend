using AutoMapper;
using VehicleParts.Application.DTOs.Customer;
using VehicleParts.Application.Interface.IRepository;
using VehicleParts.Application.Interface.IServices;
using VehicleParts.Domain.Models;

namespace VehicleParts.Infrastructure.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerResponseDto>> GetAllCustomersAsync()
    {
        var customers = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
    }

    public async Task<CustomerResponseDto?> GetCustomerByIdAsync(int id)
    {
        var customer = await _repository.GetByIdAsync(id);
        return customer is null ? null : _mapper.Map<CustomerResponseDto>(customer);
    }

    public async Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerDto dto)
    {
        var customer = _mapper.Map<Customer>(dto);
        var created = await _repository.CreateAsync(customer);
        return _mapper.Map<CustomerResponseDto>(created);
    }

    public async Task<CustomerResponseDto?> UpdateCustomerAsync(int id, UpdateCustomerDto dto)
    {
        var customer = _mapper.Map<Customer>(dto);
        var updated = await _repository.UpdateAsync(id, customer);
        return updated is null ? null : _mapper.Map<CustomerResponseDto>(updated);
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
