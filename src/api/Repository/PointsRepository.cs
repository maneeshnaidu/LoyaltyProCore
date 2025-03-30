using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Data;
using api.Dtos.Points;
using api.Helpers;
using api.Interfaces;
using api.Models;

using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PointsRepository : IPointsRepository
    {
        private readonly ApplicationDBContext _context;
        public PointsRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Points> CreateAsync(Points model)
        {
            await _context.Points.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Points?> DeleteAsync(int id)
        {
            var pointsModel = await _context.Points.FirstOrDefaultAsync(x => x.Id == id);
            if (pointsModel == null)
            {
                return null;
            }
            _context.Points.Remove(pointsModel);
            await _context.SaveChangesAsync();
            return pointsModel;
        }

        public async Task<List<Points>> GetAllAsync(QueryObject query)
        {
            var points = _context.Points.AsQueryable();
            // if (!string.IsNullOrWhiteSpace(query.CompanyName))
            // {
            //     devices = devices.Where(x => x.Name.Contains(query.CompanyName));
            // }

            // if (!string.IsNullOrWhiteSpace(query.SortBy))
            // {
            //     if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            //     {
            //         devices = query.IsDecsending ? devices.OrderByDescending(v => v.Name) : devices.OrderBy(v => v.Name);
            //     }
            // }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await points.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<List<Points>> GetByCustomerIdAsync(string customerId)
        {
            var points = _context.Points.AsQueryable();
            if (!string.IsNullOrWhiteSpace(customerId))
            {
                points = points.Where(x => x.CustomerId == customerId);
            }
            return await points.ToListAsync();
        }

        public async Task<Points?> GetByIdAsync(int id)
        {
            return await _context.Points.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> PointsExists(int id)
        {
            return await _context.Points.AnyAsync(x => x.Id == id);
        }

        public async Task<Points?> UpdateAsync(int id, UpsertPointsDto pointsDto)
        {
            var existingPoints = await _context.Points.FirstOrDefaultAsync(x => x.Id == id);
            if (existingPoints == null)
            {
                return null;
            }
            existingPoints.CustomerId = pointsDto.CustomerId;
            existingPoints.VendorId = pointsDto.VendorId;
            existingPoints.OutletId = pointsDto.OutletId;
            existingPoints.Point = pointsDto.Point;
            existingPoints.Level = pointsDto.Level;
            existingPoints.LastUpdatedOn = DateTime.Now;

            await _context.SaveChangesAsync();
            return existingPoints;
        }
    }
}