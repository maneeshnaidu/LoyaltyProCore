using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Data;
using api.Dtos.Device;
using api.Helpers;
using api.Interfaces;
using api.Mappers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IVendorRepository _vendorRepository;
        public DeviceController(ApplicationDBContext context, IDeviceRepository deviceRepository, IVendorRepository vendorRepository)
        {
            _deviceRepository = deviceRepository;
            _vendorRepository = vendorRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var devices = await _deviceRepository.GetAllAsync(query);

            var deviceDto = devices.Select(d => d.ToDeviceDto()).ToList();

            return Ok(deviceDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var device = await _deviceRepository.GetByIdAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device.ToDeviceDto());
        }

        [HttpPost("{vendorId}")]
        [Authorize]
        public async Task<IActionResult> Create([FromRoute] int vendorId, CreateDeviceDto deviceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vendor = await _vendorRepository.VendorExists(vendorId);

            if (vendor == false)
            {
                return BadRequest("Vendor does not exist");
            }

            var deviceModel = deviceDto.ToDeviceFromCreateDto();
            await _deviceRepository.CreateAsync(deviceModel);
            return CreatedAtAction(nameof(GetById), new { id = deviceModel.Id }, deviceModel.ToDeviceDto());
        }

        [HttpPut]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDeviceDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deviceModel = await _deviceRepository.UpdateAsync(id, updateDto);

            if (deviceModel == null)
            {
                return NotFound();
            }

            return Ok(deviceModel.ToDeviceDto());
        }

        [HttpDelete]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deviceModel = await _deviceRepository.DeleteAsync(id);

            if (deviceModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}