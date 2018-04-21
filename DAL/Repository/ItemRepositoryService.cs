using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface ItemRepositoryService
    {
        IEnumerable<Item> ReadItems();
        Item ReadItem(string id);
        Item ReadItem(int id);
        Item CreateItem(Item item);
        Item UpdateItem(Item item);
        IEnumerable<Debit> BulkAddDebitItems(List<Debit> debitList);
        IEnumerable<Deposit> BulkAddDepositItems(List<Deposit> depositList);
    }
}
