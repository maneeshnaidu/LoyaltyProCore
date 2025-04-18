using api.Helpers;
using api.Data;
using api.Dtos.Vendor;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Controllers
{
    [Route("api/vendors")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IVendorRepository _vendorRepository;
        private readonly IOutletRepository _outletRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VendorController(
            ApplicationDBContext context,
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            IUserService userService,
            IVendorRepository vendorRepository,
            IOutletRepository outletRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _tokenService = tokenService;
            _userService = userService;
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
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var vendorModel = vendorRequestDto.ToVendorFromCreateDto();

                var adminUser = new ApplicationUser
                {
                    UserCode = await _userService.GenerateAdminUserCodeAsync(),
                    FirstName = vendorRequestDto.FirstName,
                    LastName = vendorRequestDto.LastName,
                    UserName = vendorRequestDto.Username,
                    Email = vendorRequestDto.Email
                };

                var createdUser = await _userManager.CreateAsync(adminUser, vendorRequestDto.Password);
                var roles = await _userManager.GetRolesAsync(adminUser);

                if (createdUser.Succeeded)
                {
                    // Create vendor and associate with the user
                    vendorModel.AdminId = adminUser.Id;
                    await _vendorRepository.CreateAsync(vendorModel);

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

        private async Task<string> UploadFile(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));


            return "/" + folderPath;
        }
    }
}