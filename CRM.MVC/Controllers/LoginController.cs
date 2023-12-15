using CRM.Entities.Dtos;
using CRM.MVC.Models;
using CRM.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.MVC.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
	{
		private readonly IUserService _userService;

		public LoginController(IUserService userService)
		{
			_userService = userService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> SignIn([FromBody] UserLoginDto dto)
		{
			var result = await _userService.SignIn(dto);
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
		{
			var result = await _userService.Register(dto);
			return Ok(result);
		}

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
		{
			var result = await _userService.ResetPassword(dto);
			return Ok(result);
		}
	}
}
