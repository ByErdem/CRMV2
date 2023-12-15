namespace CRM.Shared.Entities.Abstract
{
    public abstract class FullAuditedEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual DateTime ModifiedDate { get; set; } = DateTime.Now;
        public virtual bool IsDeleted { get; set; } = false;
        public virtual bool IsActive { get; set; } = true;
        public virtual int CreatedByUserId { get; set; } = -1;
        public virtual int ModifiedByUserId { get; set; } = -1;
    }
}
