namespace GymManagmentDAL.Repository;

public  class UnitOfWork : IUnitOfWork
{
    private readonly GymManagmentDbContext _dbContext;
    private readonly Dictionary<Type, Object> _reposatory = new();
    public UnitOfWork(GymManagmentDbContext dbContext,ISessionRepo sessionReposatory )
    {
        _dbContext = dbContext;
        this.sessionReposatory = sessionReposatory;
    }
        public ISessionRepo sessionReposatory { get; }

    public IGenericRepo<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
    {
        var entitytype = typeof(TEntity);
        if (_reposatory.TryGetValue(entitytype, out var repo))
            return (IGenericRepo<TEntity>)repo;
        var newrepo = new GenericRepo<TEntity>(_dbContext);
        _reposatory[entitytype] = newrepo;
        return newrepo;
    }
    public int SaveChange()
    {
        return _dbContext.SaveChanges();
    }
}
