using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using api.Data;
using api.Dtos.Reward;
using api.Helpers;
using api.Interfaces;
using api.Mappers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("api/rewards")]
    public class RewardController : ControllerBase
    {
        private readonly IRewardRepository _rewardRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly ILogger<RewardController> _logger;

        public RewardController(
            IRewardRepository rewardRepository,
            IVendorRepository vendorRepository,
            ILogger<RewardController> logger)
        {
            _rewardRepository = rewardRepository;
            _vendorRepository = vendorRepository;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rewards = await _rewardRepository.GetAllAsync(query);

            var rewardDto = rewards.Select(r => r.ToRewardDto()).ToList();

            return Ok(rewardDto);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reward = await _rewardRepository.GetByIdAsync(id);

            return reward == null ? NotFound() : Ok(reward.ToRewardDto());
        }

        [HttpPost]
        [Authorize]
        [Route("{vendorId:int}")]
        public async Task<IActionResult> Create([FromRoute] int vendorId, CreateRewardDto rewardDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vendor = await _vendorRepository.VendorExists(vendorId);

            if (vendor == false)
            {
                return BadRequest("Vendor does not exist");
            }

            var rewardModel = rewardDto.ToRewardFromCreateDto();
            await _rewardRepository.CreateAsync(rewardModel);
            return CreatedAtAction(nameof(GetById), new { id = rewardModel.Id }, rewardModel.ToRewardDto());
        }

        [HttpPut]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRewardDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rewardModel = await _rewardRepository.UpdateAsync(id, updateDto);

            if (rewardModel == null)
            {
                return NotFound();
            }

            return Ok(rewardModel.ToRewardDto());
        }

        [HttpDelete]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rewardModel = await _rewardRepository.DeleteAsync(id);

            return rewardModel == null ? NotFound() : NoContent();
        }

        [HttpGet("get-redeemable-rewards")]
        [Authorize]
        [Route("{outletId:int}/{customerCode:int}")]
        public async Task<IActionResult> GetRedeemableRewards([FromRoute] int outletId, [FromRoute] int customerCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rewards = await _rewardRepository.GetRedeemableRewardsAsync(outletId, customerCode);

            if (rewards == null || !rewards.Any())
                return NotFound();

            var rewardDtos = rewards.Select(r => r.ToRewardDto()).ToList();
            return Ok(rewardDtos);
        }
    }
}