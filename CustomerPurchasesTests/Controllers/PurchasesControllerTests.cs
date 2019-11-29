using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerPurchases.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomerPurchases.Data;
using CustomerPurchases.Models;
using Microsoft.Extensions.Logging.Abstractions;

namespace CustomerPurchases.Controllers.Tests
{
    [TestClass]
    public class PurchasesControllerTests
    {
        [TestMethod]
        public async void GetPurchaseTest()
        {
            // Arrange
            var purchases = new List<Purchase>
            {
                new Purchase { Id = 1, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 1, Qty = 2},
                new Purchase { Id = 2, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 2, Qty = 7}
            };
            var repo = new FakePurchaseRepo(purchases);
            var controller = new PurchasesController(repo, new NullLogger<PurchasesController>());
            var purchaseId = 1;

            // Act
            var result = await controller.GetPurchase(purchaseId);

            // Assert
            Assert.AreEqual(purchases.FirstOrDefault(p => p.Id == purchaseId), result.Value);
        }

        [TestMethod]
        public void GetPurchaseTest_NotExists()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void PutPurchaseTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void PutPurchaseTest_NotExists()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void PostPurchaseTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void PostPurchaseTest_Exists()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void PostPurchaseTest_InvalidPurchase()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void DeletePurchaseTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void DeletePurchaseTest_NotExists()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetPurchaseAccountTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetPurchaseAccountTest_NotExists()
        {
            Assert.Fail();
        }
    }
}