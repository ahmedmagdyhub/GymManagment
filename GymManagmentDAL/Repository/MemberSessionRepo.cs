using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository
{
    public class MemberSessionRepo : GenericRepo<MemberSession>  ,IMemberSessionRepo
    {
        private readonly GymManagmentDbContext _dbContext;

        public MemberSessionRepo(GymManagmentDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public IEnumerable<MemberSession> GetSessionById(int sessionId)
        {
            return _dbContext.MemberSessions.Where(b => b.SessionId == sessionId)
                .Include(b => b.Member)
                .ToList();
        }
    }

}
