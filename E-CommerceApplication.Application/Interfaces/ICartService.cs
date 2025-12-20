using E_CommerceApplication.Core.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Application.Interfaces {
    public interface ICartService {
        Task<CartResponseDto> GetCartByUserAsync(string UserId);
        Task<CartResponseDto> AddToCartAsync(string UserId, AddToCartDto dto);
        Task<CartResponseDto> UpdateCartItemAsync(string Userid, UpdateCartItemDto dto);
        Task<CartResponseDto> DeleteCartItemAsync(string UserId, Guid cartItemId);
        Task<bool> clearCartAsync(string userID);
    }
}
