

namespace GymManagmentDAL.Repository;

public  interface IGenericRepo <TEntity> where TEntity : BaseEntity, new()
{
    IEnumerable<TEntity> Getall(Func <TEntity ,bool>? condition=null);
    TEntity? GetById(int id);
    void Add(TEntity entity);
    void  Upadte(TEntity entity);
    void Delet(TEntity entity);

}
