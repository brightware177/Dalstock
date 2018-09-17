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

        public void AddBobbin(Bobbin bobbin)
        {
            repo.Items.CreateBobbin(bobbin);
        }

        public void AddBobbinDebit(BobbinDebit bobbinDebit)
        {
            var bobbin = repo.Items.ReadBobbin(bobbinDebit.BobbinId);
            bobbin.AmountRemains = bobbin.AmountRemains - bobbinDebit.AmountUsed;
            repo.Items.CreateBobbinDebit(bobbinDebit);
            repo.Items.UpdateBobbin(bobbin);
            repo.SaveChanges();
        }

        public Item AddItem(Item item)
        {
            var newItem = repo.Items.CreateItem(item);
            repo.SaveChanges();
            return newItem;
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

        public void ChangeBobbin(Bobbin bobbin)
        {
            repo.Items.UpdateBobbin(bobbin);
            repo.SaveChanges();
        }

        public Item ChangeItem(Item item)
        {
            return repo.Items.UpdateItem(item);
        }

        public Bobbin GetBobbin(int id)
        {
            return repo.Items.ReadBobbin(id);
        }

        public IEnumerable<Bobbin> GetBobbins()
        {
            return repo.Items.ReadBobbins();
        }

        public IEnumerable<CableType> GetCableTypes()
        {
            return repo.Items.ReadCableTypes();
        }

        public Debit GetDebit(int id)
        {
            return repo.Items.ReadDebit(id);
        }

        public IEnumerable<Debit> GetDebits()
        {
            return repo.Items.ReadDebits();
        }

        public IEnumerable<Deposit> GetDeposits()
        {
            return repo.Items.ReadDeposits();
        }

        public Item GetItem(int id)
        {
            return repo.Items.ReadItem(id);
        }

        public Item GetItem(string id)
        {
            return repo.Items.ReadItem(id);
        }

        public IEnumerable<Item> GetItems(string selector = null)
        {
            return repo.Items.ReadItems(selector);
        }

        public Bobbin GetLatestBobbin()
        {
            return repo.Items.ReadLatestBobbin();
        }

        public void RemoveBobbin(int id)
        {
            repo.Items.DeleteBobbin(id);
        }

        public Bobbin RemoveBobbinDebit(int id)
        {
            var bobbinDebit = repo.Items.ReadBobbinDebit(id);
            var bobbin = repo.Items.ReadBobbin(bobbinDebit.BobbinId);
            bobbin.AmountRemains += bobbinDebit.AmountUsed;
            repo.Items.DeleteBobbinDebit(id);
            repo.SaveChanges();
            return bobbin;
        }

        public void RemoveDebit(Debit debit)
        {
            var item = repo.Items.ReadItem(debit.ItemId);
            item.Amount += debit.Amount;
            repo.Items.UpdateItem(item);
            repo.Items.DeleteDebit(debit.DebitId);
            repo.SaveChanges();
        }

        public void RemoveItem(int id)
        {
            repo.Items.DeleteItem(id);
        }
    }
}
