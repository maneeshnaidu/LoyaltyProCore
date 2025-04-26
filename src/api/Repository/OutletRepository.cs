using api.Helpers;
using api.Data;
using api.Dtos.Outlet;
using api.Interfaces;
using api.Models;

using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class OutletRepository : IOutletRepository
    {
        private readonly ApplicationDBContext _context;
        public OutletRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Outlet> CreateAsync(Outlet outletModel)
        {
            await _context.Outlets.AddAsync(outletModel);
            await _context.SaveChangesAsync();
            return outletModel;
        }

        public async Task<Outlet?> DeleteAsync(int id)
        {
            var outletModel = await _context.Outlets.FirstOrDefaultAsync(x => x.Id == id);

            if (outletModel == null)
            {
                return null;
            }

            _context.Outlets.Remove(outletModel);
            await _context.SaveChangesAsync();
            return outletModel;
        }

        public async Task<List<Outlet>> GetAllAsync(QueryObject query)
        {
            var outlets = _context.Outlets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Address))
            {
                outlets = outlets.Where(x => x.Address.Contains(query.Address));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Address", StringComparison.OrdinalIgnoreCase))
                {
                    outlets = query.IsDecsending ? outlets.OrderByDescending(o => o.Address) : outlets.OrderBy(o => o.Address);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await outlets.Skip(skipNumber).Take(query.PageSize).ToListAsync();

        }

        public async Task<Outlet?> GetByIdAsync(int id)
        {
            return await _context.Outlets.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<string?> GetOutletAddressByIdAsync(int id)
        {
            var outlet = await _context.Outlets.FirstOrDefaultAsync(o => o.Id == id);
            return outlet?.Address;
        }

        public async Task<bool> OutletExists(int id)
        {
            return await _context.Outlets.AnyAsync(o => o.Id == id && o.IsActive == true);
        }

        public Task<bool> OutletExistsByVendor(int id)
        {
            return _context.Outlets.AnyAsync(o => o.VendorId == id && o.IsActive == true);
        }

        public async Task<Outlet?> UpdateAsync(int id, UpdateOutletDto outletDto)
        {
            var existingOutlet = await _context.Outlets.FirstOrDefaultAsync(x => x.Id == id);

            if (existingOutlet == null)
            {
                return null;
            }

            existingOutlet.Address = outletDto.Address;
            existingOutlet.IsActive = outletDto.IsActive;
            existingOutlet.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existingOutlet;
        }

    }
}