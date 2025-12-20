using E_CommerceApplication.Core.DTOs.Common;
using E_CommerceApplication.Core.DTOs.Order;
using E_CommerceApplication.Core.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Application.Interfaces {
    public interface IOrderService {
        Task<OrderResponseDto?> GetOrderByIdAsync(Guid id, string userId);
        Task<PagedResult<OrderResponseDto>> GetUserOrdersAsync(string userId, PaginationParam pagination);
        Task<OrderResponseDto> CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<OrderResponseDto> UpdateOrderStatusAsync(Guid id, OrderStatus status);
        Task<bool> CancelOrderAsync(Guid id, string userId);
    }
}
