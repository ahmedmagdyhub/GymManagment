using GymManagmentBLL.Service.Classes;
using GymManagmentBLL.Service.InterFaces;
using GymManagmentBLL.ViewModels.MemberViewMode_s;
using GymManagmentBLL.ViewModels.TrainerViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace GymManagmentPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerServise _trainerServise;

        public TrainerController(ITrainerServise trainerServise)
        {
            _trainerServise = trainerServise;
        }
        public IActionResult Index()
        {
            var trainer = _trainerServise.GetAllTrainers();
            return View(trainer );
        }
        
        public ActionResult Details(int id)
        {
            
            var trainer = _trainerServise.GetTrainerDetails(id);
            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTrainer(CreateTrainerViewModel createtrainer)
        {
            if (!ModelState.IsValid)
            {
                //ModelState.AddModelError("DataMissed", "check date and Missing Field");
                return View(nameof(Create), createtrainer);
            }
            var result = _trainerServise.CreateTrainer(createtrainer);
            if (result)
            {
                TempData["SuccessMessage"] = "Trainer Created Successfully";
                return RedirectToAction(nameof(Index));

            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed To create";
            }
            return RedirectToAction(nameof(Index));
        }
       
        public ActionResult Edit(int id)
        {
            var trainer = _trainerServise.GetTrainerToUpdate(id);
            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
            
        }
        [HttpPost]
        public ActionResult Edit(int id, TrainerToUpdateViewModel updatetrainer)
        {
            if (!ModelState.IsValid)
            {
                return View(updatetrainer);
            }
            var result = _trainerServise.UpdateTrainerDetails(updatetrainer, id);
            if (result)
            {
                TempData["SuccessMessage"] = "Trainer Update Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed To Upadet";
            }
            return RedirectToAction(nameof(Index));

        }
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of trainer cant be 0 or 1";
                return RedirectToAction(nameof(Index));

            }
            var trainer = _trainerServise.GetTrainerDetails(id);
            if(trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));

            }ViewBag.TrainerId = trainer.Id;
            return View();
        }
        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            var result = _trainerServise.RemoveTrainer(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Trainer Delete Successfully";

            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed To Delete";
            }
            return RedirectToAction(nameof(Index));

        }


    }
}
