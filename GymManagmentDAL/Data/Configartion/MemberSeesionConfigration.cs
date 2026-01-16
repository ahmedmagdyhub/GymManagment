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
    public class MemberSeesionConfigration : IEntityTypeConfiguration<MemberSession>
    {
        void IEntityTypeConfiguration<MemberSession>.Configure(EntityTypeBuilder<MemberSession> builder)
        {
            builder.Property(X => X.CreatedAt).HasColumnName("BookingDay");
         }
    }
}
