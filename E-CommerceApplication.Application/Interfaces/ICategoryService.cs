using E_CommerceApplication.Core.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Application.Interfaces {
    public interface ICategoryService {
        Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync();
        Task<CategoryResponseDto?> GetCategoryByIdAsync(int categoryId);
        Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto dto);
        Task<CategoryResponseDto?> UpdateCategoryAsync(int categoryId, UpdateCategoryDto dto);
        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}
