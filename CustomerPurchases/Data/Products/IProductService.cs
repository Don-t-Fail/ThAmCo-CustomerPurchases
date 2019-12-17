using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerPurchases.Models;
using CustomerPurchases.Models.DTOs;

namespace CustomerPurchases.Data.Products
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAll();
        Task<ProductDto> GetProduct(int id);
    }
}
