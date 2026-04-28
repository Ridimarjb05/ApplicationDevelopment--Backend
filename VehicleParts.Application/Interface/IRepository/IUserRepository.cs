using VehicleParts.Domain.Models;

namespace VehicleParts.Application.Interface.IRepository;

// this is the contract for talking to the Users table in the database
// the actual code that talks to the database will be written in Infrastructure/Repository
public interface IUserRepository
{
    // find a user by their email address (used for login)
    Task<User?> GetByEmailAsync(string email);

    // find a user by their ID
    Task<User?> GetByIdAsync(int id);

    // add a new user to the database
    Task<User> AddAsync(User user);

    // save any changes made to the database
    Task SaveChangesAsync();
}
