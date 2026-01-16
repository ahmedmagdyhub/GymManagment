namespace GymManagmentDAL.Repository;

public  class UnitOfWork : IUnitOfWork
{
    private readonly GymManagmentDbContext _dbContext;
    private readonly Dictionary<Type, object> _reposatory = new();

    public UnitOfWork(
        GymManagmentDbContext dbContext,
        IMemberSessionRepo memberSessionReposatory,
        ISessionRepo sessionReposatory,
        IMembershipRepository membershipRepository)
    {
        _dbContext = dbContext;
        MemberSessionRepo = memberSessionReposatory;
        this.sessionReposatory = sessionReposatory;
        MembershipRepository = membershipRepository;
    }
    public IMembershipRepository MembershipRepository { get; }

    public IMemberSessionRepo MemberSessionRepo { get; }
    public ISessionRepo sessionReposatory { get; }

    public IGenericRepo<TEntity> GetRepository<TEntity>()
        where TEntity : BaseEntity, new()
    {
        var entitytype = typeof(TEntity);
        if (_reposatory.TryGetValue(entitytype, out var repo))
            return (IGenericRepo<TEntity>)repo;

        var newrepo = new GenericRepo<TEntity>(_dbContext);
        _reposatory[entitytype] = newrepo;
        return newrepo;
    }

    public int SaveChange()
        => _dbContext.SaveChanges();
}
