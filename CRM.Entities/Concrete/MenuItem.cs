using CRM.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Entities.Concrete
{
    public class MenuItem:AuditedEntity,IEntity
    {
        public string? Title { get; set; }
        public string? Link { get; set; }
        public string? Icon { get; set; }
        public int? ParentId { get; set; } // Üst menü öğesi için nullable
        public virtual MenuItem? Parent { get; set; }
        public virtual ICollection<MenuItem>? Children { get; set; } // Alt menü öğeleri
        public virtual ICollection<RoleMenuItem>? RoleMenuItems { get; set; } // Bu menü öğesine erişimi olan roller
    }
}
