using CRM.Entities.Concrete;
using CRM.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services.Abstract
{
    public interface IMenuService
    {
        Task<ResponseDto<List<MenuItem>>> GetMenusForUser(int userId);
    }
}
