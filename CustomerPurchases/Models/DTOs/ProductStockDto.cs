using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerPurchases.Models.DTOs
{
    public class ProductStockDto
    {
        public int ProductID { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
    }
}
