using GymManagmentBLL.ViewModels.MemberViewMode_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.InterFaces
{
    internal interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMember();

        bool CreateMember(CreateMemberViewmodel createMemberViewmodel);

     
        MemberViewModel? getMemberDeatails(int memberid);

        HealthRecordViewModel? GetMemberHealthRecordDeatails(int memberid);

        MemberToUpdateViewModel? GetMemberToUpdate(int memberid);

        bool MemberToUpdate(int memberid, MemberToUpdateViewModel membertoupdate);
        bool Removemember(int memberid);
    }
}
