using CustomerPurchases.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerPurchases.Data.Products
{
    public class ProductService : IProductService
    {
        private readonly PurchaseDbContext _context;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<ProductService> _logger;

        public ProductService(PurchaseDbContext context, IHttpClientFactory clientFactory, IConfiguration config, ILogger<ProductService> logger)
        {
            _context = context;
            _clientFactory = clientFactory;
            _config = config;
            _logger = logger;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var products = await _context.Product.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToListAsync();
            // If products exist in database, connection is working, return
            if (products.Count != 0)
            {
                return products;
            }

            // Else, try to GET from products service
            var client = _clientFactory.CreateClient("RetryAndBreak");
            client.BaseAddress = new Uri(_config["ProductsUrl"]);

            _logger.LogInformation("Contacting Products Service");

            var resp = await client.GetAsync("api/products/GetAll");
            var productsExt = new List<ProductDto>();

            if (resp.IsSuccessStatusCode)
            {
                products = await resp.Content.ReadAsAsync<List<ProductDto>>();
                return productsExt;
            }

            return null;
        }

        public Task<ProductDto> GetProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
