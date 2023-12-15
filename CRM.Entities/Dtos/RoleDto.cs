using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Entities.Dtos
{
    public class RoleDto
    {
        public int? UserId { get; set; }
        public string? RoleName { get; set; }
        public List<int>? MenuItemIds { get; set; }
    }
}
