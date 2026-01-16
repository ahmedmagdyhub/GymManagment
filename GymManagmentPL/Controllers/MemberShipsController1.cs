using GymManagmentBLL.Service.InterFaces;
using GymManagmentBLL.ViewModels.MemberPlan;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    public class MembershipController : Controller
    {
        private readonly IMemberPlanService _memberPlanService;

        public MembershipController(IMemberPlanService memberPlanService)
        {
            _memberPlanService = memberPlanService;
        }
        public ActionResult Index()
        {
            var memberships = _memberPlanService.GetAll();
            return View(memberships);
        }
        public IActionResult Create()
        {
            var model = _memberPlanService.GetCreateFormData();
            ViewData["Title"] = "Create";

            return View(model);
        }
        [HttpPost]
        public IActionResult Create(CreateMemberPlanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = _memberPlanService.GetCreateFormData();
                return View(model);
            }

            var result = _memberPlanService.Create(model);
            if (result)
            {
                TempData["SuccessMessage"] = "Membership created successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Failed to create membership. Check if member already has an active plan.";
            model = _memberPlanService.GetCreateFormData();
            return View(model);
        }
        public IActionResult Cancel(int id)
        {

            var result = _memberPlanService.Cancel(id);
            if (result)
                TempData["SuccessMessage"] = "Membership cancelled successfully!";
            else
                TempData["ErrorMessage"] = "Failed to cancel membership. Maybe it's already expired or doesn't exist.";

            return RedirectToAction(nameof(Index));
        }

    }
}
