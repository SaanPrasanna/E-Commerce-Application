using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Core.DTOs.Cart {
    public class AddToCartDto {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateCartItemDto {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CartItemResponseDto {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? ProductImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
        public bool InStock { get; set; }
    }

    public class CartResponseDto {
        public Guid Id { get; set; }
        public List<CartItemResponseDto> Items { get; set; } = new List<CartItemResponseDto>();
        public decimal TotalAmount { get; set; }
        public int TotalItems => Items.Sum(i => i.Quantity);
    }
}
