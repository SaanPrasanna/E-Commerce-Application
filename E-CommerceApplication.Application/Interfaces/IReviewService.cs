using E_CommerceApplication.Core.DTOs.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Application.Interfaces {
    public interface IReviewService {
        Task<ReviewResponseDto> CreateReviewAsync(string userId, CreateReviewDto dto);
        Task<IEnumerable<ReviewResponseDto>> GetReviewsAsync(Guid productId);
        Task<bool> DeleteReviewAsync(Guid reviewId, string userId);
    }
}
