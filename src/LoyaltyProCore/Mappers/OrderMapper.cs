using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LoyaltyProCore.Dtos.Order;
using LoyaltyProCore.Models;

namespace LoyaltyProCore.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDto(this Order orderModel)
        {
            return new OrderDto
            {
                Id = orderModel.Id,
                ExternalId = orderModel.ExternalId,
                CustomerId = orderModel.CustomerId,
                OutletId = orderModel.OutletId,
                Amount = orderModel.Amount,
                PointsEarned = orderModel.PointsEarned,
                CreatedOn = orderModel.CreatedOn
            };
        }

        public static Order ToOrderFromCreateDto(this CreateOrderRequestDto orderRequestDto)
        {
            return new Order
            {
                ExternalId = orderRequestDto.ExternalId,
                CustomerId = orderRequestDto.CustomerId,
                OutletId = orderRequestDto.OutletId,
                Amount = orderRequestDto.Amount
            };
        }

    }
}