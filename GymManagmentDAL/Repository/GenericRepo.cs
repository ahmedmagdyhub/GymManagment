using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository
{
    public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly GymManagmentDbContext _dbContext;

        public GenericRepo(GymManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(TEntity entity) => _dbContext.Set<TEntity>().Add(entity); 
        

        public void Delet(TEntity entity)=>_dbContext.Set<TEntity>().Remove(entity);



        public IEnumerable<TEntity> Getall(Func<TEntity, bool>? condition = null)
        {
            if (condition is null) 
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();
            else
                return _dbContext.Set<TEntity>().AsNoTracking().Where (condition).ToList();

        }

        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id);


        public void Upadte(TEntity entity)=> _dbContext.Set<TEntity>().Update(entity);

    }
}
