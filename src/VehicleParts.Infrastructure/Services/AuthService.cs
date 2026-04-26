using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using VehicleParts.Application.Common;
using VehicleParts.Application.DTOs;
using VehicleParts.Application.Interfaces;
using VehicleParts.Domain.Enums;
using VehicleParts.Infrastructure.Identity;

namespace VehicleParts.Infrastructure.Services;

// checks user passwords and creates their login token
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _config;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto dto)
    {
        // Find user by email
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null)
            return ApiResponse<LoginResponseDto>.Fail("Invalid email or password.");

        // Check account is not locked out
        if (await _userManager.IsLockedOutAsync(user))
            return ApiResponse<LoginResponseDto>.Fail("Account is deactivated. Contact an administrator.");

        // Verify password
        if (!await _userManager.CheckPasswordAsync(user, dto.Password))
            return ApiResponse<LoginResponseDto>.Fail("Invalid email or password.");

        // Build JWT
        var roles = await _userManager.GetRolesAsync(user);
        var token = GenerateToken(user, roles);
        var expMin = int.TryParse(_config["Jwt:ExpMinutes"], out var m) ? m : 60;

        return ApiResponse<LoginResponseDto>.Ok(new LoginResponseDto
        {
            Token = token,
            Email = user.Email ?? "",
            FullName = user.FullName,
            Roles = roles,
            ExpiresAt = DateTime.UtcNow.AddMinutes(expMin)
        }, "Login successful.");
    }

    public async Task<ApiResponse<string>> RegisterCustomerAsync(RegisterCustomerRequestDto dto)
    {
        var existingUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existingUser is not null)
            return ApiResponse<string>.Fail("An account with this email already exists.");

        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            FullName = dto.FullName,
            PhoneNumber = dto.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return ApiResponse<string>.Fail($"Failed to create account: {errors}");
        }

        // Assign Customer role
        await _userManager.AddToRoleAsync(user, AppRoles.Customer);

        return ApiResponse<string>.Ok(user.Id, "Account created successfully.");
    }

    // JWT generation 

    private string GenerateToken(ApplicationUser user, IList<string> roles)
    {
        var jwtSection = _config.GetSection("Jwt");
        var key = new SymmetricSecurityKey(
                             Encoding.UTF8.GetBytes(jwtSection["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expMin = int.TryParse(jwtSection["ExpMinutes"], out var m) ? m : 60;

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub,   user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new("fullName",                    user.FullName),
            new(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString())
        };

        // Add role claims so [Authorize(Roles="Admin")] works
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var token = new JwtSecurityToken(
            issuer: jwtSection["Issuer"],
            audience: jwtSection["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expMin),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
