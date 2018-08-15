using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface WorkplaceRepositoryService
    {
        IEnumerable<Workplace> ReadWorkplaces();
        Workplace ReadWorkplace(string id);
        Workplace ReadWorkplace(int id);
        Workplace CreateWorkplace(Workplace workplace);
        Staff ReadStaff(string applicationUserId);
        void CreateStaff(Staff staff);
        IEnumerable<City> ReadCities();
        Workplace UpdateWorkplace(Workplace model);
        void DeleteWorkplace(Workplace workplace);
        IEnumerable<Infrastructure> ReadInfrastructures();
    }
}
