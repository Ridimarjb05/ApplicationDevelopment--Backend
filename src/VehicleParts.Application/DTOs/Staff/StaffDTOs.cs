namespace VehicleParts.Application.DTOs.Staff;

// Request DTOs 

/// Payload for POST /api/admin/staff — creates a new staff user
public class CreateStaffDto
{
    public string  FullName    { get; set; } = string.Empty;
    public string  Email       { get; set; } = string.Empty;
    public string  PhoneNumber { get; set; } = string.Empty;

    /// Optional — if omitted, a temporary password is generated
    public string? Password    { get; set; }

    /// Target role: Admin or Staff (default: Staff).
    public string  Role        { get; set; } = "Staff";
}

/// Payload for PUT /api/admin/staff/{id}/role
public class UpdateRoleDto
{
    public string NewRole { get; set; } = string.Empty;
}

/// Payload for PUT /api/admin/staff/{id}/status
public class UpdateStatusDto
{
    public bool IsActive { get; set; }
}

// Response DTOs 

///Staff user data returned by GET and POST endpoints
public class StaffResponseDto
{
    public string        Id          { get; set; } = string.Empty;
    public string        FullName    { get; set; } = string.Empty;
    public string        Email       { get; set; } = string.Empty;
    public string        PhoneNumber { get; set; } = string.Empty;
    public IList<string> Roles       { get; set; } = new List<string>();
    public bool          IsActive    { get; set; }
    public DateTime      CreatedAt   { get; set; }
}

/// Returned after creating a staff user — includes temp password if generated
public class CreateStaffResponseDto : StaffResponseDto
{
    public string? TempPassword { get; set; }
}
