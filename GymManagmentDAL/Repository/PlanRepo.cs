using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository
{
    public  class PlanRepo : IPlanRepo
    {
        private readonly GymManagmentDbContext _dbContext;

        public PlanRepo(GymManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Plan> GetAll() => _dbContext.Plans.ToList();

        public Plan? GetById(int id) => _dbContext.Plans.Find();


        public int Update(Plan plan)
        {
            _dbContext.Plans.Update(plan);
            return _dbContext.SaveChanges();
        }

    }
}
