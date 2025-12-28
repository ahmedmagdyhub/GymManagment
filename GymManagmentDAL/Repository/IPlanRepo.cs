namespace GymManagmentDAL.Repository;

public interface IPlanRepo
{
    IEnumerable<Plan> GetAll();
    Plan? GetById(int id);
    int Update(Plan plan);
}
