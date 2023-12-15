using CRM.Data.Abstract;
using CRM.Data.Concrete;
using CRM.Entities.Concrete;
using CRM.Entities.Dtos;
using CRM.Services.Abstract;
using CRM.Shared.Utilities.ComplexTypes;

namespace CRM.Services.Concrete
{
	public class RoleManager : IRoleService
	{
		private readonly IUnitOfWork _uow;

		public RoleManager(IUnitOfWork uow)
		{
			_uow = uow;
		}

		/// <summary>
		/// Sadece Rol adını ve hangi kullanıcıya atayacağınızı belirtmeniz yeterli
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		public async Task<ResponseDto<UserRole>> AssignRoleToUser(RoleDto dto)
		{
			var rsp = new ResponseDto<UserRole>();
			var role = await _uow.Roles.GetAsync(r => r.Name == dto.RoleName);
			if (role != null)
			{
				var userRole = new UserRole { UserId = dto.UserId, RoleId = role.Id };
				await _uow.UserRoles.AddAsync(userRole);
				await _uow.SaveAsync();

				rsp.Data = userRole;
				rsp.ResultStatus = ResultStatus.Success;

			}
			else
			{
				rsp.ErrorMessage = "Sistemde " + dto.RoleName + " isimli bir rol bulunamadı";
				rsp.Data = null;
				rsp.ResultStatus = ResultStatus.Error;
			}

			return rsp;	
		}

		public async Task<int> CreateRoleWithMenus(RoleDto dto)
		{
			// Rolü oluştur ve kaydet
			var role = new Role { Name = dto.RoleName };
			await _uow.Roles.AddAsync(role);

			// Seçilen menü ve alt menü öğeleri için rol-menü ilişkisi oluştur
			foreach (var menuItemId in dto.MenuItemIds)
			{
				var menuItem = await _uow.MenuItems.GetAsync(m => m.Id == menuItemId);
				if (menuItem != null)
				{
					// Üst menü için rol-menü ilişkisi
					var roleMenuItem = new RoleMenuItem { RoleId = role.Id, MenuItemId = menuItem.Id };
					await _uow.RoleMenuItems.AddAsync(roleMenuItem);

					// Alt menüler için rol-menü ilişkisi
					foreach (var childMenuItem in menuItem.Children)
					{
						var childRoleMenuItem = new RoleMenuItem { RoleId = role.Id, MenuItemId = childMenuItem.Id };
						await _uow.RoleMenuItems.AddAsync(childRoleMenuItem);
					}
				}
			}
			await _uow.SaveAsync();
			return 1;
		}
	}
}
