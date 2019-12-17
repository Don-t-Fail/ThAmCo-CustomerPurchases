using CustomerPurchases.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerPurchases.Data
{
    public interface IPurchaseRepo
    {
        Task<List<Purchase>> GetAll();
        Task<Purchase> GetPurchase(int id);
        Task<List<Purchase>> GetPurchaseByAccount(int accId);
        void InsertPurchase(Purchase purchase);
        void DeletePurchase(int id);
        void UpdatePurchase(Purchase purchase);
        Task Save();
    }
}
