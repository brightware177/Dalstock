using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface ItemManagerService
    {
        IEnumerable<Item> GetItems();
        Item GetItem(string id);
        Item GetItem(int id);
        Item AddItem(Item item);
        Item ChangeItem(Item item);
        
        IEnumerable<Debit> BulkAddDebitItems(List<Debit> debitList);
        void AddItemAndWorkplace(Item item, Workplace workplace);
        IEnumerable<Deposit> BulkAddDepositItems(List<Deposit> depositList);
        //Staff GetStaffWithApplicationUserId(string id);
    }
}
