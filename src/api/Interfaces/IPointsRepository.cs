using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Dtos.Points;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IPointsRepository
    {
        Task<List<Points>> GetAllAsync(QueryObject query);
        Task<Points?> GetByIdAsync(int id);
        Task<List<Points>> GetByCustomerIdAsync(string customerId);
        Task<Points> CreateAsync(Points pointsModel);
        Task<Points?> UpdateAsync(int id, UpsertPointsDto pointsDto);
        Task<Points?> DeleteAsync(int id);
        Task<bool> PointsExists(int id);
    }
}