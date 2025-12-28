

namespace GymManagmentDAL.Entities;

public class MemberSession : BaseEntity
{//Bookingday is CreateAt
    public  bool IsAttended { get; set; }
    public  int MemberId { get; set; }
    public Member Member { get; set; } = null!;
    public  int SessionId { get; set; }
    public Session Session { get; set; } = null!;
}
