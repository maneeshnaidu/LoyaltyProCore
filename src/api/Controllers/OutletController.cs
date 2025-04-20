using api.Helpers;
using api.Data;
using api.Dtos.Outlet;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/outlets")]
    public class OutletController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IOutletRepository _outletRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly IUserService _userService;
        public OutletController(
            ApplicationDBContext context,
            IOutletRepository outletRepository,
            IVendorRepository vendorRepository,
            IUserService userService
            )
        {
            _outletRepository = outletRepository;
            _vendorRepository = vendorRepository;
            _userService = userService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var outlets = await _outletRepository.GetAllAsync(query);

            var outletDto = outlets.Select(o => o.ToOutletDto()).ToList();

            return Ok(outletDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var outlet = await _outletRepository.GetByIdAsync(id);

            if (outlet == null)
            {
                return NotFound();
            }

            return Ok(outlet.ToOutletDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] CreateOutletDto outletDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var adminUser = await _userService.GetUserByUserNameAsync(outletDto.UserName);
            if (adminUser == null)
            {
                return BadRequest("Admin user not found");
            }

            var vendor = await _vendorRepository.GetByAdminAsync(adminUser.Id);
            if (vendor == null)
            {
                return BadRequest("Vendor does not exist");
            }

            var outletModel = outletDto.ToOutletFromCreateDto();
            outletModel.VendorId = vendor.Id;
            await _outletRepository.CreateAsync(outletModel);
            return CreatedAtAction(nameof(GetById), new { id = outletModel.Id }, outletModel.ToOutletDto());
        }

        [HttpPut]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOutletDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var outletModel = await _outletRepository.UpdateAsync(id, updateDto);

            if (outletModel == null)
            {
                return NotFound();
            }

            return Ok(outletModel.ToOutletDto());
        }

        [HttpDelete]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var outletModel = await _outletRepository.DeleteAsync(id);

            if (outletModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}