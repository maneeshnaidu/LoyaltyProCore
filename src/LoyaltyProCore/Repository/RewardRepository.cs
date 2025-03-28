using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoyaltyProCore.Data;
using LoyaltyProCore.Dtos.Reward;
using LoyaltyProCore.Helpers;
using LoyaltyProCore.Interfaces;
using LoyaltyProCore.Models;

using Microsoft.EntityFrameworkCore;

namespace LoyaltyProCore.Repository
{
    public class RewardRepository : IRewardRepository
    {
        private readonly ApplicationDBContext _context;
        public RewardRepository(ApplicationDBContext context)
        {
            _context = context;
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
    }
}