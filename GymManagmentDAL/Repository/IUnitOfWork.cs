using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repository
{
    public  interface IUnitOfWork
    {
        public ISessionRepo sessionReposatory();
        IGenericRepo<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new();

        int SaveChange();
    }

    
}
