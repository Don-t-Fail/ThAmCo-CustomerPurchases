using System.Collections.Generic;

namespace CustomerPurchases.Models.DTOs
{
    public class CustomerInfoDto
    {
        public int CustomerId { get; set; }
        public List<AddressDto> Addresses { get; set; }
        public List<TelDto> Tels { get; set; }
    }
}