using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Dtos.RewardPoints;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IPointsRepository
    {
        Task<List<RewardPoints>> GetAllAsync(QueryObject query);
        Task<RewardPoints?> GetByIdAsync(int id);
        Task<List<RewardPoints>> GetUserPoints(ApplicationUser user);
        Task<List<CustomerRewards>> GetUserRewards(ApplicationUser user);
        Task<RewardPoints?> GetUserPointsByVendor(string customerId, int vendorId);
        Task<RewardPoints?> GetUserPointsByReward(string customerId, int rewardId);
        Task<RewardPoints> CreateAsync(RewardPoints pointsModel);
        Task<RewardPoints?> UpdateAsync(UpsertPointsDto pointsDto);
        Task<RewardPoints?> RedeemPointsAsync(int customerCode, UpsertPointsDto pointsDto);
        Task<RewardPoints?> DeleteAsync(int id);
        Task<bool> PointsExists(int id);
    }
}