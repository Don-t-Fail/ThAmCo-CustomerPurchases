using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerPurchases.Models.ViewModels
{
    public class PurchaseCreateViewModel
    {
        public int Id { get; set; }
        [Required,Display(Name="Product")]
        public int ProductId { get; set; }
        [Range(0, 99,
            ErrorMessage = "Value for {0} must be between {1} and {2}"),
         Display(Name="Quantity")]
        public int Qty { get; set; }
        [Required,
        Display(Name="Address")]
        public int AddressId { get; set; }
        [Required,
        Display(Name="Account")]
        public int AccountId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
