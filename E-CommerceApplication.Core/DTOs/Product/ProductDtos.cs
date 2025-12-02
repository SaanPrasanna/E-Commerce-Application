using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Core.DTOs.Product {
    public class CreateProductDto {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int StockQuantity { get; set; }
        public string? MainImageUrl { get; set; }
        public List<string>? ImageUrls { get; set; } = new List<string>();
        public string? SKU { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsFeatured { get; set; }
    }

    public class UpdateProductDto {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int StockQuantity { get; set; }
        public string? MainImageUrl { get; set; }
        public List<string>? ImageUrls { get; set; } = new List<string>();
        public string? SKU { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsFeatured { get; set; }
    }

    public class ProductResponseDto {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? Discountprice { get; set; }
        public int StockQuantity { get; set; }
        public string? MainImageUrl { get; set; }
        public List<string>? ImageUrls { get; set; } = new List<string>();
        public string? SKU { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public bool InStock => StockQuantity > 0;

        // CATEGORY
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        // RATING & REVIEW
        public decimal AverateRating { get; set; }
        public int ReviewCount { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class ProductListDto {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? Discountprice { get; set; }
        public string? MainImageUrl { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
        public bool InStock { get; set; }
        public decimal AveragePrice { get; set; }
        public int ReviewCount { get; set; }
        public string CategoryName { get; set; } = string.Empty;

    }
}
