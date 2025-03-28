using api.Dtos.Vendor;
using api.Models;

namespace api.Mappers
{
    public static class VendorMapper
    {
        public static VendorDto ToVendorDto(this Vendor vendorModel)
        {
            return new VendorDto
            {
                Id = vendorModel.Id,
                Name = vendorModel.Name,
                Description = vendorModel.Description,
                Category = vendorModel.Category,
                CoverImageUrl = vendorModel.CoverImageUrl,
                LogoImageUrl = vendorModel.LogoImageUrl,
                IsActive = vendorModel.IsActive,
                CreatedOn = vendorModel.CreatedOn,
                Outlets = vendorModel.Outlets.Select(o => o.ToOutletDto()).ToList()
            };
        }

        public static Vendor ToVendorFromCreateDto(this CreateVendorRequestDto vendorRequestDto)
        {
            return new Vendor
            {
                Name = vendorRequestDto.Name,
                Description = vendorRequestDto.Description,
                Category = vendorRequestDto.Category,
                CoverImageUrl = vendorRequestDto.CoverImageUrl,
                LogoImageUrl = vendorRequestDto.LogoImageUrl,
                IsActive = vendorRequestDto.IsActive
            };
        }


    }
}