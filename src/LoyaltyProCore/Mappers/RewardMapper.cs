using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoyaltyProCore.Dtos.Reward;
using LoyaltyProCore.Models;

namespace LoyaltyProCore.Mappers
{
    public static class RewardMapper
    {
        public static RewardDto ToRewardDto(this Reward rewardModel)
        {
            return new RewardDto
            {
                Id = rewardModel.Id,
                VendorId = rewardModel.VendorId,
                Title = rewardModel.Title,
                PointsRequired = rewardModel.PointsRequired,
                Description = rewardModel.Description,
                IsActive = rewardModel.IsActive
            };
        }

        public static Reward ToRewardFromCreateDto(this CreateRewardDto rewardModel)
        {
            return new Reward
            {
                VendorId = rewardModel.VendorId,
                Title = rewardModel.Title,
                PointsRequired = rewardModel.PointsRequired,
                Description = rewardModel.Description,
                IsActive = rewardModel.IsActive
            };
        }
    }
}