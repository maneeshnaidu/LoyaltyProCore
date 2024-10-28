using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoyaltyProCore.Dtos.Outlet;
using LoyaltyProCore.Helpers;
using LoyaltyProCore.Models;

namespace LoyaltyProCore.Interfaces
{
    public interface IOutletRepository
    {
        Task<List<Outlet>> GetAllAsync(QueryObject query);
        Task<Outlet?> GetByIdAsync(int id);
        Task<Outlet> CreateAsync(Outlet outletModel);
        Task<Outlet?> UpdateAsync(int id, UpdateOutletDto outletDto);
        Task<Outlet?> DeleteAsync(int id);
        Task<bool> OutletExists(int id);
        Task<bool> OutletExistsByVendor(int id);
    }
}