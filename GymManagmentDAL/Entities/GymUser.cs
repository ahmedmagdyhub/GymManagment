namespace GymManagmentDAL.Entities;

public abstract  class GymUser : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateOnly DAteOfBirth { get; set; } 

    public Gender Gender { get; set; }

    public  Address? Address { get; set; }
}
[Owned]
public class  Address
{
    public  int BuldingNo { get; set; }

    public string Street { get; set; } = null!;
    public  string  City { get; set; } = null!;

}
