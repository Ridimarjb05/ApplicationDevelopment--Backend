using VehicleParts.Application.Common;
using VehicleParts.Application.DTOs.Staff;

namespace VehicleParts.Application.Interfaces;


/// Manages staff user registration, role assignment, and activation — Feature 2.

public interface IStaffService
{
    /// Creates a new Identity user and assigns the given role.
    Task<ApiResponse<CreateStaffResponseDto>> CreateStaffAsync(CreateStaffDto dto);

    /// Returns paginated staff list with optional search and role filter.
    Task<ApiResponse<PagedResult<StaffResponseDto>>> GetAllStaffAsync(
        string? search, string? role, int page, int pageSize);

    /// Replaces the user's current role(s) with the new role
    Task<ApiResponse<StaffResponseDto>> UpdateRoleAsync(string userId, UpdateRoleDto dto);

    ///Activates or deactivates a staff account via lockout.
    Task<ApiResponse<StaffResponseDto>> UpdateStatusAsync(string userId, UpdateStatusDto dto);
}
