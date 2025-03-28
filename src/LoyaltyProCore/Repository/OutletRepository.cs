using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoyaltyProCore.Data;
using LoyaltyProCore.Dtos.Outlet;
using LoyaltyProCore.Helpers;
using LoyaltyProCore.Interfaces;
using LoyaltyProCore.Models;

using Microsoft.EntityFrameworkCore;

namespace LoyaltyProCore.Repository
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

        public Task<bool> OutletExists(int id)
        {
            return _context.Outlets.AnyAsync(o => o.Id == id && o.IsActive == true);
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

            await _context.SaveChangesAsync();

            return existingOutlet;
        }

    }
}