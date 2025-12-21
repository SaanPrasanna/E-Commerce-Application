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

        public async Task<PagedResult<ProductListDto>> GetProductAsync(ProductFilterParam filterParam) {
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

        public Task<ProductResponseDto> CreateProductAsync(CreateProductDto createProductDto) {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteProductAsync(int productId) {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<ProductListDto>> GetFeaturedProductsAsync(int count = 10) {
            throw new NotImplementedException();
        }
        public Task<ProductResponseDto> GetProductByIdAsync(int productId) {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<ProductListDto>> GetRelatedProductsAsync(Guid id, int count = 5) {
            throw new NotImplementedException();
        }
        public Task<ProductResponseDto> UpdateProductAsync(int productId, UpdateProductDto updateProductDto) {
            throw new NotImplementedException();
        }

        Task<ProductResponseDto> IProductService.GetProductAsync(ProductFilterParam filterParam) {
            throw new NotImplementedException();
        }

        public Task<ProductResponseDto> GetProductByIdAsync(Guid productId) {
            throw new NotImplementedException();
        }

        public Task<ProductResponseDto> UpdateProductAsync(Guid productId, UpdateProductDto updateProductDto) {
            throw new NotImplementedException();
        }
    }
}
