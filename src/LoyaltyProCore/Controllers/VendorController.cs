using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoyaltyProCore.Data;
using LoyaltyProCore.Dtos.Vendor;
using LoyaltyProCore.Mappers;

using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProCore.Controllers
{
    [ApiController]
    [Route("api/vendor")]
    public class VendorController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public VendorController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var vendors = _context.Vendors.ToList()
            .Select(v => v.ToVendorDto());

            return Ok(vendors);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var vendor = _context.Vendors.FirstOrDefault(v => v.Id == id);

            if (vendor == null)
            {
                return NotFound();
            }

            return Ok(vendor.ToVendorDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateVendorRequestDto vendorRequestDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var vendorModel = vendorRequestDto.ToVendorFromCreateDto();
            _context.Vendors.Add(vendorModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id = vendorModel.Id}, vendorModel.ToVendorDto());
        }
    }
}