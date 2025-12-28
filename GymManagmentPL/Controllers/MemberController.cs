using GymManagmentBLL.Service.InterFaces;
using GymManagmentBLL.ViewModels.MemberViewMode_s;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public IActionResult Index()
        {
            var member = _memberService.GetAllMember();
            return View(member);
        }
        public ActionResult MemberDetails(int id)
        {
            if (id <= 0) {
                //TempData["ErrorMessage"] = "Id of Member cant be 0 or 1";
                return RedirectToAction(nameof(Index));
            }
            var member = _memberService.getMemberDeatails(id);
            if (member is null)
            {
                //TempData["ErrorMessage"] = "Member not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }
        public ActionResult HealthRecordDeatails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member cant be 0 or 1";
                return RedirectToAction(nameof(Index));
            }
            var helthrecord = _memberService.GetMemberHealthRecordDeatails(id);
            if (helthrecord is null)
            {
                TempData["ErrorMessage"] = "Member not Found";
                return RedirectToAction(nameof(Index));
            }
            return View(helthrecord);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMember(CreateMemberViewmodel createmember)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "check date and Missing Field");
                return View(nameof(Create), createmember);
            }
            bool result = _memberService.CreateMember(createmember);
            if(result)
            {
                TempData["SuccessMessage"] = "Member Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed To create";
            }
            return RedirectToAction(nameof(Index));
        }
        public ActionResult MemberEdit(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member cant be 0 or 1";
                return RedirectToAction(nameof(Index));

            }
            var meber = _memberService.GetMemberToUpdate(id);
            if(meber is null)
            {
                TempData["ErrorMessage"] = "Member not Found";
                return RedirectToAction(nameof(Index));

            }
            return View(meber);
        }
        [HttpPost]
        public ActionResult MemberEdit([FromRoute]int id ,MemberToUpdateViewModel membertoupdate)
        {
            if (!ModelState.IsValid)
                return View(membertoupdate);
            var result = _memberService.MemberToUpdate(id, membertoupdate);
            if (result)
            {
                TempData["SuccessMessage"] = "Member Update Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed To Update";
            }
            return RedirectToAction(nameof(Index));

        }
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Member cant be 0 or 1";
                return RedirectToAction(nameof(Index));

            }
            var member = _memberService.getMemberDeatails(id);
            if (member is null)
            {
                TempData["ErrorMessage"] = "Member not Found";
                return RedirectToAction(nameof(Index));

            }
            ViewBag.MemberId = id;
            return View();
        }
        [HttpPost]
        public ActionResult DeleteConfirm([FromForm] int id)
        {
            var result = _memberService.Removemember(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Member Delete Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed To Delete";
            }
            return RedirectToAction(nameof(Index));

        }

    }
}
