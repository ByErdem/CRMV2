using CRM.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Data.Concrete.EntityFramework.Mappings
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Phone).HasMaxLength(20);
            builder.Property(x => x.Fax).IsRequired(false);
            builder.Property(x => x.Fax).HasMaxLength(20);
            builder.Property(x => x.EMail).IsRequired(false);
            builder.Property(x => x.EMail).HasMaxLength(100);
            builder.Property(x => x.InvoiceEMail).IsRequired(false);
            builder.Property(x => x.InvoiceEMail).HasMaxLength(250);
            builder.Property(x => x.WebSite).IsRequired(false);
            builder.Property(x => x.WebSite).HasMaxLength(100);
            builder.Property(x => x.Address).IsRequired(false);
            builder.Property(x => x.Address).HasMaxLength(500);
            builder.Property(x => x.Note).IsRequired(false);
            builder.Property(x => x.Note).HasMaxLength(1000);
            builder.Property(x => x.OfficialName).IsRequired(false);
            builder.Property(x => x.OfficialName).HasMaxLength(250);
            builder.Property(x => x.AccountingName).IsRequired(false);
            builder.Property(x => x.AccountingName).HasMaxLength(250);
            builder.Property(x => x.TaxNo).IsRequired(false);
            builder.Property(x => x.TaxNo).HasMaxLength(50);
            builder.Property(x => x.TaxOffice).IsRequired(false);
            builder.Property(x => x.TaxOffice).HasMaxLength(250);

            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.CreatedByUserId).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedByUserId).IsRequired();
            builder.Property(x => x.ModifiedDate).IsRequired();
            builder.ToTable("Accounts");
        }
    }
}
