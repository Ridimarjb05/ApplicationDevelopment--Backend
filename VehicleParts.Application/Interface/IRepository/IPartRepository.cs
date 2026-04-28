using VehicleParts.Domain.Models;

namespace VehicleParts.Application.Interface.IRepository;

// this is the contract for the Parts table in the database
// Feature 3: Admin manages vehicle parts inventory
public interface IPartRepository
{
    // get all active parts from the database
    Task<IEnumerable<Part>> GetAllAsync();

    // find one part by its ID
    Task<Part?> GetByIdAsync(int partId);

    // check if a part with this part number already exists (no duplicates allowed)
    Task<Part?> GetByPartNumberAsync(string partNumber);

    // search parts by name or category (for the search bar in the dashboard)
    Task<IEnumerable<Part>> SearchAsync(string keyword);

    // add a new part to the database
    Task<Part> AddAsync(Part part);

    // update an existing part
    Task UpdateAsync(Part part);

    // remove a part from the database
    Task DeleteAsync(Part part);

    // add a stock transaction record when stock is added
    Task AddStockTransactionAsync(StockTransaction transaction);

    // save any changes made to the database
    Task SaveChangesAsync();
}
