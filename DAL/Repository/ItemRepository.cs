using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ItemRepository : ItemRepositoryService
    {
        private ItemDbContext ctx;

        public ItemRepository(ItemDbContext context)
        {
            ctx = context;
        }
        public IEnumerable<Debit> BulkAddDebitItems(List<Debit> debitList)
        {
            foreach (var debit in debitList)
            {
                ctx.Debits.Add(debit);
                ctx.Items.Find(debit.ItemId).Amount -= debit.Amount;
            }
            return debitList;
        }

        public IEnumerable<Deposit> BulkAddDepositItems(List<Deposit> depositList)
        {
            foreach (var deposit in depositList)
            {
                
                Item d = ctx.Items.Find(deposit.Item.Id);
                d.Amount += deposit.Amount;
                deposit.Item = null;
                ctx.Deposits.Add(deposit);
            }
            return depositList;
        }

        public void CreateBobbin(Bobbin bobbin)
        {
            ctx.Bobbins.Add(bobbin);
            ctx.SaveChanges();
        }

        public void CreateBobbinDebit(BobbinDebit bobbinDebit)
        {
            ctx.BobbinDebits.Add(bobbinDebit);
        }

        public Item CreateItem(Item item)
        {
            var newItem = ctx.Items.Add(item);
            return newItem;
        }

        public void DeleteBobbin(int id)
        {
            Bobbin bobbin = ctx.Bobbins.Find(id);

            ctx.Bobbins.Attach(bobbin);
            ctx.Bobbins.Remove(bobbin);
            ctx.SaveChanges();
        }

        public void DeleteBobbinDebit(int id)
        {
            BobbinDebit bd = ctx.BobbinDebits.Find(id);

            ctx.BobbinDebits.Attach(bd);
            ctx.BobbinDebits.Remove(bd);
        }

        public void DeleteDebit(int id)
        {
            Debit Debit = ctx.Debits.Find(id);

            ctx.Debits.Attach(Debit);
            ctx.Debits.Remove(Debit);
            ctx.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            Item item = ctx.Items.Find(id);

            ctx.Items.Attach(item);
            ctx.Items.Remove(item);
            ctx.SaveChanges();
        }

        public Bobbin ReadBobbin(int id)
        {
            return ctx.Bobbins.Include("BobbinDebits").Include("CableType").Single(x=>x.Id == id);
        }

        public BobbinDebit ReadBobbinDebit(int id)
        {
            return ctx.BobbinDebits.Find(id);
        }

        public IEnumerable<Bobbin> ReadBobbins()
        {
            return ctx.Bobbins.Include("CableType").Include("Infrastructure");
        }

        public IEnumerable<Bobbin> ReadBobbinsPerInfra(int id)
        {
            return ctx.Bobbins.Include(y=>y.Infrastructure).Include("CableType").Where(x => x.InfrastructureId == id);
        }

        public IEnumerable<Bobbin> ReadBobbinsReturned(bool isReturned)
        {
            return ctx.Bobbins.Include(y => y.Infrastructure).Include("CableType").Where(x => x.IsReturned == isReturned);
        }

        public IEnumerable<CableType> ReadCableTypes()
        {
            return ctx.CableTypes;
        }

        public Debit ReadDebit(int id)
        {
            return ctx.Debits.Find(id);
        }

        public IEnumerable<Debit> ReadDebits()
        {
            return ctx.Debits;
        }

        public IEnumerable<Deposit> ReadDeposits()
        {
            return ctx.Deposits;
        }

        public Item ReadItem(int id)
        {
            return ctx.Items.Single(x => x.Id == id);
        }

        public Item ReadItem(string id)
        {
            try
            {
                var item = ctx.Items.Single(x => x.ItemId.Equals(id));
                return item;
            }
            catch (Exception)
            {
                throw new KeyNotFoundException();
            }
        }

        public IEnumerable<Item> ReadItems(string selector)
        {
            if (selector != null)
                return ctx.Items.Where(x => 20 > x.Amount);
            return ctx.Items;
        }

        public Bobbin ReadLatestBobbin()
        {
            return ctx.Bobbins.Last();
        }

        public void UpdateBobbin(Bobbin bobbin)
        {
            ctx.Bobbins.Attach(bobbin);
            ctx.Entry(bobbin).State = EntityState.Modified;
        }

        public Item UpdateItem(Item item)
        {
            ctx.Items.Attach(item);
            ctx.Entry(item).State = EntityState.Modified;
            ctx.SaveChanges();
            return item;
        }
    }
}
