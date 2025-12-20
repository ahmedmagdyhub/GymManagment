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
    public class GymUserConfigration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(N => N.Name).HasColumnType("varchar").HasMaxLength(50);

            builder.Property(N => N.Email).HasColumnType("varchar").HasMaxLength(100);

            builder.Property(N => N.Phone).HasColumnType("varchar").HasMaxLength(11);

            builder.HasIndex(N => N.Phone).IsUnique();

            builder.HasIndex(N => N.Email).IsUnique();
            builder.ToTable(A =>
            {
                A.HasCheckConstraint("GymUserValidEmailCheck", "Email Like '_%@_%._%'");
                A.HasCheckConstraint("GymUserValidPhoneCheck", "Phone LIKE '01%' AND Phone NOT LIKE '%[^0-9]%'");



            });

            builder.OwnsOne(A => A.Address,addressbuilder =>{
                addressbuilder.Property(A => A.Street).HasColumnType("varchar").HasColumnName("street").HasMaxLength(30);
                addressbuilder.Property(A => A.City).HasColumnType("varchar").HasColumnName("City").HasMaxLength(30);
                addressbuilder.Property(A => A.BuldingNo).HasColumnName("BuildingNumber");
            });



        }
    }
}
