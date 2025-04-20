using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Outlet
{
    public class CreateOutletDto
    {
        public required string UserName { get; set; }
        public required string Address { get; set; }
        public bool IsActive { get; set; } = true;
    }
}