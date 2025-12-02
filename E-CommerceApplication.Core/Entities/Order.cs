using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Core.Entities {
    public class Order : BaseEntity {
        public string OrderNumber { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;

        // ORDER STATUS
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal TotalAmount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalAmount { get; set; }

        // SHIPPING
        public Guid ShipppingAddressId { get; set; }
        public Address ShippingAddress { get; set; } = null!;

        // PAYMENT
        public Payment? Payment { get; set; }

        // ITEMS
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // TRACKING
        public DateTime ShippedAt { get; set; }
        public DateTime DeliveredAt { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Note { get; set; }
    }
}
