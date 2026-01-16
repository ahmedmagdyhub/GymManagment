using GymManagmentBLL.ViewModels.MemberSessionViewModel;
using GymManagmentBLL.ViewModels.MemberViewMode_s;
using GymManagmentBLL.ViewModels.SessionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.InterFaces
{
    public interface IMemberSessionService
    {
        IEnumerable<SessionViewModel> GetAllSessionsWithTrainerAndCategory();
        IEnumerable<MemberSessionViewModel> GetMemberForSession(int id);
        bool CreateBooking(CreateMemberSessionViewModel createBooking);
        IEnumerable<MemberSelectViewModel> GetMembersForDropDown(int id);
        bool DeletaBooking(int MemberId);
    }
}
