using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels.MemberPlan
{
    public  class MemberPlanViewModel
    {
        
        
            public int Id { get; set; }

            public string MemberName { get; set; } = null!;

            public string PlanName { get; set; } = null!;

            public DateTime StartDate { get; set; }

            public DateTime EndDate { get; set; }

            public string Status { get; set; } = null!;
        

    }
}
