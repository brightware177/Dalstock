using Domain;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ItemDbContext : DbContext
    {
        public ItemDbContext() : base("server=mysql208.hosting.combell.com;port=3306;database=ID80395_dalcom;uid=ID80395_dalcom;password=971458Rag")
        {
            Database.SetInitializer(new MysqlItemsInitializer());
        }

        public ItemDbContext(string database, string username) : base(GetSqlConn4DbName(database, username))
        {
            Database.SetInitializer(new MysqlItemsInitializer());

        }
        public static String GetSqlConn4DbName(string dbName, string username)
        {
            var connString =
            ConfigurationManager.ConnectionStrings["Dalcom"].ConnectionString.ToString();

            return String.Format(connString, dbName, username);
        }
        public DbSet<Workplace> Workplaces { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Debit> Debits { get; set; }
        public DbSet<Bobbin> Bobbins { get; set; }
        public DbSet<BobbinDebit> BobbinDebits { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CableType> CableTypes { get; set; }
        public DbSet<Infrastructure> Infrastructures { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new Debit.DebitMapping());
        }
    }
}
