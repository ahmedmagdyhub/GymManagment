using GymManagmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Configartion
{
    public class SessionConfigration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(A =>
            {
                A.HasCheckConstraint("SessionCapacityCheck", "Capacity BETWEEN 1 AND 25");
                A.HasCheckConstraint("SessionDateCheck", "EndDate > StartDate");

            });
            builder.HasOne(A => A.SessionCategory).WithMany(A => A.Sessions).HasForeignKey(A => A.CategoryId);
            builder.HasOne(A => A.SessionTrainer).WithMany(A => A.Sessions).HasForeignKey(A => A.TrainerId );

        }
    }
}
