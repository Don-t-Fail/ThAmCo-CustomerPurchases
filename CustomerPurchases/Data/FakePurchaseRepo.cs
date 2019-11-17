using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerPurchases.Models;

namespace CustomerPurchases.Data
{
    public class FakePurchaseRepo : IPurchaseRepo
    {

        private IEnumerable<Purchase> _purchases;

        public FakePurchaseRepo(IEnumerable<Purchase> purchases)
        {
            _purchases = purchases;
        }

        public void DeletePurchase(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Purchase>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Purchase> GetPurchase(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Purchase>> GetPurchaseByAccount(int accId)
        {
            throw new NotImplementedException();
        }

        public void InsertPurchase(Purchase purchase)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public void UpdatePurchase(Purchase purchase)
        {
            throw new NotImplementedException();
        }
    }
}
