using DAL.Repository;
using DAL.UnitOfWork;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ItemManager : ItemManagerService
    {
        //ItemRepositoryService repo;
        IUnitOfWork repo;
        public ItemManager(IUnitOfWork uow)
        {
            repo = uow;
            //repo = new ItemRepository(database,username);
        }

        public Item AddItem(Item item)
        {
            return repo.Items.CreateItem(item);
        }

        public void AddItemAndWorkplace(Item item, Workplace workplace)
        {
            repo.Items.CreateItem(item);
            repo.Workplaces.CreateWorkplace(workplace);
            repo.SaveChanges();
        }

        public IEnumerable<Debit> BulkAddDebitItems(List<Debit> debitList)
        {
            var list = repo.Items.BulkAddDebitItems(debitList);
            repo.SaveChanges();
            return list;
        }

        public IEnumerable<Deposit> BulkAddDepositItems(List<Deposit> depositList)
        {
            var list = repo.Items.BulkAddDepositItems(depositList);
            repo.SaveChanges();
            return list;
        }

        public Item ChangeItem(Item item)
        {
            return repo.Items.UpdateItem(item);
        }

        public Item GetItem(int id)
        {
            return repo.Items.ReadItem(id);
        }

        public Item GetItem(string id)
        {
            return repo.Items.ReadItem(id);
        }

        public IEnumerable<Item> GetItems()
        {
            return repo.Items.ReadItems();
        }

    }
}
