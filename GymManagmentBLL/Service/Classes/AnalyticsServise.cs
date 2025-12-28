using GymManagmentBLL.Service.InterFaces;
using GymManagmentBLL.ViewModels.AnalyticsViewModels;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Classes
{
    public class AnalyticsServise : IAnalyticsServise
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticsServise(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public AnalyticsViewModel GetAnalyticsData()
        {
            var sessions = _unitOfWork.GetRepository<Session>().Getall();
            return new AnalyticsViewModel
            {
                TotalMembers = _unitOfWork.GetRepository<Member >().Getall().Count (),
                ActiveMembers  = _unitOfWork.GetRepository<MemberShip>().Getall(x=>x.Status=="Avtive").Count(),
                Trainers = _unitOfWork.GetRepository<Trainer >().Getall().Count (),
                UpcomingSession = sessions.Where(x=>x.StartDate>DateTime.Now ).Count (),
                OngoingSession = sessions .Where (x=>x.StartDate<=DateTime .Now &&x.EndDate>=DateTime .Now ).Count (),
                CompletedSession =sessions .Where (x=>x.EndDate<DateTime .Now ).Count ()
            };
        }
    }
}
