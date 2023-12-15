using CRM.Entities.Concrete;
using CRM.Entities.Dtos;
using CRM.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.MVC.Controllers
{
    public class UserAuthorizationController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IMenuService _menuService;

        public UserAuthorizationController(IRoleService roleService, IMenuService menuService)
        {
            _roleService = roleService;
            _menuService = menuService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoleWithMenus([FromBody] RoleDto dto)
        {
            var result = await _roleService.CreateRoleWithMenus(dto);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser([FromBody] RoleDto dto)
        {
            var result = await _roleService.AssignRoleToUser(dto);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetMenusForUser([FromBody] RoleDto dto)
        {
            var result = await _menuService.GetMenusForUser((int)dto.UserId);
            return Ok(result);
        }
    }
}
