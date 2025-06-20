using api.Dtos.Reward;
using api.Helpers;
using api.Data;
using api.Interfaces;
using api.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace api.Repository
{
    public class RewardRepository : IRewardRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IUserService _userService;

        public RewardRepository(ApplicationDBContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<Reward> CreateAsync(Reward model)
        {
            await _context.Rewards.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Reward?> DeleteAsync(int id)
        {
            var rewardModel = await _context.Rewards.FirstOrDefaultAsync(x => x.Id == id);

            if (rewardModel == null)
            {
                return null;
            }

            _context.Rewards.Remove(rewardModel);
            await _context.SaveChangesAsync();
            return rewardModel;
        }

        public async Task<List<Reward>> GetAllAsync(QueryObject query)
        {
            var rewards = _context.Rewards.AsQueryable();

            // Filter by UserCode if provided
            if (query.UserCode.HasValue)
            {
                var user = await _userService.GetUserByUserCodeAsync(query.UserCode.Value);
                if (user != null)
                {
                    var customerRewards = _context.CustomerRewards.Where(r => r.CustomerId == user.Id);
                    var rewardIds = customerRewards.Select(cr => cr.RewardId); // Get the reward IDs
                    rewards = rewards.Where(r => rewardIds.Contains(r.Id)); // Filter rewards based on the reward IDs
                }
            }

            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                rewards = rewards.Where(x => x.Title.Contains(query.Title));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    rewards = query.IsDecsending ? rewards.OrderByDescending(r => r.Title) : rewards.OrderBy(r => r.Title);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await rewards.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Reward?> GetByIdAsync(int id)
        {
            return await _context.Rewards.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Reward?> UpdateAsync(int id, UpdateRewardDto rewardDto)
        {
            var existingReward = await _context.Rewards.FirstOrDefaultAsync(x => x.Id == id);

            if (existingReward == null)
            {
                return null;
            }

            existingReward.Title = rewardDto.Title;
            existingReward.Description = rewardDto.Description;
            existingReward.PointsRequired = rewardDto.PointsRequired;
            existingReward.IsActive = rewardDto.IsActive;

            await _context.SaveChangesAsync();
            return existingReward;
        }

        public async Task<bool> RewardExists(int id)
        {
            return await _context.Rewards.AnyAsync(x => x.Id == id);
        }

        public async Task<CustomerRewards> AddRewardAsync(RewardPoints model)
        {
            var reward = new CustomerRewards
            {
                CustomerId = model.CustomerId,
                RewardId = model.RewardId,
                VendorId = model.VendorId,
                OutletId = model.OutletId,
                CreatedOn = DateTime.UtcNow
            };

            await _context.CustomerRewards.AddAsync(reward);
            await _context.SaveChangesAsync();
            return reward;
        }

        public async Task<List<Reward>?> GetRedeemableRewardsAsync(int outletId, int customerCode)
        {
            var user = await _userService.GetUserByUserCodeAsync(customerCode);
            if (user == null)
            {
                return null;
            }

            var userPoints = await _context.RewardPoints
                .FirstOrDefaultAsync(p => p.CustomerId == user.Id);
            if (userPoints == null)
            {
                return null; // User points not found
            }

            var outlet = await _context.Outlets.FirstOrDefaultAsync(o => o.Id == outletId);
            if (outlet == null)
            {
                return null; // Outlet not found
            }

            if (outletId != outlet.VendorId)
            {
                var vendor = await _context.Vendors.FirstOrDefaultAsync(v => v.Id == outlet.VendorId);
                if (vendor == null)
                {
                    return null; // Vendor not found
                }

                var rewards = await _context.Rewards
                .Where(r => r.VendorId == outletId && r.PointsRequired <= userPoints.Points && r.IsActive)
                .ToListAsync();

                return rewards.Count == 0 ? null : rewards;
            }
            else
            {
                var rewards = await _context.Rewards
                .Where(r => r.VendorId == outlet.VendorId && r.PointsRequired <= userPoints.Points && r.IsActive)
                .ToListAsync();

                return rewards.Count == 0 ? null : rewards;
            }
        }
    }
}