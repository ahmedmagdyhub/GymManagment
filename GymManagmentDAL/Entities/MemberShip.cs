

namespace GymManagmentDAL.Entities;

public class MemberShip : BaseEntity
{
    //CreateAt is startDate
    public  DateTime  EndDate { get; set; }
    public string Status { 
        get
        {
            if (EndDate > DateTime.Now)
                return "Active";
            else return "Expired";
        } }
           
    public  int MemberID { get; set; }
    public Member Member { get; set; } = null!;
    public  int PlanID { get; set; }
    public Plan Plan { get; set; } = null!;

}
