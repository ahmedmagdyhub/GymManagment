using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository
{
    public interface IMembershipRepository
    {
        IEnumerable<MemberShip> GetAllMembershipsWithMemberAndPlan(Func<MemberShip, bool>? filter = null);
        MemberShip? GetFirstOrDefault(Func<MemberShip, bool>? filter = null);

    }
}
