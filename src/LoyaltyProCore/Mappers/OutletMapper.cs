using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoyaltyProCore.Dtos.Outlet;
using LoyaltyProCore.Models;

namespace LoyaltyProCore.Mappers
{
    public static class OutletMapper
    {
        public static OutletDto ToOutletDto(this Outlet outletModel)
        {
            return new OutletDto
            {
                Id = outletModel.Id,
                Location = outletModel.Location,
                VendorId = outletModel.VendorId,
                CreatedOn = outletModel.CreatedOn
            };
        }
    }
}