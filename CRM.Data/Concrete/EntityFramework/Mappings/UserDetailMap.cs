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
    public class UserDetailMap : IEntityTypeConfiguration<UserDetail>
    {
        public void Configure(EntityTypeBuilder<UserDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.PlaceOfBirth).HasMaxLength(50);
            builder.Property(x => x.PhoneWork).HasMaxLength(20);
            builder.Property(x => x.PhonePersonel).HasMaxLength(20);
            builder.Property(x => x.EmailPersonal).HasMaxLength(250);
            builder.Property(x => x.Address).HasMaxLength(250);
            builder.Property(x => x.TCKN).HasMaxLength(11);
            builder.HasIndex(x => x.TCKN).IsUnique();
            builder.Property(x => x.IdentityNo).HasMaxLength(50);
            builder.Property(x => x.PassportNo).HasMaxLength(20);
            builder.Property(x => x.BloodType).HasMaxLength(10);
            builder.Property(x => x.Avatar).HasMaxLength(50);
            builder.Property(x => x.Note).HasMaxLength(500);
            builder.Property(x => x.Language).HasMaxLength(15);

            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.CreatedByUserId).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedByUserId).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();
        }
    }
}
