using CRM.Data.Abstract;
using CRM.Entities.Concrete;
using CRM.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CRM.Data.Concrete.EntityFramework.Repositories
{
    public class EfRoleMenuItemRepository : EfEntityRepositoryBase<RoleMenuItem>, IRoleMenuItemRepository
    {
        public EfRoleMenuItemRepository(DbContext context) : base(context)
        {

        }
    }
}
