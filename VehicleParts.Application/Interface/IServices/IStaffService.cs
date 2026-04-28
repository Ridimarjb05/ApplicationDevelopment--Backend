using VehicleParts.Application.DTOs;

namespace VehicleParts.Application.Interface.IServices;

// this is the contract for Feature 2 - Staff management
// the actual code is written in Infrastructure/Services/StaffService.cs
public interface IStaffService
{
    // get all staff members from the database
    Task<IEnumerable<StaffResponseDto>> GetAllStaffAsync();

    // get one staff member by their ID
    Task<StaffResponseDto?> GetStaffByIdAsync(int staffId);

    // create a new staff member (admin only)
    Task<StaffResponseDto> CreateStaffAsync(CreateStaffDto dto);

    // update an existing staff member's information
    Task<StaffResponseDto?> UpdateStaffAsync(int staffId, UpdateStaffDto dto);

    // delete a staff member from the system
    Task<bool> DeleteStaffAsync(int staffId);
}
