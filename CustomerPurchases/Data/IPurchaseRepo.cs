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
        void InsertPurchase(Purchase purchase);
        void DeletePurchase(int id);
        void UpdatePurchase(Purchase purchase);
        Task Save();
    }
}
