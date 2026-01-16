namespace GymManagmentDAL.Entities;

public class Member : GymUser
{
    //Join date is CreateAt

    public string Photo { get; set; } = null!;
    public HealthRecord HealthRecord { get; set; } = null!;

    public ICollection<MemberSession> MemberSessions = null!;

    public ICollection<MemberShip> MemberPlan = null!;

}
