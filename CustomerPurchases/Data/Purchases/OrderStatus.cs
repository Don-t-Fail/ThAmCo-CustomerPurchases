using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerPurchases.Data.Purchases
{
    public class OrderStatus
    {
        public static readonly string Created = "Created";
        public static readonly string Cancelled = "Cancelled";
        public static readonly string Shipped = "Shipped";
        public static readonly string Completed = "Completed";
    }
}
