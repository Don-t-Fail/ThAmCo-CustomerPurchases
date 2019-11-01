﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerPurchases.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public int AddressId { get; set; }
        public int AccountId { get; set; }
        public string OrderStatus { get; set; }

        public virtual Product Product { get; set; }
    }
}
