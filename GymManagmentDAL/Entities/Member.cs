using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    public class Member : GymUser
    {
        //Join date is CreateAt

        public  string?  Photo { get; set; }
        public HealthRecord HealthRecord { get; set; } = null!;

        public ICollection<MemberSession> MemberSessions = null!;

        public ICollection<MemberShip> MemberPlan = null!;

    }
}
