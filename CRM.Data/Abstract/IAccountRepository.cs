using CRM.Entities.Concrete;
using CRM.Shared.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data.Abstract
{
	public interface IAccountRepository:IEntityRepository<Account>{}
}
