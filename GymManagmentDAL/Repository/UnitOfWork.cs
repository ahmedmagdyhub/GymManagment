using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository
{
    public  class UnitOfWork : IUnitOfWork
    {
        private readonly GymManagmentDbContext _dbContext;
        private readonly Dictionary<Type, Object> _reposatory = new();
        public UnitOfWork(GymManagmentDbContext dbContext,ISessionRepo sessionReposatory )
        {
            _dbContext = dbContext;
            this.sessionReposatory = sessionReposatory;
        }
        public IGenericRepo<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var entitytype = typeof(TEntity);
            if (_reposatory.TryGetValue(entitytype, out var repo)) ;
            var newrepo = new GenericRepo<TEntity>(_dbContext);
            _reposatory[entitytype] = newrepo;
            return newrepo;
        }

        public int SaveChange()
        {
            return _dbContext.SaveChanges();
        }

        public ISessionRepo sessionReposatory { get; }
        
    }
}
