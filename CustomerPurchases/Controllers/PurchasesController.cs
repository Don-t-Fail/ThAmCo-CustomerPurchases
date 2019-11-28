using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerPurchases.Data;
using CustomerPurchases.Models;
using Microsoft.Extensions.Logging;

namespace CustomerPurchases.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseRepo _repository;
        private readonly ILogger<PurchasesController> _logger;

        public PurchasesController(IPurchaseRepo purchaseRepo, ILogger<PurchasesController> logger)
        {
            _repository = purchaseRepo;
            _logger = logger;
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
            // TODO - Update Purchase
            return NotFound();
        }

        // POST: api/Purchases
        [HttpPost]
        public async Task<ActionResult<Purchase>> PostPurchase(Purchase purchase)
        {
            _repository.InsertPurchase(purchase);
            await _repository.Save();

            return CreatedAtAction("GetPurchase", new { id = purchase.Id }, purchase);
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
        public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchaseAccount(int accId)
        {
            var purchases = await _repository.GetPurchaseByAccount(accId);
            if (purchases.Any())
            {
                return Ok(purchases);
            }

            return NotFound();
        }

        // TODO - Make Async?
        private bool PurchaseExists(int id)
        {
            return _repository.GetAll().Result.Any(p => p.Id == id);
        }
    }
}
