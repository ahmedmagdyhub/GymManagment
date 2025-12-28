namespace GymManagmentDAL.Entities;

public abstract class BaseEntity
{
    public  int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpadateAt { get; set; }
}
