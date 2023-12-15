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
    public class MenuItemMap : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Icon).HasMaxLength(200);
            builder.Property(x => x.Link).HasMaxLength(500);


            builder.HasOne(mi => mi.Parent)
                   .WithMany(p => p.Children)
                   .HasForeignKey(mi => mi.ParentId)
                   .OnDelete(DeleteBehavior.Restrict);


            // RoleMenuItems ile ilişkiyi yapılandırır
            builder.HasMany(mi => mi.RoleMenuItems)
                   .WithOne(rmi => rmi.MenuItem)
                   .HasForeignKey(rmi => rmi.MenuItemId)
                   .OnDelete(DeleteBehavior.Cascade);


            // Seed data
            builder.HasData(
                new MenuItem { Id = 1, Title = "Operation", ParentId = null, Link = null, Icon = "~/assets/img/icons/product.svg" },
                new MenuItem { Id = 2, Title = "Quote Request", ParentId = 1, Link = "~/Views/Operation/QuoteRequest.cshtml" },
                new MenuItem { Id = 3, Title = "Quote", ParentId = 1, Link = "~/Views/Operation/Quote.cshtml" },
                new MenuItem { Id = 4, Title = "Work List", ParentId = 1, Link = "~/Views/Operation/WorkList.cshtml" },
                new MenuItem { Id = 5, Title = "Stock Management", ParentId = null, Link = null, Icon = "~/assets/img/icons/product.svg" },
                new MenuItem { Id = 6, Title = "Product", ParentId = 5, Link = "~/Views/StockManagement/Product.cshtml" },
                new MenuItem { Id = 7, Title = "WareHouse", ParentId = 5, Link = "~/Views/StockManagement/WareHouse.cshtml" },
                new MenuItem { Id = 8, Title = "WareHouse Transfer", ParentId = 5, Link = "~/Views/StockManagement/Warehouse Transfer.cshtml" },
                new MenuItem { Id = 9, Title = "Purchase", ParentId = null, Link = "~/Views/Purchase/Index.cshtml", Icon = "~/assets/img/icons/product.svg" },
                new MenuItem { Id = 10, Title = "Accounts", ParentId = null, Link = "~/Views/Accounts/Index.cshtml", Icon = "~/assets/img/icons/product.svg" },
                new MenuItem { Id = 11, Title = "Finance", ParentId = null, Link = null, Icon = "~/assets/img/icons/product.svg" },
                new MenuItem { Id = 12, Title = "Expense", ParentId = 11, Link = "~/Views/Finance/Expense.cshtml" },
                new MenuItem { Id = 13, Title = "Human Resource", ParentId = null, Link = "~/Views/Finance/HumanResource.cshtml", Icon = "~/assets/img/icons/product.svg" },
                new MenuItem { Id = 14, Title = "Admin", ParentId = null, Link = "~/Views/Admin/Index.cshtml", Icon = "~/assets/img/icons/product.svg" }
            );

            builder.ToTable("MenuItems");

        }
    }
}
