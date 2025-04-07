
using api.Data;
using api.Dtos.RewardPoints;
using api.Helpers;
using api.Interfaces;
using api.Models;

using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PointsRepository : IPointsRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly IRewardRepository _rewardRepository;

        public PointsRepository(ApplicationDBContext context,
            ITransactionsRepository transactionsRepository,
            IRewardRepository rewardRepository)
        {
            _context = context;
            _transactionsRepository = transactionsRepository;
            _rewardRepository = rewardRepository;
        }

        public async Task<RewardPoints> CreateAsync(RewardPoints model)
        {
            await _context.RewardPoints.AddAsync(model);
            await _context.SaveChangesAsync();

            // Add a record to the PointsTransaction table
            var transaction = new PointsTransaction
            {
                CustomerId = model.CustomerId,
                OrderId = null,
                Points = model.Point,
                TransactionType = "EarnedPoints", // Example transaction type
                OutletId = model.OutletId,
                CreatedOn = DateTime.UtcNow // Set the created date to now
            };
            await _transactionsRepository.CreateAsync(transaction);

            // Check and update the RewardsRepository
            var reward = await _rewardRepository.GetByIdAsync(model.RewardId);
            if (reward != null)
            {
                // Add customer rewards based on points
                if (model.Point >= reward.PointsRequired)
                {
                    await _rewardRepository.AddRewardAsync(model);
                }
            }

            return model;
        }

        public async Task<RewardPoints?> DeleteAsync(int id)
        {
            var pointsModel = await _context.RewardPoints.FirstOrDefaultAsync(x => x.Id == id);
            if (pointsModel == null)
            {
                return null;
            }
            _context.RewardPoints.Remove(pointsModel);
            await _context.SaveChangesAsync();
            return pointsModel;
        }

        public async Task<List<RewardPoints>> GetAllAsync(QueryObject query)
        {
            var points = _context.RewardPoints.AsQueryable();
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await points.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<List<RewardPoints>> GetUserPoints(ApplicationUser user)
        {
            var points = _context.RewardPoints.AsQueryable();
            if (user != null && user.StampCard != null && user.StampCard.Count > 0)
            {
                points = points.Where(x => x.CustomerId == user.Id);
            }
            return await points.ToListAsync();
        }

        public async Task<RewardPoints?> GetUserPointsByVendor(string customerId, int vendorId)
        {
            return await _context.RewardPoints.FirstOrDefaultAsync(x => x.CustomerId == customerId && x.VendorId == vendorId);
        }

        public async Task<RewardPoints?> GetUserPointsByReward(string customerId, int rewardId)
        {
            return await _context.RewardPoints.FirstOrDefaultAsync(x => x.CustomerId == customerId && x.RewardId == rewardId);
        }

        public async Task<RewardPoints?> GetByIdAsync(int id)
        {
            return await _context.RewardPoints.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> PointsExists(int id)
        {
            return await _context.RewardPoints.AnyAsync(x => x.Id == id);
        }

        public async Task<RewardPoints?> UpdateAsync(int id, UpsertPointsDto pointsDto)
        {
            var existingPoints = await _context.RewardPoints.FirstOrDefaultAsync(x => x.Id == id);
            if (existingPoints == null)
            {
                return null;
            }
            existingPoints.CustomerId = pointsDto.CustomerId;
            existingPoints.RewardId = pointsDto.RewardId;
            existingPoints.VendorId = pointsDto.VendorId;
            existingPoints.OutletId = pointsDto.OutletId;
            existingPoints.Point = pointsDto.Point;
            existingPoints.Level = pointsDto.Level;
            existingPoints.LastUpdatedOn = DateTime.Now;

            await _context.SaveChangesAsync();
            return existingPoints;
        }
    }
}