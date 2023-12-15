using CRM.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Data.Concrete.EntityFramework.Mappings
{
    public class RoleMenuItemMap : IEntityTypeConfiguration<RoleMenuItem>
    {
        public void Configure(EntityTypeBuilder<RoleMenuItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Role).WithMany(r => r.RoleMenuItems).HasForeignKey(x => x.RoleId);
            builder.HasOne(x => x.MenuItem).WithMany(x => x.RoleMenuItems).HasForeignKey(x => x.MenuItemId);

            builder.ToTable("RoleMenuItems");
        }
    }
}
