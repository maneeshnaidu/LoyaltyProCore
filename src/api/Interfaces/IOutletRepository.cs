using api.Helpers;
using api.Dtos.Outlet;
using api.Models;

namespace api.Interfaces
{
    public interface IOutletRepository
    {
        Task<List<Outlet>> GetAllAsync(QueryObject query);
        Task<Outlet?> GetByIdAsync(int id);
        Task<string?> GetOutletAddressByIdAsync(int id);
        Task<Outlet> CreateAsync(Outlet outletModel);
        Task<Outlet?> UpdateAsync(int id, UpdateOutletDto outletDto);
        Task<Outlet?> DeleteAsync(int id);
        Task<bool> OutletExists(int id);
        Task<bool> OutletExistsByVendor(int id);
    }
}