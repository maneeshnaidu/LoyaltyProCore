using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoyaltyProCore.Dtos.Device;
using LoyaltyProCore.Models;

namespace LoyaltyProCore.Mappers
{
    public static class DeviceMapper
    {
        public static DeviceDto ToDeviceDto(this Device deviceModel)
        {
            return new DeviceDto
            {
                Id = deviceModel.Id,
                DeviceId = deviceModel.DeviceId,
                DeviceType = deviceModel.DeviceType,
                DeviceToken = deviceModel.DeviceToken,
                VendorId = deviceModel.VendorId,
                IsActive = deviceModel.IsActive
            };
        }

        public static Device ToDeviceFromCreateDto(this CreateDeviceDto createDeviceDto)
        {
            return new Device
            {
                DeviceId = createDeviceDto.DeviceId,
                DeviceType = createDeviceDto.DeviceType,
                DeviceToken = createDeviceDto.DeviceToken,
                VendorId = createDeviceDto.VendorId,
                IsActive = createDeviceDto.IsActive
            };
        }
    }
}