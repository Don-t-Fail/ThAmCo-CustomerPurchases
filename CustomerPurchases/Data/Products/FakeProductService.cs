using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerPurchases.Models.DTOs;

namespace CustomerPurchases.Data.Products
{
    public class FakeProductService : IProductService
    {
        private List<ProductDto> _products;

        // TODO - Redo with HTTP Mocking

        public FakeProductService(List<ProductDto> products)
        {
            _products = products;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            return await Task.FromResult(_products.ToList());
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            return await Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
        }
    }
}
