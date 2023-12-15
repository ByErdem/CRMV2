using CRM.Data.Abstract;
using CRM.Entities.Concrete;
using CRM.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data.Concrete.EntityFramework.Repositories
{
    public class EfUserRoleRepository : EfEntityRepositoryBase<UserRole>, IUserRoleRepository
    {
        public EfUserRoleRepository(DbContext context) : base(context)
        {

        }
    }
}
