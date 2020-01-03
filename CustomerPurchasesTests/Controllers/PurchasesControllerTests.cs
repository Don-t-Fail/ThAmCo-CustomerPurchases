using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CustomerPurchases.Controllers;
using CustomerPurchases.Data;
using CustomerPurchases.Data.Products;
using CustomerPurchases.Data.Purchases;
using CustomerPurchases.Models;
using CustomerPurchases.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

namespace CustomerPurchasesTests.Controllers
{
    [TestClass]
    public class PurchasesControllerTests
    {
        private static class TestData
        {
            public static List<Purchase> Purchases() => new List<Purchase>
            {
                new Purchase { Id = 1, AccountId = 1, AddressId = 1, OrderStatus = OrderStatus.Completed, ProductId = 1, Qty = 2},
                new Purchase { Id = 2, AccountId = 1, AddressId = 1, OrderStatus = OrderStatus.Completed, ProductId = 2, Qty = 7},
                new Purchase { Id = 3, AccountId = 1, AddressId = 1, OrderStatus = OrderStatus.Completed, ProductId = 1, Qty = 2},
                new Purchase { Id = 4, AccountId = 2, AddressId = 1, OrderStatus = OrderStatus.Shipped, ProductId = 1, Qty = 2}
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
            using (var controller = new PurchasesController(repo, productRepo, null, null))
            {
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
        }

        [TestMethod]
        public async Task GetPurchaseDetailsTest_NotExists()
        {
            // Arrange
            var repo = new FakePurchaseRepo(TestData.Purchases());
            var productRepo = new FakeProductService(TestData.Products());
            using (var controller = new PurchasesController(repo, productRepo, null, null))
            {
                var purchaseId = 7000;

                //Act
                var result = await controller.Details(purchaseId);

                // Assert
                Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public async Task GetPurchaseDetailsTest_OutOfBoundsID()
        {
            // Arrange
            var repo = new FakePurchaseRepo(TestData.Purchases());
            var productRepo = new FakeProductService(TestData.Products());
            using (var controller = new PurchasesController(repo, productRepo, null, null))
            {
                var purchaseId = -6;

                //Act
                var result = await controller.Details(purchaseId);

                // Assert
                Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public async Task DeletePurchase_Success()
        {
            // Arrange
            var repo = new FakePurchaseRepo(TestData.Purchases());
            var productRepo = new FakeProductService(TestData.Products());
            using (var controller = new PurchasesController(repo, productRepo, null, null))
            {
                var purchaseId = 1;

                // Act
                await controller.DeleteConfirmed(purchaseId);

                // Assert
                var purchase = await controller.Details(purchaseId);
                Assert.IsInstanceOfType(purchase.Result, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public async Task DeletePurchase_OutOfBounds()
        {
            // Arrange
            var repo = new FakePurchaseRepo(TestData.Purchases());
            var productRepo = new FakeProductService(TestData.Products());
            using (var controller = new PurchasesController(repo, productRepo, null, null))
            {
                var purchaseId = -6;

                // Act
                var result = await controller.DeleteConfirmed(purchaseId);

                // Assert
                Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            }
        }

        [TestMethod]
        public async Task GetOrderHistoryTest_Success()
        {
            // Arrange
            var repo = new FakePurchaseRepo(TestData.Purchases());
            var productRepo = new FakeProductService(TestData.Products());
            using (var controller = new PurchasesController(repo, productRepo, null, null))
            {
                var accId = 1;

                // Act
                var result = await controller.OrderHistory(accId);

                // Assert
                Assert.IsNotNull(result);
                var objResult = result.Result as OkObjectResult;
                Assert.IsNotNull(objResult);
                var retResult = objResult.Value as List<Purchase>;
                Assert.IsNotNull(retResult);
                //foreach (Purchase purchase in retResult)
                //{
                //    Assert.AreEqual(await repo.GetPurchase(purchase.Id), purchase);
                //}
            }
        }

        [TestMethod]
        public async Task GetOrderHistory_NotExists()
        {
            // Arrange
            var repo = new FakePurchaseRepo(TestData.Purchases());
            var productRepo = new FakeProductService(TestData.Products());
            using (var controller = new PurchasesController(repo, productRepo, null, null))
            {
                var accId = 1000;

                // Act
                var result = await controller.OrderHistory(accId);

                // Assert
                Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public async Task GetOrderHistory_OutOfBounds()
        {
            // Arrange
            var repo = new FakePurchaseRepo(TestData.Purchases());
            var productRepo = new FakeProductService(TestData.Products());
            using (var controller = new PurchasesController(repo, productRepo, null, null))
            {
                var accId = -6;

                // Act
                var result = await controller.OrderHistory(accId);

                // Assert
                Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
            }
        }

        [TestMethod]
        public async Task GetAll_Success()
        {
            // Arrange
            var repo = new FakePurchaseRepo(TestData.Purchases());
            var productRepo = new FakeProductService(TestData.Products());
            using (var controller = new PurchasesController(repo, productRepo, null, null))
            {
                // Act
                var result = await controller.GetAll();

                // Assert
                Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
                var objResult = result.Result as OkObjectResult;
                Assert.IsNotNull(objResult);
            }
        }
    }
}