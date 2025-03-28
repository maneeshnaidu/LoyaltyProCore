using api.Helpers;
using api.Interfaces;
using api.Data;
using api.Dtos.Device;
using api.Models;

namespace api.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ApplicationDBContext _context;
        public DeviceRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public Task<Device> CreateAsync(Device model)
        {
            // await _context.Devices.AddAsync(model);
            // await _context.SaveChangesAsync();
            // return model;
            throw new NotImplementedException();
        }

        public Task<Device?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeviceExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Device>> GetAllAsync(QueryObject query)
        {
            throw new NotImplementedException();
        }

        public Task<Device?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Device?> UpdateAsync(int id, UpdateDeviceDto deviceRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}