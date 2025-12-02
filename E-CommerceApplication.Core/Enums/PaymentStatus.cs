using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Core.Enums {
    public enum PaymentStatus {
        Pending = 1,
        Processing = 2,
        Completed = 3,
        Failed = 4,
        Refunded = 5
    }
}
