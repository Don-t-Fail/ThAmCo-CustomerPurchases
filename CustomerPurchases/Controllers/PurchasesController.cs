using System;
using System.Collections.Generic;
using System.Net.Http;
using CustomerPurchases.Data;
using CustomerPurchases.Data.Products;
using CustomerPurchases.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CustomerPurchases.Models.DTOs;
using Microsoft.Extensions.Configuration;
using System.Linq;
using CustomerPurchases.Models.ViewModels;

namespace CustomerPurchases.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly IPurchaseRepo _repository;
        private readonly IProductService _productServ;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public PurchasesController(IPurchaseRepo repository, IProductService prodServ, IHttpClientFactory factory, IConfiguration config)
        {
            _repository = repository;
            _productServ = prodServ;
            _clientFactory = factory;
            _config = config;
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            var repo = await _repository.GetAll();
            return View(repo);
        }

        // GET: Purchases/Details/5
        public async Task<ActionResult<PurchaseDetailsDto>> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _repository.GetPurchase(id.Value);
            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(new PurchaseDetailsDto
            {
                Id = purchase.Id,
                AccountId = purchase.AccountId,
                AddressId = purchase.AddressId,
                OrderStatus = purchase.OrderStatus,
                ProductId = purchase.ProductId,
                Qty = purchase.Qty,
                TimeStamp = purchase.TimeStamp
            });
        }

        // GET: Purchases/Create
        public async Task<IActionResult> Create()
        {
            var products = await _productServ.GetAll();
            if (products.Count != 0)
            {
                ViewData["ProductId"] = new SelectList(products, "Id", "Name");
                
                // TODO - Pre-Populate and make unalterable certain values, once proper routing and security is setup

                return View();
            }
            return BadRequest("An error occurred connecting to the Products Service");
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,Qty,AddressId,AccountId,OrderStatus")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient("RetryAndBreak");
                client.BaseAddress = new System.Uri(_config["StockURL"]);
                var resp = await client.GetAsync("api/stock/");

                if (resp.IsSuccessStatusCode)
                {
                    var product = await resp.Content.ReadAsAsync<ProductStockDto>();
                    if (product.Stock <= 0)
                    {
                        return BadRequest("Product is out of stock");
                    }
                    // TODO - Check Account info (Address, Phone)
                    purchase.TimeStamp = DateTime.Now;
                    _repository.InsertPurchase(purchase);
                    await _repository.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["ProductId"] = new SelectList(await _productServ.GetAll(), "Id", "Id", purchase.ProductId);
            var purchaseView = new PurchaseCreateViewModel
            {
                AccountId = purchase.AccountId,
                AddressId = purchase.AddressId,
                Id = purchase.Id,
                OrderStatus = purchase.OrderStatus,
                ProductId = purchase.ProductId,
                Qty = purchase.Qty,
                TimeStamp = purchase.TimeStamp
            };
            return View(purchaseView);
        }

        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _repository.GetPurchase(id.Value);
            if (purchase == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(await _productServ.GetAll(), "Id", "Id", purchase.ProductId);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Qty,AddressId,AccountId,OrderStatus")] Purchase purchase)
        {
            if (id != purchase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdatePurchase(purchase);
                    await _repository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PurchaseExists(purchase.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(await _productServ.GetAll(), "Id", "Id", purchase.ProductId);
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _repository.GetPurchase(id.Value);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null || id < 0)
            {
                return BadRequest();
            }
            _repository.DeletePurchase(id);
            await _repository.Save();
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult<List<Purchase>>> OrderHistory(int accId)
        {
            if (accId < 0)
            {
                return BadRequest();
            }
            var purchases = await _repository.GetPurchaseByAccount(accId);
            if (purchases.Any())
            {
                return Ok(purchases);
            }

            return NotFound();
        }
        
        // TODO - Implement separately
        //public async Task<IActionResult> OrderHistory(int accId)
        //{
        //    var purchases = await _repository.GetPurchaseByAccount(accId);
        //    if (purchases.Any())
        //    {
        //        return View("OrderHistory",purchases);
        //    }

        //    return View();
        //}

        private async Task<bool> PurchaseExists(int id)
        {
            return await _repository.GetPurchase(id) != null;
        }
    }
}