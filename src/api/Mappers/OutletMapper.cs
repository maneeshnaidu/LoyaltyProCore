using api.Dtos.Outlet;
using api.Models;

namespace api.Mappers
{
    public static class OutletMapper
    {
        public static OutletDto ToOutletDto(this Outlet outletModel)
        {
            return new OutletDto
            {
                Id = outletModel.Id,
                Address = outletModel.Address,
                VendorId = outletModel.VendorId,
                IsActive = outletModel.IsActive
            };
        }

        public static Outlet ToOutletFromCreateDto(this CreateOutletDto outletDto)
        {
            return new Outlet
            {
                VendorId = outletDto.VendorId,
                Address = outletDto.Address,
                IsActive = outletDto.IsActive
            };
        }
    }
}