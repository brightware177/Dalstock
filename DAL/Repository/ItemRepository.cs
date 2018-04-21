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
                ctx.Deposits.Add(deposit);
                ctx.Items.Find(deposit.ItemId).Amount += deposit.Amount;
            }
            return depositList;
        }

        public Item CreateItem(Item item)
        {
            var newItem = ctx.Items.Add(item);
            return newItem;
        }

        public Item ReadItem(int id)
        {
            return ctx.Items.Single(x => x.Id == id);
        }

        public Item ReadItem(string id)
        {
            return ctx.Items.Single(x => x.ItemId.Equals(id));
        }

        public IEnumerable<Item> ReadItems()
        {
            return ctx.Items;
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
