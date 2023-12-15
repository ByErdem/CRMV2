using CRM.Entities.Dtos;
using CRM.MVC.Models;
using CRM.Services.Abstract;
using CRM.Services.Filter;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CRM.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,IHomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetDataForHomePage()
        {
            var result = await _homeService.GetDataForHomePage();
            return Ok(result);
        }
    }
}
