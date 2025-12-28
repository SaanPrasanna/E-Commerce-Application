using E_CommerceApplication.Application.Interfaces;
using E_CommerceApplication.Core.DTOs.Common;
using E_CommerceApplication.Core.DTOs.Product;
using E_CommerceApplication.Core.Entities;
using E_CommerceApplication.Core.interfaces;
using E_CommerceApplication.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Application.Services {
    public class ProductService : IProductService {

        private readonly IRepository<Product> _productRepository;
        private readonly ApplicationDbContext _context;

        public ProductService(IRepository<Product> productRepository, ApplicationDbContext context) {
            _productRepository = productRepository;
            _context = context;
        }

        public async Task<PagedResult<ProductListDto>> GetProductsAsync(ProductFilterParam filterParam) {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .Where(p => p.IsActive)
                .AsQueryable();

            // Apply filters
            if (filterParam.CategoryId.HasValue) {
                query = query.Where(p => p.CategoryId == filterParam.CategoryId.Value);
            }

            if (filterParam.MinPrice.HasValue) {
                query = query.Where(p => p.Price >= filterParam.MinPrice.Value);
            }

            if (filterParam.MaxPrice.HasValue) {
                query = query.Where(p => p.Price <= filterParam.MaxPrice.Value);
            }

            if (!string.IsNullOrEmpty(filterParam.SearchTerm)) {
                query = query.Where(p => p.Name.Contains(filterParam.SearchTerm) || p.Description.Contains(filterParam.SearchTerm));
            }

            if (filterParam.InStock.HasValue) {
                if (filterParam.InStock.Value) {
                    query = query.Where(p => p.StockQuantity > 0);
                } else {
                    query = query.Where(p => p.StockQuantity == 0);
                }
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(filterParam.SortBy)) {
                switch (filterParam.SortBy.ToLower()) {
                    case "price":
                        query = filterParam.SortByDescending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
                        break;
                    case "name":
                        query = filterParam.SortByDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                        break;
                    case "createdat":
                        query = filterParam.SortByDescending ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt);
                        break;
                    default:
                        query = query.OrderBy(p => p.Name);
                        break;
                }
            } else {
                query = query.OrderBy(p => p.Name);
            }

            // Apply pagination
            var totalItems = await query.CountAsync();

            var products = await query
                .Skip((filterParam.PageNumber - 1) * filterParam.PageSize)
                .Take(filterParam.PageSize)
                .Select(p => new ProductListDto {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    DiscountPrice = p.Discount,
                    MainImageUrl = p.MainImageUrl,
                    InStock = p.StockQuantity > 0,
                    AverageRating = p.AverageRating,
                    ReviewCount = p.ReviewCount,
                    CategoryName = p.Category.Name
                })
                .ToListAsync();

            return new PagedResult<ProductListDto> {
                Items = products,
                TotalCount = totalItems,
                PageNumber = filterParam.PageNumber,
                PageSize = filterParam.PageSize
            };
        }

        public async Task<IEnumerable<ProductListDto>> GetFeaturedProductsAsync(int count = 10) {
            return await _context.Products
                .Where(p => p.IsFeatured && p.IsActive && p.StockQuantity > 0)
                .OrderByDescending(p => p.CreatedAt)
                .Take(count)
                .Select(p => new ProductListDto {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    DiscountPrice = p.Discount,
                    MainImageUrl = p.MainImageUrl,
                    InStock = p.StockQuantity > 0,
                    AverageRating = p.AverageRating,
                    ReviewCount = p.ReviewCount,
                    CategoryName = p.Category.Name
                })
                .ToListAsync();
        }

        public async Task<ProductResponseDto> GetProductByIdAsync(Guid id) {
            var product = await _context.Products.Include(p => p.Category).Include(p => p.Reviews).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) throw new KeyNotFoundException($"Product with ID {id} was not found.");

            return new ProductResponseDto {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Discountprice = product.Discount,
                StockQuantity = product.StockQuantity,
                MainImageUrl = product.MainImageUrl,
                ImageUrls = product.ImageUrls.ToList(),
                SKU = product.SKU,
                IsActive = product.IsActive,
                IsFeatured = product.IsFeatured,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                AverateRating = product.AverageRating,
                ReviewCount = product.ReviewCount,
                CreatedAt = product.CreatedAt
            };

        }

        public async Task<IEnumerable<ProductListDto>> GetRelatedProductsAsync(Guid id, int count = 5) {
            var product = await _context.Products.FindAsync(id);

            if (product == null) throw new KeyNotFoundException($"Product with ID {id} was not found.");

            return await _context.Products
                .Where(p => p.CategoryId == product.CategoryId && p.Id != id && p.IsActive)
                .OrderByDescending(p => p.CreatedAt)
                .Take(count)
                .Select(p => new ProductListDto {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    DiscountPrice = p.Discount,
                    MainImageUrl = p.MainImageUrl,
                    InStock = p.StockQuantity > 0,
                    AverageRating = p.AverageRating,
                    ReviewCount = p.ReviewCount,
                    CategoryName = p.Category.Name
                })
                .ToListAsync();
        }

        public Task<ProductResponseDto> CreateProductAsync(CreateProductDto createProductDto) {
            throw new NotImplementedException();
        }

        public Task<ProductResponseDto> UpdateProductAsync(Guid productId, UpdateProductDto updateProductDto) {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteProductAsync(Guid productId) {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null) throw new KeyNotFoundException($"Product with ID {productId} was not found.");

            await _productRepository.DeleteAsync(product);
            return true;
        }
    }
}
