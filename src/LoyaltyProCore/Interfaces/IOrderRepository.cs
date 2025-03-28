using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoyaltyProCore.Dtos.Order;
using LoyaltyProCore.Helpers;
using LoyaltyProCore.Models;

namespace LoyaltyProCore.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync(QueryObject query);
        Task<Order?> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order OrderModel);
        Task<Order?> UpdateAsync(int id, UpdateOrderRequestDto OrderRequestDto);
        Task<Order?> DeleteAsync(int id);
        Task<bool> OrderExists(int id);   
    }
}