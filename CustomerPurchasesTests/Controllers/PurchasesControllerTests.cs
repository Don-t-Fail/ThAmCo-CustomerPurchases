﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerPurchases.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CustomerPurchases.Data;
using CustomerPurchases.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using Microsoft.CodeAnalysis.CSharp;
using Moq;
using Moq.Protected;

namespace CustomerPurchases.Controllers.Tests
{
    [TestClass]
    public class PurchasesControllerTests
    {
        [TestMethod]
        public async Task GetPurchaseTest()
        {
            // Arrange
            var purchases = new List<Purchase>
            {
                new Purchase { Id = 1, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 1, Qty = 2},
                new Purchase { Id = 2, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 2, Qty = 7}
            };
            var repo = new FakePurchaseRepo(purchases);
            var factory = new Mock<IHttpClientFactory>();
            var controller = new PurchasesController(repo, new NullLogger<PurchasesController>(), factory.Object, null);
            var purchaseId = 1;

            // Act
            var result = await controller.GetPurchase(purchaseId);

            // Assert
            Assert.AreEqual(purchases.FirstOrDefault(p => p.Id == purchaseId), result.Value);
        }

        [TestMethod]
        public async Task GetPurchaseTest_NotExists()
        {
            // Arrange
            var purchases = new List<Purchase>
            {
                new Purchase { Id = 1, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 1, Qty = 2},
                new Purchase { Id = 2, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 2, Qty = 7}
            };
            var repo = new FakePurchaseRepo(purchases);
            var factory = new Mock<IHttpClientFactory>();
            var controller = new PurchasesController(repo, new NullLogger<PurchasesController>(), factory.Object, null);
            var purchaseId = 3;

            // Act
            var result = await controller.GetPurchase(purchaseId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutPurchaseTest()
        {
            // Arrange
            var purchases = new List<Purchase>
            {
                new Purchase { Id = 1, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 1, Qty = 2},
                new Purchase { Id = 2, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 2, Qty = 7}
            };
            var repo = new FakePurchaseRepo(purchases);
            var factory = new Mock<IHttpClientFactory>();
            var controller = new PurchasesController(repo, new NullLogger<PurchasesController>(), factory.Object, null);
            var newPurchase = new Purchase
            {
                Id = 3, AccountId = 1, AddressId = 1, ProductId = 2, Qty = 5, OrderStatus = "In Progress"
            };

            // Act
            var result = await controller.PutPurchase(newPurchase.Id, newPurchase);

            // Assert
            Assert.AreEqual(purchases.FirstOrDefault(p => p.Id == newPurchase.Id), newPurchase);
        }

        [TestMethod]
        public async Task PutPurchaseTest_AlreadyExists()
        {
            // Arrange
            var purchases = new List<Purchase>
            {
                new Purchase { Id = 1, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 1, Qty = 2},
                new Purchase { Id = 2, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 2, Qty = 7}
            };
            var repo = new FakePurchaseRepo(purchases);
            var factory = new Mock<IHttpClientFactory>();
            var controller = new PurchasesController(repo, new NullLogger<PurchasesController>(), factory.Object, null);
            var newPurchase = new Purchase
            {
                Id = 1,
                AccountId = 1,
                AddressId = 1,
                ProductId = 2,
                Qty = 5,
                OrderStatus = "In Progress"
            };

            // Act
            var result = await controller.PutPurchase(newPurchase.Id, newPurchase);
            
            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutPurchaseTest_IdNoMatch()
        {
            // Arrange
            var purchases = new List<Purchase>
            {
                new Purchase { Id = 1, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 1, Qty = 2},
                new Purchase { Id = 2, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 2, Qty = 7}
            };
            var repo = new FakePurchaseRepo(purchases);
            var factory = new Mock<IHttpClientFactory>();
            var controller = new PurchasesController(repo, new NullLogger<PurchasesController>(), factory.Object, null);
            var newPurchase = new Purchase
            {
                AccountId = 1,
                AddressId = 1,
                ProductId = 2,
                Qty = 5,
                OrderStatus = "In Progress"
            };

            // Act
            var result = await controller.PutPurchase(6, newPurchase);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        // TODO - Mocks with working factories & clients
        //[TestMethod]
        //public void PostPurchaseTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void PostPurchaseTest_Exists()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void PostPurchaseTest_InvalidPurchase()
        //{
        //    Assert.Fail();
        //}

        [TestMethod]
        public async Task DeletePurchaseTest()
        {
            // Arrange
            var purchases = new List<Purchase>
            {
                new Purchase { Id = 1, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 1, Qty = 2},
                new Purchase { Id = 2, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 2, Qty = 7}
            };
            var repo = new FakePurchaseRepo(purchases);
            var factory = new Mock<IHttpClientFactory>();
            var controller = new PurchasesController(repo, new NullLogger<PurchasesController>(), factory.Object, null);
            var purchaseId = 1;

            // Act
            var result = await controller.DeletePurchase(purchaseId);

            // Assert
            Assert.IsNull(purchases.Find(p => p.Id == purchaseId));
        }

        [TestMethod]
        public async Task DeletePurchaseTest_NotExists()
        {
            // Arrange
            var purchases = new List<Purchase>
            {
                new Purchase { Id = 1, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 1, Qty = 2},
                new Purchase { Id = 2, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 2, Qty = 7}
            };
            var repo = new FakePurchaseRepo(purchases);
            var factory = new Mock<IHttpClientFactory>();
            var controller = new PurchasesController(repo, new NullLogger<PurchasesController>(), factory.Object, null);
            var purchaseId = 3;

            // Act
            var result = await controller.DeletePurchase(purchaseId);

            // Assert
            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public async Task GetPurchaseAccountTest()
        {
            // Arrange
            var purchases = new List<Purchase>
            {
                new Purchase { Id = 1, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 1, Qty = 2},
                new Purchase { Id = 2, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 2, Qty = 7},
                new Purchase { Id = 1, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 1, Qty = 2},
                new Purchase { Id = 1, AccountId = 2, AddressId = 1, OrderStatus = "Complete", ProductId = 1, Qty = 2}
            };
            var repo = new FakePurchaseRepo(purchases);
            var factory = new Mock<IHttpClientFactory>();
            var controller = new PurchasesController(repo, new NullLogger<PurchasesController>(), factory.Object, null);
            var accId = 1;

            // Act
            var expected = purchases.Where(p => p.AccountId == accId);
            var result = await controller.GetPurchaseAccount(accId);

            // Assert
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [TestMethod]
        public async Task GetPurchaseAccountTest_NotExists()
        {
            // Arrange
            var purchases = new List<Purchase>
            {
                new Purchase { Id = 1, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 1, Qty = 2},
                new Purchase { Id = 2, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 2, Qty = 7},
                new Purchase { Id = 1, AccountId = 1, AddressId = 1, OrderStatus = "Complete", ProductId = 1, Qty = 2},
                new Purchase { Id = 1, AccountId = 2, AddressId = 1, OrderStatus = "Complete", ProductId = 1, Qty = 2}
            };
            var repo = new FakePurchaseRepo(purchases);
            var factory = new Mock<IHttpClientFactory>();
            var controller = new PurchasesController(repo, new NullLogger<PurchasesController>(), factory.Object, null);
            var accId = 42;

            // Act
            var result = await controller.GetPurchaseAccount(accId);

            // Assert
            Assert.IsNull(result);
        }
    }
}