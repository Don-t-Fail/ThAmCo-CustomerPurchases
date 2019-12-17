using System.Collections.Generic;

namespace CustomerPurchases.Models
{
    public class Account
    {
        public int Id { get; set; }
        public bool IsStaff { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<CustomerTel> CustomerTels { get; set; }
        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }
    }
}
