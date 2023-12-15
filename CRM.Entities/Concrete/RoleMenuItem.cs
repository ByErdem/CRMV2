using CRM.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Entities.Concrete
{
    public class RoleMenuItem:AuditedEntity,IEntity
    {
        public int? RoleId { get; set; }
        public int? MenuItemId { get; set; }
        public virtual Role? Role { get; set; }
        public virtual MenuItem? MenuItem { get; set; }
    }
}
