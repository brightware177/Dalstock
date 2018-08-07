using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public interface WorkplaceManagerService
    {
        IEnumerable<Workplace> GetWorkplaces();
        Workplace GetWorkplace(string id);
        Workplace GetWorkplace(int id);
        Workplace AddWorkplace(Workplace workplace);
        Staff GetStaffWithApplicationUserId(string ApplicationUserId);
        IEnumerable<City> GetCities();
        Workplace ChangeWorkplace(Workplace model);
        void RemoveWorkplace(Workplace workplace);
        IEnumerable<Infrastructure> GetInfrastructures();
    }
}
