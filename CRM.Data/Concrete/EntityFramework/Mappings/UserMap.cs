using CRM.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Data.Concrete.EntityFramework.Mappings
{
	public class UserMap : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.Property(x => x.Email).IsRequired();
			builder.Property(x => x.Email).HasMaxLength(50);
			builder.HasIndex(x => x.Email).IsUnique();
			builder.Property(x => x.NormalizedEmail).IsRequired();
			builder.Property(x => x.NormalizedEmail).HasMaxLength(50);
			builder.HasIndex(x => x.NormalizedEmail).IsUnique();

			builder.Property(x => x.PhoneNumber).IsRequired();
			builder.Property(x => x.PhoneNumber).HasMaxLength(15);
			builder.HasIndex(x => x.PhoneNumber).IsUnique();

			builder.Property(x => x.PasswordHash).IsRequired();
			builder.Property(x => x.PasswordHash).HasMaxLength(200);

			builder.Property(x => x.SaltPassword).IsRequired();
			builder.Property(x => x.SaltPassword).HasMaxLength(50);

			builder.Property(x => x.LockoutEnabled).IsRequired();
			builder.Property(x => x.TwoFactorEnabled).IsRequired();
			builder.Property(x => x.PhoneNumberConfirmed).IsRequired();
			builder.Property(x => x.AccessFailedCount).IsRequired();

			builder.Property(x => x.IsDeleted).IsRequired();
			builder.Property(x => x.IsActive).IsRequired();
			builder.Property(x => x.CreatedByUserId).IsRequired();
			builder.Property(x => x.CreatedDate).IsRequired();
			builder.Property(x => x.ModifiedByUserId).IsRequired();
			builder.Property(x => x.ModifiedDate).IsRequired();

			builder.HasOne(x => x.UserDetail).WithOne(x => x.User).HasForeignKey<UserDetail>(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(x => x.Account).WithOne(x => x.User).HasForeignKey<Account>(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
			builder.ToTable("Users");
		}
	}
}
