using VehicleParts.Domain.Models;

namespace VehicleParts.Application.Interface.IRepository;

// this is the contract for the Staff table in the database
// Feature 2: Admin manages staff members
public interface IStaffRepository
{
    // get all staff members from the database
    Task<IEnumerable<StaffDetail>> GetAllAsync();

    // find one staff member by their ID
    Task<StaffDetail?> GetByIdAsync(int staffId);

    // find staff by their linked user ID
    Task<StaffDetail?> GetByUserIdAsync(int userId);

    // add a new staff member to the database
    Task<StaffDetail> AddAsync(StaffDetail staff);

    // update an existing staff member
    Task UpdateAsync(StaffDetail staff);

    // remove a staff member from the database
    Task DeleteAsync(StaffDetail staff);

    // save any changes made to the database
    Task SaveChangesAsync();
}
