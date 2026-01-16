using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels.MemberPlan
{
    public class CreateMemberPlanViewModel
    {
        [Required(ErrorMessage = "Member is required")]
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Plan is required")]
        public int PlanId { get; set; }

        public IEnumerable<SelectListItem>? Members { get; set; }

        public IEnumerable<SelectListItem>? Plans { get; set; }
    }
}
