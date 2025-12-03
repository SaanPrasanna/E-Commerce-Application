using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace E_CommerceApplication.Core.Entities {
    public class User : IdentityUser {
        public string? FullName { get; set; }
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public Cart? Cart { get; set; }
        public Wishlist? Wishlist { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}
