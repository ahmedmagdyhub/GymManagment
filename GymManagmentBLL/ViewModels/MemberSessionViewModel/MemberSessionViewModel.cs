using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels.MemberSessionViewModel
{
    public class MemberSessionViewModel
    {
        public string MemberName { get; set; } = null!;
        public string MemberId { get; set; } = null!;
        public bool IsAttended { get; set; }
        public string BookingDate { get; set; } = null!;
    }
}
