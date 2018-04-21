using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Data.Entity;

namespace DAL.Repository
{
    public class WorkplaceRepository : WorkplaceRepositoryService
    {
        private ItemDbContext ctx;

        public WorkplaceRepository(ItemDbContext context)
        {
            ctx = context;
        }
        public Workplace ReadWorkplace(string id)
        {
            return ctx.Workplaces.Single(x => x.WorkplaceId.Equals(id));
        }

        public IEnumerable<Workplace> ReadWorkplaces()
        {
            return ctx.Workplaces;
        }
        public Workplace CreateWorkplace(Workplace workplace)
        {
            var newItem = ctx.Workplaces.Add(workplace);
            
            return newItem;
        }

        public Staff ReadStaff(string applicationUserId)
        {
            return ctx.Staff.Single(x => x.ApplicationUserId.Equals(applicationUserId));
        }

        public IEnumerable<City> ReadCities()
        {
            return ctx.Cities;
        }

        public Workplace UpdateWorkplace(Workplace model)
        {
            ctx.Workplaces.Attach(model);
            ctx.Entry(model).State = EntityState.Modified;
            ctx.SaveChanges();
            return model;
        }

        public void DeleteWorkplace(Workplace workplace)
        {
            ctx.Workplaces.Remove(workplace);
            ctx.SaveChanges();
        }

        public Workplace ReadWorkplace(int id)
        {
            return ctx.Workplaces.Single(x => x.Id == id);
        }
    }
}
