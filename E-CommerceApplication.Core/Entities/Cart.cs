using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Core.Entities {
    public class Cart : BaseEntity {
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public decimal TotalAmount => CartItems.Sum(item => item.SubTotal);
    }
}
