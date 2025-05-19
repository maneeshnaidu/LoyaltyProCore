using api.Dtos.Account;
using api.Interfaces;
using api.Mappers;
using api.Models;

using CloudinaryDotNet.Actions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly IUserService _userService;
        private readonly IVendorRepository _vendorRepository;
        public AccountController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserService userService,
        ITokenService tokenService,
        IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
            _userManager = userManager;
            _signinManager = signInManager;
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

            var roles = await _userManager.GetRolesAsync(user);

            // Generate token
            var (accessToken, refreshToken) = _tokenService.GenerateToken(user, roles.ToList());

            // Save refresh token to the database
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // Set refresh token expiry
            await _userManager.UpdateAsync(user);

            return Ok(
                new NewUserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    UserCode = user.UserCode,
                    Vendor = user.VendorId,
                    Token = accessToken,
                    RefreshToken = refreshToken,
                    Roles = [.. roles]
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new ApplicationUser
                {
                    UserCode = await _userService.GenerateUserCodeAsync(),
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    VendorId = registerDto.VendorId,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (!string.IsNullOrWhiteSpace(registerDto.Role))
                {
                    await _userManager.AddToRoleAsync(appUser, registerDto.Role);
                }
                else
                {
                    await _userManager.AddToRoleAsync(appUser, "User");
                }

                var roles = await _userManager.GetRolesAsync(appUser);

                // Generate token
                var (accessToken, refreshToken) = _tokenService.GenerateToken(appUser, roles.ToList());

                // Save refresh token to the database
                appUser.RefreshToken = refreshToken;
                appUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // Set refresh token expiry
                var result = await _userManager.UpdateAsync(appUser);

                if (result.Succeeded)
                {
                    return Ok(
                        new NewUserDto
                        {
                            UserCode = appUser.UserCode,
                            FirstName = appUser.FirstName,
                            LastName = appUser.LastName,
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            Vendor = appUser.VendorId,
                            Token = accessToken,
                            RefreshToken = refreshToken,
                            Roles = [.. roles],
                        }
                    );

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

        [HttpPost("register-vendor")]
        public async Task<IActionResult> RegisterVendor([FromBody] RegisterVendorDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var vendorModel = registerDto.ToVendorFromRegisterDto();
                var newVendor = await _vendorRepository.CreateAsync(vendorModel);
                if (newVendor != null)
                {
                    var adminUser = new ApplicationUser
                    {
                        UserCode = await _userService.GenerateAdminUserCodeAsync(),
                        FirstName = registerDto.FirstName,
                        LastName = registerDto.LastName,
                        UserName = registerDto.Username,
                        Email = registerDto.Email
                    };

                    var createdUser = await _userManager.CreateAsync(adminUser, registerDto.Password);
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
                                new NewUserDto
                                {
                                    FirstName = adminUser.FirstName,
                                    LastName = adminUser.LastName,
                                    UserName = adminUser.UserName,
                                    Email = adminUser.Email,
                                    Vendor = adminUser.VendorId,
                                    Token = accessToken,
                                    RefreshToken = refreshToken,
                                    Roles = [.. roles]
                                }
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
                return StatusCode(500);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signinManager.SignOutAsync();
                return NoContent(); // Return 204 No Content on successful logout
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message); // Return 500 Internal Server Error if something goes wrong
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshTokenDto.RefreshToken);

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return Unauthorized("Invalid or expired refresh token");

            var roles = await _userManager.GetRolesAsync(user);

            // Generate new tokens
            var (accessToken, refreshToken) = _tokenService.GenerateToken(user, roles);

            // Update refresh token in the database
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }

        // [HttpPost("change-password")]

    }
}