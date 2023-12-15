using Microsoft.AspNetCore.Mvc;

namespace CRM.MVC.Controllers
{
    public class OperationController : Controller
    {
        public IActionResult Quote()
        {
            return View();
        }

        public IActionResult QuoteRequest()
        {
            return View();
        }

        public IActionResult WorkList()
        {
            return View();
        }
    }
}
