using GymManagmentBLL.Service.InterFaces;
using GymManagmentBLL.ViewModels.MemberSessionViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagmentPL.Controllers
{
    public class MemberSessionController : Controller
    {
        private readonly IMemberSessionService _memberssessionsercice;

        public MemberSessionController(IMemberSessionService memberssessionsercice)
        {
            _memberssessionsercice = memberssessionsercice;
        }
        public ActionResult Index()
        {
            var sessions = _memberssessionsercice.GetAllSessionsWithTrainerAndCategory();
            return View(sessions);
        }
        public ActionResult GetMemberForUpcomingSession(int id)
        {
            var members = _memberssessionsercice.GetMemberForSession(id);
            return View(members);
        }
        public ActionResult GetMemberForOngoingSession(int id)
        {
            var members = _memberssessionsercice.GetMemberForSession(id);
            return View(members);
        }
        public ActionResult Create(int id)
        {
            LoadDropDownsForMembers(id);
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateMemberSessionViewModel createBooking)
        {
            var result = _memberssessionsercice.CreateBooking(createBooking);
            if (result)
                TempData["SuccessMessage"] = "Booking Created Successfully";
            else
                TempData["ErrorMessage"] = "Failed To Create Booking";

            return RedirectToAction(nameof(GetMemberForOngoingSession), new { id = createBooking.SessionId });
        }
        [HttpPost]
        public ActionResult Cancel(int id)
        {
            var result = _memberssessionsercice.DeletaBooking(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Booking Deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Delete Booking.";


            }
            return RedirectToAction(nameof(Index));

        }
        private void LoadDropDownsForMembers(int id)
        {
            var members = _memberssessionsercice.GetMembersForDropDown(id);
            ViewBag.Memberships = new SelectList(members, "Id", "Name");
        }
    }

}
