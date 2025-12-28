using GymManagmentBLL.Service.InterFaces;
using GymManagmentDAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnalyticsServise _analyticsServise;

        public HomeController(IAnalyticsServise analyticsServise)
        {
            _analyticsServise = analyticsServise;
        }
        public ActionResult index()
        {
            var data = _analyticsServise.GetAnalyticsData();
            return View(data);
        }
        
    }
}
