using api.Helpers;
using api.Data;
using api.Dtos.Vendor;
using api.Interfaces;
using api.Models;

using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class VendorRepository : IVendorRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IUserService _userService;
        public VendorRepository(ApplicationDBContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<Vendor> CreateAsync(Vendor vendorModel)
        {
            await _context.Vendors.AddAsync(vendorModel);
            await _context.SaveChangesAsync();
            return vendorModel;
        }

        public async Task<Vendor?> DeleteAsync(int id)
        {
            var vendorModel = await _context.Vendors.FirstOrDefaultAsync(x => x.Id == id);

            if (vendorModel == null)
            {
                return null;
            }

            _context.Vendors.Remove(vendorModel);
            await _context.SaveChangesAsync();
            return vendorModel;
        }

        public async Task<List<Vendor>> GetAllAsync(QueryObject query)
        {
            var vendors = _context.Vendors.Include(v => v.Outlets).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                vendors = vendors.Where(x => x.Name.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    vendors = query.IsDecsending ? vendors.OrderByDescending(v => v.Name) : vendors.OrderBy(v => v.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await vendors.Skip(skipNumber).Take(query.PageSize).ToListAsync();

        }

        public async Task<Vendor?> GetByIdAsync(int id)
        {
            return await _context.Vendors.Include(o => o.Outlets).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> VendorExists(int id)
        {
            return await _context.Vendors.AnyAsync(v => v.Id == id);
        }

        public async Task<Vendor?> UpdateAsync(int id, UpdateVendorRequestDto vendorDto)
        {
            var existingVendor = await _context.Vendors.FirstOrDefaultAsync(x => x.Id == id);

            if (existingVendor == null)
            {
                return null;
            }

            existingVendor.Name = vendorDto.Name;
            existingVendor.Description = vendorDto.Description;
            existingVendor.Category = vendorDto.Category;

            await _context.SaveChangesAsync();

            return existingVendor;
        }

        public async Task<Vendor?> ToggleStatusAsync(int id)
        {
            var vendor = await _context.Vendors.FirstOrDefaultAsync(x => x.Id == id);
            if (vendor == null)
            {
                return null;
            }

            vendor.IsActive = !vendor.IsActive;
            await _context.SaveChangesAsync();
            return vendor;
        }

        public async Task<Vendor?> GetByAdminAsync(string userId)
        {
            var user = await _userService.GetVendorByIdAsync(userId); // Await the result of the async method
            if (user != null)
            {
                return await _context.Vendors
                    .Include(v => v.Outlets)
                    .FirstOrDefaultAsync(v => v.Id == user.Value); // Use user.Value to access the nullable int
            }

            return null; // Return null if user is null
        }
    }
}