using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Dtos.User;
using api.Models;

namespace api.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this ApplicationUser user, IList<string> roles)
        {
            return new UserDto
            {
                Id = user.Id,
                UserCode = user.UserCode,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                VendorId = user.VendorId,
                OutletId = user.OutletId,
                Roles = roles.ToList(),
                StampCard = user.StampCard?.Select(s => s.ToPointDto()).ToList()
            };
        }
    }
}