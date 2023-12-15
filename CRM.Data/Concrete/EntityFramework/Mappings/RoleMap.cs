using CRM.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data.Concrete.EntityFramework.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(250);
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();

            //UserRole ile ilişkisini yapılandır.
            builder.HasMany(x => x.UserRoles).WithOne(x => x.Role).HasForeignKey(x => x.Id);

            //RoleMenuItem ilişkisini yapılandır.
            builder.HasMany(x => x.RoleMenuItems).WithOne(x => x.Role).HasForeignKey(x => x.Id);

            builder.ToTable("Roles");
        }
    }
}
