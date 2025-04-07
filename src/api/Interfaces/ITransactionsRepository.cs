using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Dtos.Transaction;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface ITransactionsRepository
    {
        Task<List<PointsTransaction>> GetAllAsync(QueryObject query);
        Task<PointsTransaction?> GetByIdAsync(int id);
        Task<PointsTransaction> CreateAsync(PointsTransaction transactionModel);
        Task<PointsTransaction?> UpdateAsync(int id, UpsertTransactionDto transactionDto);
        Task<PointsTransaction?> DeleteAsync(int id);
    }
}