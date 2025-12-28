namespace GymManagmentDAL.Entities;

public class Trainer :GymUser 
{
    // HireDate is CreateAt
    public Specialties Specialties { get; set; }

    public ICollection<Session> Sessions { get; set; } = null!;

}
