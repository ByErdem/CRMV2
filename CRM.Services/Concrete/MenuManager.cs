using CRM.Data.Abstract;
using CRM.Data.Concrete;
using CRM.Entities.Concrete;
using CRM.Entities.Dtos;
using CRM.Services.Abstract;
using CRM.Shared.Utilities.ComplexTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Services.Concrete
{
    public class MenuManager : IMenuService
    {
        private readonly IUnitOfWork _uow;

        public MenuManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ResponseDto<List<MenuItem>>> GetMenusForUser(int userId)
        {
            var rsp = new ResponseDto<List<MenuItem>>();

            var userRoles = await _uow.UserRoles.GetAllAsync(ur => ur.UserId == userId);
            var roleIds = userRoles.Select(ur => ur.RoleId).ToList();

            var roleMenuItems = await _uow.RoleMenuItems.GetAllAsync(
                rmi => roleIds.Contains(rmi.RoleId),
                rmi => rmi.MenuItem);

            var menuItemIds = roleMenuItems.Select(rmi => rmi.MenuItemId).Distinct().ToList();

            var menuItems = await _uow.MenuItems.GetAllAsync(x => menuItemIds.Contains(x.Id));

            // Burada menü ve alt menüler arasında erişim kontrolü uygulayın

            rsp.SuccessMessage = "Liste başarılı bir şekilde yüklendi";
            rsp.ResultStatus = ResultStatus.Success;
            rsp.Data = menuItems.Where(x => x.ParentId == null).ToList();

            return rsp;
        }
    }
}
