using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Core.Entities {
    public class Payment: BaseEntity {
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public string PaymentMethod { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; } // THIS IS ENUM FIELD

        // PAYMENT GATEWAY INTEGRATION DETAILS
        public string? TransactionId { get; set; }
        public string? PayemntIntentId { get; set; }
        public DateTime? PaidAt { get; set; }

        public string? FailureReason { get; set; }

    }
}
