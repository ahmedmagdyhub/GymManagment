using GymManagmentBLL.Service.InterFaces;
using GymManagmentBLL.ViewModels.PlanViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }
        public IActionResult Index()
        {
            var plan = _planService.GetAllPlan();
            return View(plan);
        }
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invaled plan id";
                return RedirectToAction(nameof(Index));
            }
            var plan = _planService.GetPlanById(id);
            if(plan == null)
            { 
                TempData["ErrorMessage"] = "Plan Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invaled plan id";
                return RedirectToAction(nameof(Index));
            }
            var plan = _planService.GetPlanToUpdate(id);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan cant be updated";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }
        [HttpPost]
        public ActionResult Edit(int id ,UpdatePlanViewModel updateplan)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Check data validation");
                return View(updateplan);
            }
            var result = _planService.UpdatePlan(id,updateplan);
            if (result)
            {
                TempData["SuccessMessage"] = "plan Update Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "plan Failed To Update";
            }
            return RedirectToAction(nameof(Index));


        }
        [HttpPost]
        public ActionResult Activate(int id)
        {
            var result = _planService.Tooglestation(id);
               if (result)
            {
                TempData["SuccessMessage"] = "plan status change";
            }
            else
            {
                TempData["ErrorMessage"] = "plan status failed to change";
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
