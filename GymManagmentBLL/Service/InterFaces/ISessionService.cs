using GymManagmentBLL.ViewModels.SessionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.InterFaces
{
    public  interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSession();
        SessionViewModel? GetSessionById(int sessionid);

        bool CreateSession(CreateSessionViewModel createSessionViewModel);

        UpdateSessionViewModel? getSessionToUpdate(int sessionid);

        bool UpdateSession(int sessionid , UpdateSessionViewModel updateSession);

        bool RemoveSession(int sessionid);
        IEnumerable<TrainerSelectViewModel> GetTrainerForDropDown();

        IEnumerable<CategorySelectViewModel> GetCategoryForDropDown();

    }
}
