﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerPurchases.Models.DTOs
{
    public class PurchaseDetailsDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public int AddressId { get; set; }
        public int AccountId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
