using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerPurchases.Data;
using CustomerPurchases.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CustomerPurchases.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseRepo _repository;
        private readonly ILogger<PurchasesController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public PurchasesController(IPurchaseRepo purchaseRepo, ILogger<PurchasesController> logger, IHttpClientFactory clientFactory, IConfiguration config)
        {
            _repository = purchaseRepo;
            _logger = logger;
            _clientFactory = clientFactory;
            _config = config;
        }

        // GET: api/Purchases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Purchase>> GetPurchase(int id)
        {
            var purchase = await _repository.GetPurchase(id);

            if (purchase == null)
            {
                return NotFound();
            }

            return purchase;
        }

        // PUT: api/Purchases/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchase(int id, Purchase purchase)
        {
            if (id != purchase.Id)
            {
                return BadRequest();
            }

            _repository.UpdatePurchase(purchase);

            // TODO - Implement Pushing updated purchases to Reviews Service

            try
            {
                await _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await PurchaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Purchases
        [HttpPost]
        public async Task<ActionResult<Purchase>> PostPurchase(Purchase purchase)
        {
            _repository.InsertPurchase(purchase);
            await _repository.Save();

            _logger.LogInformation("Communicating with Reviews API");
            var client = _clientFactory.CreateClient("RetryAndBreak");
            client.BaseAddress = new System.Uri(_config["ReviewsURL"]);

            var resp = await client.PostAsJsonAsync("api/Purchases/", purchase);
            if (resp.IsSuccessStatusCode)
            {
                return CreatedAtAction("GetPurchase", new { id = purchase.Id }, purchase);
            }

            return BadRequest();
        }

        // DELETE: api/Purchases/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Purchase>> DeletePurchase(int id)
        {
            // TODO - Soft Deletion
            var purchase = await _repository.GetPurchase(id);
            if (purchase == null)
            {
                return NotFound();
            }

            _repository.DeletePurchase(id);
            await _repository.Save();

            return purchase;
        }

        // GET: api/Purchases?AccId=5
        [HttpGet]
        public async Task<IEnumerable<Purchase>> GetPurchaseAccount(int accId)
        {
            var purchases = await _repository.GetPurchaseByAccount(accId);
            if (purchases.Any())
            {
                return purchases;
            }

            return null;
        }
        private async Task<bool> PurchaseExists(int id)
        {
            return await _repository.GetPurchase(id) != null;
        }
    }
}
