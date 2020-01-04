using CustomerPurchases.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerPurchases.Data.Products
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAll();

        Task<ProductDto> GetProduct(int id);
    }
}