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
    public class HealthRecordConfigration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable("Members").HasKey(X => X.Id);
            builder.HasOne<Member>().WithOne(x => x.HealthRecord).HasForeignKey<HealthRecord>(X => X.Id);
            builder.Ignore(Z => Z.CreatedAt);
        }
    }
}
