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
        IMemberSessionRepo MemberSessionRepo { get; }

        public ISessionRepo sessionReposatory { get; }
        IGenericRepo<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new();
        public IMembershipRepository MembershipRepository { get; }

        int SaveChange();
    }

    
}
