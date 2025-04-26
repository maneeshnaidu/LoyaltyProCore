using api.Dtos.VendorIntegration;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IIntegrationRepository
    {
        Task<List<VendorIntegration>> GetAllAsync(QueryObject query);
        Task<VendorIntegration?> GetByIdAsync(int id, string? category);
        Task<VendorIntegration> CreateAsync(VendorIntegration model);
        Task<VendorIntegration?> UpdateAsync(int id, UpsertIntegrationDto requestDto);
        Task<VendorIntegration?> DeleteAsync(int id);
        Task<bool> VendorIntegrationExists(int id);
    }
}