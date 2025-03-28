using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Helpers;

using api.Dtos.Device;
using api.Models;

namespace api.Interfaces
{
    public interface IDeviceRepository
    {
        Task<List<Device>> GetAllAsync(QueryObject query);
        Task<Device?> GetByIdAsync(int id);
        Task<Device> CreateAsync(Device deviceModel);
        Task<Device?> UpdateAsync(int id, UpdateDeviceDto deviceRequestDto);
        Task<Device?> DeleteAsync(int id);
        Task<bool> DeviceExists(int id);
    }
}