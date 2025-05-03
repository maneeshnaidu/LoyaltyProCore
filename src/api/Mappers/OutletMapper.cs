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
                VendorId = outletModel.VendorId,
                Name = outletModel.Name,
                Description = outletModel.Description,
                Category = outletModel.Category,
                CoverImageUrl = outletModel.CoverImageUrl,
                Address = outletModel.Address,
                IsActive = outletModel.IsActive,
                CreatedOn = outletModel.CreatedOn,
                UpdatedOn = outletModel.UpdatedOn,
            };
        }

        public static Outlet ToOutletFromCreateDto(this CreateOutletDto outletDto)
        {
            return new Outlet
            {
                VendorId = outletDto.VendorId,
                Name = outletDto.Name,
                Description = outletDto.Description,
                Category = outletDto.Category,
                CoverImageUrl = outletDto.CoverImageUrl,
                Address = outletDto.Address,
                IsActive = outletDto.IsActive,
            };
        }
    }
}