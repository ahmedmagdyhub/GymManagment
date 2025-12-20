using GymManagmentBLL.Service.InterFaces;
using GymManagmentBLL.ViewModels.SessionViewModel;
using GymManagmentDAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Classes
{
    public class SessionService : ISessionService
    {
        private readonly UnitOfWork _unitOfWork;

        public SessionService(UnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }
        public IEnumerable<SessionViewModel> GetAllSession()
        {
            var sessions = _unitOfWork.sessionReposatory.GetAllSessionWithTrainerAndCategory();
            if (!sessions.Any()) return [];
            return sessions.Select(x => new SessionViewModel
            {
                ID = x.Id,
                Catogry_Name = x.SessionCategory.CategoryName.ToString(),
                Capicity = x.Capacity,
                Description = x.Description,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Trainer_name = x.SessionTrainer.Name,
                AvielableSlot = x.Capacity - _unitOfWork.sessionReposatory.GetCountOfBookedSlot(x.Id)

            });
        }
    }
}
