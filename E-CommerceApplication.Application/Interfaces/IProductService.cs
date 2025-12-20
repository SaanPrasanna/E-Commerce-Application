using E_CommerceApplication.Core.DTOs.Common;
using E_CommerceApplication.Core.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Application.Interfaces {
    public interface IProductService {
        Task<ProductResponseDto> GetProductAsync(ProductFilterParam filterParam);
        Task<ProductResponseDto> GetProductByIdAsync(int productId);
        Task<ProductResponseDto> CreateProductAsync(CreateProductDto createProductDto);
        Task<ProductResponseDto> UpdateProductAsync(int productId, UpdateProductDto updateProductDto);
        Task<bool> DeleteProductAsync(int productId);
        Task<IEnumerable<ProductListDto>> GetFeaturedProductsAsync(int count = 10);
        Task<IEnumerable<ProductListDto>> GetRelatedProductsAsync(Guid id, int count = 5);
    }
}
