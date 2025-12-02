using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Core.Entities {
    public class Wishlist : BaseEntity {
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = new List<Product>(); 
    }
}
