using Microsoft.AspNetCore.Mvc;

namespace CRM.MVC.Controllers
{
    public class FinanceController : Controller
    {
        public IActionResult Expense()
        {
            return View();
        }

        public IActionResult HumanResource()
        {
            return View();
        }
    }
}
