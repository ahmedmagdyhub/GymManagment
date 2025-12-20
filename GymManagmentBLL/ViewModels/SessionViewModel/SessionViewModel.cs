using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels.SessionViewModel
{
    public  class SessionViewModel
    {
        public int ID { get; set; }

        public string Catogry_Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        //public string Status { get; set; } = null!;

        public string Trainer_name { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Capicity { get; set; }

        public int AvielableSlot { get; set; }

        #region Computed Property

        public string DateDisplay => $"{StartDate: mmm dd,yyyy}";

        public string TimeRange => $"{StartDate: hh:mm tt}-{EndDate: hh:mm tt}";

        public TimeSpan duration => EndDate - StartDate;

        public string Status
        {
            get
            {
                if (StartDate > DateTime.Now) return "UpComing";
                else if (StartDate <= DateTime.Now && EndDate >= DateTime.Now) return "OnGoing";
                else return "Completed";
            }
        }

        #endregion

    }
}
