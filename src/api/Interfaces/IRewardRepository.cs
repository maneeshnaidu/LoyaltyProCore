using api.Dtos.Reward;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IRewardRepository
    {
        Task<List<Reward>> GetAllAsync(QueryObject query);
        Task<Reward?> GetByIdAsync(int id);
        Task<Reward> CreateAsync(Reward RewardModel);
        Task<Reward?> UpdateAsync(int id, UpdateRewardDto RewardDto);
        Task<Reward?> DeleteAsync(int id);
        Task<bool> RewardExists(int id);
        Task<CustomerRewards> AddRewardAsync(RewardPoints model);
        Task<List<Reward>?> GetRedeemableRewardsAsync(int outletId, int customerCode);
    }
}