using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoyaltyProCore.Data;
using LoyaltyProCore.Dtos.Vendor;
using LoyaltyProCore.Helpers;
using LoyaltyProCore.Interfaces;
using LoyaltyProCore.Mappers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProCore.Controllers
{
    [Route("api/vendor")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IVendorRepository _vendorRepository;
        private readonly IOutletRepository _outletRepository;
        public VendorController(ApplicationDBContext context, IVendorRepository vendorRepository, IOutletRepository outletRepository)
        {
            _vendorRepository = vendorRepository;
            _outletRepository = outletRepository;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vendors = await _vendorRepository.GetAllAsync(query);

            var vendorDto = vendors.Select(v => v.ToVendorDto()).ToList();

            return Ok(vendorDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vendor = await _vendorRepository.GetByIdAsync(id);

            if (vendor == null)
            {
                return NotFound();
            }

            return Ok(vendor.ToVendorDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateVendorRequestDto vendorRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vendorModel = vendorRequestDto.ToVendorFromCreateDto();

            await _vendorRepository.CreateAsync(vendorModel);

            return CreatedAtAction(nameof(GetById), new { id = vendorModel.Id }, vendorModel.ToVendorDto());
        }

        [HttpPut]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateVendorRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vendorModel = await _vendorRepository.UpdateAsync(id, updateDto);

            if (vendorModel == null)
            {
                return NotFound();
            }

            return Ok(vendorModel.ToVendorDto());
        }

        [HttpDelete]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingOutlet = await _outletRepository.OutletExistsByVendor(id);

            if (existingOutlet == true)
            {
                var vendorModel = await _vendorRepository.DeleteAsync(id);

                if (vendorModel == null)
                {
                    return NotFound();
                }

                return NoContent();
            }

            return BadRequest("Deletion failed. Vendor has exisiting outlets");


        }
    }
}