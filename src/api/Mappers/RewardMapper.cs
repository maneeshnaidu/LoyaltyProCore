using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Dtos.Reward;
using api.Models;

namespace api.Mappers
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

        public static RedeemableRewardDto ToReedemableRewardFromCustomerRewardDto(this CustomerRewards rewardModel)
        {
            return new RedeemableRewardDto
            {
                Id = rewardModel.Id,
                Title = rewardModel.Reward?.Title ?? string.Empty,
                Description = rewardModel.Reward?.Description ?? string.Empty,
                ExpiryDate = rewardModel.ExpiryDate
            };
        }
    }
}