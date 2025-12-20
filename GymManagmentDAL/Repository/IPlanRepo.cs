using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository
{
     public  interface IPlanRepo
    {
        IEnumerable<Plan> GetAll();
        Plan? GetById(int id);
        int Update(Plan plan);

    }
}
