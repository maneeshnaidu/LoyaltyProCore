using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Data;
using api.Dtos.Transaction;
using api.Helpers;
using api.Interfaces;
using api.Models;

using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class TransactionRepository : ITransactionsRepository
    {
        private readonly ApplicationDBContext _context;
        public TransactionRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<PointsTransaction> CreateAsync(PointsTransaction transaction)
        {
            await _context.PointsTransactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<PointsTransaction?> DeleteAsync(int id)
        {
            var transaction = await _context.PointsTransactions.FirstOrDefaultAsync(t => t.Id == id);
            if (transaction == null) return null;
            _context.PointsTransactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<List<PointsTransaction>> GetAllAsync(QueryObject query)
        {
            var transactions = _context.PointsTransactions.AsQueryable();
            if (query.CreatedDate != null)
            {
                transactions = transactions.Where(t => t.CreatedOn == query.CreatedDate);
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await transactions
                .Skip(skipNumber)
                .Take(query.PageSize)
                .ToListAsync();
        }

        public async Task<PointsTransaction?> GetByIdAsync(int id)
        {
            return await _context.PointsTransactions.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<PointsTransaction?> UpdateAsync(int id, UpsertTransactionDto transactionDto)
        {
            var existingTransaction = await _context.PointsTransactions.FirstOrDefaultAsync(t => t.Id == id);
            if (existingTransaction == null) return null;
            existingTransaction.CustomerId = transactionDto.CustomerId;
            existingTransaction.OrderId = transactionDto.OrderId;
            existingTransaction.Points = transactionDto.Points;
            existingTransaction.TransactionType = transactionDto.TransactionType;
            existingTransaction.OutletId = transactionDto.OutletId;

            await _context.SaveChangesAsync();
            return existingTransaction;
        }
    }
}