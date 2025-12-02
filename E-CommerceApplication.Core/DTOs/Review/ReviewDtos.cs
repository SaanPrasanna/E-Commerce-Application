using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Core.DTOs.Review {
    public class CreateReviewDto {
        public Guid ProductId { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
    }

    public class  ReviewResponseDto {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public bool IsVerifiedPurchased { get; set; }
        public int HelfulCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
