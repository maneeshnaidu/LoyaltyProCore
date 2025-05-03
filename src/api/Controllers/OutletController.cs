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
        private readonly IOutletRepository _outletRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly IUploadFileService _uploadFileService;
        private readonly IUserService _userService;
        public OutletController(
            IOutletRepository outletRepository,
            IVendorRepository vendorRepository,
            IUploadFileService uploadFileService,
            IUserService userService
            )
        {
            _outletRepository = outletRepository;
            _vendorRepository = vendorRepository;
            _uploadFileService = uploadFileService;
            _userService = userService;
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

            // var vendor = await _vendorRepository.GetByAdminAsync(adminUser.Id);
            // if (vendor == null)
            // {
            //     return BadRequest("Vendor does not exist");
            // }

            var outletModel = outletDto.ToOutletFromCreateDto();
            // Upload images if provided
            try
            {
                if (outletDto.CoverImage != null)
                {
                    var uploadCover = await _uploadFileService.UploadFileAsync(outletDto.CoverImage);
                    outletModel.CoverImageUrl = uploadCover.Url;
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while uploading the file... " + ex);
            }
            // outletModel.VendorId = vendor.Id;
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

            if (await _outletRepository.OutletExists(id))
            {
                // Upload images if provided
                try
                {
                    if (updateDto.CoverImage != null)
                    {
                        var uploadCover = await _uploadFileService.UploadFileAsync(updateDto.CoverImage);
                        updateDto.CoverImageUrl = uploadCover.Url;
                    }
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "An error occurred while uploading the file... " + ex);
                }

                var outletModel = await _outletRepository.UpdateAsync(id, updateDto);
                return outletModel == null ? NotFound() : Ok(outletModel.ToOutletDto());
            }
            else
            {
                return NotFound("Outlet not found.");
            }
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