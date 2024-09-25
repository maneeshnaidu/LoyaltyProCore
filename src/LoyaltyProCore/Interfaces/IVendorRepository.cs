using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoyaltyProCore.Dtos.Vendor;
using LoyaltyProCore.Helpers;
using LoyaltyProCore.Models;

namespace LoyaltyProCore.Interfaces
{
    public interface IVendorRepository
    {
        Task<List<Vendor>> GetAllAsync(QueryObject query);
        Task<Vendor?> GetByIdAsync(int id);
        Task<Vendor> CreateAsync(Vendor vendorModel);
        Task<Vendor?> UpdateAsync(int id, UpdateVendorRequestDto vendorRequestDto);
        Task<Vendor?> DeleteAsync(int id);
    }
}