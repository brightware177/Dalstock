namespace DAL.Migrations
{
    using Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.ItemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.ItemDbContext context)
        {
            City city = new City()
            {
                Zipcode = "9990",
                Name = "test",
                Up = "TEST",
                structure = "9999 Test"
            };
            Item item = new Item()
            {
                ItemId = "200017",
                Description = "BOCHT PE 30� DN160 SDR 17,6",
                Amount = 15,
                Image = "www.test.be"
            };
            Item item2 = new Item()
            {
                ItemId = "200023",
                Description = "BUIS PE DN63 SDR 11 L 6M",
                Amount = 10,
                Image = "www.test.be"
            };
            Item item3 = new Item()
            {
                ItemId = "55",
                Description = "KABELSCHOEN HS SCHR AL:50-150 CU:50-120",
                Amount = 5,
                Image = "www.test.be"
            };
            Workplace workplace = new Workplace()
            {
                WorkplaceId = "20221766",
                Address = "STATIONSSTRAAT",
                City = city
            };
            Workplace workplace2 = new Workplace()
            {
                WorkplaceId = "20223080",
                Address = "Lokerenstraat",
                City = city
            };
            Staff staff = new Staff()
            {
                Firstname = "Bilal",
                Lastname = "Yildirim",
                ApplicationUserId = "9a9733fb-2aef-4e26-a5de-446c7ebdd7b2",
            };
            Debit debit = new Debit()
            {
                Item = item,
                Amount = 5,
                Approved_By_Staff_Id = staff.StaffId,
                Debited_By_Staff_Id = staff.StaffId,
                Date = DateTime.Now,
                Workplace = workplace,
                DebitState = DebitState.Pending
            };
            Debit debit2 = new Debit()
            {
                Item = item2,
                Amount = 5,
                Approved_By_Staff_Id = staff.StaffId,
                Debited_By_Staff_Id = staff.StaffId,
                Date = DateTime.Now,
                Workplace = workplace,
                DebitState = DebitState.Approved
            };
            Debit debit3 = new Debit()
            {
                Item = item3,
                Amount = 5,
                Approved_By_Staff_Id = staff.StaffId,
                Debited_By_Staff_Id = staff.StaffId,
                Date = DateTime.Now,
                Workplace = workplace2,
                DebitState = DebitState.Approved
            };
            context.Items.Add(item);
            context.Items.Add(item2);
            context.Items.Add(item3);
            context.Workplaces.Add(workplace);
            context.Workplaces.Add(workplace2);
            context.Staff.Add(staff);
            context.Debits.Add(debit);
            context.Debits.Add(debit2);
            context.Debits.Add(debit3);
            context.SaveChanges();
        }
    }
}
