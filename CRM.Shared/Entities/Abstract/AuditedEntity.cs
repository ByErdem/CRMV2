namespace CRM.Shared.Entities.Abstract
{
    public abstract class AuditedEntity
    {
        public virtual int Id { get; set; }
        public virtual bool IsDeleted { get; set; } = false;
        public virtual bool IsActive { get; set; } = true;
    }
}
