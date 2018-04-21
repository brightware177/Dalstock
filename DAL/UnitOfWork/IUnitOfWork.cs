using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ItemRepositoryService Items { get; }
        WorkplaceRepositoryService Workplaces { get; }

        int SaveChanges();
    }
}
