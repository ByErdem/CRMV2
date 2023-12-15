using CRM.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Entities.Concrete
{
    public class Role : AuditedEntity, IEntity
    {
        public string? Name  { get; set; }
        public virtual ICollection<UserRole>? UserRoles { get; set; }
        public virtual ICollection<RoleMenuItem>? RoleMenuItems { get; set; }
    }
}
