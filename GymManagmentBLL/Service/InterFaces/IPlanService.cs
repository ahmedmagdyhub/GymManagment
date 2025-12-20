using GymManagmentBLL.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.InterFaces
{
    internal interface IPlanService
    {
        IEnumerable<PlanViewModel> GetAllPlan();
        PlanViewModel? GetPlanById(int planId);
        UpdatePlanViewModel? GetPlanToUpdate(int planid);
        bool UpdatePlan(int planid, UpdatePlanViewModel PlanToUpdate);
        bool Tooglestation(int planid);

    }
}
