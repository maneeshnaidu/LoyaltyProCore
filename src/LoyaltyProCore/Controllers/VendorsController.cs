using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorsController : ControllerBase
    {
        [HttpGet("id:guid")]
        public IActionResult Get(Guid id)
        {
            // get the vendor

            // return 200 ok response
            return Ok(
                // resource
            );
        }
        [HttpPost]
        public IActionResult Create(CreateVendorRequest request)
        {
            // create the vendor

            // return 201 created response
            return CreatedAtAction(
                // method
                actionName: nameof(Get), 
                // parameters needed for this method
                routeValues: new { id = Guid.NewGuid()},
                // resource
                value: request
            );
        }

        public record CreateVendorRequest(string Name, string Description);
    }
}