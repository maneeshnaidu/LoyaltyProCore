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
                Customer = transactionModel.Customer,
                OrderNumber = transactionModel.OrderNumber,
                OutletAddress = transactionModel.OutletAddress,
                Points = transactionModel.Points,
                TransactionType = transactionModel.TransactionType,
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