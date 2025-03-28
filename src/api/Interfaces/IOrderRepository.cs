using api.Helpers;
using api.Dtos.Order;
using api.Models;

namespace api.Interfaces
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