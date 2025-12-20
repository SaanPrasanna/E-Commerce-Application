using E_CommerceApplication.Core.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Application.Interfaces {
    public interface IWishlistService {
        Task<IEnumerable<ProductListDto>> GetWishlistAsync(string userId);
        Task<bool> AddToWishListAsync(string userId, Guid productId);
        Task<bool> RemoveFromWishlistAsync(string userId, Guid productId);
        Task<bool> IsInWishlistAsync(string userId, Guid productId);
    }
}
