
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
        private readonly IUserService _userService;

        public PointsRepository(ApplicationDBContext context,
            ITransactionsRepository transactionsRepository,
            IRewardRepository rewardRepository,
            IUserService userService
            )
        {
            _context = context;
            _transactionsRepository = transactionsRepository;
            _rewardRepository = rewardRepository;
            _userService = userService;
        }

        public async Task<RewardPoints> CreateAsync(RewardPoints model)
        {
            await _context.RewardPoints.AddAsync(model);
            await _context.SaveChangesAsync();

            // Add a record to the PointsTransaction table
            var transaction = new PointsTransaction
            {
                CustomerId = model.CustomerId,
                Customer = await _userService.GetUsernameByIdAsync(model.CustomerId),
                StaffId = model.StaffId,
                AddedBy = await _userService.GetUsernameByIdAsync(model.StaffId),
                OrderId = model.OrderId,
                Points = model.Points,
                TransactionType = "EarnedPoints", // Example transaction type
                OutletId = model.OutletId
            };
            await _transactionsRepository.CreateAsync(transaction);

            // Check and update the RewardsRepository
            var reward = await _rewardRepository.GetByIdAsync(model.RewardId);
            if (reward != null)
            {
                // Add customer rewards based on points
                if (model.Points >= reward.PointsRequired)
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

        public async Task<RewardPoints?> UpdateAsync(UpsertPointsDto pointsDto)
        {
            var existingPoints = await _context.RewardPoints
                .FirstOrDefaultAsync(r => r.RewardId == pointsDto.RewardId && r.CustomerId == pointsDto.CustomerId);
            if (existingPoints == null)
            {
                return null;
            }
            existingPoints.Points += 1;
            existingPoints.Level = pointsDto.Level;
            existingPoints.LastUpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingPoints;
        }
    }
}