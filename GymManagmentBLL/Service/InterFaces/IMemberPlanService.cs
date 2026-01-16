using GymManagmentBLL.ViewModels.MemberPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.InterFaces
{
    public interface IMemberPlanService
    {
        IEnumerable<MemberPlanViewModel> GetAll();

        CreateMemberPlanViewModel GetCreateFormData();

        bool Create(CreateMemberPlanViewModel model);

        bool Cancel(int memberPlanId );
    }
}
