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
        IEnumerable<Item> GetItems(string selector = null);
        Item GetItem(string id);
        Item GetItem(int id);
        Item AddItem(Item item);
        Item ChangeItem(Item item);

        IEnumerable<Debit> BulkAddDebitItems(List<Debit> debitList);
        void AddItemAndWorkplace(Item item, Workplace workplace);
        IEnumerable<Deposit> BulkAddDepositItems(List<Deposit> depositList);
        IEnumerable<Bobbin> GetBobbins();
        IEnumerable<CableType> GetCableTypes();
        void AddBobbin(Bobbin bobbin);
        Bobbin GetBobbin(int id);
        void AddBobbinDebit(BobbinDebit bobbinDebit);
        void ChangeBobbin(Bobbin bobbin);
        Bobbin RemoveBobbinDebit(int id);
        void RemoveBobbin(int id);
        Debit GetDebit(int id);
        IEnumerable<Debit> GetDebits();
        void RemoveDebit(Debit debit);
        IEnumerable<Deposit> GetDeposits();
        void RemoveItem(int id);
        Bobbin GetLatestBobbin();
        IEnumerable<Bobbin> GetBobbinsPerInfra(int id);
        IEnumerable<Bobbin> GetBobbinsReturned(bool isReturned);
        //Staff GetStaffWithApplicationUserId(string id);
    }
}
