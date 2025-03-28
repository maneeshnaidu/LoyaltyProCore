using api.Helpers;
using api.Dtos.Vendor;
using api.Models;

namespace api.Interfaces
{
    public interface IVendorRepository
    {
        Task<List<Vendor>> GetAllAsync(QueryObject query);
        Task<Vendor?> GetByIdAsync(int id);
        Task<Vendor> CreateAsync(Vendor vendorModel);
        Task<Vendor?> UpdateAsync(int id, UpdateVendorRequestDto vendorRequestDto);
        Task<Vendor?> DeleteAsync(int id);
        Task<bool> VendorExists(int id);
    }
}