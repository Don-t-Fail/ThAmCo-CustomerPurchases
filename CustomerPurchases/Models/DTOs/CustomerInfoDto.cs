using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerPurchases.Models.DTOs
{
    public class CustomerInfoDto
    {
        public int CustomerId { get; set; }
        public List<AddressDto> Addresses { get; set; }
        public List<TelDto> Tels { get; set; }
    }
}
