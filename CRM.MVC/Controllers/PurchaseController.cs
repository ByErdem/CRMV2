using Microsoft.AspNetCore.Mvc;

namespace CRM.MVC.Controllers
{
    public class PurchaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
