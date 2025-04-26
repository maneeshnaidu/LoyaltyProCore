using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Dtos.VendorIntegration;
using api.Helpers;
using api.Interfaces;
using api.Mappers;

using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IntegrationController : ControllerBase
    {
        private readonly IIntegrationRepository _integrationRepository;
        private readonly IVendorRepository _vendorRepository;

        public IntegrationController(IIntegrationRepository integrationRepository, IVendorRepository vendorRepository)
        {
            _integrationRepository = integrationRepository;
            _vendorRepository = vendorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var integrations = await _integrationRepository.GetAllAsync(query);

            var integrationDto = integrations.Select(i => i.ToIntegrationDto()).ToList();

            return Ok(integrationDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var integration = await _integrationRepository.GetByIdAsync(id, null);

            if (integration == null)
            {
                return NotFound();
            }

            return Ok(integration.ToIntegrationDto());
        }

        [HttpPost("{vendorId}")]
        public async Task<IActionResult> Create([FromRoute] int vendorId, UpsertIntegrationDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vendor = await _vendorRepository.VendorExists(vendorId);

            if (vendor == false)
            {
                return BadRequest("Vendor does not exist");
            }

            var integrationModel = requestDto.ToIntegrationFromUpsertDto();
            await _integrationRepository.CreateAsync(integrationModel);
            return CreatedAtAction(nameof(GetById), new { id = integrationModel.Id }, integrationModel.ToIntegrationDto());
        }

        [HttpPut]
        // [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpsertIntegrationDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var integrationModel = await _integrationRepository.UpdateAsync(id, updateDto);

            if (integrationModel == null)
            {
                return NotFound();
            }

            return Ok(integrationModel.ToIntegrationDto());
        }

        [HttpDelete]
        // [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var integrationModel = await _integrationRepository.DeleteAsync(id);

            if (integrationModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}