using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoyaltyProCore.Dtos.Reward;
using LoyaltyProCore.Helpers;
using LoyaltyProCore.Models;

namespace LoyaltyProCore.Interfaces
{
    public interface IRewardRepository
    {
        Task<List<Reward>> GetAllAsync(QueryObject query);
        Task<Reward?> GetByIdAsync(int id);
        Task<Reward> CreateAsync(Reward RewardModel);
        Task<Reward?> UpdateAsync(int id, UpdateRewardDto RewardDto);
        Task<Reward?> DeleteAsync(int id);
        Task<bool> RewardExists(int id);
    }
}