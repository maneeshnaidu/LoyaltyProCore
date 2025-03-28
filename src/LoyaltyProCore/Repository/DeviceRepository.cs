using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoyaltyProCore.Data;
using LoyaltyProCore.Dtos.Device;
using LoyaltyProCore.Helpers;
using LoyaltyProCore.Interfaces;
using LoyaltyProCore.Models;

namespace LoyaltyProCore.Repository
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