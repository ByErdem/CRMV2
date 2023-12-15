using Microsoft.AspNetCore.Mvc;

namespace CRM.MVC.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
