using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Dtos.Transaction;
using api.Models;

namespace api.Mappers
{
    public static class TransactionMapper
    {
        public static TransactionDto ToTransactionDto(this PointsTransaction transactionModel)
        {
            return new TransactionDto
            {
                Id = transactionModel.Id,
                CustomerId = transactionModel.CustomerId,
                OrderId = transactionModel.OrderId,
                Points = transactionModel.Points,
                TransactionType = transactionModel.TransactionType,
                OutletId = transactionModel.OutletId,
                CreatedOn = transactionModel.CreatedOn
            };
        }

        public static PointsTransaction ToTransactionFromUpsertDto(this UpsertTransactionDto transactionDto)
        {
            return new PointsTransaction
            {
                CustomerId = transactionDto.CustomerId,
                OrderId = transactionDto.OrderId,
                Points = transactionDto.Points,
                TransactionType = transactionDto.TransactionType,
                OutletId = transactionDto.OutletId
            };
        }
    }
}