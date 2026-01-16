using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository
{
    public class MembershipRepository : GenericRepo<MemberShip>, IMembershipRepository
    {
        private readonly GymManagmentDbContext _dbContext;

        public MembershipRepository(GymManagmentDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<MemberShip> GetAllMembershipsWithMemberAndPlan(Func<MemberShip, bool>? filter = null)
        {
            return _dbContext.MemberShips.Include(m => m.Member).Include(m => m.Plan)
                .Where(filter ?? (_ => true));
        }

        public MemberShip? GetFirstOrDefault(Func<MemberShip, bool>? filter = null)
        {
            return _dbContext.MemberShips.FirstOrDefault(filter ?? (_ => true));
        }
    }
}
