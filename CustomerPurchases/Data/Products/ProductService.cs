using CustomerPurchases.Models;
using CustomerPurchases.Models.DTOs;
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
            var client = _clientFactory.CreateClient("RetryAndBreak");
            client.BaseAddress = new Uri(_config["ProductsUrl"]);

            _logger.LogInformation("Contacting Products Service");

            var resp = await client.GetAsync("products/GetAllProducts");

            if (resp.IsSuccessStatusCode)
            {
                var products = await resp.Content.ReadAsAsync<List<ProductDto>>();
                foreach (var product in products)
                {
                    if (!_context.Product.Any(p => p.Id == product.Id))
                    {
                        await _context.Product.AddAsync(new Product { Name = product.Name });
                    }
                }

                await _context.SaveChangesAsync();
                return products;
            }

            return null;
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            var client = _clientFactory.CreateClient("RetryAndBreak");
            client.BaseAddress = new Uri(_config["ProductsUrl"]);

            _logger.LogInformation("Contacting Products Service");

            var resp = await client.GetAsync("products/GetProduct/" + id);

            if (resp.IsSuccessStatusCode)
            {
                var product = await resp.Content.ReadAsAsync<ProductDto>();
                return product;
            }

            return null;
        }
    }
}