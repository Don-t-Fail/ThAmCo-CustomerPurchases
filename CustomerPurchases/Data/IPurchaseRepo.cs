using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerPurchases.Models;

namespace CustomerPurchases.Data
{
    interface IPurchaseRepo
    {
        Task<IEnumerable<Purchase>> GetAll();
        Task<Purchase> GetPurchase(int id);
        Task<IEnumerable<Purchase>> GetPurchaseByAccount(int accId);
        void InsertPurchase(Purchase purchase);
        void DeletePurchase(int id);
        void UpdatePurchase(Purchase purchase);
        Task Save();
    }
}
