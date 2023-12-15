using CRM.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Entities.Concrete
{
    public class UserRole : AuditedEntity, IEntity
    {
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public virtual User? User { get; set; }
        public virtual Role? Role { get; set; }
    }
}
