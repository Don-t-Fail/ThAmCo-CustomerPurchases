using CustomerPurchases.Data;
using CustomerPurchases.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CustomerPurchases.Data.Products;
using CustomerPurchases.Data.Purchases;
using CustomerPurchases.Models.DTOs;

namespace CustomerPurchases.Controllers.Tests
{
    [TestClass]
    public class PurchasesControllerTests
    {
        private static class TestData
        {
            public static List<Purchase> Purchases() => new List<Purchase>
            {
                new Purchase { Id = 1, AccountId = 1, AddressId = 1, OrderStatus = OrderStatus.Completed, ProductId = 1, Qty = 2},
                new Purchase { Id = 2, AccountId = 1, AddressId = 1, OrderStatus = OrderStatus.Completed, ProductId = 2, Qty = 7}
            };

            public static List<ProductDto> Products() => new List<ProductDto>
            {
                new ProductDto { Id = 1, Name = "Product"}
            };
        }

        private Mock<HttpMessageHandler> CreateHttpMock(HttpResponseMessage expected)
        {
            var mock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>(
        "SendAsync",
        ItExpr.IsAny<HttpRequestMessage>(),
        ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expected)
                .Verifiable();
            return mock;
        }

        [TestMethod]
        public async Task GetPurchaseTest_Valid()
        {
            // Arrange
            var repo = new FakePurchaseRepo(TestData.Purchases());
            var productRepo = new FakeProductService(TestData.Products());
            var controller = new PurchasesController(repo, productRepo, null, null);
            var purchaseId = 1;

            // Act
            var result = await controller.Details(purchaseId);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var retResult = objResult.Value as PurchaseDetailsDto;
            Assert.IsNotNull(retResult);

            Assert.AreEqual(TestData.Purchases()[purchaseId - 1].ProductId, retResult.Id);
            Assert.AreEqual(TestData.Purchases()[purchaseId - 1].ProductId, retResult.ProductId);
            Assert.AreEqual(TestData.Purchases()[purchaseId - 1].Qty, retResult.Qty);
            Assert.AreEqual(TestData.Purchases()[purchaseId - 1].AddressId, retResult.AddressId);
            Assert.AreEqual(TestData.Purchases()[purchaseId - 1].AccountId, retResult.AccountId);
            Assert.AreEqual(TestData.Purchases()[purchaseId - 1].OrderStatus, retResult.OrderStatus);
            Assert.AreEqual(TestData.Purchases()[purchaseId - 1].TimeStamp, retResult.TimeStamp);
        }

        [TestMethod]
        public async Task GetPurchaseDetailsTest_NotExists()
        {
            // Arrange
            var repo = new FakePurchaseRepo(TestData.Purchases());
            var productRepo = new FakeProductService(TestData.Products());
            var controller = new PurchasesController(repo, productRepo, null, null);
            var purchaseId = 7000;

            //Act
            var result = await controller.Details(purchaseId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetPurchaseDetailsTest_OutOfBoundsID()
        {
            // Arrange
            var repo = new FakePurchaseRepo(TestData.Purchases());
            var productRepo = new FakeProductService(TestData.Products());
            var controller = new PurchasesController(repo, productRepo, null, null);
            var purchaseId = -6;

            //Act
            var result = await controller.Details(purchaseId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeletePurchase_Success()
        {
            // Arrange
            var repo = new FakePurchaseRepo(TestData.Purchases());
            var productRepo = new FakeProductService(TestData.Products());
            var controller = new PurchasesController(repo, productRepo, null, null);
            var purchaseId = 1;

            // Act
            var result = await controller.DeleteConfirmed(purchaseId);

            // Assert
            var purchase = await repo.GetPurchase(purchaseId);
            Assert.IsNull(purchase);
        }

        //[TestMethod]
        //public async Task GetPurchaseAccountTest()
        //{
        //    // Arrange
        //    var purchases = new List<Purchase>
        //    {
        //        new Purchase { Id = 1, AccountId = 1, AddressId = 1, OrderStatus = OrderStatus.Completed, ProductId = 1, Qty = 2},
        //        new Purchase { Id = 2, AccountId = 1, AddressId = 1, OrderStatus = OrderStatus.Completed, ProductId = 2, Qty = 7},
        //        new Purchase { Id = 1, AccountId = 1, AddressId = 1, OrderStatus = OrderStatus.Completed, ProductId = 1, Qty = 2},
        //        new Purchase { Id = 1, AccountId = 2, AddressId = 1, OrderStatus = OrderStatus.Completed, ProductId = 1, Qty = 2}
        //    };
        //    var repo = new FakePurchaseRepo(purchases);
        //    var factory = new Mock<IHttpClientFactory>();
        //    var controller = new PurchasesApiController(repo, new NullLogger<PurchasesApiController>(), factory.Object, null);
        //    var accId = 1;

        //    // Act
        //    var expected = purchases.Where(p => p.AccountId == accId);
        //    var result = await controller.GetPurchaseAccount(accId);

        //    // Assert
        //    Assert.IsTrue(result.SequenceEqual(expected));
        //}

        //[TestMethod]
        //public async Task GetPurchaseAccountTest_NotExists()
        //{
        //    // Arrange
        //    var purchases = new List<Purchase>
        //    {
        //        new Purchase { Id = 1, AccountId = 1, AddressId = 1, OrderStatus = OrderStatus.Completed, ProductId = 1, Qty = 2},
        //        new Purchase { Id = 2, AccountId = 1, AddressId = 1, OrderStatus = OrderStatus.Completed, ProductId = 2, Qty = 7},
        //        new Purchase { Id = 1, AccountId = 1, AddressId = 1, OrderStatus = OrderStatus.Completed, ProductId = 1, Qty = 2},
        //        new Purchase { Id = 1, AccountId = 2, AddressId = 1, OrderStatus = OrderStatus.Completed, ProductId = 1, Qty = 2}
        //    };
        //    var repo = new FakePurchaseRepo(purchases);
        //    var factory = new Mock<IHttpClientFactory>();
        //    var controller = new PurchasesApiController(repo, new NullLogger<PurchasesApiController>(), factory.Object, null);
        //    var accId = 42;

        //    // Act
        //    var result = await controller.GetPurchaseAccount(accId);

        //    // Assert
        //    Assert.IsNull(result);
        //}
    }
}