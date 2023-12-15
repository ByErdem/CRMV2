using CRM.Data.Concrete.EntityFramework.Mappings;
using CRM.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data.Concrete.EntityFramework.Contexts
{
    public class CRMContext:DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<UserDetail>? UserDetails { get; set; }
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<UserRole>? UserRoles { get; set; }
        public DbSet<MenuItem>? MenuItems { get; set; }
        public DbSet<RoleMenuItem>? RoleMenuItems { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
            }

            optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=CRM;Persist Security Info=True;User ID=sa;Password=123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserDetailMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());
            modelBuilder.ApplyConfiguration(new MenuItemMap());
            modelBuilder.ApplyConfiguration(new RoleMenuItemMap());
        }
    }
}
