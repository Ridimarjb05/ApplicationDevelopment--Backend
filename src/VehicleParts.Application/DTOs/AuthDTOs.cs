namespace VehicleParts.Application.DTOs;

// Request DTOs

///Payload for POST /api/auth/login
public class LoginRequestDto
{
    public string Email    { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

/// Payload for POST /api/auth/register-customer
public class RegisterCustomerRequestDto
{
    public string FullName    { get; set; } = string.Empty;
    public string Email       { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password    { get; set; } = string.Empty;
}

//Response DTOs

/// JWT token returned on successful login
public class LoginResponseDto
{
    public string        Token     { get; set; } = string.Empty;
    public string        Email     { get; set; } = string.Empty;
    public string        FullName  { get; set; } = string.Empty;
    public IList<string> Roles     { get; set; } = new List<string>();
    public DateTime      ExpiresAt { get; set; }
}
