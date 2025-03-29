using api.Helpers;
using api.Interfaces;
using api.Data;
using api.Dtos.Device;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ApplicationDBContext _context;
        public DeviceRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Device> CreateAsync(Device model)
        {
            await _context.Devices.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Device?> DeleteAsync(int id)
        {
            var deviceModel = await _context.Devices.FirstOrDefaultAsync(x => x.Id == id);
            if (deviceModel == null)
            {
                return null;
            }
            _context.Devices.Remove(deviceModel);
            await _context.SaveChangesAsync();
            return deviceModel;
        }

        public Task<bool> DeviceExists(int id)
        {
            return _context.Devices.AnyAsync(x => x.Id == id);
        }

        public async Task<List<Device>> GetAllAsync(QueryObject query)
        {
            var devices = _context.Devices.AsQueryable();

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

            return await devices.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Device?> GetByIdAsync(int id)
        {
            return await _context.Devices.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Device?> UpdateAsync(int id, UpdateDeviceDto deviceRequestDto)
        {
            var existingDevice = await _context.Devices.FirstOrDefaultAsync(x => x.Id == id);
            if (existingDevice == null)
            {
                return null;
            }
            existingDevice.DeviceId = deviceRequestDto.DeviceId;
            existingDevice.DeviceToken = deviceRequestDto.DeviceToken;
            existingDevice.DeviceType = deviceRequestDto.DeviceType;
            existingDevice.IsActive = deviceRequestDto.IsActive;

            await _context.SaveChangesAsync();
            return existingDevice;
        }
    }
}