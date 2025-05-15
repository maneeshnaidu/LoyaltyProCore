using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Dtos.RewardPoints;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/points")]
    public class PointsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IVendorRepository _vendorRepository;
        private readonly IRewardRepository _rewardRepository;
        private readonly IPointsRepository _pointsRepository;
        private readonly IUserService _userService;

        public PointsController(UserManager<ApplicationUser> userManager,
        IVendorRepository vendorRepository,
        IRewardRepository rewardRepository,
        IPointsRepository pointsRepository,
        IUserService userService)
        {
            _userManager = userManager;
            _vendorRepository = vendorRepository;
            _rewardRepository = rewardRepository;
            _pointsRepository = pointsRepository;
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPoints()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            if (appUser == null) return BadRequest("User not found");
            var userPoints = await _pointsRepository.GetUserPoints(appUser);
            return Ok(userPoints);
        }

        [HttpPost]
        [Authorize]
        [Route("{customerCode:int}")]
        public async Task<IActionResult> AddPoints([FromRoute] int customerCode, UpsertPointsDto pointsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var customer = await _userService.GetUserByUserCodeAsync(customerCode);
            if (customer != null)
            {
                pointsDto.CustomerId = customer.Id;
            }

            if (appUser != null)
            {
                pointsDto.StaffId = appUser.Id;
            }
            var reward = await _rewardRepository.GetByIdAsync(pointsDto.RewardId);
            if (appUser == null || customer == null) return BadRequest("User not found");
            if (reward == null) return BadRequest("Reward not found");

            var userPoints = await _pointsRepository.GetUserPointsByReward(customer.Id, pointsDto.RewardId);

            var pointModel = pointsDto.ToPointFromCreateDto();

            if (userPoints == null)
            {
                await _pointsRepository.CreateAsync(pointModel);
                return CreatedAtAction(nameof(GetUserPoints), new { id = pointModel.Id }, pointModel);
            }

            var updatedPoint = await _pointsRepository.UpdateAsync(pointModel.ToUpsertDtoFromModel());

            return updatedPoint == null ? StatusCode(500, "Points not updated") : (IActionResult)Created();
        }

        [HttpDelete]
        [Authorize]
        [Route("{rewardId:int}")]
        public async Task<IActionResult> RedeemReward([FromRoute] int rewardId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await _rewardRepository.RedeemRewardAsync(rewardId);

            if (model == null)
            {
                return NotFound("No rewards available for user.");
            }

            return NoContent();
        }
    }
}