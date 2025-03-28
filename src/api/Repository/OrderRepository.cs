using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Helpers;
using api.Data;
using api.Dtos.Order;
using api.Interfaces;
using api.Models;

using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;
        public OrderRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateAsync(Order OrderModel)
        {
            await _context.Orders.AddAsync(OrderModel);
            await _context.SaveChangesAsync();
            return OrderModel;
        }

        public async Task<Order?> DeleteAsync(int id)
        {
            var orderModel = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (orderModel == null)
            {
                return null;
            }

            _context.Orders.Remove(orderModel);
            await _context.SaveChangesAsync();
            return orderModel;
        }

        public async Task<List<Order>> GetAllAsync(QueryObject query)
        {
            var orders = _context.Orders.AsQueryable();

            // if (!string.IsNullOrWhiteSpace(query.CreatedDate))
            // {
            //     orders = orders.Where(x => x.CreatedOn.Contains(query.CompanyName));
            // }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    orders = query.IsDecsending ? orders.OrderByDescending(o => o.CreatedOn) : orders.OrderBy(o => o.CreatedOn);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await orders.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public Task<bool> OrderExists(int id)
        {
            return _context.Orders.AnyAsync(o => o.Id == id);
        }

        public async Task<Order?> UpdateAsync(int id, UpdateOrderRequestDto orderRequestDto)
        {
            var existingOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (existingOrder == null)
            {
                return null;
            }

            existingOrder.Amount = orderRequestDto.Amount;

            await _context.SaveChangesAsync();

            return existingOrder;
        }
    }
}