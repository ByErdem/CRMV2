using CRM.Data.Abstract;
using CRM.Entities.Concrete;
using CRM.Entities.Dtos;
using CRM.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CRM.MVC.Controllers
{
	public class AdminController : Controller
	{
		private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Users()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> GetAllUsersByNonDeleted()
		{
			var result = await _userService.GetAllUsersByNonDeleted();
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteUserById([FromBody] UserDto dto)
		{
			var result = await _userService.DeleteUserById(dto);
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> AddUser([FromBody] UserDto dto)
		{
			var result = await _userService.AddUser(dto);
			return Ok(result);
		}
	}
}
