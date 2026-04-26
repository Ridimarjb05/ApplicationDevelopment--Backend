using Microsoft.AspNetCore.Identity;

namespace VehicleParts.Infrastructure.Identity;


/// Extends IdentityUser with the extra fields required by this application.
/// Stored in the AspNetUsers table via EF Core Identity.

public class ApplicationUser : IdentityUser
{
    ///Staff or customer's display name
    public string FullName  { get; set; } = string.Empty;

    /// UTC timestamp of when this account was created.
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
