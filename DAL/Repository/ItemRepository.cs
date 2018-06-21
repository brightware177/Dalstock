﻿using Domain;
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
            return ctx.Bobbins.Include("CableType");
        }

        public IEnumerable<CableType> ReadCableTypes()
        {
            return ctx.CableTypes;
        }

        public Debit ReadDebit(int id)
        {
            return ctx.Debits.Find(id);
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
