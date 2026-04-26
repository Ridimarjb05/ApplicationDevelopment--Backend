using Microsoft.AspNetCore.Identity;
using VehicleParts.Application.Common;
using VehicleParts.Application.DTOs.Staff;
using VehicleParts.Application.Interfaces;
using VehicleParts.Domain.Enums;
using VehicleParts.Infrastructure.Identity;

namespace VehicleParts.Infrastructure.Services;

// this handles adding staff and changing their roles
// we put it here because it uses identity user manager
public class StaffService : IStaffService
{
    private readonly UserManager<ApplicationUser>    _userManager;
    private readonly RoleManager<IdentityRole>       _roleManager;

    // Only Admin and Staff are valid for this endpoint — Customer is excluded.
    private static readonly HashSet<string> AllowedRoles =
        new(StringComparer.OrdinalIgnoreCase) { AppRoles.Admin, AppRoles.Staff };

    public StaffService(
        UserManager<ApplicationUser>  userManager,
        RoleManager<IdentityRole>     roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // POST /api/admin/staff

    public async Task<ApiResponse<CreateStaffResponseDto>> CreateStaffAsync(CreateStaffDto dto)
    {
        // Validate role
        var targetRole = string.IsNullOrWhiteSpace(dto.Role) ? AppRoles.Staff : dto.Role;
        if (!AllowedRoles.Contains(targetRole))
            return ApiResponse<CreateStaffResponseDto>.Fail(
                $"Role '{targetRole}' is not valid for staff. Allowed: Admin, Staff.");

        // Reject duplicate email
        if (await _userManager.FindByEmailAsync(dto.Email) is not null)
            return ApiResponse<CreateStaffResponseDto>.Fail(
                $"A user with email '{dto.Email}' already exists.");

        // Generate temp password if none supplied
        bool tempGenerated   = string.IsNullOrWhiteSpace(dto.Password);
        string password      = tempGenerated
            ? $"Temp@{Guid.NewGuid().ToString("N")[..8]}1!"
            : dto.Password!;

        var user = new ApplicationUser
        {
            UserName    = dto.Email,
            Email       = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            FullName    = dto.FullName,
            CreatedAt   = DateTime.UtcNow,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return ApiResponse<CreateStaffResponseDto>.Fail(
                "Failed to create user.",
                result.Errors.Select(e => e.Description));

        await _userManager.AddToRoleAsync(user, targetRole);

        var response = await BuildCreateResponseAsync(user, tempGenerated ? password : null);
        return ApiResponse<CreateStaffResponseDto>.Ok(response, "Staff member created successfully.");
    }

    //  GET /api/admin/staff

    public async Task<ApiResponse<PagedResult<StaffResponseDto>>> GetAllStaffAsync(
        string? search, string? role, int page, int pageSize)
    {
        var allUsers = _userManager.Users.ToList();

        // Build (user, roles) pairs, filtering out pure-Customer accounts
        var staffList = new List<(ApplicationUser user, IList<string> roles)>();
        foreach (var u in allUsers)
        {
            var roles = await _userManager.GetRolesAsync(u);

            bool isPureCustomer = roles.Contains(AppRoles.Customer)
                                && !roles.Contains(AppRoles.Admin)
                                && !roles.Contains(AppRoles.Staff);
            if (isPureCustomer) continue;

            // Optional role filter
            if (!string.IsNullOrWhiteSpace(role) &&
                !roles.Contains(role, StringComparer.OrdinalIgnoreCase))
                continue;

            staffList.Add((u, roles));
        }

        // Search by full name / email / phone
        if (!string.IsNullOrWhiteSpace(search))
        {
            var q = search.ToLower();
            staffList = staffList.Where(x =>
                x.user.FullName.ToLower().Contains(q)       ||
                (x.user.Email       ?? "").ToLower().Contains(q) ||
                (x.user.PhoneNumber ?? "").ToLower().Contains(q)
            ).ToList();
        }

        var totalCount = staffList.Count;
        var items = staffList
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => BuildStaffResponse(x.user, x.roles))
            .ToList();

        return ApiResponse<PagedResult<StaffResponseDto>>.Ok(new PagedResult<StaffResponseDto>
        {
            Items      = items,
            TotalCount = totalCount,
            Page       = page,
            PageSize   = pageSize
        });
    }

    // PUT /api/admin/staff/{id}/role 

    public async Task<ApiResponse<StaffResponseDto>> UpdateRoleAsync(string userId, UpdateRoleDto dto)
    {
        if (!AllowedRoles.Contains(dto.NewRole))
            return ApiResponse<StaffResponseDto>.Fail(
                $"Role '{dto.NewRole}' is not valid. Allowed: Admin, Staff.");

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return ApiResponse<StaffResponseDto>.Fail("Staff member not found.");

        // Swap roles atomically: remove all → add new
        var currentRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, currentRoles);
        await _userManager.AddToRoleAsync(user, dto.NewRole);

        var updatedRoles = await _userManager.GetRolesAsync(user);
        return ApiResponse<StaffResponseDto>.Ok(
            BuildStaffResponse(user, updatedRoles),
            $"Role updated to '{dto.NewRole}' successfully.");
    }

    // PUT /api/admin/staff/{id}/status

    public async Task<ApiResponse<StaffResponseDto>> UpdateStatusAsync(string userId, UpdateStatusDto dto)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return ApiResponse<StaffResponseDto>.Fail("Staff member not found.");

        if (dto.IsActive)
        {
            // Activate: lift the lockout
            await _userManager.SetLockoutEnabledAsync(user, false);
            await _userManager.SetLockoutEndDateAsync(user, null);
        }
        else
        {
            // Deactivate: lock out indefinitely
            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
        }

        var roles    = await _userManager.GetRolesAsync(user);
        var response = BuildStaffResponse(user, roles);
        response.IsActive = dto.IsActive;   // reflect intended state immediately

        return ApiResponse<StaffResponseDto>.Ok(
            response,
            dto.IsActive ? "Staff member activated." : "Staff member deactivated.");
    }

    // Private helpers

    private static StaffResponseDto BuildStaffResponse(ApplicationUser user, IList<string> roles)
    {
        bool isActive = !(user.LockoutEnabled && user.LockoutEnd >= DateTimeOffset.UtcNow);

        return new StaffResponseDto
        {
            Id          = user.Id,
            FullName    = user.FullName,
            Email       = user.Email ?? string.Empty,
            PhoneNumber = user.PhoneNumber ?? string.Empty,
            Roles       = roles,
            IsActive    = isActive,
            CreatedAt   = user.CreatedAt
        };
    }

    private async Task<CreateStaffResponseDto> BuildCreateResponseAsync(
        ApplicationUser user, string? tempPassword)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return new CreateStaffResponseDto
        {
            Id           = user.Id,
            FullName     = user.FullName,
            Email        = user.Email ?? string.Empty,
            PhoneNumber  = user.PhoneNumber ?? string.Empty,
            Roles        = roles,
            IsActive     = true,
            CreatedAt    = user.CreatedAt,
            TempPassword = tempPassword
        };
    }
}
