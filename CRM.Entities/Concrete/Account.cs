using CRM.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Entities.Concrete
{
	public class Account : FullAuditedEntity, IEntity
	{
        public int UserId { get; set; } 
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? EMail { get; set; }
        public string? InvoiceEMail { get; set; }
        public string? WebSite { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public bool? IsClient { get; set; }
        public bool? IsSP { get; set; }
        public bool? IsOfficialBody { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; } 
        public string? OfficialName { get; set; }
        public string? AccountingName { get; set; }
        public string? TaxNo { get; set; }
        public string? TaxOffice { get; set; }
        public int? PaymentType { get; set; }
        public virtual User? User { get; set; } 
    }
}
