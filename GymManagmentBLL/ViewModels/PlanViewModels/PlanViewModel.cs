using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels.PlanViewModels
{
    internal class PlanViewModel
    {
        public  int Id { get; set; }
        public string Name { get; set; } = null!;
        public  int DurationDayes { get; set; }

        public  string  Description { get; set; }

        public  decimal  price { get; set; }

        public  bool IsActive { get; set; }
    }
}
