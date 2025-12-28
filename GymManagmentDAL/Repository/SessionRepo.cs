using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository
{
    public  class SessionRepo : GenericRepo<Session>,ISessionRepo
    {
        private readonly GymManagmentDbContext _dbContext;

        public SessionRepo(GymManagmentDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Session> GetAllSessionWithTrainerAndCategory()
        {
            return _dbContext.Sessions.Include(x => x.SessionTrainer).Include(X => X.SessionCategory).ToList();
        }

        public int GetCountOfBookedSlot(int sessionid)
        {
            return _dbContext.MemberSessions.Where(x => x.SessionId == sessionid).Count();
        }

        public Session? GetSessionWithTrainerAndCategory(int sessionId)
        {
            return _dbContext.Sessions.Include(x => x.SessionTrainer)
                .Include(X => X.SessionCategory).FirstOrDefault(X => X.Id == sessionId);
        }
    }
}
