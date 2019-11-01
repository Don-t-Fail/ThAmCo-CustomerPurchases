using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerPurchases.Models
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Address { get; set; }

        public virtual Account Account { get; set; }
    }
}
