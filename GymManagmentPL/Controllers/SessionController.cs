using GymManagmentBLL.Service.Classes;
using GymManagmentBLL.Service.InterFaces;
using GymManagmentBLL.ViewModels.MemberViewMode_s;
using GymManagmentBLL.ViewModels.SessionViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagmentPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public IActionResult Index()
        {
            var sessions = _sessionService.GetAllSession();
            return View(sessions);
        }
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid id ";
                return RedirectToAction(nameof(Index));
            }
            var Session = _sessionService.GetSessionById(id);
            if (Session is null)
            {
                TempData["ErrorMessage"] = "Session Not Found ";
                return RedirectToAction(nameof(Index));

            }
            return View(Session);
        }
        public ActionResult Create()
        {
            LoadDropDownCategory();
            LoadDropDownTrainer();
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateSessionViewModel createsession)
        {
            if (!ModelState.IsValid)
            {

                LoadDropDownCategory();
                LoadDropDownTrainer();
                return View( createsession);
            }
            var result = _sessionService.CreateSession(createsession);
            if (result)
            {
                TempData["SuccessMessage"] = "Session Created Successfully";
                return RedirectToAction(nameof(Index));

            }
            else
            {
                TempData["ErrorMessage"] = "Session Failed To create";

                LoadDropDownCategory();
                LoadDropDownTrainer();
                return View(createsession);

            }
        }

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid id of session";
                return RedirectToAction(nameof(Index));
            }
            var session = _sessionService.getSessionToUpdate(id);
            if (session is null)
            {
                TempData["ErrorMessage"] = "Session not Found";
                return RedirectToAction(nameof(Index));
            }
            LoadDropDownTrainer();
            return View(session);

        }
        [HttpPost]
        public ActionResult Edit([FromRoute]int id ,UpdateSessionViewModel updatesession)
        {
            if (!ModelState.IsValid)
            {
                LoadDropDownTrainer();
                return View(updatesession);

            }
            var result = _sessionService.UpdateSession(id, updatesession);
            if (result)
            {
                TempData["SuccessMessage"] = "Session Update Successfully";

            }
            else
            {
                TempData["ErrorMessage"] = "Session Failed To Update";

            }
            return RedirectToAction(nameof(Index));

        }
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid id of session";
                return RedirectToAction(nameof(Index));
            }
            var session = _sessionService.GetSessionById(id);
            if (session is null)
            {
                TempData["ErrorMessage"] = "Session not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SessionId = session.ID;
            return View();
        }
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = _sessionService.RemoveSession(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Session Delete Successfully";

            }
            else
            {
                TempData["ErrorMessage"] = "Session Failed To Delete";

            }
            return RedirectToAction(nameof(Index));

        }

        private void LoadDropDownCategory()
        {
            var category = _sessionService.GetCategoryForDropDown();
            ViewBag.Categories = new SelectList(category, "Id", "Name");

        }
        private void LoadDropDownTrainer()
        {
            var trainers = _sessionService.GetTrainerForDropDown();
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
        }

    }
}
