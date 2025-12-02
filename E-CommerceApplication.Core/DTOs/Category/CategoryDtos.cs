using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Core.DTOs.Category {
    public class CreateCategoryDto {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }

    public class UpdateCategoryDto {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
    }

    public class  CategoryResponseDto {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public List<CategoryResponseDto> Responses { get; set; } = new List<CategoryResponseDto>();
        public int ProductCount { get; set; }
    }
}
