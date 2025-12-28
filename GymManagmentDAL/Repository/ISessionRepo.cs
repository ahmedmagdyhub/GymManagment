using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository
{
    public  interface ISessionRepo : IGenericRepo<Session>
    {
        IEnumerable<Session> GetAllSessionWithTrainerAndCategory();

        int GetCountOfBookedSlot(int sessionid);

        Session? GetSessionWithTrainerAndCategory(int sessionId);
    }
}
