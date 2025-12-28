using GymManagmentBLL.ViewModels.AnalyticsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.InterFaces
{
    public  interface IAnalyticsServise
    {
        AnalyticsViewModel GetAnalyticsData();
    }
}
