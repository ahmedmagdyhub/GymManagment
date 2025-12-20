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
    public class PlanConfigration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(A => A.Name).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(A => A.Description).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(A => A.Price).HasPrecision(10,2);
            builder.ToTable(A =>
            {
                A.HasCheckConstraint("PlanDurationCheck", "DurationDays between 1 and 365");
            });

        }
    }
}
