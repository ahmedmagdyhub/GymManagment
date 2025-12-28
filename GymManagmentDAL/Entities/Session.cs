namespace GymManagmentDAL.Entities;

public class Session : BaseEntity
{
    public string Description { get; set; } = null!;
    public  int Capacity { get; set; }

    public  DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public  int TrainerId { get; set; }
    public Trainer SessionTrainer { get; set; } = null!;
    public  int CategoryId { get; set; }
    public Category SessionCategory { get; set; } = null!;

    public ICollection<MemberSession> SessionsMember = null!;

}
