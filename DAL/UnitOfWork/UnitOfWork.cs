using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ItemDbContext db;

        public UnitOfWork(string username, string database)
        {
            db = new ItemDbContext(database,username);
        }

        private ItemRepositoryService _Items;
        public ItemRepositoryService Items
        {
            get
            {
                if (this._Items == null)
                {
                    this._Items = new ItemRepository(db);
                }
                return this._Items;
            }
        }

        private WorkplaceRepositoryService _Workplaces;
        public WorkplaceRepositoryService Workplaces
        {
            get
            {
                if (this._Workplaces == null)
                {
                    this._Workplaces = new WorkplaceRepository(db);
                }
                return this._Workplaces;
            }
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
