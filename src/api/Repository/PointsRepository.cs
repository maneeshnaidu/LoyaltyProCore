
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
                Customer = await _userService.GetUsernameByIdAsync(model.CustomerId) ?? string.Empty,
                StaffId = model.StaffId,
                AddedBy = await _userService.GetUsernameByIdAsync(model.StaffId) ?? string.Empty,
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
                    model.Points -= reward.PointsRequired;
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
            if (user != null)
            {
                var points = _context.RewardPoints
                .Include(p => p.Vendor)
                .Where(p => p.CustomerId == user.Id);

                return await points.ToListAsync();
            }

            return [];

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
            existingPoints.Points += pointsDto.Point;
            existingPoints.Level = pointsDto.Level;
            existingPoints.LastUpdatedOn = DateTime.UtcNow;

            // Add a record to the PointsTransaction table
            var transaction = new PointsTransaction
            {
                CustomerId = pointsDto.CustomerId,
                Customer = await _userService.GetUsernameByIdAsync(pointsDto.CustomerId) ?? string.Empty,
                StaffId = pointsDto.StaffId,
                AddedBy = await _userService.GetUsernameByIdAsync(pointsDto.StaffId) ?? string.Empty,
                OrderId = pointsDto.OrderId,
                Points = pointsDto.Point,
                TransactionType = "EarnedPoints", // Example transaction type
                OutletId = pointsDto.OutletId
            };
            await _transactionsRepository.CreateAsync(transaction);

            // Check and update the RewardsRepository
            // var reward = await _rewardRepository.GetByIdAsync(existingPoints.RewardId);
            // if (reward != null)
            // {
            //     // Add customer rewards based on points
            //     if (existingPoints.Points >= reward.PointsRequired)
            //     {
            //         existingPoints.Points -= reward.PointsRequired;
            //         await _rewardRepository.AddRewardAsync(existingPoints);
            //     }
            // }
            await _context.SaveChangesAsync();
            return existingPoints;
        }

        public async Task<List<CustomerRewards>> GetUserRewards(ApplicationUser user)
        {
            var customerRewards = _context.CustomerRewards
                .Include(cr => cr.Reward)
                .AsQueryable();
            if (customerRewards != null)
            {
                customerRewards = customerRewards.Where(cr => cr.CustomerId == user.Id &&
                    cr.RedeemedOn == null && cr.ExpiryDate > DateTime.UtcNow); // Filter by customer ID and redeemed status

                return await customerRewards.ToListAsync();
            }

            return [];
        }

        public async Task<RewardPoints?> RedeemPointsAsync(int customerCode, UpsertPointsDto pointsDto)
        {
            var existingPoints = await _context.RewardPoints
                .FirstOrDefaultAsync(r => r.CustomerId == pointsDto.CustomerId && r.VendorId == pointsDto.VendorId);
            if (existingPoints == null)
            {
                return null; // No points found for the given customer and vendor
            }

            var reward = await _rewardRepository.GetByIdAsync(pointsDto.RewardId);
            if (reward == null)
            {
                return null; // Reward not found
            }

            // Check if the user has enough points to redeem the reward
            if (existingPoints.Points < reward.PointsRequired)
            {
                return null; // Not enough points
            }

            // Deduct the points and update the database
            existingPoints.Points -= reward.PointsRequired;
            existingPoints.LastUpdatedOn = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            // Add a record to the PointsTransaction table
            var transaction = new PointsTransaction
            {
                CustomerId = pointsDto.CustomerId,
                Customer = await _userService.GetUsernameByIdAsync(pointsDto.CustomerId) ?? string.Empty,
                StaffId = pointsDto.StaffId,
                AddedBy = await _userService.GetUsernameByIdAsync(pointsDto.StaffId) ?? string.Empty,
                OrderId = pointsDto.OrderId,
                Points = reward.PointsRequired,
                TransactionType = "RedeemedPoints", // Example transaction type
                OutletId = pointsDto.OutletId
            };
            await _transactionsRepository.CreateAsync(transaction);

            return existingPoints;
        }
    }
}