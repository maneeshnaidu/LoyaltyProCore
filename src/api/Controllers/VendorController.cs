using api.Helpers;
using api.Data;
using api.Dtos.Vendor;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Controllers
{
    [Route("api/vendors")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IUploadFileService _uploadFileService;
        private readonly IVendorRepository _vendorRepository;
        private readonly IOutletRepository _outletRepository;
        public VendorController(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            IUserService userService,
            IVendorRepository vendorRepository,
            IOutletRepository outletRepository,
            IUploadFileService uploadFileService)
        {
            _uploadFileService = uploadFileService;
            _userManager = userManager;
            _tokenService = tokenService;
            _userService = userService;
            _vendorRepository = vendorRepository;
            _outletRepository = outletRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vendors = await _vendorRepository.GetAllAsync(query);

            var vendorDtos = vendors.Select(v => v.ToVendorDto()).ToList();

            return Ok(vendorDtos);
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
        public async Task<IActionResult> Create([FromForm] CreateVendorRequestDto vendorRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var vendorModel = vendorRequestDto.ToVendorFromCreateDto();
                // Upload images if provided
                try
                {
                    if (vendorRequestDto.CoverImage != null)
                    {
                        var uploadCover = await _uploadFileService.UploadFileAsync(vendorRequestDto.CoverImage);
                        vendorModel.CoverImageUrl = uploadCover.Url;
                    }
                    if (vendorRequestDto.LogoImage != null)
                    {
                        var uploadLogo = await _uploadFileService.UploadFileAsync(vendorRequestDto.LogoImage);
                        vendorModel.LogoImageUrl = uploadLogo.Url;
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

                var newVendor = await _vendorRepository.CreateAsync(vendorModel);

                if (newVendor != null)
                {
                    var adminUser = new ApplicationUser
                    {
                        UserCode = await _userService.GenerateAdminUserCodeAsync(),
                        FirstName = vendorRequestDto.FirstName,
                        LastName = vendorRequestDto.LastName,
                        UserName = vendorRequestDto.Username,
                        Email = vendorRequestDto.Email,
                        VendorId = newVendor.Id
                    };

                    var createdUser = await _userManager.CreateAsync(adminUser, vendorRequestDto.Password);
                    var roles = await _userManager.GetRolesAsync(adminUser);
                    if (createdUser.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(adminUser, "Admin");
                        if (roleResult.Succeeded)
                        {
                            // Generate token
                            var (accessToken, refreshToken) = _tokenService.GenerateToken(adminUser, roles.ToList());

                            // Save refresh token to the database
                            adminUser.RefreshToken = refreshToken;
                            adminUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // Set refresh token expiry
                            await _userManager.UpdateAsync(adminUser);

                            return Ok(
                                CreatedAtAction(
                                    nameof(GetById),
                                    new { id = vendorModel.Id },
                                    vendorModel.ToVendorDto()
                                )
                            );
                        }
                        else
                        {
                            return StatusCode(500, roleResult.Errors);
                        }
                    }
                    else
                    {
                        return StatusCode(500, createdUser.Errors);
                    }
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
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

        [HttpPatch]
        [Authorize]
        [Route("{id:int}/toggle-active")]
        public async Task<IActionResult> ToggleIsActive([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Retrieve the vendor by ID
            var vendor = await _vendorRepository.GetByIdAsync(id);

            if (vendor == null)
                return NotFound("Vendor not found.");

            // Update the vendor in the database
            var updatedVendor = await _vendorRepository.ToggleStatusAsync(id);

            if (updatedVendor == null)
                return StatusCode(500, "Failed to update vendor status.");

            return Ok(updatedVendor.ToVendorDto());
        }

    }
}