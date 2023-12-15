using CRM.Shared.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace CRM.Entities.Concrete
{
    public class UserDetail : FullAuditedEntity, IEntity
    {
        public int UserId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; } 
        public int? PlaceOfBirthCountryId { get; set; }
        public string? PhoneWork { get; set; }
        public string? PhonePersonel { get; set; }
        public string? EmailPersonal { get; set; }
        public string? Address { get; set; }
        public string? TCKN { get; set; }
        public string? IdentityNo { get; set; }
        public string? PassportNo { get; set; }
        public int WorkStatusId { get; set; }
        public DateTime? WorkingSince { get; set; }
        public DateTime? LeavingDate { get; set; }
        public string? BloodType { get; set; }
        public string? Avatar { get; set; }
        public int? ManagerId { get; set; }
        public string? Note { get; set; }
        public int? GenderId { get; set; }  //Enum
        public int? NationalityId { get; set; }
        public string? Language { get; set; }
        public virtual User? User { get; set; }
    }
}
