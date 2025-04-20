using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Dtos.User;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVendorRepository _vendorRepository;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(
            UserManager<ApplicationUser> userManager,
            IVendorRepository vendorRepository,
            IUserService userService,
            ITokenService tokenService
            )
        {
            _userManager = userManager;
            _vendorRepository = vendorRepository;
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var users = await _userService.GetAllAsync(query);
            // Retrieve roles for each user and map to UserDto
            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                if (user == null) continue; // Skip if user is null
                var roles = await _userManager.GetRolesAsync(user); // Fetch roles for the user
                userDtos.Add(user.ToUserDto(roles));
            }

            return Ok(userDtos);
        }

        [HttpPost("add-staff")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> CreateStaff([FromBody] CreateUserDto userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var vendor = await _vendorRepository.GetByAdminAsync(userDto.Admin);

                if (vendor != null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = userDto.UserName,
                        Email = userDto.Email,
                        FirstName = userDto.FirstName,
                        LastName = userDto.LastName,
                        UserCode = await _userService.GenerateAdminUserCodeAsync(),
                        VendorId = vendor.Id
                    };

                    var createdUser = await _userManager.CreateAsync(user, userDto.Password);
                    var roles = await _userManager.GetRolesAsync(user);

                    // Generate token
                    var (accessToken, refreshToken) = _tokenService.GenerateToken(user, roles.ToList());

                    // Save refresh token to the database
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // Set refresh token expiry
                    await _userManager.UpdateAsync(user);

                    if (createdUser.Succeeded)
                    {
                        // Assign role to the user
                        var roleResult = await _userManager.AddToRoleAsync(user, "Staff");
                        if (roleResult.Succeeded)
                        {
                            return CreatedAtAction(
                                nameof(GetAll),
                                new { id = user.Id }, user.ToUserDto(roles)
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

                return NotFound("Vendor not found");
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByUserCode([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.GetUserByUserCodeAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(user.ToUserDto(roles));
        }
    }
}